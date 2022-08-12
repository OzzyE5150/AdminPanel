using AdminPanel.Services.Inventory;
using AdminPanel.Web.Serialization;
using AdminPanel.Web.ViewModels;

using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Web.Controllers
{
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private IInventoryService _inventoryService;
        private readonly ILogger<InventoryController> _logger;

        public InventoryController(IInventoryService inventoryService, ILogger<InventoryController> logger)
        {
            _inventoryService = inventoryService;
            _logger = logger;
        }

        [HttpGet("/api/inventory")]
        public ActionResult GetCurrentInventory()
        {
            _logger.LogInformation("Getting all inventory");
            
            var inventory = _inventoryService.GetCurrentInventory()
                .Select(pi => new ProductInventoryModel
                {
                    Id = pi.Id,
                    Product = ProductMapper.SerializeProductModel(pi.Product),
                    IdealQuantity = pi.IdealQuantity,
                    QuantityOnHand = pi.QuantityOnHand
                }).OrderBy(inv => inv.Product.Name)
                .ToList();

            return Ok(inventory);
        }

        [HttpPatch("/api/inventory")]
        public ActionResult UpdateInventory([FromBody] ShipmentModel shipment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _logger.LogInformation("Updating inventory "+ $"for {shipment.ProductId} - " +$"Adjustment: {shipment.Adjustment}");

            var id = shipment.ProductId;
            var adjustment = shipment.Adjustment;
            var inventory = _inventoryService.UpdateUnitsAvailable(id, adjustment);
            return Ok(inventory);
        }

        [HttpGet("/api/inventory/snapshot")]
        public ActionResult GetSnapshotHistory()
        {
            _logger.LogInformation("Getting snapshot history");

            try
            {
                var snapshotHistory = _inventoryService.GetSnapshotHistory();

                var timelineMarkers = snapshotHistory
                    .Select(t => t.SnapshotTime)
                    .Distinct()
                    .ToList();

                var snapshots = snapshotHistory
                    .GroupBy(hist => hist.Product, hist => hist.QuantityOnHand,
                    (key, g) => new ProductInventorySnapshotModel
                    {
                        ProductId = key.Id,
                        QuantityOnHand = g.ToList()
                    })
                    .OrderBy(hist => hist.ProductId)
                    .ToList();

                var viewModel = new SnapshotResponse
                {
                    Timeline = timelineMarkers,
                    ProductInventorySnapshots = snapshots
                };
                return Ok(viewModel);
            }
            catch(Exception e)
            {
                _logger.LogError("Error getting snapshot history");
                _logger.LogError(e.StackTrace);
                return BadRequest("Error retrieving history");
            }
        }
    }
}
