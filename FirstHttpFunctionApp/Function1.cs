using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FirstHttpFunctionApp
{
    public static class Function1
    {
        [FunctionName("MyFirstAzureFunction1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var responseMessage = "";

			if (!string.IsNullOrEmpty(requestBody))
            {
                var jsonObject = JObject.Parse(requestBody);
				responseMessage =  $"Hello, {(jsonObject["name"] ?? "NA")}.with age {(jsonObject["age"] ?? "N/A")} and country {(jsonObject["country"] ?? "N/A")} This HTTP triggered function executed successfully.";
            }
            else
            {
				responseMessage = string.IsNullOrEmpty(name)
					? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
					: $"Hello,{name} This HTTP triggered function executed successfully.";
			}
			return new OkObjectResult(responseMessage);
        }
    }
}
