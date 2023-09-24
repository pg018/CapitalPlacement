
using CapitalPlacement.CoreLevel.ServiceContracts;
using CapitalPlacement.Infrastructure;
using Microsoft.Azure.Cosmos;
using System.Text.Json;

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

        public async Task<string?> ReadItemAsyncString(string id)
        {

            var sqlQueryText = $"SELECT * FROM c WHERE c.id = '{id}'";
            var queryDefinition = new QueryDefinition(sqlQueryText);
            string? content = null;

            var queryResultSetIterator = _cosmosContainer.GetItemQueryStreamIterator(queryDefinition);

            while (queryResultSetIterator.HasMoreResults)
            {
                using (ResponseMessage responseMessage = await queryResultSetIterator.ReadNextAsync())
                {
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        using (StreamReader streamReader = new StreamReader(responseMessage.Content))
                        {
                            content = await streamReader.ReadToEndAsync();
                        }
                        JsonDocument document = JsonDocument.Parse(content);
                        var docElement = JsonDocument.Parse(document.RootElement.GetProperty("Documents").ToString())
                            .RootElement
                            .EnumerateArray().ToList();
                        // getting the first document only as id is unique and we called for one only
                        content = docElement[0].ToString();
                    }
                }
            }
            return content;
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
                        break;
                    }
                }
            }
            return finalDoc;
        }

        public async Task ReplaceItemAsync(TDocument document)
        {
            await _cosmosContainer.ReplaceItemAsync<TDocument>(document, document.id);
        }

        public async Task ReplaceItemAsyncString(string finalDoc, string id)
        {
            var document = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(finalDoc);
            await _cosmosContainer.ReplaceItemAsync(document, id);
        }
    }
}
