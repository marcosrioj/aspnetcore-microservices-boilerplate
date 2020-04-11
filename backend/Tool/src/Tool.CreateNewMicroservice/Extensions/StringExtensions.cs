using System.IO;

namespace Tool.CreateNewMicroservice.Extensions
{
    internal static class StringExtensions
    {
        public static string ToLowerFirstLetter(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;
            char[] letters = source.ToCharArray();
            letters[0] = char.ToLower(letters[0]);
            return new string(letters);
        }
    }
}
