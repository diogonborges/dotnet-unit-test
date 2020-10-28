using System;

namespace Palindrome
{
    public class StringHelper
    {
        public bool IsPalindrome(string input)
        {
            throw new NotImplementedException();
        }
    }

    // ONE SOLUTION
    //if (input == null) { throw new ArgumentNullException(); }

    //for (int i = 0, j = input.Length - 1; j >= input.Length / 2; i++, j--)
    //{
    //    if (input[i] != input[j])
    //    {
    //        return false;
    //    }
    //}
    //return true;

    // OTHER SOLUTION
    //var arr = input.ToCharArray();
    //Array.Reverse(arr);
    //return new string (arr) == input;
}
