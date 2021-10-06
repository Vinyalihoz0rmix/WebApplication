using System.Collections.Generic;
using WebApplication.Models.Provider;

namespace WebApplication.Interfaces
{
    public interface IProviderHelper
    {
        IEnumerable<ProviderViewModel> GetProviders();
        IEnumerable<ProviderViewModel> GetProvidersFavorite();
        List<string> GetSearchSelection(bool isAdmin);
    }
}
