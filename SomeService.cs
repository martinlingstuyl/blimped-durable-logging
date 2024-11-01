using Microsoft.Extensions.Logging;

namespace Blimped.Function;

public interface ISomeService {
    void Communicate(string name);
}

public interface ISomeServiceB {
    void Communicate(string name);
}

public interface ISomeServiceC {
    void Communicate(string name);
}
public class SomeService : ISomeService {
    private readonly ILogger<SomeService> _logger;
    
    public SomeService(ILogger<SomeService> logger)
    {
        _logger = logger;
    }

    public void Communicate(string name)
    {
        if (name == "London")
        {
            _logger.LogWarning("London is not allowed to communicate.");
            return;
        }
        else if (name == "Rome")
        {
            _logger.LogError("Sorry, it was too busy in Rome today!");
            return;
        }
        else {
            _logger.LogInformation($"{name} says hi back!!");
        }
    }
}

public class SomeServiceB : ISomeServiceB {
    private readonly ILogger<SomeServiceB> _logger;
    
    public SomeServiceB(ILogger<SomeServiceB> logger)
    {
        _logger = logger;
    }

    public void Communicate(string name)
    {
        if (name == "London")
        {
            _logger.LogWarning("B London is not allowed to communicate.");
            return;
        }
        else if (name == "Rome")
        {
            _logger.LogError("B Sorry, it was too busy in Rome today!");
            return;
        }
        else {
            _logger.LogInformation($"B {name} says hi back!!");
        }
    }
}


public class SomeServiceC : ISomeServiceC {
    private readonly ILogger<SomeServiceC> _logger;
    
    public SomeServiceC(ILogger<SomeServiceC> logger)
    {
        _logger = logger;
    }

    public void Communicate(string name)
    {
        if (name == "London")
        {
            _logger.LogWarning("C London is not allowed to communicate.");
            return;
        }
        else if (name == "Rome")
        {
            _logger.LogError("C Sorry, it was too busy in Rome today!");
            return;
        }
        else {
            _logger.LogInformation($"C {name} says hi back!!");
        }
    }
}