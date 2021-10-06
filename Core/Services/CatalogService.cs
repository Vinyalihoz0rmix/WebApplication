using Core.DTO;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace Core.Services
{
    public class CatalogService : ICatalogService
    {
        private IUnitOfWork Database { get; set; }

        public CatalogService(IUnitOfWork iow)
        {
            Database = iow;
        }

        public IEnumerable<CatalogDTO> GetCatalogs()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Catalog, CatalogDTO>()).CreateMapper();
            var сatalogs = mapper.Map<IEnumerable<Catalog>, List<CatalogDTO>>(Database.Catalog.GetAll());

            return сatalogs;
        }

        public IEnumerable<CatalogDTO> GetСatalogs(int? providerId)
        {
            if (providerId == null)
                throw new ValidationException("Идентификатор доставщика не установлен", string.Empty);

            var provider = Database.Provider.Get(providerId.Value);

            if (provider == null)
                throw new ValidationException("Доставщик не найден", string.Empty);

            return GetCatalogs().Where(p => p.ProviderId == providerId).ToList();
        }

        public void AddСatalog(CatalogDTO catalogDTO)
        {
            if (catalogDTO.Name == null)
                throw new ValidationException("Имя не установлено", string.Empty);

            Catalog menu = new Catalog()
            {
                Info = catalogDTO.Info,
                Name = catalogDTO.Name,
                ProviderId = catalogDTO.ProviderId
            };

            Database.Catalog.Create(menu);
            Database.Save();
        }

        public void DeleteСatalog(int? id)
        {
            if (id == null)
                throw new ValidationException("Идентификатор каталога не установлен", string.Empty);

            var provider = Database.Catalog.Get(id.Value);

            if (provider == null)
                throw new ValidationException("Каталог не найден", string.Empty);

            var dishesInMenu = Database.MenuDishes.GetAll().Where(p => p.Dish.CatalogId == id.Value);

            foreach (var dishInMenu in dishesInMenu)
            {
                Database.MenuDishes.Delete(dishInMenu.Id);
            }

            Database.Catalog.Delete(id.Value);
            Database.Save();
        }

        public CatalogDTO GetСatalog(int? id)
        {
            if (id == null)
                throw new ValidationException("Идентификатор каталога не установлен", string.Empty);

            var сatalog = Database.Catalog.Get(id.Value);

            if (сatalog == null)
                throw new ValidationException("Каталог не найден", string.Empty);

            CatalogDTO сatalogDTO = new CatalogDTO()
            {
                Id = сatalog.Id,
                Info = сatalog.Info,
                Name = сatalog.Name,
                ProviderId = сatalog.ProviderId
            };

            return сatalogDTO;
        }

        public void EditСatalog(CatalogDTO сatalogDTO)
        {
            if (сatalogDTO.Name == null)
                throw new ValidationException("Название не установлено", string.Empty);

            Catalog сatalog = Database.Catalog.Get(сatalogDTO.Id);

            if (сatalog == null)
                throw new ValidationException("Каталог не найден", string.Empty);

            сatalog.Info = сatalogDTO.Info;
            сatalog.Name = сatalogDTO.Name;

            Database.Catalog.Update(сatalog);
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
