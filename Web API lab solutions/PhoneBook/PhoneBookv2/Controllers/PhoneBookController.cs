// Phone Book Controller
// data in memory

using PhoneBook.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace PhoneBook.Controllers
{
    /*
    * GET /api/phonebook/number     get entry for number                       GetEntry(number)
    * GET /api/phonebook?name=      get entries for name                       GetEntriesForName(name)
   */

    [RoutePrefix("phonebook")]
    public class PhoneBookController : ApiController
    {
        List<PhoneBookEntry> phoneBook;         // list of phone book entries

        // populate list in memory
        public PhoneBookController()
        {
            phoneBook = new List<PhoneBookEntry>();
            phoneBook.Add(new PhoneBookEntry() { Number = "01 1111111", Name = "John Doe", Address = "No 1 Bev Hills"});
            phoneBook.Add(new PhoneBookEntry() { Number = "01 2222222", Name = "Jane Doe", Address = "No 2 Bev Hills" });
            phoneBook.Add(new PhoneBookEntry() { Number = "01 3333333", Name = "Joe Soap", Address = "No 3 Bev Hills" });

            // this should really be in persistent storage
        }

        [Route("number/{number}")]
        // GET phonebook/number/01 1111111
        public IHttpActionResult GetEntry(String number)
        {
            // LINQ query, find matching entry for number
            var entry = phoneBook.FirstOrDefault(e => e.Number.ToUpper() == number.ToUpper());
            if (entry == null)
            {
                return NotFound();
            }
            return Ok(entry);
        }

        [Route("name/{name}")]
        // GET phonebook/name/Jane Doe
        public IHttpActionResult GetEntriesForName(String name)
        {
            // LINQ query, find matching entries for name
            var entries = phoneBook.Where(r => r.Name.ToUpper() == name.ToUpper());
            if (entries == null)
            {
                return NotFound();
            }
            return Ok(entries);
        }
    }
}
