using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeoplePetsApp.Model
{
    public class Person
    {
        [System.ComponentModel.DataAnnotations.Key]
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public List<Pet> Pets { get; set; }
    }
}
