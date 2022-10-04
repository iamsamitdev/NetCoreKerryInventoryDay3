using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetcoreKerryInventory.Models;

[ModelMetadataType(typeof(ProductMetadata))]
public partial class Product
{

}

public class ProductMetadata
{

    [Display(Name = "หมวดหมู่")]
    [Required(AllowEmptyStrings = false, ErrorMessage ="ป้อนหมวดหมู่ก่อน")]
    public int? CategoryID { get; set; }

    [Display(Name = "ชื่อสินค้า")]
    [Required(AllowEmptyStrings = false, ErrorMessage ="ป้อนชื่อชื่อสินค้า")]
    public string? ProductName { get; set; }

    [Display(Name = "ราคา")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:n}")]
    [Required(AllowEmptyStrings = false, ErrorMessage ="ป้อนราคาก่อน")]
    public decimal? UnitPrice { get; set; }

    [Display(Name = "รูปภาพ")]
    [Required(AllowEmptyStrings = false, ErrorMessage ="ป้อนรูปภาพก่อน")]
    public string? ProductPicture { get; set; }
    [Display(Name = "จำนวน")]
    [Required(AllowEmptyStrings = false, ErrorMessage ="ป้อนจำนวนก่อน")]
    public int? UnitInStock { get; set; }

    [Display(Name = "วันที่เพิ่ม")]
    [DataType(DataType.Date)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
    [Required(AllowEmptyStrings = false, ErrorMessage ="ป้อนวันที่เพิ่มก่อน")]
    public DateTime? CreatedDate { get; set; }
}
