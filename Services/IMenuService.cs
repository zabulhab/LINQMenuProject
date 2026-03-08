namespace MenuAPI.Services;
using Microsoft.EntityFrameworkCore;
using MenuAPI.DTOs;

// Interface for common methods needed by Dishes, Ingredients, and other kinds of DBSets in a MenuContext
// within the context of a Web API service
public interface IMenuService<T>
{
    public List<T> GetAll();

    public T? Get(long id);

    public T Add(IMenuAPICreateDTO dto);

    // TODO: Change to return (non-http) exception types
    public int Update(UpdateIngredientDTO dto);

    public int Delete(long id);

}