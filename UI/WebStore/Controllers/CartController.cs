using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.DTO.Orders;
using WebStore.Domain.Identity;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;

namespace WebStore.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly UserManager<User> _userManager;

        public CartController(ICartService cartService, UserManager<User> userManager)
        {
            _cartService = cartService;
            _userManager = userManager;
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

        public IActionResult Index() {
            User user = _userManager.FindByNameAsync(User.Identity.Name).Result;

            ViewData["UserId"] = user.Id;

            return View(new CartOrderViewModel() { Cart = _cartService.TransformToViewModel() });
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(OrderViewModel viewModel, [FromServices] IOrderService orderService) {
                        
            if (!ModelState.IsValid)
                return RedirectToAction("Index", "Cart");

            List<OrderItemDTO> orderItemsDTOs = _cartService.TransformToViewModel().Items
                .Select(item => new OrderItemDTO
                (
                    item.product.Id,
                    null,
                    item.product.Price,
                    item.quantity
                )).ToList();

            await orderService.CreateOrder(new CreateOrderModel(viewModel, orderItemsDTOs));

            _cartService.Clear();

            return RedirectToAction("Index", "Home");
        }
    }
}
