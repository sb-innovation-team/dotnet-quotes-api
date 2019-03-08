using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using socialbrothers_quotes_api.Models;
using socialbrothers_quotes_api.Util;

namespace socialbrothers_quotes_api.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class QuotesController : Controller {
        private readonly QuoteContext _context;

        public QuotesController(QuoteContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Quote>>> ReadQuotes() {
            var quotes = await _context.Quotes.ToListAsync();
            return FilterAndSortQuotes(quotes, Request.Query).ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Quote>> ReadQuote(long id) {
            var quote = await _context.Quotes.FindAsync(id);

            if (quote == null)
                return NotFound();

            return quote;
        }

        [HttpPost]
        public async Task<ActionResult<Quote>> CreateQuote([FromBody] Quote quote) {
            _context.Quotes.Add(quote);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(CreateQuote), new {id = quote.Id}, quote);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuote(long id, [FromBody] Quote quote) {
            if (id != quote.Id)
                return BadRequest();

            var itemExists = await _context.Quotes.AnyAsync(q => q.Id == id);
            if (!itemExists)
                return BadRequest();

            _context.Entry(quote).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuote(long id) {
            var quote = await _context.Quotes.FindAsync(id);

            if (quote == null)
                return NotFound();

            _context.Quotes.Remove(quote);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private IEnumerable<Quote> FilterAndSortQuotes(IEnumerable<Quote> collection, IQueryCollection query) {
            var filteredQuotes = FiltersUtil.GetFilteredQuotes(collection, query);
            filteredQuotes = SortUtil.GetSortedQuotes(filteredQuotes, query);
            return filteredQuotes.ToList();
        }
    }
}