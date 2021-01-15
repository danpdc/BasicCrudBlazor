using BlazorCrud.Server.Dal;
using BlazorCrud.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorCrud.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly WeatherDbContext _ctx;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, WeatherDbContext ctx)
        {
            _logger = logger;
            _ctx = ctx;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var forecasts = await _ctx.Forecasts.ToListAsync();
            return Ok(forecasts);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var forecast = await _ctx.Forecasts.Where(f => f.Id == id).FirstOrDefaultAsync();
            return Ok(forecast);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] WeatherForecast forecast)
        {
            _ctx.Forecasts.Add(forecast);
            await _ctx.SaveChangesAsync();
            return Ok(forecast);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(int id, WeatherForecast updatedForecast)
        {
            _ctx.Forecasts.Update(updatedForecast);
            await _ctx.SaveChangesAsync();
            return Ok(updatedForecast);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var forecast = await _ctx.Forecasts.Where(f => f.Id == id).FirstOrDefaultAsync();
            _ctx.Forecasts.Remove(forecast);
            await _ctx.SaveChangesAsync();
            return Ok(forecast);
        }
    }
}
