using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Net35Essentials.Tests
{
	[Serializable] //won't be inherited
	[AttributeUsage(AttributeTargets.Class)] //will be inherited
	public class TestClass : Attribute //only an attribute so we can use attributeusgae for testing
	{

	}

	public class TestClassInherited : TestClass
	{

	}

	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public class MultipleUseTestAttribute : Attribute
	{

	}

	[MultipleUseTestAttribute]
	[MultipleUseTestAttribute]
	public class MultipleAttributeTestClass
	{

	}


	[TestFixture]
	public static class CustomAttributeExtensionsTests
	{

		//Tests for GetCustomAttributes returning IEnumerabe<Attribute>
		#region  GetCustomAttributes
		[Test]
		public static void Test_Find_Attributes_No_Inheritance()
		{
			//arrange
			var attriList = typeof(TestClass).GetCustomAttributes<SerializableAttribute>(false);

			//assert
			Assert.NotNull(attriList);
			Assert.AreEqual(attriList.Count(), 1);
		}

		[Test]
		public static void Test_Find_Attributes_With_Inheritance()
		{
			//arrange
			var attriList = typeof(TestClassInherited).GetCustomAttributes<AttributeUsageAttribute>(true);

			//assert
			Assert.NotNull(attriList);
			Assert.AreEqual(attriList.Count(), 1);
		}

		[Test]
		public static void Test_Find_Attributes_Without_Inheritance_Unexpecting()
		{
			//arrange
			var attriList = typeof(TestClassInherited).GetCustomAttributes<AttributeUsageAttribute>(false);

			//assert
			Assert.NotNull(attriList);
			Assert.AreEqual(attriList.Count(), 0); //should be an empty list.
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public static void Test_Find_Attributes_On_Null_Type()
		{		
			//arrange
			Type t = null;
			var attriList = t.GetCustomAttributes<AttributeUsageAttribute>(false);

			//assert
			Assert.Fail("Expected to fail when a null Type has an extension method invoked.");
		}
		#endregion

		//********************************************************************************************

		//Tests for GetCustomAttribute<T> returning T
		[Test]
		public static void Test_Find_Attribute_No_Inheritance()
		{
			//arrange
			var attri = typeof(TestClass).GetCustomAttribute<SerializableAttribute>(false);

			//assert
			Assert.NotNull(attri);
		}

		[Test]
		public static void Test_Find_Attribute_With_Inheritance()
		{
			//arrange
			var attri = typeof(TestClassInherited).GetCustomAttribute<AttributeUsageAttribute>(true);

			//assert
			Assert.NotNull(attri);
		}

		[Test]
		public static void Test_Find_Attribute_Without_Inheritance_Unexpecting()
		{
			//arrange
			var attri= typeof(TestClassInherited).GetCustomAttribute<AttributeUsageAttribute>(false);

			//assert
			Assert.IsNull(attri);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public static void Test_Find_Attribute_On_Null_Type()
		{
			//arrange
			Type t = null;
			var attriList = t.GetCustomAttribute<AttributeUsageAttribute>(false);

			//assert
			Assert.Fail("Expected to fail when a null Type has an extension method invoked.");
		}

		[Test]
		[ExpectedException(typeof(AmbiguousMatchException))]
		public static void Test_Find_Attribute_Multiple_Attributes()
		{
			//arrange
			var attriList = typeof(MultipleAttributeTestClass).GetCustomAttribute<MultipleUseTestAttribute>(false);

			//assert
			Assert.Fail("Expected to fail when a null Type has an extension method invoked.");
		}
	}
}
