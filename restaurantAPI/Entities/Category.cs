namespace RestaurantAPI.Entites;

public class Category : AuditableEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
}