using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BeautySaloonAPI.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace BeautySaloonAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly BeautySaloonContext _context;

        

        public ServicesController(BeautySaloonContext context)
        {
            _context = context;
        }
        [SwaggerOperation(
Summary = "Получает список услуг",
Description = "Получает  список пользователей"
)]
        // GET: api/Services
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Services>>> GetServices()
        {
            return await _context.Services.ToListAsync();
        }

        [SwaggerOperation(
Summary = "Выборка услуг по её Id категории",
Description = "Этот метод выводит все услуги с  Id категории"
)]
        // GET: api/Services/5
        [HttpGet("{categoryId}")]
        public async Task<ActionResult<IEnumerable<Services>>> GetServices(int Categoryid)
        {
            
            var services = await _context.Services.Where(x=>x.CategoryId==Categoryid).ToListAsync();

            if (services == null)
            {
                return NotFound();
            }

            return services;
        }
        [SwaggerOperation(
Summary = "Убрать услугу",
Description = "Этот метод удаляет услугу (сервис) по её Id"
)]
        // PUT: api/Services/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutServices(int id, Services services)
        {
            if (id != services.Id)
            {
                return BadRequest();
            }

            _context.Entry(services).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServicesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Services
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Services>> PostServices(Services services)
        {
            _context.Services.Add(services);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetServices", new { id = services.Id }, services);
        }
        [SwaggerOperation(
Summary = "Удаление услуги по его id",
Description = "Этот метод выводит все услуги с  Id категории"
)]
        // DELETE: api/Services/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Services>> DeleteServices(int id)
        {
            var services = await _context.Services.FindAsync(id);
            if (services == null)
            {
                return NotFound();
            }

            _context.Services.Remove(services);
            await _context.SaveChangesAsync();

            return services;
        }

        private bool ServicesExists(int id)
        {
            return _context.Services.Any(e => e.Id == id);
        }
    }
}
