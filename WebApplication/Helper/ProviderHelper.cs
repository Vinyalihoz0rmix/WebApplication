using AutoMapper;
using Core.Constants;
using Core.DTO;
using Core.Interfaces;
using System.Collections.Generic;
using WebApplication.Interfaces;
using WebApplication.Models.Provider;

namespace WebApplication.Helper
{
    public class ProviderHelper : IProviderHelper
    {
        private readonly IProviderService _providerService;

        private readonly string _path;

        public ProviderHelper(IProviderService providerService)
        {
            _providerService = providerService;
            _path = PathConstants.PATH_PROVIDER;
        }

        public IEnumerable<ProviderViewModel> GetProviders()
        {
            IEnumerable<ProviderDTO> providersDTOs = _providerService.GetProviders();

            return ConvertProvidersDtoToView(providersDTOs);
        }

        public IEnumerable<ProviderViewModel> GetProvidersFavorite()
        {
            IEnumerable<ProviderDTO> providersDTOs = _providerService.GetFavoriteProviders();

            return ConvertProvidersDtoToView(providersDTOs);
        }

        public List<string> GetSearchSelection(bool isAdmin)
        {
            List<string> searchSelection = new List<string>() { "Поиск по" };

            if (isAdmin)
            {
                searchSelection.Add("Id");
            }

            searchSelection.AddRange(new string[] { "Имени", "Электронной почте", "Время работы с", "Время работы до", "Работает", "Не работает" });

            return searchSelection;
        }

        private IEnumerable<ProviderViewModel> ConvertProvidersDtoToView(IEnumerable<ProviderDTO> providersDTOs)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ProviderDTO, ProviderViewModel>()).CreateMapper();
            var providers = mapper.Map<IEnumerable<ProviderDTO>, List<ProviderViewModel>>(providersDTOs);

            foreach (var p in providers)
            {
                p.Path = _path + p.Path;
            }

            return providers;
        }
    }
}
