using System.ComponentModel.DataAnnotations;

namespace BibleQuiz.Core
{
	public class RegisterCredentialsApiModel
	{
		/// <summary>
		/// The email of the user
		/// </summary>
		[EmailAddress]
		[Required(ErrorMessage = "Email is a required field")]
		public string Email { get; set; }

		/// <summary>
		/// The password of the user
		/// </summary>
		[DataType(DataType.Password)]
		[Required(ErrorMessage = "Password is a required field")]
		public string Password { get; set; }
	}
}
