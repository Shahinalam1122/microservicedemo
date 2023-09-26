using Catalog.API.Interfaces.Manager;
using Catalog.API.Models;
using Catalog.API.Repository;
using MongoRepo.Manager;
using MongoRepo.Repository;

namespace Catalog.API.Manager
{
    public class ProductManager : CommonManager<Product>, IProductManager
    {
        //public ProductManager(CommonRepository<Product> repository) : base(repository)
        //{
        //}
        public ProductManager() : base(new ProductRepository())
        {
        }

        public List<Product> GetByCategory(string category)
        {
            return GetAll(c=>c.Category==category).ToList();
        }
    }
}
