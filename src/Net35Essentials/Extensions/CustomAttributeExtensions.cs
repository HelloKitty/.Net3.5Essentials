using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

//Use System anmesapce so it can be available when Type is being used and also in sub-namespace System.Reflection.
namespace System
{
	public static class CustomAttributeExtensions
	{
		//We switched to MemberInfo because Type and the base-type of reflection objects pulled off of types inherit
		//from MemberInfo so this extension method extends to all of those.
		public static IEnumerable<TAttributeType> GetCustomAttributes<TAttributeType>(this MemberInfo mi, bool inherit)
		where TAttributeType : Attribute
		{
			if (mi == null)
				throw new ArgumentNullException("mi", "MemberInfo: mi as parameter in extension method must not be null.");

			return mi.GetCustomAttributes(typeof(TAttributeType), inherit) as IEnumerable<TAttributeType>;
		}

		public static TAttributeType GetCustomAttribute<TAttributeType>(this MemberInfo mi, bool inherit)
			where TAttributeType : Attribute
		{
			if (mi == null)
				throw new ArgumentNullException("mi", "MemberInfo: mi as parameter in extension method must not be null.");

			var attriList = mi.GetCustomAttributes(typeof(TAttributeType), inherit) as IEnumerable<TAttributeType>;

			if (attriList.Count() > 1)
				throw new AmbiguousMatchException("More than one attribute of Type: " + typeof(TAttributeType).ToString() + " found on MemberInfo: " + mi.ToString() + ".");

			return attriList.FirstOrDefault();
		}
	}
}


