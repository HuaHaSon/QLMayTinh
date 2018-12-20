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
    public class KhachHangsController : Controller
    {
        private readonly DockerDemoContext _context;

        static HttpClient client = new HttpClient();

        static readonly string address = Environment.GetEnvironmentVariable("CustomerUrl").ToString();

        static Uri apiAddress = new Uri(address);

        static void GetAPI()
        {
            client.BaseAddress = apiAddress;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        const string path = "api/QLKhachHang";


        public KhachHangsController(DockerDemoContext context)
        {
            _context = context;
        }

        // GET: KhachHangs
        public async Task<IActionResult> Index()
        {
            if (!apiAddress.Equals(client.BaseAddress))
            {
                GetAPI();
            }

            List<KhachHang> khachHang = new List<KhachHang>();

            HttpResponseMessage respond = await client.GetAsync(path);

            if (respond.IsSuccessStatusCode)
            {
                // Gán dữ liệu API đọc được
                var khachHangJsonString = await respond.Content.ReadAsStringAsync();

                var deserialized = JsonConvert.DeserializeObject<IEnumerable<KhachHang>>(khachHangJsonString);

                khachHang = deserialized.ToList();
            }

            return View(khachHang);
        }

        // GET: KhachHangs/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (!apiAddress.Equals(client.BaseAddress))
            {
                GetAPI();
            }

            KhachHang khachHang = null;

            HttpResponseMessage respond = await client.GetAsync($"{path}/{id}");

            if (respond.IsSuccessStatusCode)
            {
                // Gán dữ liệu API đọc được
                khachHang = await respond.Content.ReadAsAsync<KhachHang>();
            }

            return View(khachHang);
        }

        // GET: KhachHangs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: KhachHangs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaKh,TenKh,GioiTinh,NgaySinh,Sdt,DiaChi")] KhachHang khachHang)
        {
            if (!apiAddress.Equals(client.BaseAddress))
            {
                GetAPI();
            }

            if (ModelState.IsValid)
            {
                HttpResponseMessage respond = await client.PostAsJsonAsync(path, khachHang);
                respond.EnsureSuccessStatusCode();

                return RedirectToAction(nameof(Index));
            }

            return View(khachHang);
        }

        // GET: KhachHangs/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khachHang = await _context.KhachHang.FindAsync(id);
            if (khachHang == null)
            {
                return NotFound();
            }
            return View(khachHang);
        }

        // POST: KhachHangs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaKh,TenKh,GioiTinh,NgaySinh,Sdt,DiaChi")] KhachHang khachHang)
        {
            if (!apiAddress.Equals(client.BaseAddress))
            {
                GetAPI();
            }

            if (ModelState.IsValid)
            {
                HttpResponseMessage respond = await client.PutAsJsonAsync($"{path}/{id}", khachHang);
                respond.EnsureSuccessStatusCode();

                return RedirectToAction(nameof(Index));
            }

            return View(khachHang);
        }

        // GET: KhachHangs/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khachHang = await _context.KhachHang
                .FirstOrDefaultAsync(m => m.MaKh == id);
            if (khachHang == null)
            {
                return NotFound();
            }

            return View(khachHang);
        }

        // POST: KhachHangs/Delete/5
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

        private bool KhachHangExists(string id)
        {
            return _context.KhachHang.Any(e => e.MaKh == id);
        }
    }
}
