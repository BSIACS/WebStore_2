using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Interfaces;

namespace WebStore.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            this._cartService = cartService;
        }

        public IActionResult AddToCart(int id) {

            string urlToReturn = Request.Headers["Referer"].ToString();

            _cartService.AddToCart(id);
                        
            return Redirect(urlToReturn);
        }

        public IActionResult DecrementFromCart(int id){

            string urlToReturn = Request.Headers["Referer"].ToString();

            _cartService.DecrementFromCart(id);

            return Redirect(urlToReturn); 
        }

        public IActionResult RemoveFromCart(int id)
        {
            string urlToReturn = Request.Headers["Referer"].ToString();

            _cartService.RemoveFromCart(id);
            
            return Redirect(urlToReturn);
        }

        public IActionResult Index() => View(new CartOrderViewModel() { Cart = _cartService.TransformToViewModel()});

        [HttpPost]
        public async Task<IActionResult> Checkout(OrderViewModel viewModel, [FromServices] IOrderService orderService, [FromServices] ILogger<CartController> logger) {

            if (!ModelState.IsValid)
                return RedirectToAction("Index", "Cart");

            try
            {
                await orderService.CreateOrder(viewModel);
            }
            catch (Exception e) {
                logger.LogError(e.Message);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
