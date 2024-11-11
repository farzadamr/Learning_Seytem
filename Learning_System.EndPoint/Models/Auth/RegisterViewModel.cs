using System.ComponentModel.DataAnnotations;

namespace Learning_System.EndPoint.Models.Auth
{
	public class RegisterViewModel
	{
		[Required(ErrorMessage = "نام را وارد کنید")]
		public string FirstName { get; set; }
		[Required(ErrorMessage = "نام خانوادگی را وارد کنید")]
		public string LastName { get; set; }
		[Required(ErrorMessage = "ایمیل را وارد کنید")]
		[EmailAddress]
		public string Email { get; set; }
		[Required(ErrorMessage = "شماره تلفن را وارد کنید")]
		[MaxLength(11,ErrorMessage = "شماره تلفن باید 11 رقم باشد")]
		[MinLength(11,ErrorMessage = "شماره تلفن باید 11 رقم باشد")]
		public string PhoneNumber { get; set; }
		[Required(ErrorMessage = "کلمه عبور را وارد کنید")]
		[MaxLength(8,ErrorMessage = "کلمه عبور بیشتر از 8 کاراکتر نمیتواند باشد")]
		public string Password { get; set; }
		[Required(ErrorMessage = "تکرار کلمه عبور را وارد کنید")]
		[Compare(nameof(Password),ErrorMessage = "کلمه عبور و تکرار آن باید برابر باشند")]
		public string Repassword { get; set; }
        public string AvatarPath { get; set; } = "~/assets/image/default-profile.jpg";
	}
}
