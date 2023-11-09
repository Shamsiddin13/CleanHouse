using CleanHouse.Service.Interfaces.Commons;
using Microsoft.AspNetCore.Mvc;

namespace CleanHouse.Api.Controllers;

public class FilesController : ControllerBase
{
    private readonly IFileService _fileService;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public FilesController(IFileService fileService, IWebHostEnvironment webHostEnvironment)
    {
        this._fileService = fileService;
        this._webHostEnvironment = webHostEnvironment;
    }

    [HttpPost]
    public async Task<IActionResult> UploadAsync(IFormFile file)
    {
        string path = Path.Combine(this._webHostEnvironment.WebRootPath, "Media", "Files", file.FileName);

        return Ok(await this._fileService.UploadAsync(file.OpenReadStream(), path));
    }

    [HttpGet]
    public async Task<IActionResult> DownloadAsync(string relativePath)
    {
        var path = Path.Combine(this._webHostEnvironment.WebRootPath, relativePath);
        string fileName = Path.GetFileName(path);

        return File(await this._fileService.DownloadAsync(path), "application/octet-stream", fileName);
    }

}

