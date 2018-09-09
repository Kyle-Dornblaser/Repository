using System.Collections.Generic;

namespace ContactListApp
{
    internal class Contact: IModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
        public List<string> EmailAddresses { get; set; }
        public List<string> PhoneNumbers { get; set; }
        public int Id { get; set; }

        public override string ToString()  
        {
            return $"FirstName: {FirstName}, LastName: {LastName} \n" +
                    $"EmailAddresses: {string.Join(",", EmailAddresses)} \n" +
                    $"PhoneNumbers: {string.Join(", ", PhoneNumbers)}";
        }
    }
}