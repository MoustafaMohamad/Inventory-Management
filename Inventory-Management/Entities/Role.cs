namespace Inventory_Management.Entities
{
    public class Role :BaseModel
    {
        public string Name { get; set; }
        public ICollection<User> Users { get; set; }
    }
    
}
