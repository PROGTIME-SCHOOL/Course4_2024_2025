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

    public IActionResult Upload(IFormFile file)
    {
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
}