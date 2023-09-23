
using CapitalPlacement.Infrastructure;

namespace CapitalPlacement.CoreLevel.ServiceContracts
{
    public interface ICosmosService<Document> where Document: ICosmosDocument
    {
        public Task CreateItemAsync(Document document);
        public Task<Document?> GetByIdAsync(string id);
        public Task ReplaceItemAsync(Document document);
    }
}
