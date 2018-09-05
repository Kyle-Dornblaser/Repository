using System;

namespace database
{
    class Program
    {
        static void Main(string[] args)
        {
            var filePath = "people.txt";
            var peopleRepository = new Repository(filePath);
            peopleRepository.Add(new Person {
                Name = "Bob",
                Age = 20,
                PhoneNumber = "1234567890"
            });
            var allPeople = peopleRepository.GetAll<Person>();
            foreach (Person p in allPeople)
            {
                Console.WriteLine(p);
            }
        }
    }
}
