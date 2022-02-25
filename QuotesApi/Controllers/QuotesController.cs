using QuotesApi.Data;
using QuotesApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace QuotesApi.Controllers 
{
   
    public class QuotesController : ApiController
    {
        private QuotesDbContext _quotesDb = new QuotesDbContext();

        // GET: api/Quotes
        [HttpGet]
        [Route("api/Quotes/{sort=}")]
        public IHttpActionResult Get(string sort)
        {
            IQueryable<Quote> quotes;

            switch (sort)
            {
                case "desc":
                    quotes = _quotesDb.Quotes.OrderByDescending(q => q.CreatedAt);
                    break;
                case "asc":
                    quotes = _quotesDb.Quotes.OrderBy(q => q.CreatedAt);
                    break;
                default:
                    quotes = _quotesDb.Quotes;
                    break;

            }
           
            return Ok(quotes);
        }

        [HttpGet]
        [Route("api/Quotes/PagingQuote/{pageNumber=1}/{pageSize=5}")]
        public IHttpActionResult PagingQuote(int pageNumber, int pageSize)
        {
            var quotes = _quotesDb.Quotes.OrderBy(q => q.Id);            
            return Ok(quotes.Skip((pageNumber - 1) * pageSize).Take(pageSize));
        }

        [HttpGet]
        [Route("api/Quotes/SearchQuote/{type=}")]
        public IHttpActionResult SearchQuote(string type)
        {
            var quotes = _quotesDb.Quotes.Where(q => q.Type.StartsWith(type));
            return Ok(quotes);
        }

        [HttpGet]        
        public IHttpActionResult GetAll()
        {
            return Ok(_quotesDb.Quotes);
        }

        // GET: api/Quotes/5
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var quote = _quotesDb.Quotes.Find(id);
            return Ok(quote);
        }

        // POST: api/Quotes
        [HttpPost]
        public IHttpActionResult Post([FromBody]Quote quote)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _quotesDb.Quotes.Add(quote);
            _quotesDb.SaveChanges();
            return StatusCode(HttpStatusCode.Created);
        }

        // PUT: api/Quotes/5
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody]Quote quote)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = _quotesDb.Quotes.FirstOrDefault(e => e.Id == id);
            
            if (entity == null)
                return BadRequest("No record found");

            
            entity.Title = quote.Title;
            entity.Description = quote.Description;
            entity.Author = quote.Author;
            _quotesDb.SaveChanges();

            return Ok("Record Updated");
        }

        // DELETE: api/Quotes/5
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var quote = _quotesDb.Quotes.Find(id);

            if (quote == null)
                return BadRequest("Record not found");

            _quotesDb.Quotes.Remove(quote);
            _quotesDb.SaveChanges();

            return Ok("Record deleted.");
        }
    }
}
