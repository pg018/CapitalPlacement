
using CapitalPlacement.CoreLevel.ServiceContracts;
using CapitalPlacement.Infrastructure;
using Microsoft.Azure.Cosmos;

namespace CapitalPlacement.CoreLevel.Services
{
    public class CosmosService<TDocument> : ICosmosService<TDocument> where TDocument : CosmosDocument
    {
        // this service is for all database related actions
        private readonly Container _cosmosContainer;

        public CosmosService(Container cosmosContainer)
        {
            _cosmosContainer = cosmosContainer;
        }

        public async Task CreateItemAsync(TDocument document)
        {
            await _cosmosContainer.CreateItemAsync(document);
        }

        public async Task<TDocument?> GetByIdAsync(string id)
        {
            var queryText = $"SELECT * FROM c WHERE c.id = '{id}'";
            var queryDefinition = new QueryDefinition(queryText);
            var queryResultIterator = _cosmosContainer.GetItemQueryIterator<TDocument>(queryDefinition);
            TDocument? finalDoc = null;
            while (queryResultIterator.HasMoreResults)
            {
                // checking if the document exists or not
                var hasDocument = await queryResultIterator.ReadNextAsync();
                if (hasDocument.Any())
                {
                    foreach (var doc in hasDocument)
                    {
                        finalDoc = doc;
                    }
                }
            }
            return finalDoc;
        }

        public async Task ReplaceItemAsync(TDocument document)
        {
            await _cosmosContainer.ReplaceItemAsync<TDocument>(document, document.id);
        }
    }
}
