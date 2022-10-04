using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetcoreKerryInventory.Models;

[ModelMetadataType(typeof(CategoryMetadata))]
public partial class Category
{

}

public class CategoryMetadata
{
    [Display(Name = "ไอดี")]
    [Required(AllowEmptyStrings = false, ErrorMessage ="ป้อนไอดีก่อน")]
    public int CategoryID { get; set; }

    [Display(Name = "ชื่อหมวดหมู่")]
    [Required(AllowEmptyStrings = false, ErrorMessage ="ป้อนชื่อหมวดหมู่")]
    public string CategoryName { get; set; } = null!;

    [Display(Name = "สถานะ")]
    [Required(AllowEmptyStrings = false, ErrorMessage ="ป้อนสถานะหมวดหมู่กอน")]
    public int CategoryStatus { get; set; }
}