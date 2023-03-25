using System.ComponentModel.DataAnnotations;

namespace BibleQuiz.Core
{
	public class RegisterCredentialsApiModel
	{
		/// <summary>
		/// The first name of the user
		/// </summary>
		[Required(ErrorMessage = "First name is a required field")]
		public string FirstName { get; set; }

		/// <summary>
		/// The last name of the user
		/// </summary>
		[Required(ErrorMessage = "Last name is a required field")]
		public string LastName { get; set; }

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
