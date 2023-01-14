using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entites;
namespace RestaurantAPI.Repositories.CategoryRepo;

public class CategoryRepository : ICategoryRepository
{
    private readonly DbSet<Category> _table;
    private readonly ApplicationContext _context;
    public CategoryRepository(ApplicationContext context)
    {
        _context = context;
        _table = context.Set<Category>();
    }

    public IEnumerable<Category> GetWhere(Expression<Func<Category, bool>> expression)
    {
        IQueryable<Category> result = _context.Categories.Where(x => x.IsActive);
        var categories = result.Where(expression).ToList<Category>();
        return categories;
    }

    public void Create(Category category)
    {
        _table.Add(category);
    }

    public void Delete(Category category)
    {
        this.Update(category);
    }

    public IEnumerable<Category> GetAll()
    {
        IQueryable<Category> categories = _table.Where(x => x.IsActive && !x.IsDelete);
        return categories.ToList();
    }

    public Category GetById(int id) => _table.Find(id);

    public void SaveChages()
    {
        _context.SaveChanges();
    }

    public void Update(Category category)
    {
        _table.Update(category);
    }
}