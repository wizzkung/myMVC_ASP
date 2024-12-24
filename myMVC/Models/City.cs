using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace myMVC.Models
{
    public class City
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Не указано имя")]
        [MaxLength(5, ErrorMessage = "Not more 5")]
        [MinLength(2, ErrorMessage ="Min 2")]
        [Display(Name = "Название")]
        public string Name { get; set; }
        //[DataType(DataType.Date)]
        //public DateTime dt { get; set; }
        //[RegularExpression(@"^\+7\d{10}$", ErrorMessage = "Not correct phone number ") ]
        //public string phoneNum { get; set; }
        public int age { get; set; }
        public double height { get; set; }

    }
}
