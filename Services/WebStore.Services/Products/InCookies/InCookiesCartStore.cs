using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;

namespace WebStore.Services.Products.InCookies
{
    public class InCookiesCartStore : ICartStore
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _cartName;

        public Cart Cart
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

        public InCookiesCartStore(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

            var user = httpContextAccessor.HttpContext.User;
            var userName = user.Identity.IsAuthenticated ? $"{user.Identity.Name}" : null;

            _cartName = $"WebStore{userName}";
        }

        private void ReplaceCookie(IResponseCookies responseCookies, string key, string value)
        {
            responseCookies.Delete(key);
            responseCookies.Append(key, value);
        }
    }
}
