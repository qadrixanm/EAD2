// Phone Book controller v2
// CRUD to database

using System;
using System.Linq;
using System.Web.Http;

using PhoneBookv2.Models;

namespace PhoneBookv2.Controllers
{
    /*
    * GET /api/phonebook/number     get entry for number                       GetEntry(number)
    * GET /api/phonebook?name=      get entries for name                       GetEntriesForName(name)
    * POST /api/phonebook           add phone book entry                       PostAddEntry(entry)
    * PUT /api/phonebook            update entry for number                    PutUpdateEntry(number, newEntry)
    * DELETE /api/phonebook         delete entry for number                    DeleteEntry(number)
   */

    public class PhoneBookController : ApiController
    {
        private PhoneBookContext db = new PhoneBookContext();
        
        // GET api/phonebook/number
        public IHttpActionResult GetEntry(String number)
        {
            // LINQ query, find matching entry for number
            var entry = db.PhoneBook.Find(number);
            if (entry == null)
            {
                return NotFound();
            }
            return Ok(entry);
        }
        // GET api/phonebook?name=
        public IHttpActionResult GetEntriesForName(String name)
        {
            // LINQ query, find matching entries for name
            var entries = db.PhoneBook.Where(r => r.Name.ToUpper() == name.ToUpper());
            if (entries == null)
            {
                return NotFound();
            }
            return Ok(entries);
        }

        // POST api/phonebook
        public IHttpActionResult PostAddEntry(PhoneBookEntry entry)
        {
            if (ModelState.IsValid)                                             // model class validation ok?
            {
                // check for duplicate number
                var record = db.PhoneBook.Find(entry.Number);
                if (record == null)
                {
                    db.PhoneBook.Add(entry);
                    db.SaveChanges();                                           // commit

                    // create http response with Created status code and listing serialised as content and Location header set to URI for new resource
                    string uri = Url.Link("DefaultApi", new { number = entry.Number});         // name of default route in WebApiConfig.cs
                    return Created(uri, entry);
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        // update an entry i.e. update the entry for specified number
        public IHttpActionResult PutUpdateEntry(string number, PhoneBookEntry newEntry)                  // listing will be in request body
        {
            if (ModelState.IsValid)
            {
                var record = db.PhoneBook.Find(number);
                if (record == null)
                {
                    return NotFound();
                }
                else
                {
                    db.PhoneBook.Remove(record);
                    db.PhoneBook.Add(newEntry);
                    db.SaveChanges();                       // commit
                    return Ok();
                }  
            }
            else
            {
                return BadRequest();
            }
        }


        // delete the entry for specified number
        public IHttpActionResult DeleteEntry(String number)
        {
            var record = db.PhoneBook.Find(number);
            if (record != null)
            {
                db.PhoneBook.Remove(record);
                db.SaveChanges();                   // commit
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
