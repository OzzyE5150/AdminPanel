using AdminPanel.Web.ViewModels;

namespace AdminPanel.Web.Serialization
{
    public static class ProductMapper
    {
        /// <summary>
        /// Maps a Product model to a ProductModel view model
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public static ProductModel SerializeProductModel(Data.Models.Product product)
        {
            return new ProductModel
            {
                Id = product.Id,
                CreatedOn = product.CreatedOn,
                UpdatedOn = product.UpdatedOn,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                IsArchived = product.IsArchived,
                IsTaxable = product.IsTaxable
            };
        }

        /// <summary>
        /// Maps a ProductModel view model to a Product model
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public static Data.Models.Product SerializeProductModel(ProductModel product)
        {
            return new Data.Models.Product
            {
                Id = product.Id,
                CreatedOn = product.CreatedOn,
                UpdatedOn = product.UpdatedOn,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                IsArchived = product.IsArchived,
                IsTaxable = product.IsTaxable
            };
        }
    }
}
