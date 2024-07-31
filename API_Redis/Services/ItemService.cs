using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace API_Redis.Services;

public class ItemService
{
    public static void Publish(Item item)
    {
        var body = JsonSerializer.Serialize(item);

        
    }

    public static List<Item>? GetItems()
    {
        return new List<Item>();
    }
}