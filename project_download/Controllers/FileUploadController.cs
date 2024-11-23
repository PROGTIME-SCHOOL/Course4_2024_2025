using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace project_download.Controllers;

public class FileUploadController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Downloads()
    {
        var currentDirectory = Directory.GetCurrentDirectory();
        var uploadsFolder = Path.Combine(currentDirectory, "UploadedFiles");

        if (!Directory.Exists(uploadsFolder))
        {
            return Content("Error. Directory does not exist!");
        }

        var files = Directory.GetFiles(uploadsFolder).
            Select(file => new FileInfo(file)).
            Select(file => new {
                Name = file.Name,
                Size = $"{(file.Length / 1024f / 1024f):F2} МБ"
            }).ToList();

        return View(files);
    }

    public IActionResult Upload(IFormFile file)
    {
        // альтернативный вариант загрузки через HttpContext
        // var files = HttpContext.Request.Form.Files.FirstOrDefault();

        if (file == null || file.Length == 0)
        {
            return Content("Файл не выбран!");
        }

        var currentDirectory = Directory.GetCurrentDirectory();
        var uploadsFolder = Path.Combine(currentDirectory, "UploadedFiles");

        if (!Directory.Exists(uploadsFolder))
        {
            Directory.CreateDirectory(uploadsFolder);
        }

        var filePath = Path.Combine(uploadsFolder, file.FileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            file.CopyTo(stream);
        }

        return Content($"Файл <{file.FileName}> загружен!");
    }

    public IActionResult DownloadFile(string fileName)
    {
        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles");

        var filePath = Path.Combine(uploadsFolder, fileName);

        if (!System.IO.File.Exists(filePath))
        {
            return Content("Файл не найден!");
        }

        byte[] bytes = System.IO.File.ReadAllBytes(filePath);
        return File(bytes, "application/octet-stream", fileName);
    }

    public IActionResult DeleteFile(string fileName)
    {
        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles");

        var filePath = Path.Combine(uploadsFolder,fileName);

        if (System.IO.File.Exists(filePath))
        {
            System.IO.File.Delete(filePath);
            return Content($"File <{fileName}> was deleted");
        }

        return Content($"IMPORTANT!!! File <{fileName}> was not deleted!!!");
    }
}