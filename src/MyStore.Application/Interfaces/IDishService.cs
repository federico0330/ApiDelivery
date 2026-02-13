using MyStore.Application.DTOs;

namespace MyStore.Application.Interfaces;

public interface IDishService
{
    Task<DishDTO> CreateDish(CreateDishDTO dishDTO);
    Task<List<DishDTO>> GetAllDishes(string? name, int? categoryId, string? orderBy);
    Task<DishDTO> UpdateDish(Guid id, CreateDishDTO dishDTO);
}