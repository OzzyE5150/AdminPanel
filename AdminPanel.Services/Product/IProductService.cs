using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.Services.Product
{
    public interface IProductService
    {
        List<AdminPanel.Data.Models.Product> GetAllProducts();
        AdminPanel.Data.Models.Product GetProductById(int id);
        ServiceResponse<bool> CreateProduct(AdminPanel.Data.Models.Product product);
        ServiceResponse<bool> ArchiveProduct(int id);
    }
}
