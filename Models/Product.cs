namespace ecomServer.Models
{
    public class Product
    {
        public int ProductId {get; set;}
        public string Name {get;set;}
        public string SKU {get;set;}
        public decimal Price {get;set;}
        public int CategoryId {get;set;}
        public Category Category {get;set;} // Many-to-One Relationship with Category
        public Inventory Inventory {get;set;} // One=to-One Relationship with Inventory
        public ICollection<OrderItem> OrderItems {get;set;} // One-to-many Relationship with OrderItems
        public byte[] ImageData {get;set;} // for Image data
        public string ImageMimeType {get;set;} // Stoe Image string
    }
}