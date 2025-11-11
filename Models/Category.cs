namespace ecomServer.Models
{
    public class Category
    {
        public int CategoryId {get;set;}
        public string Name {get;set;}
        public ICollection<Product> Products {get;set;} // One-to-many Relationship with Product
    }
}