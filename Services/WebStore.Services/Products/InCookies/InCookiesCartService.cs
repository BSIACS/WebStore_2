using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Linq;
using WebStore.Domain.Entities;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Interfaces;
using WebStore.Services.Products;

namespace WebStore.Services.Products.InCookies
{
    public class InCookiesCartService : ICartService
    {
        private readonly IProductData _prductData;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _cartName;

        private Cart Cart
        {
            get
            {
                var context = _httpContextAccessor.HttpContext;
                var cookies = context.Response.Cookies;
                var cartCookie = context.Request.Cookies[_cartName];
                if (cartCookie is null)
                {
                    var cart = new Cart();
                    cookies.Append(_cartName, JsonConvert.SerializeObject(cart));
                    return cart;
                }

                return JsonConvert.DeserializeObject<Cart>(cartCookie);

            }
            set
            {
                var context = _httpContextAccessor.HttpContext;
                var cookies = context.Response.Cookies;
                string serializedCart = JsonConvert.SerializeObject(value);
                ReplaceCookie(cookies, _cartName, serializedCart);
            }
        }

        public InCookiesCartService(IProductData prductData, IHttpContextAccessor httpContextAccessor)
        {
            _prductData = prductData;
            _httpContextAccessor = httpContextAccessor;

            var user = httpContextAccessor.HttpContext.User;
            var userName = user.Identity.IsAuthenticated ? $"{user.Identity.Name}" : null;

            _cartName = $"WebStore{userName}";
        }

        public void ReplaceCookie(IResponseCookies responseCookies, string key, string value)
        {
            responseCookies.Delete(key);
            responseCookies.Append(key, value);
        }

        public void AddToCart(int id)
        {
            Cart cart = Cart;

            CartItem cartItem = cart.Items.FirstOrDefault(i => i.ProductId == id);

            if (cartItem != null)
            {
                cartItem.Quantity++;
            }
            else
            {
                cart.Items.Add(new CartItem { ProductId = id, Quantity = 1 });
            }

            Cart = cart;
        }

        public void DecrementFromCart(int id)
        {
            Cart cart = Cart;

            CartItem cartItem = cart.Items.FirstOrDefault(i => i.ProductId == id);

            if (cartItem is null)
                return;

            if (cartItem.Quantity > 0)
                cartItem.Quantity--;

            if (cartItem.Quantity == 0)
                cart.Items.Remove(cartItem);

            Cart = cart;
        }

        public void RemoveFromCart(int id)
        {
            Cart cart = Cart;

            CartItem cartItem = cart.Items.FirstOrDefault(i => i.ProductId == id);

            if (cartItem != null)
            {
                cart.Items.Remove(cartItem);
            }

            Cart = cart;
        }

        public void Clear()
        {
            Cart cart = Cart;

            cart.Items.Clear();

            Cart = cart;
        }

        public CartViewModel TransformToViewModel()
        {
            Cart cart = Cart;

            var products = _prductData.GetProducts(new Domain.ProductFilter() { ProductsIds = cart.Items.Select(i => i.ProductId).ToArray() });

            return new CartViewModel
            {
                Items = cart.Items.Select(i => (new ProductViewModel
                {
                    Id = i.ProductId,
                    Name = products.FirstOrDefault(p => p.Id == i.ProductId)?.Name,
                    Brand = products.FirstOrDefault(p => p.Id == i.ProductId)?.Brand.Name,
                    Price = products.FirstOrDefault(p => p.Id == i.ProductId).Price,
                    ImageUrl = products.FirstOrDefault(p => p.Id == i.ProductId)?.ImageUrl,
                }, i.Quantity))
            };
        }



    }
}
