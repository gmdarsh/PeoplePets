using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using PeoplePetsApp.Model;

namespace PeoplePetsApp.Controllers
{
    [Produces("application/json")]    
    [Route("api/[controller]")]
    public class PeopleController : Controller
    {        
        [HttpGet]
        public async Task<IActionResult> GetPerson()
        {
            //return _context.Person;
            using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
            {                
                var response = await client.GetAsync("http://agl-developer-test.azurewebsites.net/people.json");
                if (response != null)
                {
                    var value = await response.Content.ReadAsStringAsync();
                    if (string.IsNullOrEmpty(value) == false)
                    {
                        var result = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Person>>(value);

                        if (result != null)
                        {                             
                            return Ok(result.OrderBy(s => s.Name).ToList());
                        }
                    }
                }
            }             

            return NotFound();
        }

        [HttpGet("[action]")]
        public IEnumerable<PetFilterByOwnerGenderResult> GetPetsByOwnerGender(string petType)
        {
            var peopleContext = GetPerson().Result;

            if (peopleContext is OkObjectResult && (peopleContext as OkObjectResult).Value is List<Person>)
            {
                var resultPetResults = from p in (peopleContext  as OkObjectResult).Value as List<Person>
                                       group p by p.Gender into g
                                       select new PetFilterByOwnerGenderResult()
                                       {
                                           Gender = g.Key,
                                           Pets = g.Any(s => s.Pets != null && 
                                           s.Pets.Any(d => d.Type.Equals(petType, StringComparison.OrdinalIgnoreCase))) ? 
                                           g.Where(s => s.Pets != null)
                                           .SelectMany(s => s.Pets.Where(d => d.Type.Equals(petType, StringComparison.OrdinalIgnoreCase)))
                                           .OrderBy(x => x.Name).ToList() : null
                                       };
                return resultPetResults;
            }

            return new List<PetFilterByOwnerGenderResult>();
        }
       
    }

   
}