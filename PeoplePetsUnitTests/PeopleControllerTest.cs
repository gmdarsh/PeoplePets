using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeoplePetsUnitTests
{
    [TestClass]
    public class PeopleControllerTest
    {
        PeoplePetsApp.Controllers.PeopleController peopleController;
        public PeopleControllerTest()
        {
            peopleController = new PeoplePetsApp.Controllers.PeopleController();
        }
        
        [TestMethod]
        public async Task GetPeopleData()
        { 
            var result = await peopleController.GetPeople();
            Assert.AreEqual(result.Count(), 6);
        }

        [TestMethod]
        public async Task GetGenderByPetType()
        {
            var result = await peopleController.GetPetsByOwnerGender("cat");
            Assert.AreEqual(result.Count(), 2);
        }

        [TestMethod]
        //Filtering Test
        public async Task GetPetsByGenderAndPetType()
        {
            var result = await peopleController.GetPetsByOwnerGender("cat");
            
            if(result.Any(s => s.Gender.ToLower() == "Male".ToLower()))
            {
                var maleResult = result.Where(s => s.Gender.ToLower() == "Male".ToLower()).SelectMany(s=>s.Pets);
                Assert.AreEqual(maleResult.Count(), 4);               
            }

            if (result.Any(s => s.Gender.ToLower() == "Female".ToLower()))
            {
                var maleResult = result.Where(s => s.Gender.ToLower() == "Female".ToLower()).SelectMany(s => s.Pets);
                Assert.AreEqual(maleResult.Count(), 3);
            }

        }

        [TestMethod]
        //Ordering Test
        public async Task GetPetsSortByName()
        {
            var result = await peopleController.GetPetsByOwnerGender("cat");

            if (result.Any())
            {
                Assert.AreEqual(result.FirstOrDefault().Gender.ToLower(), "female");
                Assert.IsNotNull(result.FirstOrDefault().Pets);
                var expectedList = result.FirstOrDefault().Pets.OrderBy(x => x.Name);
                Assert.IsTrue(expectedList.SequenceEqual(result.FirstOrDefault().Pets));
            }            

        }

      
    }
}
