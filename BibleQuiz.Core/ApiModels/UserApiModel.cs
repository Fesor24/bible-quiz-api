namespace BibleQuiz.Core
{
	/// <summary>
	/// User api model
	/// </summary>
	public class UserApiModel
	{
		/// <summary>
		/// The email to be returned
		/// </summary>
		public string Email { get; set; }

		/// <summary>
		/// Then token to be returned
		/// </summary>
		public string Token { get; set; }

		/// <summary>
		/// The permission of this user
		/// </summary>
		public Permission Permission { get; set; }
	}

	/// <summary>
	/// Api model to fetch all users api model
	/// </summary>
	public class FetchAllUsersApiModel
	{
		/// <summary>
		/// The email of this user
		/// </summary>
		public string Email { get; set; }

		/// <summary>
		/// The id of the user
		/// </summary>
		public string Id { get; set; }
	}
}
