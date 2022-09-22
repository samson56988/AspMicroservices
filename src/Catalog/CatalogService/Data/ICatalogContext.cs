using CatalogService.Entities;
using MongoDB.Driver;

namespace CatalogService.Data
{
    public  interface ICalogContext
    {
        IMongoCollection<Products> Products { get; }
    }
}
