using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using SAAS_task.Context;
using SAAS_task.User.DTOs;
using SAAS_task.User.Interfaces;
using SAAS_task.User.Models;

namespace SAAS_task.User.Services
{
    public class PersonServices : PersonInterface
    {
        private readonly AppDbContext _context;
        public PersonServices(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Person>> GetAll()
        {
            return await _context.Persons.ToListAsync();
        }
        public async Task<Person> GetPersonbyId(int id)
        {
            var person = await _context.Persons.FindAsync(id);
            if (person == null)
            {
                throw new KeyNotFoundException($"Person with id {id} not found");
            }
            return person;
        }
        public async Task<Person> CreatePerson(PersonCreateDTO person)
        {
            var data = new Person
            {
                Name = person.Name,
                Age = person.Age
            };
            _context.Persons.Add(data);
            await _context.SaveChangesAsync();
            return data;
        }
        public async Task<bool> UpdatePerson(int id, PersonUpdateDTO person)
        {
            var data = await _context.Persons.FindAsync(id);
            if (data == null)
            {
                return false;
            }
            data.Name = person.Name;
            data.Age = person.Age;

            _context.Entry(person).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeletePerson(int id)
        {
            var person = await _context.Persons.FindAsync(id);
            if (person == null)
            {
                return false;
            }
            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
