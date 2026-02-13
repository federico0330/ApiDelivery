using MyStore.Application.Interfaces;
using MyStore.Domain;
using MyStore.Infrastructure.Persistence;

namespace MyStore.Infrastructure.Commands;

public class DishCommand : IDishCommand
{
    private readonly AppDbContext _context;

    public DishCommand(AppDbContext context)
    {
        _context = context;
    }

    public Task DeleteDish(Dish dish)
    {
        throw new NotImplementedException();
    }

    public async Task InsertDish(Dish dish)
    {
        _context.Add(dish);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateDish(Dish dish)
    {
        _context.Update(dish);
        await _context.SaveChangesAsync();

    }
}