using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _catalogContext;

        public ProductRepository(ICatalogContext catalogContext)
        {
            _catalogContext = catalogContext;
        }

        public async Task CreateProduct(Product product)
        {
            await _catalogContext.Products.InsertOneAsync(product);
        }

        public async Task<bool> DeleteProduct(string id)
        {
            var result = await _catalogContext.Products.DeleteOneAsync(id);
            
            return result.IsAcknowledged && result.DeletedCount > 0;
        }

        public async Task<Product> GetProduct(string id)
        {
            var result = await _catalogContext
                             .Products
                             .Find(p => p.ItemId == id)
                             .FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<Product>> GetProductByCatagory(string catagoryName)
        {
            var result = await _catalogContext
                            .Products
                            .Find(p => p.Category == catagoryName)
                            .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            var result = await _catalogContext
                            .Products
                            .Find(p => p.Name == name)
                            .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            var result = await _catalogContext
                            .Products
                            .Find(p => true)
                            .ToListAsync();
            return result;
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var filter = Builders<Product>.Filter.Eq(p=> p.ItemId, product.ItemId);
            var result = await _catalogContext.Products.ReplaceOneAsync(filter, product);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }
    }
}
