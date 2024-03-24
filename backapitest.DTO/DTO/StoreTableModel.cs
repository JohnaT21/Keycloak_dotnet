namespace backapitest.DTO;

public class StoreTableModel
{
    public String PackageName {  get; set; }
    public int Quantity { get; set; }
    public String CreatedAt  { get; set; } 
    public String CreatedBy { get; set; }
}

public class StoreRegistrationModel
{
    public String PackageName {  get; set; }
    public int Quantity { get; set; }
}