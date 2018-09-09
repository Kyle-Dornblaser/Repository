using System;
using System.Collections.Generic;
using Repository;

namespace ContactListApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Which type of repository would you like to use?");
            Console.WriteLine("1) IORepository");
            Console.WriteLine("2) MemoryRepository");
            var repositorySelection = Console.ReadLine();
            IRepository<Contact> repository = null;
            if (repositorySelection == "1")
            {
                Console.WriteLine("IORepository selected");
                Console.WriteLine("Please enter a filename.");
                var filename = Console.ReadLine();
                repository = new IORepository<Contact>(filename);
            }
            else if(repositorySelection == "2")
            {
                Console.WriteLine("MemoryRepository selected");
                repository = new MemoryRepository<Contact>();
            }
            else
            {
                Console.WriteLine("Invalid Selection");
            }

            if (repository != null)
            {
                var contacts = GetContacts();
                foreach(Contact c in contacts)
                {
                    Console.WriteLine($"Adding {c.FullName} to repository");
                    repository.Create(c);
                }

                Console.WriteLine("Selecting 2nd contact");
                var secondContact = repository.Single(x => x.Id == 2);
                Console.WriteLine($"The 2nd contact is {secondContact.FullName}");
                Console.WriteLine($"Updating {secondContact.FullName} to John Smith");
                secondContact.FirstName = "John";
                secondContact.LastName = "Smith";
                repository.Update(secondContact);
                Console.WriteLine("Selecting 3rd contact");
                var thirdContact = repository.Single(x => x.Id == 3);
                Console.WriteLine($"The 3rd contact is {thirdContact.FullName}");
                Console.WriteLine($"Deleting {thirdContact.FullName}");
                repository.Delete(thirdContact);
                Console.WriteLine("Listing all contacts");
                var allContacts = repository.List();
                foreach(Contact c in allContacts)
                {
                    Console.WriteLine(c);
                    Console.WriteLine("--------------------");
                }
            }
            
        }

        private static Contact[] GetContacts()
        {
            var contacts = new Contact[5];

            for (int i = 0; i < contacts.Length; i++)
            {
                contacts[i] = new Contact
                {
                    FirstName = Faker.Name.First(),
                    LastName = Faker.Name.Last(),
                    EmailAddresses = new List<string>()
                    {
                        Faker.Internet.Email(),
                        Faker.Internet.Email()
                    },
                    PhoneNumbers = new List<string>()
                    {
                        Faker.Phone.Number(),
                        Faker.Phone.Number()
                    }
                };
            }
            return contacts;
        }
    }
}
