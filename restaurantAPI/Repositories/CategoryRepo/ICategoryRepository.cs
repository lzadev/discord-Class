using System.Linq.Expressions;
using RestaurantAPI.Entites;
namespace RestaurantAPI.Repositories.CategoryRepo;

public interface ICategoryRepository
{
    IEnumerable<Category> GetWhere(Expression<Func<Category, bool>> expression);
    IEnumerable<Category> GetAll();
    Category GetById(int id);
    void Create(Category category);
    void Update(Category category);
    void Delete(Category category);
    void SaveChages();
}