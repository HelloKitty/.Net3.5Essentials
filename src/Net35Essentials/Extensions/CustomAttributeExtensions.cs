using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;


public static class CustomAttributeExtensions
{
	public static IEnumerable<TAttributeType> GetCustomAttributes<TAttributeType>(this Type t, bool inherit)
		where TAttributeType : Attribute
	{
		if (t == null)
			throw new ArgumentNullException("t", "Type: t as parameter in extension method must not be null.");

		return t.GetCustomAttributes(typeof(TAttributeType), inherit) as IEnumerable<TAttributeType>;
	}

	public static TAttributeType GetCustomAttribute<TAttributeType>(this Type t, bool inherit)
		where TAttributeType : Attribute
	{
		if (t == null)
			throw new ArgumentNullException("t", "Type: t as parameter in extension method must not be null.");

		var attriList = t.GetCustomAttributes(typeof(TAttributeType), inherit) as IEnumerable<TAttributeType>;

		if (attriList.Count() > 1)
			throw new AmbiguousMatchException("More than one attribute of Type: " + typeof(TAttributeType).ToString() + " found on Type: " + t.ToString() + ".");

		return attriList.FirstOrDefault();
	}
}

