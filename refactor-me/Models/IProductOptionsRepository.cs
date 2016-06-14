using System;
using System.Collections.Generic;

namespace ProductManagement.Models
{
    public interface IProductOptionsRepository
    {
        IEnumerable<ProductOption> GetAllProductOptions();
        IEnumerable<ProductOption> GetProductOptions(Guid productId);
        void DeleteProductOption(Guid productOptionId);
        void Save(ProductOption option);
        ProductOption Create(Guid id);
    }
}
