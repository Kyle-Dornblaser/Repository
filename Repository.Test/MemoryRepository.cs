using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository;

namespace Repository.Test
{
    [TestClass]
    public class MemoryReposity
    {

        private Person[] GetPeople()
        {
            return new Person[] {
                new Person {
                    Name = "Bob",
                    Age = 20,
                    PhoneNumber = "1234567890"
                },
                new Person {
                    Name = "Joe",
                    Age = 25,
                    PhoneNumber = "5555555555"
                },
                new Person {
                    Name = "Amy",
                    Age = 30,
                    PhoneNumber = "0987654321"
                }
            };
        }

        private void AddArrayToRepository<T>(MemoryRepository<T> repository, T[] array) where T: IModel
        {
            foreach(T entry in array) {
                repository.Create(entry);
            }
        }

        [TestMethod]
        public void Create()
        {
            var person = new Person
            {
                Name = "Bob",
                Age = 20,
                PhoneNumber = "1234567890"
            };
            var repository = new MemoryRepository<Person>();

            repository.Create(person);

            Assert.AreEqual(person, repository.Single(x => x.Id == 1));
        }

        [TestMethod]
        public void Update()
        {
            var people = GetPeople();
            var repository = new MemoryRepository<Person>();
            AddArrayToRepository<Person>(repository, people);
            var updatedPerson = repository.Single(x => x.Id == 2);
            updatedPerson.Name = "John";

            repository.Update(updatedPerson);

            Assert.AreEqual(updatedPerson, repository.Single(x => x.Id == 2));

        }

        [TestMethod]
        public void List()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void Delete()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void Single()
        {
            Assert.Fail();
        }
    }
}
