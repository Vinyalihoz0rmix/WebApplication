using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication.Models.Provider
{
    public class ProviderListViewModel
    {
        public SelectList SearchSelection { get; set; }
        public ListProviderViewModel ListProviders { get; set; }
        public string SearchString { get; set; }
        public string SearchSelectionString { get; set; }
    }
}
