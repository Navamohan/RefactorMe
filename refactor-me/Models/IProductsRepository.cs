using System;
using System.Collections.Generic;

namespace ProductManagement.Models
{
    public interface IProductsRepository
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductByName(string name);
        void DeleteProduct(Guid productId);
        void Save(Product product);
        Product Create(Guid id);
    }
}
