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
            var persons = _context.People.Include(x => x.Address).ToList();

            var stringlist = _manager.CustomSerializer(persons);

            return Ok(stringlist);
        }

        [HttpPost("addperson")]
        public IActionResult AddPerson(string value)
        {
            Person person = new Person();
            var obj = _manager.DeSerialize(value, person);

            _context.People.Add(obj);
            _context.SaveChanges();

            return Ok(obj);
        }
    }
}
