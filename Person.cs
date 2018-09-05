namespace database
{
    public class Person
    {        
        public string Name {get; set;}
        public int Age {get; set;}
        public string PhoneNumber {get; set;}

        public override string ToString() {
            return $"Name: {Name}, Age: {Age}, PhoneNumber: {PhoneNumber}";
        }
    }
}