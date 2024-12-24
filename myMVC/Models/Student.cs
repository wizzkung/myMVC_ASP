using System.ComponentModel.DataAnnotations;

namespace myMVC.Models
{
    public class Student
    {
        //[Range(1, 100)]
        //[Required(ErrorMessage = "Обязательно к заполнению")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Обязательно к заполнению")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Неверный формат")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Обязательно к заполнению")]
        [RegularExpression(@"^\+7\d{10}$", ErrorMessage = "Номер должен начинаться с + и иметь 10 цифр ")]
        public string PhoneNumber { get; set; }

        //public Student()
        //{
        //    Id = 2;
        //    Name = "Kostya";
        //    StudentId = 231;
        //}
    }
}
