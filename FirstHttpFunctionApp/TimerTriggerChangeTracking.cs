using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace FirstHttpFunctionApp
{
	public class TimerTriggerChangeTracking
	{
		[FunctionName("TimerTriggerChangeTracking")]
		public static void  Run([TimerTrigger("0 */1 * * * *")] TimerInfo myTimer, ILogger log, [Sql("SELECT * FROM [dbo].[product]", "SqlConnectionString")] IEnumerable<Object> result)
		{
			log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

			string connectionString = "SqlConnectionString";

			string trackingQuery = "select * from dbo.product";
			foreach (var item in result)
			{
				log.LogInformation($"Change detected:{item} ");
			}

			//using (SqlConnection connection = new SqlConnection(connectionString))
			//{
			//	connection.Open();

			//	using (SqlCommand command = new SqlCommand(trackingQuery, connection))
			//	{
			//		SqlDataReader reader = command.ExecuteReader();
			//		while (reader.Read())
			//		{
			//			// Process each change
			//			log.LogInformation($"Change detected: {reader["name"]}");
			//			// Add your logic here to handle the change
			//		}
			//	}
			//}
		}

		
	}
}
