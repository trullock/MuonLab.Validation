using System;
using NUnit.Framework;

namespace MuonLab.Validation.Tests
{
	public static class ShouldExtensions
	{
		public static void ShouldBeEmpty(this string aString)
		{
			Assert.IsEmpty(aString);
		}

		public static string ShouldBeEqualIgnoringCase(this string actual, string expected)
		{
			Assert.AreEqual(expected.ToLowerInvariant(), actual.ToLowerInvariant());
			return expected;
		}

		public static void ShouldBeFalse(this bool condition)
		{
			Assert.False(condition);
		}

		public static System.IComparable ShouldBeGreaterThan(this System.IComparable arg1, System.IComparable arg2)
		{
			Assert.AreEqual(1, arg1.CompareTo(arg2));
			return arg2;
		}

		public static System.IComparable ShouldBeLessThan(this System.IComparable arg1, System.IComparable arg2)
		{
			Assert.AreEqual(-1, arg1.CompareTo(arg2));
			return arg2;
		}

		public static void ShouldBeNull(this object anObject)
		{
			Assert.Null(anObject);
		}

		public static void ShouldBeOfType<T>(this object actual)
		{
			Assert.IsInstanceOf<T>(actual);
		}

		public static void ShouldBeOfType(this object actual, Type expected)
		{
			Assert.IsInstanceOf(expected, actual);
		}

		public static object ShouldBeTheSameAs(this object actual, object expected)
		{
			Assert.AreSame(expected, actual);
			return expected;
		}

		public static void ShouldBeTrue(this bool condition)
		{
			Assert.True(condition);
		}

		public static void ShouldContain(this string actual, string expected)
		{
			Assert.True(actual.Contains(expected));
		}

		public static void ShouldContainErrorMessage(this Exception exception, string expected)
		{
			Assert.True(exception.Message.Contains(expected));
		}

		public static void ShouldEndWith(this string actual, string expected)
		{
			Assert.True(actual.EndsWith(expected));
		}

		public static void ShouldEqual<T>(this T actual, T expected)
		{
			Assert.AreEqual(expected, actual);
		}

		public static void ShouldNotBeEmpty(this string aString)
		{
			Assert.IsNotEmpty(aString);
		}

		public static void ShouldNotBeNull(this object anObject)
		{
			Assert.NotNull(anObject);
		}

		public static void ShouldNotBeOfType(this object actual, Type expected)
		{
			Assert.IsNotInstanceOf(expected, actual);
		}

		public static void ShouldNotBeTheSameAs(this object actual, object expected)
		{
			Assert.AreNotSame(expected, actual);
		}

		public static void ShouldNotEqual<T>(this T actual, T expected)
		{
			Assert.AreNotEqual(expected, actual);
		}

		public static void ShouldStartWith(this string actual, string expected)
		{
			Assert.True(actual.StartsWith(expected));
		}

		public static void ShouldBeNullOrEmpty(this string actual)
		{
			Assert.True(string.IsNullOrEmpty(actual), "Expected null or empty, got: " + actual);
		}
	}
}