﻿using Core.DTO;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Core.Services
{
    public class CartService : ICartService
    {
        private IUnitOfWork Database { get; set; }

        public CartService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public Cart Create(string applicationUserId)
        {
            if (applicationUserId == null)
                throw new ValidationException("Не установлен id пользователя", "");

            Database.Cart.Create(new Cart() { ApplicationUserId = applicationUserId });
            Database.Save();

            return Database.Cart.Find(p => p.ApplicationUserId == applicationUserId).FirstOrDefault();
        }

        public CartDTO GetCart(string applicationUserId)
        {
            if (applicationUserId == null)
                throw new ValidationException("Идентификатор пользователя не установлен", string.Empty);

            var cart = Database.Cart.Find(p => p.ApplicationUserId == applicationUserId).FirstOrDefault();

            cart = cart ?? Create(applicationUserId);

            CartDTO cartDTO = new CartDTO()
            {
                ApplicationUserId = cart.ApplicationUserId,
                Id = cart.Id
            };

            return cartDTO;
        }

        public IEnumerable<CartDishesDTO> GetCartDishes(string applicationUserId)
        {
            if (applicationUserId == null)
                throw new ValidationException("Идентификатор пользователя не установлен", string.Empty);

            var cart = GetCart(applicationUserId);

            if (cart == null)
                throw new ValidationException("Корзина не найдена", string.Empty);

            var cartDishes = Database.CartDishes.Find(p => p.CartId == cart.Id);

            var cartDishesDTO = new List<CartDishesDTO>();

            foreach (var cartD in cartDishes)
            {
                cartDishesDTO.Add(new CartDishesDTO()
                {
                    CartId = cart.Id,
                    Id = cartD.Id,
                    Count = cartD.Count,
                    DishId = cartD.DishId,
                    Info = cartD.Dish.Info,
                    Name = cartD.Dish.Name,
                    Path = cartD.Dish.Path,
                    Price = cartD.Dish.Price,
                    Weight = cartD.Dish.Weight
                });
            }

            return cartDishesDTO;
        }

        public void DeleteCartDish(int? id, string applicationUserId)
        {
            if (applicationUserId == null)
                throw new ValidationException("Не установлен id пользователя", string.Empty);

            var cart = GetCart(applicationUserId);

            if (cart == null)
                throw new ValidationException("Корзина не найдена", string.Empty);

            if (id == null)
                throw new ValidationException("Не установлено id удаляемого блюда в корзине", string.Empty);

            var cartDish = Database.CartDishes.Get(id.Value);

            if (cartDish.CartId != cart.Id)
                throw new ValidationException("Блюдо в корзине не найдено", string.Empty);

            Database.CartDishes.Delete(id.Value);
            Database.Save();
        }

        public void AddDishToCart(int? dishId, string applicationUserId)
        {
            if (applicationUserId == null)
                throw new ValidationException("Идентификатор пользователя не установлен", string.Empty);

            var cart = GetCart(applicationUserId);

            if (cart == null)
                throw new ValidationException("Корзина не найдена", string.Empty);

            if (dishId == null)
                throw new ValidationException("Меню блюдо добавить идентификатор в корзину не установлено", string.Empty);

            // if it already exists in the basket, then it is increased by 1(if not, then created with the number of 1)
            if (GetCartDishes(applicationUserId).Where(p => p.DishId == dishId.Value).Count() > 0)
            {
                var cartDish = Database.CartDishes.Find(p => p.CartId == cart.Id).Where(p => p.DishId == dishId.Value).FirstOrDefault();
                cartDish.Count++;
                Database.CartDishes.Update(cartDish);
                Database.Save();
            }
            else
            {
                Dish dish = Database.Dish.Get(dishId.Value);

                if (dish == null)
                    throw new ValidationException("Блюдо не найдено", string.Empty);

                Database.CartDishes.Create(new CartDishes()
                {
                    CartId = cart.Id,
                    Count = 1,
                    DishId = dishId.Value
                });

                Database.Save();
            }
        }

        public void AllDeleteDishesToCart(string applicationUserId)
        {
            if (applicationUserId == null)
                throw new ValidationException("Идентификатор пользователя не установлен", string.Empty);

            var cart = GetCart(applicationUserId);

            if (cart == null)
                throw new ValidationException("Корзина не найдена", string.Empty);

            var cartDish = GetCartDishes(applicationUserId);

            if (cartDish.Count() < 1)
                throw new ValidationException("Корзина пуста", string.Empty);

            foreach (var cartD in cartDish)
            {
                Database.CartDishes.Delete(cartD.Id);
            }

            Database.Save();
        }

        public void UpdateCountDishInCart(string applicationUserId, int? dishCartId, int count)
        {
            if (applicationUserId == null)
                throw new ValidationException("Идентификатор пользователя не установлен", string.Empty);

            var cart = GetCart(applicationUserId);

            if (cart == null)
                throw new ValidationException("Корзина не найдена", string.Empty);

            var cartDishes = GetCartDishes(applicationUserId);

            if (cartDishes.Count() < 1)
                throw new ValidationException("Корзина пуста", string.Empty);

            if (cartDishes.Where(p => p.Id == dishCartId).Count() < 1)
                throw new ValidationException("Указанного блюда нет в корзине", string.Empty);

            CartDishes cartDishe = Database.CartDishes.Find(p => p.Id == dishCartId).FirstOrDefault();

            if (count <= 0)
                throw new ValidationException("Количество должно быть положительным целым числом", string.Empty);

            cartDishe.Count = count;

            Database.CartDishes.Update(cartDishe);
            Database.Save();
        }

        public decimal FullPriceCart(string applicationUserId)
        {
            decimal fullPrice = 0;

            var cartDishes = GetCartDishes(applicationUserId);

            foreach (var cartDish in cartDishes)
            {
                fullPrice += cartDish.Count * cartDish.Price;
            }

            return fullPrice;
        }
    }
}
