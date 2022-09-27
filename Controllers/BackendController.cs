using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetcoreKerryInventory.Models;

namespace NetcoreKerryInventory.Controllers;

[SessionCheck]
public class BackendController : Controller
{

    private readonly InventoryDBContext _context;

    public BackendController(InventoryDBContext context)
    {
        _context = context;
    }


    public IActionResult Dashboard()
    {
        return View();
    }


    // อ่านข้อมูลสินค้าทั้งหมด
    // GET: Products
    public async Task<IActionResult> Product()
    {
        return View("~/Views/Backend/Product.cshtml", await _context.Products.ToListAsync());
    }

    // อ่านหมวดหมู่ทั้งหมด
    // GET: Category
    public async Task<IActionResult> Category()
    {
        return View("~/Views/Backend/Category.cshtml", await _context.Categories.ToListAsync());
    }

    public IActionResult Logout()
    {
        // Remove Session
        HttpContext.Session.Clear();
        return RedirectToAction("Login","Frontend");
    }

}
