namespace MyStore.Domain;

public class Status
{
    public int Id {get;set;}
    public string Name {get;set;}

    //Relación con OrderItem
    public List<OrderItem> OrderItems {get;set;}

    //Relación con Order
    public List<Order> Orders {get;set;}
}