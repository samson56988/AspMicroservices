using CatalogService.Entities;
using MongoDB.Driver;

namespace CatalogService.Data
{
    public class CatalogContext : ICalogContext
    {
        

        public CatalogContext(IConfiguration configuration)
        {
            
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));
             Products = database.GetCollection<Products>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
            CatalogContextSeed.SeedData(Products);
        
        }
        public IMongoCollection<Products> Products { get; }
    }
}
