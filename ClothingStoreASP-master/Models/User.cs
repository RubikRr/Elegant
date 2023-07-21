using System.ComponentModel.DataAnnotations;
using WomanShop.Areas.Admin.Models;

namespace WomanShop.Models
{
    public class User
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Не указана почта")]
        [EmailAddress(ErrorMessage = "Введите валидный email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Необходимо заполнить поле с паролем")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Введите имя")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Введите фамилию")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Впишите номер телефона")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Задайте права пользователя")]
        public string RoleName { get; set; }
        public User(){
            Id=Guid.NewGuid();
        }

        public User(string email, string password):this()
        {
            Email = email;
            Password = password;
        }
        public User(string email, string password, string firstName, string lastName, string phone) : this(email, password)
        {
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
        }
    }   
}
