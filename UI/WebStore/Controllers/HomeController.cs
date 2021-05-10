using Microsoft.AspNetCore.Mvc;
using System;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();

        public IActionResult Blog() => View();

        public IActionResult BlogSingle() => View();

        public IActionResult SecondAction() => Content("Second controllers action");

        public IActionResult Error404() => View();

        public IActionResult ErrorStatus(string errorStatusCode) => errorStatusCode switch {
                "404" => RedirectToAction(nameof(Error404)),
                _ => Content($"Error status code {errorStatusCode}")
        };

        public IActionResult Throw(string id) => throw new ApplicationException(id);

        public IActionResult ContactUs() => View();
        
    }
}
