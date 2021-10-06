using System;

namespace WebApplication.Models.Catalog
{
    public class CatalogViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public int ProviderId { get; set; }
        public string ProviderName { get; set; }
    }
}
