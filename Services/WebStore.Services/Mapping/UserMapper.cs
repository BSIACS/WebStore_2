using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Identity;
using WebStore.Domain.ViewModels;

namespace WebStore.Services.Mapping
{
    public static class UserMapper
    {
        public static UserViewModel ToViewModel(this User user)
        {
            UserViewModel viewModel = new UserViewModel
            {
                Id = user.Id,
                Name = user.UserName,
                Email = user.Email,
                Description = user.Description
            };

            return viewModel;
        }
    }
}
