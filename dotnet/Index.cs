namespace DotNetRuntime;

using System.Text.Json;
using Appwrite;
using Appwrite.Services;
using Appwrite.Models;

public class Handler
{
    static readonly HttpClient http = new();

    public async Task<RuntimeOutput> Main(RuntimeContext Context)
    {
        string id = "1";

        // You can log messages to the console
        Context.Log("Hello, Logs!");

        // If something goes wrong, log an error
        Context.Error("Hello, Errors!");

        if (!(Context.Req.Body is String))
        {
            object? idObject;
            Dictionary<string, object> body = (Dictionary<string, object>)Context.Req.Body;
            body.TryGetValue("id", out idObject);

            if (idObject != null)
            {
                id = ((JsonElement)idObject).ToString();
            }
        }

        var response = await http.GetStringAsync($"https://jsonplaceholder.typicode.com/todos/{id}");
        var todo = JsonSerializer.Deserialize<Dictionary<string, object>>(response) ?? new Dictionary<string, object>();

        return Context.Res.Json(new()
        {
            { "message", "Hello Open Runtimes 👋" },
            { "todo", todo }
        });
    }
}
