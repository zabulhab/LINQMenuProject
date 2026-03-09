using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.JsonPatch.SystemTextJson;
using MenuAPI.DTOs;

namespace MenuAPI.Services;

// Interface for common methods needed by Dishes, Ingredients, and other kinds of DBSets in a MenuContext
// within the context of a Web API service
public interface IMenuService<T> where T: class
{
    public List<T> GetAll();

    public T? Get(long id);

    public T Add(IMenuAPICreateDTO<T> dto);

    // TODO: Change to return (non-http) exception types instead of ints
    public int Overwrite(IMenuAPIUpdateDTO<T> dto);

    public T Update (long id, JsonPatchDocument<T> patchDoc);

    public int Delete(long id);

}