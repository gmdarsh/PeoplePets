using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeoplePetsApp.Model
{
    public class Pet
    {
        [System.ComponentModel.DataAnnotations.Key]
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
