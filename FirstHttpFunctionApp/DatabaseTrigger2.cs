using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FirstHttpFunctionApp
{
    public static class DatabaseTrigger2
    {
        // Visit https://aka.ms/sqlbindingsoutput to learn how to use this output binding
        [FunctionName("DatabaseTrigger2")]
        public static async Task<CreatedResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            [Sql("[dbo].[product]", "myconnection1")] IAsyncCollector<ToDoItem> output,

			ILogger log)
        {
            log.LogInformation("C# HTTP trigger with SQL Output Binding function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            ToDoItem todoitem = JsonConvert.DeserializeObject<ToDoItem>(requestBody) ?? new ToDoItem
                {
                    Id = "1",
                    Priority = 1,
                    Description = "Hello World"
                };
            await output.AddAsync(todoitem);

            return new CreatedResult(req.Path, todoitem);
        }
    }

    public class ToDoItem
    {
        public string Id { get; set; }
        public int Priority { get; set; }
        public string Description { get; set; }
    }
}
