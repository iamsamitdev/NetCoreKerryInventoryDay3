using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetcoreKerryInventory.Models;
using System.Collections;

using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace NetcoreKerryInventory.Controllers;

[SessionCheck]
public class BackendController : Controller
{

    private readonly InventoryDBContext _context;

    public BackendController(InventoryDBContext context)
    {
        _context = context;
    }

    // ตัวอย่างการสร้าง Report PDF ด้วย QuestPDF
    public RedirectResult ReportPDF()
    // public String ReportPDF()
    {

        var reportname = "Report_"+ DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss");

        Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(1, Unit.Centimetre);
                page.DefaultTextStyle(
                    x => x.FontSize(12).FontFamily("Tahoma")
                );
                
                page.Header()
                    .Text("รายการสินค้า")
                    .SemiBold().FontSize(18).FontColor(Colors.Blue.Medium);
                
                page.Content()
                    .PaddingVertical(5, Unit.Millimetre)
                    .Border(1)
                    .Table(async table => 
                    {
                        table.ColumnsDefinition(columns => {
                            columns.ConstantColumn(10, Unit.Millimetre);
                            // columns.ConstantColumn(12, Unit.Millimetre);
                            columns.RelativeColumn();
                            columns.ConstantColumn(20, Unit.Millimetre);
                            columns.ConstantColumn(10, Unit.Millimetre);
                            columns.ConstantColumn(10, Unit.Millimetre);
                            columns.RelativeColumn();
                        });

                        table.Header(header =>
                        {
                            header.Cell().Element(CellStyle).AlignCenter().Text("#");
                            // header.Cell().Element(CellStyle).Text("Images");
                            header.Cell().Element(CellStyle).Text("Name");
                            header.Cell().Element(CellStyle).Text("Price");
                            header.Cell().Element(CellStyle).Text("Qty");
                            header.Cell().Element(CellStyle).Text("Cat");
                            header.Cell().Element(CellStyle).Text("Create");
                            
                            static IContainer CellStyle(IContainer container)
                            {
                                return container.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(3).PaddingBottom(5).BorderBottom(1).BorderColor(Colors.Black);
                            }
                        });
                        
                        foreach(var item in _context.Products.ToList())
                        {
                            table.Cell().Element(CellStyle).AlignCenter().Text(item.ProductID);
                            // table.Cell().Element(CellStyle).Image(item.ProductPicture);
                            table.Cell().Element(CellStyle).Text(item.ProductName);
                            table.Cell().Element(CellStyle).Text(item.UnitPrice);
                            table.Cell().Element(CellStyle).Text(item.UnitInStock);
                            table.Cell().Element(CellStyle).Text(item.CategoryID);
                            table.Cell().Element(CellStyle).Text(item.CreatedDate);

                            static IContainer CellStyle(IContainer container)
                            {
                                return container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(3).PaddingBottom(5);
                            }
                        }
                    });


                
                page.Footer()
                    .AlignCenter()
                    .Text(x =>
                    {
                        x.Span("Page ");
                        x.CurrentPageNumber();
                    });
            });
        })
        
        .GeneratePdf(Url.Content("wwwroot/Reports/"+reportname+".pdf"));

        return Redirect(Url.Content("~/Reports/"+reportname+".pdf"));
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
