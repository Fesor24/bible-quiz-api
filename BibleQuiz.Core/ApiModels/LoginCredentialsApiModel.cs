using System.ComponentModel.DataAnnotations;

namespace BibleQuiz.Core
{
    public class LoginCredentialsApiModel
    {
		[Required(ErrorMessage = "Email is a required field")]
		[EmailAddress]
		public string Email { get; set; }

		/// <summary>
		/// The password of the user
		/// </summary>
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}
