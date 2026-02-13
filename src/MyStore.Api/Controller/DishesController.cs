using Microsoft.AspNetCore.Mvc;
using MyStore.Application.DTOs;
using MyStore.Application.Interfaces;
using MyStore.Domain;

namespace MyStore.Api.Controller;

[ApiController]
[Route("api/[controller]")]
public class DishesController : ControllerBase
{
    private readonly IDishService _dishservice;
    public DishesController(IDishService _dishservice)
    {   
        this._dishservice = _dishservice;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateDish([FromBody] CreateDishDTO dishDTO)
    {
        //Validación de datos de entrada
        if (dishDTO.Name == null)
        {
            return BadRequest("El campo nombre no puede estar vacio.");
        }
        if (dishDTO.Description == null)
        {
            return BadRequest("El campo descripción no puede estar vacio.");
        }
        if (dishDTO.Price <= 0)
        {
            return BadRequest("El campo precio debe ser mayor o igual a cero.");
        }
        if (dishDTO.ImageUrl == null)
        {
            return BadRequest("El campo ImageURL no puede estar vacio.");
        }
        if (dishDTO.CategoryId < 1 || dishDTO.CategoryId > 10)
        {
            return BadRequest("No existe una categoría con ese ID.");
        }
        var result = await _dishservice.CreateDish(dishDTO);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllDishes(
        [FromQuery] string? name,
        [FromQuery] int? categoryId,
        [FromQuery] string? orderBy)
    {
        //Validaciones de entrada
        if (categoryId.HasValue && (categoryId < 1 || categoryId > 10))
        {
            return BadRequest("categoryId erroneo.");
        }
        if (orderBy != null)
        {
            if (orderBy.ToLower() != "asc" && orderBy.ToLower() != "desc")
            {
                orderBy = null;
            }
        }

        var result = await _dishservice.GetAllDishes(name,categoryId,orderBy);
        return new JsonResult(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDish(Guid id, [FromBody] CreateDishDTO dishDTO)
    {
        if (dishDTO.Name == null)
        {
            return BadRequest("El campo nombre no puede estar vacio.");
        }
        if (dishDTO.Description == null)
        {
            return BadRequest("El campo descripción no puede estar vacio.");
        }
        if (dishDTO.Price <= 0)
        {
            return BadRequest("El campo precio debe ser mayor o igual a cero.");
        }
        if (dishDTO.ImageUrl == null)
        {
            return BadRequest("El campo ImageURL no puede estar vacio.");
        }
        if (dishDTO.CategoryId < 1 || dishDTO.CategoryId > 10)
        {
            return BadRequest("No existe una categoría con ese ID.");
        }
        
        var result = await _dishservice.UpdateDish(id, dishDTO);
        return Ok(result);
    }
}