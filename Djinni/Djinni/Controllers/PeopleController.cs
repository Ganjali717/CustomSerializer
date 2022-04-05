using Djinni.Data;
using Djinni.Data.Entities;
using Djinni.DTOs.PersonDto;
using Djinni.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Djinni.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IJsonManager _manager;

        public PeopleController(DataContext context, IJsonManager manger)
        {
            _context = context;
            _manager = manger;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var persons = _context.People.Include(x => x.address).ToList();

            var stringlist = _manager.CustomSerializer(persons);

            return Ok(stringlist);
        }

        [HttpPost("addperson")]
        public Task<long> AddPerson(string value)
        {
            PersonPostDto person = new PersonPostDto();
            var obj = _manager.DeSerialize(value, person);

            
            Address address = new Address()
            {
                city = obj.City,
                addressLine = obj.AddressLine,
            };
            Person person1 = new Person()
            {
                firstName = obj.FirstName,
                lastName = obj.LastName,
                address = address,
            }; 
            _context.People.Add(person1);
            _context.SaveChanges();

            long id = Convert.ToInt64(_context.People.Where(x => x.firstName == person.FirstName).First().Id);

            return Task.FromResult<long>(person1.Id);
        }
    }
}
