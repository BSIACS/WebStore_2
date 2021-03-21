﻿using WebStore.Domain.ViewModels;

namespace WebStore.Interfaces.Interfaces
{
    public interface ICartService
    {
        void AddToCart(int id);

        void DecrementFromCart(int id);

        void RemoveFromCart(int id);

        void Clear();

        CartViewModel TransformToViewModel();
    }
}
