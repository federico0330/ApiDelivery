namespace MyStore.Domain;

public class DeliveryType
{
    public int Id {get;set;}
    public string Name {get;set;}

    //Relación con Order
    public List<Order> Orders {get;set;}
}