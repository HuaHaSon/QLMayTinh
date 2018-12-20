using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLKhachHang.Models;

namespace QLKhachHang.Controllers
{
    [Route("api/QLKhachHang")]
    [ApiController]
    public class KhachHangs_APIController : ControllerBase
    {
        private readonly DockerDemoContext _context;

        public KhachHangs_APIController(DockerDemoContext context)
        {
            _context = context;
        }

        // GET: api/KhachHangs_API
        [HttpGet]
        public IEnumerable<KhachHang> GetKhachHang()
        {
            return _context.KhachHang;
        }

        // GET: api/KhachHangs_API/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetKhachHang([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var khachHang = await _context.KhachHang.FindAsync(id);

            if (khachHang == null)
            {
                return NotFound();
            }

            return Ok(khachHang);
        }

        // PUT: api/KhachHangs_API/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKhachHang([FromRoute] string id, [FromBody] KhachHang khachHang)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != khachHang.MaKh)
            {
                return BadRequest();
            }

            _context.Entry(khachHang).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KhachHangExists(id))
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

        // POST: api/KhachHangs_API
        [HttpPost]
        public async Task<IActionResult> PostKhachHang([FromBody] KhachHang khachHang)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.KhachHang.Add(khachHang);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (KhachHangExists(khachHang.MaKh))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetKhachHang", new { id = khachHang.MaKh }, khachHang);
        }

        // DELETE: api/KhachHangs_API/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKhachHang([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var khachHang = await _context.KhachHang.FindAsync(id);
            if (khachHang == null)
            {
                return NotFound();
            }

            _context.KhachHang.Remove(khachHang);
            await _context.SaveChangesAsync();

            return Ok(khachHang);
        }

        private bool KhachHangExists(string id)
        {
            return _context.KhachHang.Any(e => e.MaKh == id);
        }
    }
}