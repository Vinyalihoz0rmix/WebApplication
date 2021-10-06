using Core.DTO;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Core.Services
{
    public class MenuService : IMenuService
    {
        private IUnitOfWork Database { get; set; }

        public MenuService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public IEnumerable<MenuDTO> GetAllMenus()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Menu, MenuDTO>()).CreateMapper();
            var menus = mapper.Map<IEnumerable<Menu>, List<MenuDTO>>(Database.Menu.GetAll());

            return menus;
        }

        public List<int> GetMenuIdDishes(int? menuId)
        {
            if (menuId == null)
                throw new ValidationException("Идентификатор меню не установлен", string.Empty);

            var menu = Database.Menu.Get(menuId.Value);

            if (menu == null)
                throw new ValidationException("Меню не найдено", string.Empty);

            List<int> addedDish = new List<int>();

            var menuDishes = GetMenuDishes(menuId);

            foreach (var menuDish in menuDishes)
            {
                addedDish.Add(menuDish.DishId.Value);
            }

            return addedDish;
        }

        public IEnumerable<MenuDTO> GetMenus(int? providerId)
        {
            if (providerId == null)
                throw new ValidationException("Идентификатор поставщика не установлен", string.Empty);

            var provider = Database.Provider.Get(providerId.Value);

            if (provider == null)
                throw new ValidationException("Поставщик не найден", string.Empty);

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Menu, MenuDTO>()).CreateMapper();
            var menus = mapper.Map<IEnumerable<Menu>, List<MenuDTO>>(Database.Menu.GetAll());

            return menus.Where(p => p.ProviderId == providerId).ToList();
        }

        public void AddMenu(MenuDTO menuDTO)
        {
            if (menuDTO.Date == null)
                throw new ValidationException("Дата не установлена", string.Empty);

            if (menuDTO.Date.Date < DateTime.Now.Date)
                throw new ValidationException("Меню не может быть составлено за прошедшую дату", string.Empty);

            var menus = Database.Menu.GetAll().Where(p => p.ProviderId == menuDTO.ProviderId);

            if (menus.Where(p => p.Date.Date == menuDTO.Date).FirstOrDefault() != null)
                throw new ValidationException("Меню уже существует на эту дату", string.Empty);

            Menu menu = new Menu()
            {
                Date = menuDTO.Date,
                Info = menuDTO.Info,
                ProviderId = menuDTO.ProviderId
            };

            Database.Menu.Create(menu);
            Database.Save();
        }

        public void DeleteMenu(int? id)
        {
            if (id == null)
                throw new ValidationException("Идентификатор меню не установлен", string.Empty);

            var provider = Database.Menu.Get(id.Value);

            if (provider == null)
                throw new ValidationException("Меню не найдено", string.Empty);

            var dishesInMenu = Database.MenuDishes.GetAll().Where(p => p.MenuId == id.Value);

            foreach (var dishInMenu in dishesInMenu)
            {
                Database.MenuDishes.Delete(dishInMenu.Id);
            }

            Database.Menu.Delete(id.Value);
            Database.Save();
        }

        public MenuDTO GetMenu(int? id)
        {
            if (id == null)
                throw new ValidationException("Идентификатор меню не установлен", string.Empty);

            var menu = Database.Menu.Get(id.Value);

            if (menu == null)
                throw new ValidationException("Меню не найдено", string.Empty);

            MenuDTO menuDTO = new MenuDTO()
            {
                Id = menu.Id,
                Info = menu.Info,
                Date = menu.Date,
                ProviderId = menu.ProviderId
            };

            return menuDTO;
        }

        public void EditMenu(MenuDTO menuDTO)
        {
            if (menuDTO.Date == null)
                throw new ValidationException("Дата не установлена", string.Empty);

            if (menuDTO.Date.Date < DateTime.Now.Date)
                throw new ValidationException("Меню не может быть составлено за прошедшую дату", string.Empty);

            var menus = Database.Menu.GetAll().Where(p => p.ProviderId == menuDTO.ProviderId);
            var checkDateMenu = menus.Where(p => p.Date.Date == menuDTO.Date).FirstOrDefault();

            if (checkDateMenu != null && checkDateMenu.Id != menuDTO.Id)
                throw new ValidationException("Меню уже существует на эту дату", string.Empty);

            Menu menu = Database.Menu.Get(menuDTO.Id);

            if (menu == null)
                throw new ValidationException("Меню не найдено", string.Empty);

            menu.Info = menuDTO.Info;
            menu.Date = menuDTO.Date;

            Database.Menu.Update(menu);
            Database.Save();
        }

        public IEnumerable<MenuDishesDTO> GetMenuDishes(int? menuId)
        {
            if (menuId == null)
                throw new ValidationException("Идентификатор меню не установлен", string.Empty);

            var menuDishes = Database.MenuDishes.GetAll().Where(p => p.MenuId == menuId.Value).ToList();

            List<MenuDishesDTO> menuDishesDTOs = new List<MenuDishesDTO>();

            foreach (var menuDish in menuDishes)
            {
                menuDishesDTOs.Add(new MenuDishesDTO()
                {
                    Id = menuDish.Id,
                    DishId = menuDish.DishId.Value,
                    Info = menuDish.Dish.Info,
                    Name = menuDish.Dish.Name,
                    Path = menuDish.Dish.Path,
                    Price = menuDish.Dish.Price,
                    Weight = menuDish.Dish.Weight,
                    MenuId = menuDish.MenuId.Value,
                    CatalogId = menuDish.Dish.CatalogId
                });
            }

            return menuDishesDTOs;
        }

        public void MakeMenu(int? menuId, List<int> newAddedDishes, List<int> allSelect)
        {
            if (menuId == null)
                throw new ValidationException("Не установлен id меню", string.Empty);

            var menuDishes = GetMenuDishes(menuId);

            // удалить все выбранные блюда
            foreach (int id in allSelect)
            {
                // добавить в меню блюдо
                var dbMenuDish = Database.MenuDishes.GetAll().Where(p => p.MenuId == menuId).Where(p => p.DishId == id).FirstOrDefault();

                if (newAddedDishes.Contains(id))
                {
                    if (dbMenuDish == null)
                    {
                        Database.MenuDishes.Create(new MenuDishes()
                        {
                            DishId = id,
                            MenuId = menuId
                        });
                    }
                }
                else
                {
                    if (dbMenuDish != null)
                        Database.MenuDishes.Delete(dbMenuDish.Id);
                }
            }

            Database.Save();
        }

        public void DeleteDishInMenu(int? id)
        {
            if (id == null)
                throw new ValidationException("Идентификатор блюда в меню не установлен", string.Empty);

            MenuDishes menuDishes = Database.MenuDishes.Get(id.Value);

            if (menuDishes == null)
                throw new ValidationException("Блюдо в меню не найдено", string.Empty);

            Database.MenuDishes.Delete(id.Value);
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
