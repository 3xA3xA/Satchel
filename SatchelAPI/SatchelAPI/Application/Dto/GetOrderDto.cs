namespace SatchelAPI.Application.Dto;

public class GetOrderDto
{
    public ProductCartDto ProductCartDto { get; set; }
    public string OrderStatusTypeName { get; set; }
    public string DeliveryDate { get; set; }
    public int ReceiptCode { get; set; }
}