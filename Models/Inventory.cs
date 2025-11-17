namespace ecomServer.Models
{
    /// <summary>
    /// Represents an inventory entry for a product in the e-commerce system.
    /// </summary>
    public class Inventory
    {
        /// <summary>
        /// Gets or sets the unique identifier for the inventory entry.
        /// </summary>
        public int InventoryId {get;set;}

        /// <summary>
        /// Gets or sets the ID of the product associated with this inventory entry.
        /// </summary>
        public int ProductId {get;set;}

        /// <summary>
        /// Gets or sets the quantity of the product available in inventory.
        /// </summary>
        public int Quantity {get;set;}

        /// <summary>
        /// Gets or sets the product associated with this inventory entry (one-to-one relationship).
        /// </summary>
        public Product? Product {get;set;}// one-top-one relationship with Product

    }

}