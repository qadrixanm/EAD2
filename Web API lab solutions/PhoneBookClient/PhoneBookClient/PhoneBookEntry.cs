using System;

namespace PhoneBook.Models
{
    public class PhoneBookEntry
    {
        public string Number { get; set; }          // PK

        public string Name { get; set; }

        public string Address { get; set; }
    }
}

