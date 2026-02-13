using MyStore.Application.DTOs;
using MyStore.Application.Interfaces;
using MyStore.Domain;

namespace MyStore.Application.Services;

public class DishService : IDishService
{
    private readonly IDishQuery _query;
    private readonly IDishCommand _command;

    public DishService(IDishQuery query, IDishCommand command)
    {
        _query = query;
        _command = command;
    }

    public async Task<DishDTO> CreateDish(CreateDishDTO dishDTO)
    {
        Dish dish = new Dish
        {
            Name = dishDTO.Name,
            Description = dishDTO.Description,
            Price = dishDTO.Price,
            ImageUrl = dishDTO.ImageUrl,
            CategoryId = dishDTO.CategoryId
        };
        await _command.InsertDish(dish);
        return MapToDishDTO(dish);
    }

    public static DishDTO MapToDishDTO(Dish dish)
    {
        return new DishDTO
        {
            Name = dish.Name,
            Description = dish.Description,
            Price = dish.Price,
            Available = dish.Available,
            ImageUrl = dish.ImageUrl,
            CategoryName = dish.Category.Name  
        };
    }

    public async Task<List<DishDTO>> GetAllDishes(string? name, int? categoryId, string? orderBy)
    {
        // Obtenemos las entidades de la base de datos
        List<Dish> dishes = await _query.GetDishes(name, categoryId, orderBy);

        // Mapeamos cada Dish a DishDTO
        List<DishDTO> dishesDTO = dishes.Select(d => MapToDishDTO(d)).ToList();
        
        return dishesDTO;
    }

    public async Task<DishDTO> UpdateDish(Guid id, CreateDishDTO dishDTO)
    {
        //Verifico si el plato existe
        Dish existingDish = await _query.GetDishById(id);
        if (existingDish == null)
        {
            throw new Exception("El plato no existe");
        }

        //Actualizo las propiedades de la entidad con los datos del dto
        existingDish.Name = dishDTO.Name;
        existingDish.Description = dishDTO.Description;
        existingDish.Price = dishDTO.Price;
        existingDish.ImageUrl = dishDTO.ImageUrl;
        existingDish.CategoryId = dishDTO.CategoryId;

        //Ejecuto el comando de actualización
        await _command.UpdateDish(existingDish);

        //Traigo el dish actualizado
        Dish updatedDish = await _query.GetDishById(id);
        return MapToDishDTO(updatedDish);
    }
}