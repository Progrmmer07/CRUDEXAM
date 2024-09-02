namespace Models;

public class Device
{
    public int Id { get; set; }
    public int TypeId { get; set; }
    public int BrandId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public DateTime CreatedAt { get; set; }
}

