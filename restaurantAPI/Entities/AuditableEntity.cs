namespace RestaurantAPI.Entites;

public abstract class AuditableEntity
{
    public bool IsActive { get; set; }
    public bool IsDelete { get; set; }
    public bool IsModified { get; set; }
    public DateTimeOffset CreationTime { get; set; }
    public DateTimeOffset DeletionTime { get; set; }
    public DateTimeOffset ModificationTime { get; set; }
}