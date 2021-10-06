using Core.DTO;
using System.Collections.Generic;

namespace Core.Interfaces
{
    public interface ICatalogService
    {
        void AddСatalog(CatalogDTO catalogDTO);
        void DeleteСatalog(int? id);
        void EditСatalog(CatalogDTO catalogDTO);
        CatalogDTO GetСatalog(int? id);
        IEnumerable<CatalogDTO> GetCatalogs();
        IEnumerable<CatalogDTO> GetСatalogs(int? providerId);
        void Dispose();
    }
}
