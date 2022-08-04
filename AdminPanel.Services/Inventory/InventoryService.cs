using AdminPanel.Data;
using AdminPanel.Data.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.Services.Inventory
{
    public class InventoryService : IInventoryService
    {
        private readonly AdminDbContext _db;
        private readonly ILogger<InventoryService> _logger;
        public InventoryService(AdminDbContext dbContext, ILogger<InventoryService> logger)
        {
            _db = dbContext;
            _logger = logger;
        }

        public void CreateSnapshot()
        {
            throw new NotImplementedException();
        }

        public ProductInventory GetByProductId(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns current inventory from db
        /// </summary>
        /// <returns></returns>
        public List<ProductInventory> GetCurrentInventory()
        {
            return _db.ProductsInventories
                .Include(pi => pi.Product)
                .Where(pi => !pi.Product.IsArchived)
                .ToList();
        }

        public List<ProductInventorySnapshot> GetSnapshotHistory()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates the number of available products based on id
        /// Adjusts quantity on hand by given value
        /// </summary>
        /// <param name="id">productId</param>
        /// <param name="adjustment">number of units added or removed</param>
        /// <returns></returns>
        public ServiceResponse<ProductInventory> UpdateUnitsAvailable(int id, int adjustment)
        {
            var now = DateTime.UtcNow;
            try 
            {
                var inventory = _db.ProductsInventories
                    .Include(inv => inv.Product)
                    .First(inv => inv.Product.Id == id);

                inventory.QuantityOnHand += adjustment;

                try { 
                    CreateSnapshot(); 
                } 
                catch(Exception e) { 
                    _logger.LogError("Error creating inventory snapshot."); 
                    _logger.LogError(e.StackTrace); 
                    
                }

                _db.SaveChanges();

                return new ServiceResponse<ProductInventory>
                {
                    IsSuccess = true,
                    Data = inventory,
                    Message = $"Product {id} Inventory updated",
                    Time = now
                };
            }
            catch 
            {
                return new ServiceResponse<ProductInventory>
                {
                    IsSuccess = false,
                    Data = null,
                    Message = $"Error updating QuantityOnHand",
                    Time = now
                };
            }
        }
    }
}
