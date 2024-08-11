using System.ComponentModel.DataAnnotations;

namespace API_Redis;

public class Item
{
    [Required]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    
    
    
}