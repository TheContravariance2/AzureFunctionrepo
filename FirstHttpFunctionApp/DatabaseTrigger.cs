using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FirstHttpFunctionApp
{
    public static class DatabaseTrigger
    {
        // Visit https://aka.ms/sqlbindingsinput to learn how to use this input binding
    [FunctionName("DatabaseTrigger")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            [Sql("SELECT * FROM [dbo].[product]", connectionStringSetting: "SqlConnectionString")] IEnumerable<Object> result,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger with SQL Input Binding function processed a request.");

            return new OkObjectResult(result);
        }
    }

	public static class DatabaseTrigger3
	{
		// Visit https://aka.ms/sqlbindingsinput to learn how to use this input binding
		[FunctionName("DatabaseTrigger3")]
		public static IActionResult Run(
				[HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
				[Sql("SELECT * FROM [dbo].[product]", "SqlConnectionString")] IEnumerable<Object> result,
				ILogger log)
		{
			log.LogInformation("C# HTTP trigger with SQL Input Binding function processed a request.");

			return new OkObjectResult(result);
		}
	}
}
