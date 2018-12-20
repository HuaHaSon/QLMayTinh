using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Main_Web.Models;
using System.Net.Http;
using Newtonsoft.Json;

namespace Main_Web.Controllers
{
    public class SanPhamsController : Controller
    {
        private readonly DockerDemoContext _context;

        static HttpClient client = new HttpClient();

        static readonly string address = Environment.GetEnvironmentVariable("ProductUrl").ToString();

        static Uri apiAddress = new Uri(address);

        static void GetAPI()
        {
            client.BaseAddress = apiAddress;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        const string path = "api/QLSanPham";


        public SanPhamsController(DockerDemoContext context)
        {
            _context = context;
        }

        // GET: SanPhams
        public async Task<IActionResult> Index()
        {
            if (!apiAddress.Equals(client.BaseAddress))
            {
                GetAPI();
            }

            List<SanPham> sanpham = new List<SanPham>();

            HttpResponseMessage respond = await client.GetAsync(path);

            if (respond.IsSuccessStatusCode)
            {
                // Gán dữ liệu API đọc được
                var sanphamJsonString = await respond.Content.ReadAsStringAsync();

                var deserialized = JsonConvert.DeserializeObject<IEnumerable<SanPham>>(sanphamJsonString);

                sanpham = deserialized.ToList();
            }

            return View(sanpham);
        }

        // GET: SanPhams/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (!apiAddress.Equals(client.BaseAddress))
            {
                GetAPI();
            }

            SanPham sanpham = null;

            HttpResponseMessage respond = await client.GetAsync($"{path}/{id}");

            if (respond.IsSuccessStatusCode)
            {
                // Gán dữ liệu API đọc được
                sanpham = await respond.Content.ReadAsAsync<SanPham>();
            }

            return View(sanpham);
        }

        // GET: SanPhams/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SanPhams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaSp,TenSp,Dvt,DonGia,Slton")] SanPham sanPham)
        {
            if (!apiAddress.Equals(client.BaseAddress))
            {
                GetAPI();
            }

            if (ModelState.IsValid)
            {
                HttpResponseMessage respond = await client.PostAsJsonAsync(path, sanPham);
                respond.EnsureSuccessStatusCode();

                return RedirectToAction(nameof(Index));
            }

            return View(sanPham);
        }

        // GET: SanPhams/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPham.FindAsync(id);
            if (sanPham == null)
            {
                return NotFound();
            }
            return View(sanPham);
        }

        // POST: SanPhams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaSp,TenSp,Dvt,DonGia,Slton")] SanPham sanPham)
        {
            if (!apiAddress.Equals(client.BaseAddress))
            {
                GetAPI();
            }

            if (ModelState.IsValid)
            {
                HttpResponseMessage respond = await client.PutAsJsonAsync($"{path}/{id}", sanPham);
                respond.EnsureSuccessStatusCode();

                return RedirectToAction(nameof(Index));
            }

            return View(sanPham);
        }

        // GET: SanPhams/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPham
                .FirstOrDefaultAsync(m => m.MaSp == id);
            if (sanPham == null)
            {
                return NotFound();
            }

            return View(sanPham);
        }

        // POST: SanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (!apiAddress.Equals(client.BaseAddress))
            {
                GetAPI();
            }

            HttpResponseMessage respond = await client.DeleteAsync($"{path}/{id}");

            return RedirectToAction(nameof(Index));
        }

        private bool SanPhamExists(string id)
        {
            return _context.SanPham.Any(e => e.MaSp == id);
        }
    }
}
