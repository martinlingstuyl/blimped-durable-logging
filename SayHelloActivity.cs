using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Blimped.Function;

public class SayHelloActivity
{
    private readonly ISomeService _someService;
    private readonly ISomeServiceB _someServiceB;
    private readonly ISomeServiceC _someServiceC;
    
    public SayHelloActivity(ISomeService someService, ISomeServiceB someServiceB, ISomeServiceC someServiceC)
    {
        _someService = someService;
        _someServiceB = someServiceB;
        _someServiceC = someServiceC;
    }

    [Function(nameof(SayHelloActivity))]
    public string Run([ActivityTrigger] string name, string instanceId, FunctionContext executionContext)
    {
        ILogger logger = executionContext.GetLogger(nameof(SayHelloActivity));
        
        using(logger.BeginScope(new Dictionary<string, object>{ ["OrchestrationInstanceId"] = instanceId }))        
        {
            logger.LogInformation("Saying hello to {name}.", name);

            _someService.Communicate(name);
            _someServiceB.Communicate(name);
            _someServiceC.Communicate(name);

            return $"Hello {name}!";
        }
    }
}