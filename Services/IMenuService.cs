namespace MenuAPI.Services;
using Microsoft.EntityFrameworkCore;

// Interface for common methods needed by Dishes, Ingredients, and other kinds of DBSets in a MenuContext
// within the context of a Web API service
public interface IMenuService<T>
{
    public IEnumerable<T> GetAll();

    public T? Get(long id);

    public void Add(T t);

    public void Update(T t);

    public void Delete(long id);

}