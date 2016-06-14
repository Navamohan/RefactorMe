using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using ProductManagement.Models;

namespace ProductManagement.Controllers
{
    [RoutePrefix("products")]
    public class ProductsController : ApiController
    {
        private readonly IProductsRepository _products;
        private readonly IProductOptionsRepository _productOptions;

        public ProductsController(IProductsRepository products, IProductOptionsRepository productOptions)
        {
            _products = products;
            _productOptions = productOptions;
        }

        [Route]
        [HttpGet]
        public IEnumerable<Product> GetAll()
        {
            return _products.GetAllProducts();
        }

        [Route]
        [HttpGet]
        public IEnumerable<Product> SearchByName(string name)
        {
            return _products.GetProductByName(name);
        }

        [Route("{id}")]
        [HttpGet]
        public Product GetProduct(Guid id)
        {
            var product = _products.Create(id);
            if (product.IsNew)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return product;
        }

        [Route]
        [HttpPost]
        public void Create(Product product)
        {
            _products.Save(product);
        }

        [Route("{id}")]
        [HttpPut]
        public void Update(Guid id, Product product)
        {
            var orig = _products.Create(id);
            orig.Name = product.Name;
            orig.Description = product.Description;
            orig.Price = product.Price;
            orig.DeliveryPrice = product.DeliveryPrice;
            
            if (!orig.IsNew)
                _products.Save(orig);
        }

        [Route("{id}")]
        [HttpDelete]
        public void Delete(Guid id)
        {
           _products.DeleteProduct(id);
        }

        [Route("{productId}/options")]
        [HttpGet]
        public IEnumerable<ProductOption> GetOptions(Guid productId)
        {
            return _productOptions.GetProductOptions(productId);
        }

        [Route("{productId}/options/{id}")]
        [HttpGet]
        public ProductOption GetOption(Guid productId, Guid id)
        {
            var option = _productOptions.Create(id);
            if (option.IsNew)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return option;
        }

        [Route("{productId}/options")]
        [HttpPost]
        public void CreateOption(Guid productId, ProductOption option)
        {
            option.ProductId = productId;
            _productOptions.Save(option);
        }

        [Route("{productId}/options/{id}")]
        [HttpPut]
        public void UpdateOption(Guid id, ProductOption option)
        {
            var orig = _productOptions.Create(id);
            orig.Name = option.Name;
            orig.Description = option.Description;

            if (!orig.IsNew)
                _productOptions.Save(orig);
        }

        [Route("{productId}/options/{id}")]
        [HttpDelete]
        public void DeleteOption(Guid id)
        {
            _productOptions.DeleteProductOption(id);
        }
    }
}
