using LevinFsspParser.BL.Models;
using LevinIlyaService.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LevinIlya.WebService.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ItShoud_Get_Two_Record()
        { 
            // Arrange
            SearchController controller = new SearchController(null);

            // Act
            var results  = controller.SearchPerson("Дмитрий", "Добычин", "12.07.1992",77, "Олегович").ToArray();


            // Assert
            Assert.AreEqual(results.Length, 2);
        }

        [TestMethod]
        public void ItShoud_Get_Two_Record2()
        {
            // Arrange
            SearchController controller = new SearchController(null);

            GroupModel groupModel = new GroupModel();

            for (int i = 1; i < 5; i++)
            {
                PhysicalPersonModel person = new PhysicalPersonModel() { LastName = "Добычин", BirthDate = "12.07.1992", FirstName = "Дмитрий", Region = 77, SecondName = "Олегович" };

                groupModel.AddPerson(person);
            }

            // Act
            var results = controller.SearchGroup(groupModel).ToArray();



            // Assert
            Assert.AreEqual(results.Length, 4);
            Assert.AreEqual(results[1].EnforcementProceedings.Length, 2);
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
