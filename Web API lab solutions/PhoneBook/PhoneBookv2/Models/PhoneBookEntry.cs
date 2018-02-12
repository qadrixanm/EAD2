// model class 

namespace PhoneBook.Models
{
    public class PhoneBookEntry
    {    
        public string Number { get; set; }              // unique
        public string Name { get; set; }
        public string Address { get; set; }
    }
}