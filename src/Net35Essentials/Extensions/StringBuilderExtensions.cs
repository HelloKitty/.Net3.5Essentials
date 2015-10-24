using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//StringBuilder is in the Sytem.Text namespace so we'll put this ext in that namespace too.
namespace System.Text
{
	/// <summary>
	/// Provides extension methods for future functionality added to StringBuilder in 4.x versions.
	/// </summary>
	public static class StringBuilderExtensions
	{
		//Source of official Clear method http://www.dotnetframework.org/default.aspx/4@0/4@0/DEVDIV_TFS/Dev10/Releases/RTMRel/ndp/clr/src/BCL/System/Text/StringBuilder@cs/1305376/StringBuilder@cs	
		public static StringBuilder Clear(this StringBuilder sb)
		{
			sb.Length = 0;
			return sb;
		}
	}
}
