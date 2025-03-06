using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpaNewYork.Models;
using System.Diagnostics;
using System.Security.Claims;
using BC = BCrypt.Net.BCrypt;
using System.Threading.Tasks;

namespace SpaNewYork.Controllers
{
    public class HomeController : Controller
    {
        private readonly SpaNewYorkDB _context;

        public HomeController(SpaNewYorkDB context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DichVuNoiBat()
        {
            var dichVuNoiBat = _context.DanhMucHangHoaDichVu
                .Where(dv => dv.NhomHHDV == 1) // Giả sử nhóm 1 là dịch vụ nổi bật
                .ToList();
            return View(dichVuNoiBat);
        }

        // GET: Register
        [AllowAnonymous]
        public IActionResult Register(string? successMessage)
        {
            if (!string.IsNullOrEmpty(successMessage))
                TempData["ThongBao"] = successMessage;
            return View();
        }

        // POST: Register
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([Bind("ID,HoVaTen,Email,DienThoai,DiaChi,TenDangNhap,MatKhau,XacNhanMatKhau")] NguoiDung nguoiDung)
        {
            if (ModelState.IsValid)
            {
                var kiemTra = _context.NguoiDung.FirstOrDefault(r => r.TenDangNhap == nguoiDung.TenDangNhap);
                if (kiemTra == null)
                {
                    nguoiDung.MatKhau = BC.HashPassword(nguoiDung.MatKhau);
                    nguoiDung.XacNhanMatKhau = nguoiDung.MatKhau;
                    nguoiDung.Quyen = false; // Mặc định là người dùng thường

                    _context.Add(nguoiDung);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Register", "Home", new { successMessage = "Đăng ký tài khoản thành công." });
                }
                else
                {
                    TempData["ThongBaoLoi"] = "Tên đăng nhập đã tồn tại.";
                    return View(nguoiDung);
                }
            }
            return View(nguoiDung);
        }

        // GET: Login
        [AllowAnonymous]
        public IActionResult Login(string? ReturnUrl)
        {
            if (User.Identity.IsAuthenticated)
                return LocalRedirect(ReturnUrl ?? "/");

            ViewBag.LienKetChuyenTrang = ReturnUrl ?? "/";
            return View();
        }

        // POST: Login
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(DangNhap model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var nguoiDung = _context.NguoiDung.FirstOrDefault(r => r.TenDangNhap == model.TenDangNhap);
            if (nguoiDung == null || !BC.Verify(model.MatKhau, nguoiDung.MatKhau))
            {
                TempData["ThongBaoLoi"] = "Tài khoản hoặc mật khẩu không đúng.";
                return View(model);
            }

            var claims = new List<Claim>
            {
                new Claim("ID", nguoiDung.ID.ToString()),
                new Claim(ClaimTypes.Name, nguoiDung.TenDangNhap),
                new Claim("HoVaTen", nguoiDung.HoVaTen),
                new Claim(ClaimTypes.Role, nguoiDung.Quyen ? "Admin" : "User")
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = model.DuyTriDangNhap
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

            return LocalRedirect(model.LienKetChuyenTrang ?? (nguoiDung.Quyen ? "/Admin" : "/"));
        }

        // GET: Logout
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        // GET: Access Denied
        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult GioiThieu()
        {
            return View();
        }

        public IActionResult LienHe()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
