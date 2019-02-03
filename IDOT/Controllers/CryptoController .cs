using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IDOT.Models;

namespace CryptoApi.Controllers
{
    [Route("api/crypto")]
    [ApiController]
    public class CryptoController : ControllerBase
    {
        private readonly CryptoContext _context;

        public CryptoController(CryptoContext context)
        {
            _context = context;

            if (_context.CryptoItems.Count() == 0)
            {
                // Create new CryptoItems if collection is empty
                _context.CryptoItems.Add(new CryptoItem { Name = "Bitcoin" });
                _context.CryptoItems.Add(new CryptoItem { Name = "Ethereum" });
                _context.SaveChanges();
            }
        }

        // GET: api/Crypto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CryptoItem>>> GetCryptoItems()
        {
            return await _context.CryptoItems.ToListAsync();
        }

        // GET: api/Crypto/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CryptoItem>> GetCryptoItem(long id)
        {
            var cryptoItem = await _context.CryptoItems.FindAsync(id);

            if (cryptoItem == null)
            {
                return NotFound();
            }

            return cryptoItem;
        }

        // POST: api/Crypto
        [HttpPost]
        public async Task<ActionResult<CryptoItem>> PostCryptoItem(CryptoItem item)
        {
            _context.CryptoItems.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCryptoItem), new { id = item.Id }, item);
        }

        // PUT: api/Crypto/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCryptoItem(long id, CryptoItem item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Crypto/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCryptoItem(long id)
        {
            var cryptoItem = await _context.CryptoItems.FindAsync(id);

            if (cryptoItem == null)
            {
                return NotFound();
            }

            _context.CryptoItems.Remove(cryptoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}