namespace MyStore.Domain;

public class OrderItem
{
    public long OrderItemId {get;set;}
    public int Quantity {get;set;}
    public string Notes {get;set;}
    public DateTime CreateDate {get;set;} = DateTime.Now;

    //Relación con Dish
    public Guid DishId {get;set;}
    public Dish Dish {get;set;}

    //Relación con Status
    public int StatusId {get;set;}
    public Status Status {get;set;}

    //Relación con Order
    public long OrderId {get;set;}
    public Order Order {get;set;}
}