using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProEventos.Domain;
using ProEventos.Application.Contracts;
using Microsoft.AspNetCore.Http;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var events = await _eventService.GetAllEventsAsync(true);
                if (events == null) return NotFound("0 events found.");

                return Ok(events);
            }
            catch (Exception ex)
            { 
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Error on events recovery. Error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var evento = await _eventService.GetEventByIdAsync(id, true);
                if (evento == null) return NotFound("Event id not found");

                return Ok(evento);
            }
            catch (Exception ex)
            { 
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Error on events recovery. Error: {ex.Message}");
            }
        }
        [HttpGet("{theme}/theme")]
        public async Task<IActionResult> GetByTheme(string theme)
        {
            try
            {
                var events = await _eventService.GetAllEventsByThemeAsync(theme, true);
                if (events == null) return NotFound("Events by theme not found");

                return Ok(events);
            }
            catch (Exception ex)
            { 
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Error on events recovery. Error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(EventTest model)
        {
            try
            {
                var events = await _eventService.AddEvent(model);
                if (events == null) return BadRequest("Error adding event.");

                return Ok(events);
            }
            catch (Exception ex)
            { 
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Error adding event. Error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, EventTest model)
        {
            try
            {
                var events = await _eventService.UpdateEvent(id, model);
                if (events == null) return BadRequest("Error updating event.");

                return Ok(events);
            }
            catch (Exception ex)
            { 
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Error updating event. Error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (await _eventService.DeleteEvent(id)) return Ok("Deleted");
                else return BadRequest("Error deleting event");
                
            }
            catch (Exception ex)
            { 
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting event. Error: {ex.Message}");
            }
        }
    }
}