namespace Repository
{
    public class Person: IModel
    {        
        public string Name {get; set;}
        public int Age {get; set;}
        public string PhoneNumber {get; set;}
        public int Id { get; set; }

        public override string ToString() {
            return $"Id: {Id}, Name: {Name}, Age: {Age}, PhoneNumber: {PhoneNumber}";
        }
    }
}