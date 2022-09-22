using CatalogService.Entities;

namespace CatalogService.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Products>> GetProducts();
        Task<Products> GetProduct(string Id);
        Task<IEnumerable<Products>> GetProductByName(string name);
        Task<IEnumerable<Products>> GetProductByCategory(string categoryName);
        Task CreateProduct(Products products);
        Task<bool> UpdateProduct(Products products);
        Task<bool> DeleteProduct(string Id);
    }
}
