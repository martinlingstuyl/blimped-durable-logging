using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.DurableTask;
using Microsoft.DurableTask.Client;
using Microsoft.Extensions.Logging;

namespace Blimped.Function
{
    public static class DurableFunctionsOrchestration
    {
        [Function(nameof(DurableFunctionsOrchestration))]
        public static async Task<List<string>> RunOrchestrator(
            [OrchestrationTrigger] TaskOrchestrationContext context)
        {
            ILogger logger = context.CreateReplaySafeLogger(nameof(DurableFunctionsOrchestration));

            using(logger.BeginScope(new Dictionary<string, object>{ ["OrchestrationInstanceId"] = context.InstanceId }))        
            {
                logger.LogInformation("Hello World Orchestration started.");

                var outputs = new List<string>();

                // Replace name and input with values relevant for your Durable Functions Activity
                outputs.Add(await context.CallActivityAsync<string>(nameof(SayHelloActivity), "Tokyo"));
                outputs.Add(await context.CallActivityAsync<string>(nameof(SayHelloActivity), "Seattle"));
                outputs.Add(await context.CallActivityAsync<string>(nameof(SayHelloActivity), "London"));
                outputs.Add(await context.CallActivityAsync<string>(nameof(SayHelloActivity), "Rome"));

                logger.LogInformation("Hello World Orchestration complete.");
                logger.LogInformation($"{outputs.Count} hello worlds completed.");
                
                return outputs;
            }
        }

        [Function("DurableFunctionsOrchestration_HttpStart")]
        public static async Task<HttpResponseData> HttpStart(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req,
            [DurableClient] DurableTaskClient client,
            FunctionContext executionContext)
        {
            ILogger logger = executionContext.GetLogger("DurableFunctionsOrchestration_HttpStart");

            // Function input comes from the request content.
            string instanceId = await client.ScheduleNewOrchestrationInstanceAsync(
                nameof(DurableFunctionsOrchestration));

            logger.LogInformation("Started orchestration with ID = '{instanceId}'.", instanceId);

            // Returns an HTTP 202 response with an instance management payload.
            // See https://learn.microsoft.com/azure/azure-functions/durable/durable-functions-http-api#start-orchestration
            return await client.CreateCheckStatusResponseAsync(req, instanceId);
        }
    }
}
