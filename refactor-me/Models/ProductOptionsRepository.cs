using System;
using System.Collections.Generic;

namespace ProductManagement.Models
{
    /// <summary>
    /// Product Options Repository class to handle CRUD operations 
    /// </summary>
    public class ProductOptionsRepository : IProductOptionsRepository
    {
        private List<ProductOption> Options { get; set; }

        public IEnumerable<ProductOption> GetAllProductOptions()
        {
            return GetProductOptions(string.Empty);
        }

        public IEnumerable<ProductOption> GetProductOptions(Guid productId)
        {
            return GetProductOptions($"where productid = '{productId}'");
        }

        private IEnumerable<ProductOption> GetProductOptions(string where)
        {
            Options = new List<ProductOption>();
            var rdr = Helpers.ExecuteReader($"select id from productoption {where}");
            while (rdr.Read())
            {
                var id = Guid.Parse(rdr["id"].ToString());
                Options.Add(Create(id));
            }

            return Options;
        }

        public void DeleteProductOption(Guid productOptionId)
        {
            Helpers.ExecuteReader($"delete from productoption where id = '{productOptionId}'");
        }


        public void Save(ProductOption option)
        {
            var cmd = option.IsNew ?
                $"insert into productoption (id, productid, name, description) values ('{option.Id}', '{option.ProductId}', '{option.Name}', '{option.Description}')" :
                $"update productoption set name = '{option.Name}', description = '{option.Description}' where id = '{option.Id}'";
            Helpers.ExecuteNonQuery(cmd);
        }

        public ProductOption Create(Guid id)
        {
            var option = new ProductOption { Id = id };
            var rdr = Helpers.ExecuteReader($"select * from productoption where id = '{id}'");
            if (!rdr.Read())
                return option;

            option.Id = Guid.Parse(rdr["Id"].ToString());
            option.ProductId = Guid.Parse(rdr["ProductId"].ToString());
            option.Name = rdr["Name"].ToString();
            option.Description = (DBNull.Value == rdr["Description"]) ? null : rdr["Description"].ToString();

            return option;
        }
    }
}