using Core.Entities;
using System;
using System.Linq;

namespace Infrastructure.Entities
{
    public static class EntitiesInitializer
    {
        public static void Initialize(ApplicationContext context)
        {
            if (!context.Providers.Any())
            {
                context.Providers.AddRange(
                    new Provider
                    {
                        Email = "takaa@rambler.ru",
                        Info = "Американская кухня, Европейская кухня, Итальянская кухня, Кавказская кухня, Японская кухня",
                        IsActive = true,
                        IsFavorite = true,
                        Name = "Каролин",
                        Path = "carolyn.jpg",
                        TimeWorkTo = new DateTime(2011, 10, 21, 23, 00, 00),
                        TimeWorkWith = new DateTime(2011, 10, 21, 12, 00, 00),
                        WorkingDays = "Понедельник - Воскресенье"
                    },
                    new Provider
                    {
                        Email = "sushipinsk@gmail.com",
                        Info = "Китайская кухня, Японская кухня",
                        IsActive = true,
                        IsFavorite = true,
                        Name = "Sushi Fighter",
                        Path = "sushifighter.jpg",
                        TimeWorkTo = new DateTime(2017, 03, 19, 23, 00, 00),
                        TimeWorkWith = new DateTime(2017, 03, 19, 12, 00, 00),
                        WorkingDays = "Понедельник - Воскресенье"
                    },
                    new Provider
                    {
                        Email = "krytimvertim@gmail.com",
                        Info = "Итальянская кухня, Японская кухня",
                        IsActive = true,
                        IsFavorite = false,
                        Name = "Крутим Вертим",
                        Path = "krytimvertim.jpg",
                        TimeWorkTo = new DateTime(2020, 03, 19, 23, 00, 00),
                        TimeWorkWith = new DateTime(2020, 03, 19, 11, 00, 00),
                        WorkingDays = "Понедельник - Воскресенье"
                    }
                );
                context.SaveChanges();

                context.Catalogs.AddRange(
                    new Catalog
                    {
                        ProviderId = context.Providers.Where(p => p.Email == "takaa@rambler.ru").FirstOrDefault().Id,
                        Name = "Пиццы 'Каролин'",
                        Info = "Только лучшие"
                    },
                    new Catalog
                    { 
                        ProviderId = context.Providers.Where(p => p.Email == "takaa@rambler.ru").FirstOrDefault().Id,
                        Name = "Горячие блюда 'Каролин'",
                        Info = "Разные блюда"
                    },
                    new Catalog
                    { 
                        ProviderId = context.Providers.Where(p => p.Email == "takaa@rambler.ru").FirstOrDefault().Id,
                        Name = "Супы 'Каролин'",
                        Info = "Различные рецепты"
                    },
                    new Catalog
                    { 
                        ProviderId = context.Providers.Where(p => p.Email == "sushipinsk@gmail.com").FirstOrDefault().Id,
                        Name = "Супы 'Sushi Fighter'",
                        Info = "Лучшие рецепты"
                    },
                    new Catalog
                    { 
                        ProviderId = context.Providers.Where(p => p.Email == "krytimvertim@gmail.com").FirstOrDefault().Id,
                        Name = "Пиццы 'Крутим Вертим'",
                        Info = "Различные рецепты, ингредиенты и размеры"
                    },
                    new Catalog
                    {
                        ProviderId = context.Providers.Where(p => p.Email == "krytimvertim@gmail.com").FirstOrDefault().Id,
                        Name = "Напитки 'Крутим Вертим'",
                        Info = "Газировка"
                    }
                );
                context.SaveChanges();

                context.Dishes.AddRange(
                    new Dish
                    {
                        CatalogId = context.Catalogs.Where(p => p.Name == "Пиццы 'Каролин'").FirstOrDefault().Id,
                        Info = "Пицца соус, пепперони, сыр Моцарелла",
                        Name = "Пицца Пепперони",
                        Weight = 520,
                        Price = 16.00M,
                        Path = "pepperoni.jpg"
                    },
                    new Dish
                    {
                        CatalogId = context.Catalogs.Where(p => p.Name == "Горячие блюда 'Каролин'").FirstOrDefault().Id,
                        Info = "Мясная сковорода",
                        Name = "Мясная сковорода",
                        Weight = 300,
                        Price = 16.00M,
                        Path = "mysnyaskovorodka.jpg",
                    },
                    new Dish
                    {
                        CatalogId = context.Catalogs.Where(p => p.Name == "Горячие блюда 'Каролин'").FirstOrDefault().Id,
                        Info = "Сковорода Три мяса",
                        Name = "Сковорода Три мяса",
                        Weight = 300,
                        Price = 21.00M,
                        Path = "trimysaskovorodka.jpg"
                    },
                    new Dish
                    {
                        CatalogId = context.Catalogs.Where(p => p.Name == "Горячие блюда 'Каролин'").FirstOrDefault().Id,
                        Info = "Стейк из говядины с соусом Вlack Gurrant",
                        Name = "Стейк из говядины с соусом Вlack Gurrant",
                        Weight = 200,
                        Price = 20.00M,
                        Path = "steikizgovyadin.jpg"
                    },
                    new Dish
                    {
                        CatalogId = context.Catalogs.Where(p => p.Name == "Горячие блюда 'Каролин'").FirstOrDefault().Id,
                        Info = "Филе сёмги",
                        Name = "Филе сёмги",
                        Weight = 200,
                        Price = 26.00M,
                        Path = "filesemgi.jpg"
                    },
                    new Dish
                    {
                        CatalogId = context.Catalogs.Where(p => p.Name == "Супы 'Каролин'").FirstOrDefault().Id,
                        Info = "Солянка",
                        Name = "Солянка",
                        Weight = 300,
                        Price = 9.00M,
                        Path = "solyanka.jpg"
                    },
                    new Dish
                    {
                        CatalogId = context.Catalogs.Where(p => p.Name == "Супы 'Каролин'").FirstOrDefault().Id,
                        Info = "Фунги крем‑суп с грудинкой",
                        Name = "Фунги крем‑суп с грудинкой",
                        Weight = 250,
                        Price = 8.00M,
                        Path = "fyngi.jpg"
                    },
                    new Dish
                    {
                        CatalogId = context.Catalogs.Where(p => p.Name == "Супы 'Sushi Fighter'").FirstOrDefault().Id,
                        Info = "Грибы намеко, тофу сыр. +контейнер",
                        Name = "Суп Мисо",
                        Weight = 250,
                        Price = 4.50M,
                        Path = "miso.jpg"
                    },
                    new Dish
                    {
                        CatalogId = context.Catalogs.Where(p => p.Name == "Супы 'Sushi Fighter'").FirstOrDefault().Id,
                        Info = "Грибы намеко, тофу сыр, тигровые креветки. +контейнер",
                        Name = "Суп Мисо с креветками",
                        Weight = 250,
                        Price = 6.50M,
                        Path = "misowithkrivetki.jpg"
                    },
                    new Dish
                    {
                        CatalogId = context.Catalogs.Where(p => p.Name == "Супы 'Sushi Fighter'").FirstOrDefault().Id,
                        Info = "Тофу сыр, жареный лосось, номеко грибы. +контейнер",
                        Name = "Суп Мисо Саке",
                        Weight = 250,
                        Price = 6.50M,
                        Path = "misocake.jpg"
                    },
                    new Dish
                    {
                        CatalogId = context.Catalogs.Where(p => p.Name == "Супы 'Sushi Fighter'").FirstOrDefault().Id,
                        Info = "Удон лапша, тофу сыр. +контейнер",
                        Name = "Суп Мисо Удон",
                        Weight = 250,
                        Price = 4.80M,
                        Path = "misoydon.jpg"
                    },
                    new Dish
                    {
                        CatalogId = context.Catalogs.Where(p => p.Name == "Пиццы 'Крутим Вертим'").FirstOrDefault().Id,
                        Info = "Ранч (половина пиццы), томатная основа (половина пиццы), шампиньоны, перец болгарский, курица, ветчина, моцарелла",
                        Name = "Пицца 4 сезона 30 см",
                        Weight = 550,
                        Price = 12.90M,
                        Path = "sezon.jpg"
                    },
                    new Dish
                    {
                        CatalogId = context.Catalogs.Where(p => p.Name == "Пиццы 'Крутим Вертим'").FirstOrDefault().Id,
                        Info = "Томатная основа, перец Халапеньо, охотничьи колбаски, сыр Моцарелла, соус Барбекю",
                        Name = "Пицца Баварская 30 см",
                        Weight = 550,
                        Price = 12.90M,
                        Path = "bavarskaya.jpg"
                    },
                    new Dish
                    {
                        CatalogId = context.Catalogs.Where(p => p.Name == "Пиццы 'Крутим Вертим'").FirstOrDefault().Id,
                        Info = "Соус Ранч, бекон, ветчина, охотничьи колбаски, красный лук, огурец, моцарелла",
                        Name = "Пицца Богатырская 35 см",
                        Weight = 760,
                        Price = 15.90M,
                        Path = "bogataya.jpg"
                    },
                    new Dish
                    {
                        CatalogId = context.Catalogs.Where(p => p.Name == "Напитки 'Крутим Вертим'").FirstOrDefault().Id,
                        Info = "Кока-Кола",
                        Name = "Кока-Кола 1л",
                        Weight = 1,
                        Price = 3.00M,
                        Path = "kola.jpg"
                    },
                    new Dish
                    {
                        CatalogId = context.Catalogs.Where(p => p.Name == "Напитки 'Крутим Вертим'").FirstOrDefault().Id,
                        Info = "Спрайт",
                        Name = "Спрайт 1л",
                        Weight = 1,
                        Price = 3.00M,
                        Path = "sprait.jpg"
                    },
                    new Dish
                    {
                        CatalogId = context.Catalogs.Where(p => p.Name == "Напитки 'Крутим Вертим'").FirstOrDefault().Id,
                        Info = "Фанта Апельсин",
                        Name = "Фанта Апельсин 1л",
                        Weight = 1,
                        Price = 3.00M,
                        Path = "fanta.jpg"
                    });

                context.SaveChanges();

                context.Menus.AddRange(
                    new Menu
                    {
                        ProviderId = context.Providers.Where(p => p.Email == "takaa@rambler.ru").FirstOrDefault().Id,
                        Date = DateTime.Now,
                        Info = "Вкусные пиццы и не только"
                    },
                    new Menu
                    {
                        ProviderId = context.Providers.Where(p => p.Email == "sushipinsk@gmail.com").FirstOrDefault().Id,
                        Date = DateTime.Now,
                        Info = "Супы"
                    },
                    new Menu
                    {
                        ProviderId = context.Providers.Where(p => p.Email == "krytimvertim@gmail.com").FirstOrDefault().Id,
                        Date = DateTime.Now.AddDays(1),
                        Info = "Пиццы и напитки"
                    });

                context.SaveChanges();

                context.MenuDishes.AddRange(
                    new MenuDishes
                    {
                        MenuId = context.Menus.Where(p => p.Info == "Вкусные пиццы и не только").FirstOrDefault().Id,
                        DishId = context.Dishes.Where(p => p.Name == "Пицца Пепперони").FirstOrDefault().Id
                    },
                    new MenuDishes
                    {
                        MenuId = context.Menus.Where(p => p.Info == "Супы").FirstOrDefault().Id,
                        DishId = context.Dishes.Where(p => p.Name == "Суп Мисо").FirstOrDefault().Id
                    },
                    new MenuDishes
                    {
                        MenuId = context.Menus.Where(p => p.Info == "Пиццы и напитки").FirstOrDefault().Id,
                        DishId = context.Dishes.Where(p => p.Name == "Пицца Богатырская 35 см").FirstOrDefault().Id
                    },
                    new MenuDishes
                    {
                        MenuId = context.Menus.Where(p => p.Info == "Пиццы и напитки").FirstOrDefault().Id,
                        DishId = context.Dishes.Where(p => p.Name == "Кока-Кола 1л").FirstOrDefault().Id
                    },
                    new MenuDishes
                    {
                        MenuId = context.Menus.Where(p => p.Info == "Пиццы и напитки").FirstOrDefault().Id,
                        DishId = context.Dishes.Where(p => p.Name == "Фанта Апельсин 1л").FirstOrDefault().Id
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
