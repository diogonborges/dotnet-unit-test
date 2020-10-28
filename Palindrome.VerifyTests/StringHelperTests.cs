using System;
using Xunit;

namespace Palindrome.Tests
{
    public class StringHelperTests
    {
        [Fact]
        public void StringHelper_Throws_ArgumentNullException()
        {
            var stringHelper = new StringHelper();
            Assert.Throws<ArgumentNullException>(() => stringHelper.IsPalindrome(null));
        }

        [Theory]
        [InlineData("a")]
        [InlineData("1")]
        [InlineData("11")]
        [InlineData("aa")]
        [InlineData("a11a")]
        [InlineData("aba")]
        [InlineData("abba")]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("  ")]
        public void StringHelper_returns_true(string input)
        {
            var stringHelper = new StringHelper();
            var result = stringHelper.IsPalindrome(input);
            Assert.True(result);
        }

        [Fact]
        public void StringHelper_returns_true_when_string_is_long()
        {
            var stringHelper = new StringHelper();
            var result = stringHelper.IsPalindrome("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
            Assert.True(result);
        }

        [Theory]
        [InlineData("ab")]
        [InlineData("abab")]
        [InlineData("abc")]
        [InlineData("abcabc")]
        [InlineData(" a")]
        [InlineData("a ")]
        public void StringHelper_returns_false(string input)
        {
            var stringHelper = new StringHelper();
            var result = stringHelper.IsPalindrome(input);
            Assert.False(result);
        }

        [Theory]
        [InlineData("aA")]
        public void StringHelper_returns_false_When_caseSensitive_differs(string input)
        {
            var stringHelper = new StringHelper();
            var result = stringHelper.IsPalindrome(input);
            Assert.False(result);
        }
    }
}
