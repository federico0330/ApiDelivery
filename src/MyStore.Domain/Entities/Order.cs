namespace MyStore.Domain;

public class Order
{
    public long OrderId {get;set;}
    public string DeliveryTo {get;set;}
    public string Notes {get;set;}
    public decimal Price {get;set;}
    public DateTime CreateDate {get;set;} = DateTime.Now;
    public DateTime UpdateDate {get;set;}

    //Relación con DeliveryType
    public int DeliveryTypeId {get;set;}
    public DeliveryType DeliveryType {get;set;}

    //Relación con Status
    public int StatusId {get;set;}
    public Status OverallStatus {get;set;}

    //Relación con OrderItem
    public List<OrderItem> OrderItems {get;set;}
}