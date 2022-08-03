using AdminPanel.Data;
using AdminPanel.Data.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.Services.Product
{
    public class ProductService : IProductService
    {
        private readonly AdminDbContext _db;

        public ProductService(AdminDbContext dbContext)
        {
            _db = dbContext;
        }


        /// <summary>
        /// Archives a product (bool IsArchived => true)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ServiceResponse<Data.Models.Product> ArchiveProduct(int id)
        {
            try
            {
                var product = _db.Products.Find(id);
                product.IsArchived = true;
                _db.SaveChanges();

                return new ServiceResponse<Data.Models.Product>
                {
                    Data = product,
                    Time = DateTime.UtcNow,
                    Message = "Product Archived",
                    IsSuccess = true
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<Data.Models.Product>
                {
                    Data = null,
                    Time = DateTime.UtcNow,
                    Message = e.StackTrace,
                    IsSuccess = false
                };
            }

        }

        /// <summary>
        /// Adds a new product to the DB
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public ServiceResponse<Data.Models.Product> CreateProduct(Data.Models.Product product)
        {
            try
            {
                _db.Products.Add(product);

                var newInventory = new ProductInventory
                {
                    Product = product,
                    QuantityOnHand = 0,
                    IdealQuantity = 10
                };

                _db.ProductsInventories.Add(newInventory);

                _db.SaveChanges();

                return new ServiceResponse<Data.Models.Product>
                {
                    Data = product,
                    Time = DateTime.UtcNow,
                    Message = "Saved new product",
                    IsSuccess = true
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<Data.Models.Product>
                {
                    Data = product,
                    Time = DateTime.UtcNow,
                    Message = e.StackTrace,
                    IsSuccess = false
                };
            }
        }
        /// <summary>
        /// Returns all products
        /// </summary>
        /// <returns></returns>
        public List<Data.Models.Product> GetAllProducts()
        {
            return _db.Products.ToList();
        }
        /// <summary>
        /// Returns a product by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Data.Models.Product GetProductById(int id)
        {
            return _db.Products.Find(id);
        }
    }
}
