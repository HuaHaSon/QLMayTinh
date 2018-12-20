using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLSanPham.Models;

namespace QLSanPham.Controllers
{
    [Route("api/QLSanPham")]
    [ApiController]
    public class SanPhams_APIController : ControllerBase
    {
        private readonly DockerDemoContext _context;

        public SanPhams_APIController(DockerDemoContext context)
        {
            _context = context;
        }

        // GET: api/SanPhams_API
        [HttpGet]
        public IEnumerable<SanPham> GetSanPham()
        {
            return _context.SanPham;
        }

        // GET: api/SanPhams_API/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSanPham([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sanPham = await _context.SanPham.FindAsync(id);

            if (sanPham == null)
            {
                return NotFound();
            }

            return Ok(sanPham);
        }

        // PUT: api/SanPhams_API/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSanPham([FromRoute] string id, [FromBody] SanPham sanPham)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sanPham.MaSp)
            {
                return BadRequest();
            }

            _context.Entry(sanPham).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SanPhamExists(id))
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

        // POST: api/SanPhams_API
        [HttpPost]
        public async Task<IActionResult> PostSanPham([FromBody] SanPham sanPham)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.SanPham.Add(sanPham);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SanPhamExists(sanPham.MaSp))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSanPham", new { id = sanPham.MaSp }, sanPham);
        }

        // DELETE: api/SanPhams_API/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSanPham([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sanPham = await _context.SanPham.FindAsync(id);
            if (sanPham == null)
            {
                return NotFound();
            }

            _context.SanPham.Remove(sanPham);
            await _context.SaveChangesAsync();

            return Ok(sanPham);
        }

        private bool SanPhamExists(string id)
        {
            return _context.SanPham.Any(e => e.MaSp == id);
        }
    }
}