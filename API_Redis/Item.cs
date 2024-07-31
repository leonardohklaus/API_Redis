using Redis.OM.Modeling;

namespace API_Redis;

[Document(StorageType = StorageType.Json, Prefixes = new []{"Item"})]
public class Item
{
    [Indexed]
    public int? Id { get; set; }
    [Indexed]
    public string? Name { get; set; }
    [Indexed]
    public string? Description { get; set; }

    public Item(int id, string name, string description)
    {
        Id = id;
        Name = name;
        Description = description;
    }

    public Item()
    {
        
    }
    
    
}