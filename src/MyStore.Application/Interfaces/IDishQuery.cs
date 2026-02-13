using MyStore.Domain;

namespace MyStore.Application.Interfaces;

public interface IDishQuery
{
    Task<Dish> GetDishById(Guid dishId);
    Task<List<Dish>> GetDishes(string? name, int? categoryId, string? orderBy);
}