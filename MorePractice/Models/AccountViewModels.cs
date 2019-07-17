using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MorePractice.Models
{    
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Адрес электронной почты")]
        //[EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запомнить меня")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        //[EmailAddress]
        [Display(Name = "Адрес электронной почты")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Это поле обязательно для заполнения!")]
        [Display(Name = "Имя")]
        [MaxLength(20, ErrorMessage = "Слишком много символов.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Это поле обязательно для заполнения!")]
        [Display(Name = "Фамилия")]
        [MaxLength(30, ErrorMessage = "Слишком много символов.")]
        public string Surname { get; set; }

        [Display(Name = "Отчество")]
        public string Fathername { get; set; }

        [Required(ErrorMessage = "Это поле обязательно для заполнения!")]
        [Display(Name = "Дата рождения")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Это поле обязательно для заполнения!")]
        [RegularExpression(@"^((\+7|7|8)+([0-9]){10})$", ErrorMessage = "Неправильный номер телефона")]
        [Display(Name = "Номер телефона")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(100)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля")]
        [Compare("Password", ErrorMessage = "Пароль и его подтверждение не совпадают.")]
        public string ConfirmPassword { get; set; }

    }

    public class EditViewModel
    {
        public string ClientId { get; set; }

        [Required]
        //[EmailAddress]
        [Display(Name = "Адрес электронной почты")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Это поле обязательно для заполнения!")]
        [Display(Name = "Имя")]
        [MaxLength(20, ErrorMessage = "Слишком много символов.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Это поле обязательно для заполнения!")]
        [Display(Name = "Фамилия")]
        [MaxLength(30, ErrorMessage = "Слишком много символов.")]
        public string Surname { get; set; }

        public string Fathername { get; set; }

        [Required(ErrorMessage = "Это поле обязательно для заполнения!")]
        [Display(Name = "Дата рождения")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Это поле обязательно для заполнения!")]
        [RegularExpression(@"^((\+7|7|8)+([0-9]){10})$", ErrorMessage = "Неправильный номер телефона")]
        [Display(Name = "Номер телефона")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Значение {0} должно содержать не менее {2} символов.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля")]
        [Compare("Password", ErrorMessage = "Пароль и его подтверждение не совпадают.")]
        public string ConfirmPassword { get; set; }


        public EditViewModel(Client client)
        {
            ClientId = client.Id;
            Name = client.Name;
            Surname = client.Surname;
            Fathername = client.Fathername;
            BirthDate = client.BirthDate;
            PhoneNumber = client.PhoneNumber;
        }

        public EditViewModel()
        {

        }
    }
}
