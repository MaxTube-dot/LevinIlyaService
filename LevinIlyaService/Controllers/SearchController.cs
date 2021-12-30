using Microsoft.AspNetCore.Mvc;
using LevinFsspParser.BL.Models;
using Microsoft.Extensions.Logging;
using LevinFsspParser.BL;
using System.Threading;
using System.Collections.Generic;
using System;

namespace LevinIlyaService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchController : Controller
    {
        FsspManager _fssp = new FsspManager("CyTa98qdpZKx");

        private readonly ILogger<SearchController> _logger;

        public SearchController(ILogger<SearchController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<IEnforcementProceeding> SearchPerson(string firstName, string lastName, string birthDate,int region , string secoundName = null)
        {
            var enforcementProceedings = _fssp.SearchPerson( firstName,  lastName,  birthDate,  region,  secoundName);

            return enforcementProceedings;
        }

        [HttpPost]
        public IEnumerable<IResult> SearchGroup(IGroup group)
        {


            //IGroup group = new GroupModel();

            //for (int i = 0; i < 5; i++)
            //{

            //    group.AddPerson(GenerateRandomPerson());


            //}


            var results = _fssp.SearchGroup(group);


            return results;
        }
        private static PhysicalPersonModel GenerateRandomPerson()
        {
            int count = 4;

            string[] name = { "Иван", "Андрей", "Яков", "Юрий" };

            string[] lastName = { "Иванов", "Смирнов", "Кузнецов", "Попов" };

            string[] secoundName = { "Александрович", "Дмитриевич", "Геннадьевич", "Ильич" };

            int[] region = { 45, 77, 55, 18 };

            string[] birthDate = { "18.11.1999", "25.10.1988", "19.02.2020", "05.06.1977" };

            Random rnd = new Random();

            PhysicalPersonModel person = new PhysicalPersonModel();

            person.FirstName = name[rnd.Next(0, count - 1)];
            person.LastName = lastName[rnd.Next(0, count - 1)];
            person.SecondName = secoundName[rnd.Next(0, count - 1)];
            person.BirthDate = birthDate[rnd.Next(0, count - 1)];
            person.Region = region[rnd.Next(0, count - 1)];

            return person;
        }


    }
}
