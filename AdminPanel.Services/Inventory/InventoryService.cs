using AdminPanel.Data.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.Services.Inventory
{
    public class InventoryService : IInventoryService
    {
        public void CreateSnapshot()
        {
            throw new NotImplementedException();
        }

        public ProductInventory GetByProductId(int id)
        {
            throw new NotImplementedException();
        }

        public List<ProductInventory> GetCurrentInventory()
        {
            throw new NotImplementedException();
        }

        public List<ProductInventorySnapshot> GetSnapshotHistory()
        {
            throw new NotImplementedException();
        }

        public ServiceResponse<ProductInventory> UpdateUnitsAvailable(int id, int adjustment)
        {
            throw new NotImplementedException();
        }
    }
}
