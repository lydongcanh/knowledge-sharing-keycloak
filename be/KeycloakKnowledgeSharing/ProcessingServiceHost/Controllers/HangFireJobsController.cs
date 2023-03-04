using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace ProcessingServiceHost.Controllers;

[ApiController]
[Route("hangfire-jobs")]
public class HangFireJobsController : ControllerBase
{
    private readonly ILogger<HangFireJobsController> _logger;

    public HangFireJobsController(ILogger<HangFireJobsController> logger)
    {
        _logger = logger;
    }

    [HttpPost("fire-and-forget-job")]
    public IActionResult EnqueueFireAndForgetJob(string message)
    {
        var jobId = BackgroundJob.Enqueue(() => LogMessage(message));
        return Accepted(new { jobId });
    }
    
    [HttpPost("delayed-job")]
    public IActionResult EnqueueDelayedJob(string message, int delaySeconds)
    {
        var jobId = BackgroundJob.Schedule(() => LogMessage(message), TimeSpan.FromSeconds(delaySeconds));
        return Accepted(new { jobId });
    }

    [HttpPost("recurring-job")]
    public IActionResult EnqueueRecurringJob(string jobId, string message, string cronExpression)
    {
        RecurringJob.AddOrUpdate(jobId, () => LogMessage(message), () => cronExpression);
        return Accepted();
    }
    
    [ApiExplorerSettings(IgnoreApi = true)]
    public void LogMessage(string message)
    {
        _logger.LogInformation("[{NowTime}]. {Message}", DateTime.Now.ToLongTimeString(), message);
    }
}
