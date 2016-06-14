using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ProductManagement.Models
{
    public class ProductsRepository : IProductsRepository
    {
        private List<Product> Items { get; set; }

        private readonly IProductOptionsRepository _productOptions;

        public ProductsRepository(IProductOptionsRepository productOptions)
        {
            _productOptions = productOptions;
        }

        private IEnumerable<Product> GetProducts(string where)
        {
            Items = new List<Product>();
            var rdr = Helpers.ExecuteReader($"select id from product {where}");
            while (rdr.Read())
            {
                var id = Guid.Parse(rdr["id"].ToString());
                Items.Add(Create(id));
            }

            return Items;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return GetProducts(string.Empty);
        }

        public IEnumerable<Product> GetProductByName(string name)
        {
            return GetProducts($"where lower(name) like '%{name.ToLower()}%'");
        }

        public void DeleteProduct(Guid productId)
        {
            foreach (var option in _productOptions.GetProductOptions(productId))
                _productOptions.DeleteProductOption(option.Id);

            Helpers.ExecuteNonQuery($"delete from product where id = '{productId}'");
        }

        public void Save(Product product)
        {
            var cmd = product.IsNew ?
                $"insert into product (id, name, description, price, deliveryprice) values ('{product.Id}', '{product.Name}', '{product.Description}', {product.Price}, {product.DeliveryPrice})" :
                $"update product set name = '{product.Name}', description = '{product.Description}', price = {product.Price}, deliveryprice = {product.DeliveryPrice} where id = '{product.Id}'";
            Helpers.ExecuteNonQuery(cmd);
        }

        public Product Create(Guid id)
        {
            var product = new Product {Id = id};
            var rdr = Helpers.ExecuteReader($"select * from product where id = '{product.Id}'");
            if (!rdr.Read())
                return product;

            product.Id = Guid.Parse(rdr["Id"].ToString());
            product.Name = rdr["Name"].ToString();
            product.Description = (DBNull.Value == rdr["Description"]) ? null : rdr["Description"].ToString();
            product.Price = decimal.Parse(rdr["Price"].ToString());
            product.DeliveryPrice = decimal.Parse(rdr["DeliveryPrice"].ToString());

            return product;
        }
    }
}