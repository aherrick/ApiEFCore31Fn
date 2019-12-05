using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.WindowsAzure.Storage.Queue;
using System;
using System.Threading.Tasks;

namespace ApiEFCore31Fn.Fn
{
    public static class TriggerFn
    {
        [FunctionName("TriggerFn")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post")]HttpRequest req, [Queue("delete")]IAsyncCollector<string> outputQueueItem)
        {
            string venueid = req.Query["id"];

            await outputQueueItem.AddAsync(venueid);

            return new OkResult();
        }
    }

    public static class ProcessorFn
    {
        [FunctionName("ProcessorFn")]
        public static async Task Run([QueueTrigger("delete")]CloudQueueMessage myQueueItem, TraceWriter log)
        {
            try
            {
                log.Info($"C# LoadVenueBeersFn function executed at: {DateTime.Now}");
                log.Info($"C# Queue trigger function processed: {myQueueItem}");

                var venueId = int.Parse(myQueueItem.AsString);

                var db = Db.GetInstance();

                // do some processing
            }
            catch (Exception ex)
            {
                log.Info(ex.ToString());
            }
        }
    }
}