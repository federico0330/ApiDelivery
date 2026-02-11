namespace MyStore.Domain;

public class Category
{
    public int Id {get;set;}
    public string Name {get;set;}
    public string Description {get;set;}
    public int Order {get;set;}
    //Relación con Dish
    public List<Dish> Dishes {get;set;}
}