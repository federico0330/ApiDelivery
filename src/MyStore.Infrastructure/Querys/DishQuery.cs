using Microsoft.EntityFrameworkCore;
using MyStore.Application.Interfaces;
using MyStore.Domain;
using MyStore.Infrastructure.Persistence;

namespace MyStore.Infrastructure.Commands;

public class DishQuery : IDishQuery
{
    private readonly AppDbContext _context;

    public DishQuery(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Dish> GetDishById(Guid dishId)
    {
        return await _context.Dishes
        .Include(d => d.Category)
        .FirstOrDefaultAsync(d => d.DishId == dishId);
    }

    public async Task<List<Dish>> GetDishes(string? name, int? categoryId, string? orderBy)
    {
        // 1. Iniciamos la consulta incluyendo la categoría
    var query = _context.Dishes.Include(d => d.Category).AsQueryable();

    // 2. Filtro por nombre (si no es nulo)
    if (!string.IsNullOrWhiteSpace(name))
    {
        query = query.Where(d => d.Name.Contains(name));
    }

    // 3. Filtro por categoría (si tiene valor)
    if (categoryId.HasValue)
    {
        query = query.Where(d => d.CategoryId == categoryId.Value);
    }

    // 4. Ordenar por precio (Punto 2 de la consigna)
    query = orderBy?.ToLower() switch
    {
        "asc" => query.OrderBy(d => d.Price),
        "desc" => query.OrderByDescending(d => d.Price),
        _ => query.OrderBy(d => d.Name) // Orden por defecto
    };

    return await query.ToListAsync();
    }
}