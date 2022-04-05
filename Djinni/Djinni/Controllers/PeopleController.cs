using Djinni.Data;
using Djinni.Data.Entities;
using Djinni.DTOs.PersonDto;
using Djinni.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Djinni.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
            PersonPostDto personDto = new PersonPostDto();
            var obj = _manager.DeSerialize(value, personDto);
            Address address = new Address()
            {
                city = obj.City,
                addressLine = obj.AddressLine,
            };
            Person person = new Person()
            {
                firstName = obj.FirstName,
                lastName = obj.LastName,
                address = address,
            }; 
            _context.People.Add(person);
            _context.SaveChanges();
            return Task.FromResult<long>(person.Id);
        }
    }
}
