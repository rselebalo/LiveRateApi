using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

[Route("api/[controller]")]
[ApiController]
public class RatesController : ControllerBase
{
    private IRatesService _ratesService;
    private IHubContext<RatesHub> _hub;

    public RatesController(IRatesService ratesService, IHubContext<RatesHub> hub)
    {
        _ratesService = ratesService;
        _hub = hub;
    }

    public async Task<IActionResult> Get()
    {
        try
        {
            var data = DataManager.GetData();
            var saveResult = await _ratesService.SaveRatesAsync(data);

            if (saveResult)
            {
                // send data to all subscribed clients to the RatesMessageReceived event.
                new TimerManager(async () => await _hub.Clients.All.SendAsync("RatesMessageReceived", DataManager.GetData()));
            }

            return Ok(new { Message = "Request Completed" });
        }
        catch
        {
            return StatusCode(500);
        }
    }

}