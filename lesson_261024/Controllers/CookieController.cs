using System;
using Microsoft.AspNetCore.Mvc;

namespace lesson_261024.Controllers;

public class CookieController : Controller
{
    public IActionResult Index()
    {
        // create cookie
        // Response.Cookies.Append("name","Ilya");

        //var cookie = Request.Cookies["name"];

        /*foreach (var item in Request.Cookies)
        {
            System.Console.WriteLine(item.Key + " " + item.Value);
        }*/

        // удаление
        //Response.Cookies.Delete("name");

        Response.Cookies.Append("name","Ilya", new CookieOptions {
            Expires = DateTimeOffset.Now.AddDays(7)
        });

        Response.Cookies.Append("info","Привет как дела");

        return View();
    }

    public IActionResult Settings()
    {
        ViewBag.Theme = "light";

        return View();
    }
}
