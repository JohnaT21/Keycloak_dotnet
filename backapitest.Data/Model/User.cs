using Postgrest.Attributes;
using Postgrest.Models;
using System.Text.Json.Serialization;

namespace backapitest.Model;

[Table("UserTest")]
public class User : BaseModel
{


    [PrimaryKey("id")]
    public long Id { get; set; }


    [Column("name")]
    public string Name { get; set; }

    [Column("age")]
    public int Age { get; set; }
    [Column("description")]
    public string Description { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

}