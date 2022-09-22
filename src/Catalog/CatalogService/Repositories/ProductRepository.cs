
using CatalogService.Data;
using CatalogService.Entities;
using MongoDB.Driver;
using System.Xml.Linq;

namespace CatalogService.Repositories
{
    public class ProductRepository : IProductRepository
    {

        private readonly ICalogContext _calogContext;

        public ProductRepository(ICalogContext calogContext)
        {
            _calogContext = calogContext;
        }

        public async Task CreateProduct(Products products)
        {
            await _calogContext.Products.InsertOneAsync(products);
        }

        public async Task<bool> DeleteProduct(string Id)
        {
            FilterDefinition<Products> filter = Builders<Products>.Filter.Eq(p => p.ID, Id);

            DeleteResult deleteResult = await _calogContext.Products.DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;

        }

        public async Task<Products> GetProduct(string Id)
        {
            return await _calogContext.Products.Find(p => p.ID == Id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Products>> GetProductByCategory(string categoryName)
        {
            FilterDefinition<Products> filter = Builders<Products>.Filter.Eq(p => p.Category, categoryName);

            return await _calogContext.Products.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Products>> GetProductByName(string name)
        {
            FilterDefinition<Products> filter = Builders<Products>.Filter.Eq(p => p.Name, name);

            return await _calogContext.Products.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Products>> GetProducts()
        {
            return await _calogContext.Products.Find(p => true).ToListAsync();
        }

        public async Task<bool> UpdateProduct(Products products)
        {
            var updateResult = await _calogContext.Products
                .ReplaceOneAsync(filter: g => g.ID == products.ID, replacement: products);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount> 0;
        }
    }
}
