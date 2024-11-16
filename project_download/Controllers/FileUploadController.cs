
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;

namespace project_download.Controllers;

public class FileUploadController : Controller 
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Upload()
    {
        return Content("OK");
    }
}