using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;

namespace UtilitiesLibrary
{
    public static class Str
    {

        public enum _RandomChar
        {
            number,
            character
        }

        private static readonly char[] chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789".ToCharArray();


        public static bool CheckCommasNumberInput(string input)
        {
            var numbers = input.Split(',');
            var numberParsed = new List<decimal>();
            for (var i = 0; i < numbers.Length; i++)
            {
                numberParsed.Add(decimal.Parse(numbers[i]));
            }
            if (numberParsed.Count == numbers.Length)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string? GenerateRandomCharacter(_RandomChar charornumber, int length)
        {
            Random _random = new();
            switch (charornumber)
            {
                case _RandomChar.number:
                    // Generate a _random number up to 9 digits
                    var randomNumber = _random.Next(0, 1_000_000_000);
                    return randomNumber.ToString();
                case _RandomChar.character:
                    var randomString = new StringBuilder(length);
                    for (var i = 0; i < length; i++)
                    {
                        var randomChar = chars[_random.Next(chars.Length)];
                        randomString.Append(randomChar);
                    }
                    return randomString.ToString();
                default:
                    return null;
            }
        }

        public static List<decimal>? ExtractCommasNumberInputDecimal(string input)
        {
            var numbers = input.Split(',');
            var numberParsed = new List<decimal>();
            for (var i = 0; i < numbers.Length; i++)
            {
                numberParsed.Add(decimal.Parse(numbers[i]));
            }
            return numberParsed;
        }

        public static T[] GetArrayEnumsFromStringValues<T>(StringValues input) where T : struct
        {
            return input.Select(e => Enum.Parse<T>(e,true)).ToArray();
        }

        public static T? ExtractObjectFromStringValues<T>(StringValues input)
        {
            try
            {
                if (input.Count == 0 || string.IsNullOrEmpty(input.First())) return default;
                var type = typeof(T);
                var stringValue = input.First()!;
                var tswitch = Type.GetTypeCode(type);
                return tswitch switch
                {
                    TypeCode.Int32 => (T)(object)int.Parse(stringValue, NumberStyles.Any),
                    TypeCode.UInt32 => (T)(object)uint.Parse(stringValue, NumberStyles.Any),
                    TypeCode.Boolean => (T)(object)bool.Parse(stringValue),
                    TypeCode.String => (T)(object)stringValue,
                    _ => default
                };
            }
            catch (Exception)
            {
                return default;
            }
        }

    }
}
