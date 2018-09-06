using System;
using System.Collections.Generic;

namespace database
{
    class Program
    {
        static void Main(string[] args)
        {
            var filePath = "people.txt";
            var people = new Person[] {
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

            var peopleRepository = new Repository(filePath);
            
            foreach(Person p in people) {
                peopleRepository.Create(p);
            }

            var repositoryPeople = peopleRepository.List<Person>();
            PrintAll<Person>(repositoryPeople);

            people[1].Name = "Adam";

            peopleRepository.Update(people[1]);
            Console.WriteLine($"Single(2): {peopleRepository.Single<Person>(2)}");
            Console.WriteLine($"Single(-1): {peopleRepository.Single<Person>(-1)}");
            peopleRepository.Delete(people[2]);

            repositoryPeople = peopleRepository.List<Person>();
            PrintAll<Person>(repositoryPeople);
            
        }

        static void PrintAll<T>(List<T> list)
        {
            Console.WriteLine("Printing all");
            foreach (T x in list)
            {
                Console.WriteLine(x);
            }
        }
    }
}
