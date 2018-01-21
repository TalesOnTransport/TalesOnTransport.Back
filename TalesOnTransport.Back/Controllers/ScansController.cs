using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TalesOnTransport.Back.Models;

namespace TalesOnTransport.Back.Controllers
{
    [Produces("application/json")]
    [Route("api/Scans")]
    [EnableCors("MyPolicy")]
    public class ScansController : Controller
    {
        private readonly BookContext _bookContext;
        private readonly ScanContext _scanContext;

        public ScansController(ScanContext scanContext, BookContext bookContext)
        {
            _bookContext = bookContext;
            _scanContext = scanContext;
        }

        // GET: api/Scans
        [HttpGet]
        public IEnumerable<Scan> GetScan()
        {
            return _scanContext.Scan;
        }

        // GET: api/Scans/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetScan([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var scan = await _scanContext.Scan.SingleOrDefaultAsync(m => m.Id == id);

            if (scan == null)
            {
                return NotFound();
            }

            return Ok(scan);
        }

        // PUT: api/Scans/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutScan([FromRoute] Guid id, [FromBody] Scan scan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != scan.Id)
            {
                return BadRequest();
            }

            _scanContext.Entry(scan).State = EntityState.Modified;

            try
            {
                await _scanContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScanExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost("{bookId}")]
        public async Task<JsonResult> PostScan(Guid bookId)
        {
            Book book = _bookContext.Book.Find(bookId);
            if (book == null)
            {
                return new JsonResult(book) { StatusCode = (int)HttpStatusCode.NotFound };
            }

            book.TimesScanned = book.TimesScanned + 1;
            _bookContext.Book.Update(book);
            await _bookContext.SaveChangesAsync();

            Scan scan = new Scan
            {
                Id = new Guid(),
                BookId = bookId
            };
            _scanContext.Scan.Add(scan);
            await _scanContext.SaveChangesAsync();

            var json = new { author = $"{book.Author}", title = $"{book.Title}", timesScanned = $"{book.TimesScanned}" };
            return Json(json);
        }

        // DELETE: api/Scans/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteScan([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var scan = await _scanContext.Scan.SingleOrDefaultAsync(m => m.Id == id);
            if (scan == null)
            {
                return NotFound();
            }

            _scanContext.Scan.Remove(scan);
            await _scanContext.SaveChangesAsync();

            return Ok(scan);
        }

        private bool ScanExists(Guid id)
        {
            return _scanContext.Scan.Any(e => e.Id == id);
        }
    }
}