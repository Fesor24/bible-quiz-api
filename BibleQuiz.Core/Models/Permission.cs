using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibleQuiz.Core
{
	public enum Permission
	{
		/// <summary>
		/// Access is restricted
		/// </summary>
		Denied = 0,

		/// <summary>
		/// Access is granted
		/// </summary>
		Granted = 1
	}
}
