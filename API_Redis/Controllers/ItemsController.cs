
using Microsoft.AspNetCore.Mvc;
using Redis.OM;
using Redis.OM.Searching;

namespace API_Redis.Controllers;

[ApiController]
[Route("[controller]")]
public class ItemsController : ControllerBase
{
    [HttpPost]
    public async Task<Item> AddItem([FromBody] Item item)
    {
        await _item.InsertAsync(item);
        return item;
    }

    [HttpGet("filterDescription")]
    public IList<Item> FilterByDescription([FromQuery] string description)
    {       
        return _item.Where(x => x.Description!.Contains(description)).ToList();
    }
    
    [HttpPatch("{id}")]
    public IActionResult Update([FromRoute] int id, [FromBody] string newName)
    {
        foreach (var item in _item.Where(x => x.Id == id))
        {
            item.Name = newName;
        }
        _item.Save();
        return Accepted();
    }
    
    [HttpDelete("{id}")]
    public IActionResult DeleteItem([FromRoute] int id)
    {
        _provider.Connection.Unlink($"Item:{id}");
        return NoContent();
    }
    
    private readonly RedisCollection<Item> _item;
    private readonly RedisConnectionProvider _provider;
    public ItemsController(RedisConnectionProvider provider)
    {
        _provider = provider;
        _item = (RedisCollection<Item>)provider.RedisCollection<Item>();
    }
}