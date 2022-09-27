using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace NetcoreKerryInventory.Models;

[ModelMetadataType(typeof(ProductMetadata))]
public partial class Product
{

}

public class ProductMetadata
{
    [Display(Name = "Category")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "ป้อนหมวดหมู่สินค้าก่อน")]
    public int? CategoryID { get; set; }

    [Display(Name = "Name")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "ป้อนชื่อสินค้าก่อน")]
    public string ProductName { get; set; }

    [Display(Name = "Price")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "ป้อนราคาก่อน")]
    public decimal? UnitPrice { get; set; }

    [Display(Name = "Picture")]
    public string ProductPicture { get; set; }

    [Display(Name = "Unit")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "ป้อนจำนวนก่อน")]
    public int? UnitInStock { get; set; }

    [Display(Name = "Created")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "เลือกวันที่สร้างก่อน")]
    [DataType(DataType.Date)]
    [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime? CreatedDate { get; set; }

    [Display(Name = "ModifiedDate")]
    [DataType(DataType.Date)]
    [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime? ModifiedDate { get; set; }
}
