namespace Elegant.DAL.Models;

public class UserDeliveryInfo
{
    public Guid Id { get; set; }
    public string Name { get; init; } = string.Empty;
    public string Address { get; init; } = string.Empty;
    public  string Phone { get; init; } = string.Empty;
}