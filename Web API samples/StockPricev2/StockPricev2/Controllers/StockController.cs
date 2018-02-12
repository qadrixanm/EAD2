// a RESTful services which provides stock market prices for stocks and allows updates and deletes (CRUD)
// data stored in a LocalDB database using Entity Framework Code First under app_data
// /swagger for UI test page


using StockPrice2.Models;
using System;
using System.Linq;
using System.Web.Http;

namespace StockPricev2.Controllers
{
    public class StockController : ApiController
    {
      /*
       * GET /api/stock            get all stock listings                  GetAllListings
       * GET /api/stock/IBM        get price last trade for IBM            GetStockPrice   
       * POST /api/stock           add stock listing                       PostAddSListing(listing)
       * PUT /api/stock            update price for a listing              PutUpdateListing(listing)
       * DELETE /api/stock         delete stock listing                    DeleteListing(ticker)
       */

        private StockContext db = new StockContext();

        // must have default constructor
        // todo: use dependency injection and repository pattern

        // GET api/stock
        public IHttpActionResult GetAllListings()
        {
            if (db.StockListings.Count() == 0)
            {
                return NotFound();
            }
             
            else
            {
                return Ok(db.StockListings.OrderBy(s => s.TickerSymbol).ToList());       // 200 OK, listings serialized in response body 
            }                                       
        }

        // GET api/stock/GOOG or api/stock?ticker=GOOG
        public IHttpActionResult GetStockPrice(String ticker)
        {
            // LINQ query, find matching ticker (case-insensitive) or default value (null) if none matching
            StockListing listing = db.StockListings.SingleOrDefault(l => l.TickerSymbol.ToUpper() == ticker.ToUpper());
            if (listing == null)
            {
                return NotFound();          // 404
            }
            return Ok(listing.Price);
        }

        // POST api/stock, request body contains stock listing serialized as XML or JSON
        public IHttpActionResult PostAddListing(StockListing listing)
        {
            if (ModelState.IsValid)                                             // model class validation ok?
            {
                // check for duplicate
                // LINQ query - get record
                var record = db.StockListings.SingleOrDefault(l => l.TickerSymbol.ToUpper() == listing.TickerSymbol.ToUpper());
                if (record == null)
                {
                    db.StockListings.Add(listing);
                    db.SaveChanges();                                           // commit

                    // create http response with Created status code and listing serialised as content and Location header set to URI for new resource
                    string uri = Url.Link("DefaultApi", new { ticker = listing.TickerSymbol });         // name of default route in WebApiConfig.cs
                    return Created(uri, listing);
                }
                else
                {
                    return BadRequest("resource already exists");      // 400, already exists
                }
            }
            else
            {
                return BadRequest(ModelState);        // 400
            }
        }

        // update a listing i.e. update the price for specified ticker
        public IHttpActionResult PutUpdateListing(String ticker, StockListing listing)                  // listing will be in request body
        {
            if (ModelState.IsValid)
            {
                if (ticker == listing.TickerSymbol)
                {
                    var record = db.StockListings.SingleOrDefault(l => l.TickerSymbol.ToUpper() == ticker.ToUpper());
                    if (record == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        record.Price = listing.Price;               // update price
                        db.SaveChanges();                           // commit
                        return Ok(record);                          // or 204 with no content
                    }
                }
                else
                {
                    return BadRequest("invalid ticker");        // 400
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }


        // delete the listing for specified ticker
        public IHttpActionResult DeleteListing(String ticker)
        {
            var record = db.StockListings.SingleOrDefault(l => l.TickerSymbol.ToUpper() == ticker.ToUpper());
            if (record != null)
            {
                db.StockListings.Remove(record);
                db.SaveChanges();                   // commit
                return Ok(record);                  // 200 ok with entity, or 204 with no content
            }
            else
            {
                return NotFound();
            }
        }
    }

    // SingleOrDefault throws exception if more than one matching result
}
