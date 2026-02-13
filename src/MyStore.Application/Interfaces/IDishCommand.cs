using MyStore.Domain;

namespace MyStore.Application.Interfaces;

public interface IDishCommand
{
    Task InsertDish(Dish dish);
    Task UpdateDish(Dish dish);
    Task DeleteDish(Dish dish);
}