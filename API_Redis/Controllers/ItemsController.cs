

using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace API_Redis.Controllers;

[ApiController]
[Route("[controller]")]
public class ItemsController : ControllerBase
{
    private readonly IDistributedCache _cache;

    public ItemsController(IDistributedCache cache)
    {
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
    }
    
    [HttpPost]
    public async Task<ActionResult<Item>> AddItem([FromBody] Item item)
    {
        await _cache.SetStringAsync(item.Id.ToString().Trim(), JsonConvert.SerializeObject(item));
            
        return Ok(item);
    }
    
    [HttpPatch]
    public async Task<ActionResult<Item>> UpdateItem([FromBody] Item item)
    {
        await _cache.SetStringAsync(item.Id.ToString().Trim(), JsonConvert.SerializeObject(item));
            
        return Ok(item);
    }

    [HttpGet("{id}", Name = "GetItem")]
    public async Task<ActionResult<Item>> GetItem(int id)
    {
        var strItem = await _cache.GetStringAsync(id.ToString().Trim());
        
        if (String.IsNullOrEmpty(strItem))
            return null;
        
        var item = JsonConvert.DeserializeObject<Item>(strItem);
        return Ok(item ?? item);
    }
    
    [HttpDelete("{id}", Name = "DeleteItem")]
    [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteItem(int id)
    {
        await _cache.RemoveAsync(id.ToString().Trim());
        return Ok();
    }
}