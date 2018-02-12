// Code first model class and context

using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace PhoneBookv2.Models
{
    public class PhoneBookEntry
    {
        [Key]
        public string Number { get; set; }          // PK

        [Required]
        public string Name { get; set; }

        public string Address { get; set; }
    }

    public class PhoneBookContext : DbContext
    {
        public PhoneBookContext() : base("DefaultConnection")
        {

        }
        public DbSet<PhoneBookEntry> PhoneBook { get; set; }
    }
}