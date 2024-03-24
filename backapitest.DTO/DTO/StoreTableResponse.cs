namespace backapitest.DTO;

public class StoreTableResponse
{   
    public long Id { get; set; }
    public String PackageName {  get; set; }
    public int Quantity { get; set; }
    public String CreatedAt  { get; set; } 
    public String CreatedBy { get; set; }
}