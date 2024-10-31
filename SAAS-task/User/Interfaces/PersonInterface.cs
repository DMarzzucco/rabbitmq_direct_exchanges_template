using SAAS_task.User.DTOs;
using SAAS_task.User.Models;

namespace SAAS_task.User.Interfaces
{
    public interface PersonInterface
    {
        Task<IEnumerable<Person>> GetAll();
        Task<Person> GetPersonbyId(int id);
        Task<Person> CreatePerson(PersonCreateDTO person);
        Task<bool> UpdatePerson(int id, PersonUpdateDTO person);
        Task<bool> DeletePerson(int id);
    }
}
