using Postgrest.Attributes;
using Postgrest.Models;

namespace backapitest.Model;

[Table("store_table")]
public class StoreRegistration : BaseModel
{
    [PrimaryKey("id")]
    public long Id { get; set; }
    
    [Column("package_name")]
    public String PackageName {  get; set; }
    [Column("quantity")]
    public int Quantity { get; set; }
    [Column("created_at")]
    public String CreatedAt  { get; set; }
    [Column("created_by")]
    public String CreatedBy { get; set; } 
}