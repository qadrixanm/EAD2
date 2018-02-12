using PhoneBookService.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace PhoneBookService.Controllers
{
    public class PhoneBookController : ApiController
    {
        private List<PhoneBook> book;
    
        public PhoneBookController()
        {
            book = new List<PhoneBook>()
            {
                new PhoneBook{Name = "kaddy", PhoneNumber = "08352172", Address=" 1 old bawn road"},
                new PhoneBook{Name = "RB", PhoneNumber= "08378261", Address ="2 high street"},
                new PhoneBook{Name = "Alfie", PhoneNumber= "08378261", Address ="2 cork street"}
            };
        }

        public IHttpActionResult GetAllBooks()
        {
            return Ok(book.OrderBy(s => s.Name.ToList()));
        }

        public IHttpActionResult GetPhoneNumber(string number)
        {
            var num = book.FirstOrDefault(b => b.PhoneNumber.ToUpper() == number.ToUpper());
            if (num == null)
            {
                return NotFound();
            }
            return Ok(num.PhoneNumber);
        }

        public IHttpActionResult GetNames(string name)
        {
            var names = book.Where((b => b.Name.ToUpper() == name.ToUpper()));
            if (names == null)
            {
                return NotFound();
            }
            return Ok(names);
        }
    }
}
