using System;
using System.Collections.Generic;

namespace Repository
{
    class Program
    {
        static void Main(string[] args)
        {
            var filePath = "people.txt";
            var ioRepository = new IORepository<Person>(filePath);
            Console.WriteLine("Doing stuff on IO Repository");
            DoRepositoryStuff(ioRepository);

            var memoryRepository = new MemoryRepository<Person>();
            Console.WriteLine("Doing stuff on IO Repository");
            DoRepositoryStuff(memoryRepository);

        }

        static void PrintAll<T>(List<T> list)
        {
            Console.WriteLine("Printing all");
            foreach (T x in list)
            {
                Console.WriteLine(x);
            }
        }

        static void DoRepositoryStuff(IRepository<Person> repository)        {
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

            
            
            foreach(Person p in people) {
                repository.Create(p);
            }

            var repositoryPeople = repository.List();
            PrintAll<Person>(repositoryPeople);

            var people25AndUp = repository.List(x=>x.Age >= 25);
            Console.WriteLine("People 25 and up");
            PrintAll<Person>(people25AndUp);

            people[1].Name = "Adam";
            repository.Update(people[1]);
            Console.WriteLine($"Single(2): {repository.Single(x=>x.Id == 2)}");
            Console.WriteLine($"Single(-1): {repository.Single(x=> x.Id == -1)}");
            repository.Delete(people[2]);

            repositoryPeople = repository.List();
            PrintAll<Person>(repositoryPeople);
        }
    }
}
