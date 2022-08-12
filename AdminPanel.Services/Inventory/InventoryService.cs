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

        /// <summary>
        /// Creates Snapshot record for given ProductInventory instance
        /// </summary>
        /// <param name="inventory"></param>
        private void CreateSnapshot(ProductInventory inventory)
        {
            var snapshot = new ProductInventorySnapshot
            {
                SnapshotTime = DateTime.UtcNow,
                Product = inventory.Product,
                QuantityOnHand = inventory.QuantityOnHand
            };
            _db.Add(snapshot);
        }

        /// <summary>
        /// Returns ProducInventory instance by id
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public ProductInventory GetByProductId(int productId)
        {
           return _db.ProductsInventories
                .Include(pi => pi.Product)
                .FirstOrDefault(pi => pi.Product.Id == productId);
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

        /// <summary>
        /// Returns Snapshot history for the last 6h
        /// </summary>
        /// <returns></returns>
        public List<ProductInventorySnapshot> GetSnapshotHistory()
        {
            var earliest = DateTime.UtcNow - TimeSpan.FromHours(6);

            return _db.ProductInventorySnapshots
                .Include(snap => snap.Product)
                .Where(snap => snap.SnapshotTime > earliest && !snap.Product.IsArchived)
                .ToList();
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
                    CreateSnapshot(inventory); 
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
