using Microsoft.AspNetCore.Mvc;
using NetcoreKerryInventory.Models;

namespace ASPNetCoreInventory.Controllers;

public class FrontendController : Controller
{

    // ทดสอบเขียนฟังก์ชันการเชื่อมต่อ Database
    public string TestConnectDB()
    {
        // สร้าง object context
        InventoryDBContext dBContext  = new InventoryDBContext();
        if (dBContext.Database.CanConnect())
        {
            return "Connect Database Success";
        } else
        {
            return "Connect Database Fail!!";
        }
    }

    public IActionResult Index()
    {
        return View();
    }

    public ActionResult About()
    {
        return View();
    }

    public ActionResult Webdev()
    {
        return View();
    }

    public ActionResult Mobiledev()
    {
        return View();
    }

    public ActionResult Graphic()
    {
        return View();
    }

    // เรียกแสดงแบบฟอร์มลงทะเบียน
    [HttpGet]
    public ActionResult Register()
    {
        return View();
    }

    // Submit ฟอร์มลงทะเบียน
    [HttpPost]
    public ActionResult Register(User user)
    {
        string message = null!;

        // ตรวจว่า Validation ผ่านแล้ว
        if (ModelState.IsValid)
        {
            // ผ่านแล้วทำารบันทึกข้อมูลลงตาราง user
            using (InventoryDBContext db = new InventoryDBContext())
            {
                db.Users.Add(user);
                db.SaveChanges();
                ModelState.Clear(); // Reset และ  Clear ข้อมูลในฟอร์ม
                message = "<div class=\"alert alert-success text-center\">ลงทะเบียนเรียบร้อยแล้ว</div>";
                // ส่งไปหน้า Login
                // return View("<meta http-equiv=\"refresh\" content=\"10; URL=Login\"></meta>");
                // Response.AppendHeader("Refresh", "10;url=Default.aspx");
                return RedirectToAction("Login", "Frontend");
            }

        }
        else
        {
            message = "<div class=\"alert alert-danger text-center\">ป้อนข้อมูลให้ครบก่อน</div>";
        }

        ViewBag.Message = message;

        return View();
    }

    // เรียกแสดงแบบฟอร์มลงเข้าสุ่ระบบ
    [HttpGet]
    public ActionResult Login()
    {
        return View();
    }


    // Submit Form Login
    [HttpPost]
    public ActionResult Login(User user)
    {

        string message = null!;

        // ตรวจว่า Validation ผ่านแล้ว
        if (user.EmailID != null && user.Password != null)
        {
            using (InventoryDBContext db = new InventoryDBContext())
            {
                var query = db.Users.Where(u => u.EmailID == user.EmailID).FirstOrDefault();
                if (query != null)
                {
                    if (string.Compare(user.Password, query.Password) == 0)
                    {
                        // เก็บข้อมูลลง  session 
                        HttpContext.Session.SetString("Email", query.EmailID);
                        HttpContext.Session.SetString("FirstName", query.FirstName);
                        HttpContext.Session.SetString("LastName", query.LastName);

                        // Redirect ไปยัง Backend
                        return RedirectToAction("Dashboard", "Backend");
                    }
                    else
                    {
                        message = "<div class=\"alert alert-danger text-center\">ป้อนรหัสผ่านไม่ถูกต้อง</div>";
                    }
                }
                else
                {
                    message = "<div class=\"alert alert-danger text-center\">ไม่พบอีเมล์นี้</div>";
                }
            }
        }
        else
        {
            message = "<div class=\"alert alert-danger text-center\">ป้อนข้อมูลให้ครบก่อน</div>";
        }

        ViewBag.Message = message;
        return View();

    }

}

