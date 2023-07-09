using Microsoft.AspNetCore.Mvc;

namespace JF91.AppMetrics.InfluxDB2.Net7.Controllers;

[ApiController]
[Route("[controller]")]
public class MetricsController : ControllerBase
{
    // WIP
    [HttpGet("request-counter")]
    public ActionResult RequestCounter()
    {
        return Ok();
    }
    
    // WIP
    [HttpGet("request-duration")]
    public ActionResult RequestDuration()
    {
        return Ok();
    }
    
    // WIP
    [HttpGet("request-size")]
    public ActionResult RequestSize(object body)
    {
        return Ok();
    }
    
    // WIP
    [HttpGet("response-apdex")]
    public ActionResult ResponseApdex()
    {
        return Ok();
    }
    
    // WIP
    [HttpGet("response-size")]
    public ActionResult ResponseSize()
    {
        return Ok();
    }
}