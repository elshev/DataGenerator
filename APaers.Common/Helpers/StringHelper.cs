using System;

namespace APaers.Common.Helpers
{
    public static class StringHelper
    {
        public static string ToUpperFirstChar(this string s)
        {
            return char.ToUpper(s[0]) + s.Substring(1);
        }

        public static string ToLowerFirstChar(this string s)
        {
            return char.ToLower(s[0]) + s.Substring(1);
        }

        public static string GetExactWordCount(this string text, int wordCount, int maxLength)
        {
            int count = 0;
            int index = 0;
            int length = maxLength < 1 ? text.Length : Math.Min(text.Length, maxLength);
            while (index < length)
            {
                // check if current char is part of a word
                while (index < length && !char.IsWhiteSpace(text[index]) && !char.IsPunctuation(text[index]))
                    index++;

                count++;
                if (count >= wordCount) break;

                // skip whitespace until next word
                while (index < length && (char.IsWhiteSpace(text[index]) || char.IsPunctuation(text[index])))
                    index++;
            }
            return text.Substring(0, index);
        }

        public static string GetExactWordCount(this string text, int wordCount)
        {
            return GetExactWordCount(text, wordCount, 0);
        }
    }
}