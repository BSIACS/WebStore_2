using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Interfaces.TestApi;

namespace WebStore.Controllers
{
    public class WebAPIController : Controller
    {
        private readonly IValuesService _valueService;

        public WebAPIController(IValuesService valueService)
        {
            this._valueService = valueService;
        }

        public IActionResult Index()
        {
            var values = _valueService.Get();

            return View(values);
        }
    }
}
