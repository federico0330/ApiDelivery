namespace MyStore.Domain;

public class Dish
{
    public Guid DishId {get;set;}
    public string Name {get;set;}
    public string Description {get;set;}
    public decimal Price {get;set;}
    public bool Available {get; set;}
    public string ImageUrl {get;set;}
    public DateTime CreateDate {get;set;} = DateTime.Now;
    public DateTime UpdateDate {get;set;}
    //Relación con Category
    public int CategoryId {get;set;}
    public Category Category {get;set;}
    //Relación con OrderItem
    public List<OrderItem> OrderItems {get;set;}
}