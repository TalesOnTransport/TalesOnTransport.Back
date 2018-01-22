using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore;
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

        [HttpPost("{pin:int}")]
        public async Task<JsonResult> PostScan(int pin)
        {
            if (pin == 111111)
            {
                var json = new { author = "FAKE Orson Scott Card", id = "6811b546-a97c-4c71-9166-70b7e8e13341", title = "FAKE Ender's Game", timesScanned = "a number of" };
                return Json(json);

            }

            if (pin == 222222)
            {
                var json = new { author = "FAKE Liu Cixin", id = "b210efc1-bd90-43dd-b333-c566d7da170a", title = "FAKE The Three Body Problem", timesScanned = "a number of" };
                return Json(json);

            }

            if (pin == 333333)
            {
                var json = new { author = "FAKE Neal Stephenson", id = "ffc6ba8e-fac7-4869-b614-72482ac53710", title = "FAKE Snow Crash", timesScanned = "a number of" };
                return Json(json);

            }

            return new JsonResult(null) { StatusCode = (int)HttpStatusCode.NotFound };
            
            // fds
            //Book book = _bookContext.Book.Where(b => b.PIN == pin).First();


            //if (book == null)
            //{
            //    return new JsonResult(book) { StatusCode = (int)HttpStatusCode.NotFound };
            //}

            //using (var db = new BookContext() { })
            //{
            //    var book = db.Book
            //               .Where(b => b.PIN == pin)
            //               .First();

            //    var a = 1;

            //}

            //var db = new BookContext();
            //var book = db.Book
            //               .Where(b => b.PIN == pin)
            //               .First();

            //return Json(new { hello = "world" });
        }


        [HttpPost("{bookId:guid}")]
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

            var json = new { author = $"{book.Author}", id = $"{book.Id}", timesScanned = $"{book.TimesScanned}", title = $"{book.Title}" };
            return Json(json);
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