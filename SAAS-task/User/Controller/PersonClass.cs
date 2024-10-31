using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAAS_task.Context;
using SAAS_task.User.DTOs;
using SAAS_task.User.Models;
using SAAS_task.User.Services;
using System.Data;

namespace SAAS_task.User.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly PersonServices _service;
        public PersonController(PersonServices service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetAllPersons()
        {
            return Ok(await _service.GetAll());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPerson(int id)
        {
            return await _service.GetPersonbyId(id);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson(int id, PersonUpdateDTO person)
        {
            if (!await _service.UpdatePerson(id, person))
            {
                return NotFound();
            }
            return NoContent();
        }
        [HttpPost]
        public async Task<ActionResult<Person>> CreateAPerson(PersonCreateDTO person)
        {
            var body = await _service.CreatePerson(person);
            return CreatedAtAction(nameof(GetPerson), new { id = body.Id }, body);
        }
        [HttpDelete]
        public async Task<IActionResult> DeletePerson(int id)
        {
            if (!await _service.DeletePerson(id))
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
