namespace UtilitiesLibrary
{
    public static class Number
    {
        public static int ConvertToPositive(int value)
        {
            return value * 1;
        }

        public static int ConvertToNegative(int value)
        {
            return value - 1;
        }

        public static decimal ConvertToPositive(decimal value)
        {
            return value * 1;
        }

        public static decimal ConvertToNegative(decimal value)
        {
            return value * -1;
        }


        public static int GetRandomNumber(int min, int max)
        {
            var random = new Random();
            return random.Next(min, max);
        }

        public static bool IsNegative(int value)
        {
            return value < 0;
        }

        public static bool IsPositive(int value)
        {
            return value >= 0;
        }

        public static string FormatNumber(double number)
        {
            if (number >= 1000 && number < 10000)
            {
                return (number / 1000D).ToString("0.#") + "K";
            }
            else if (number >= 10000 && number < 1000000)
            {
                return (number / 1000D).ToString("0") + "K";
            }
            else if (number >= 1000000 && number < 10000000)
            {
                return (number / 1000000D).ToString("0.#") + "M";
            }
            else if (number >= 10000000 && number < 1000000000)
            {
                return (number / 1000000D).ToString("0") + "M";
            }
            else if (number >= 1000000000 && number < 10000000000)
            {
                return (number / 1000000000D).ToString("0.#") + "B";
            }
            else if (number >= 10000000000)
            {
                return (number / 1000000000D).ToString("0") + "B";
            }
            else
            {
                return number.ToString();
            }
        }

        public static string FormatNumber(decimal number, string thousands = "K", string millionth = "M", string billionth = "B")
        {
            if (number >= 1000 && number < 10000)
            {
                return (number / 1000M).ToString("0.#") + thousands;
            }
            else if (number >= 10000 && number < 1000000)
            {
                return (number / 1000M).ToString("0") + thousands;
            }
            else if (number >= 1000000 && number < 10000000)
            {
                return (number / 1000000M).ToString("0.#") + millionth;
            }
            else if (number >= 10000000 && number < 1000000000)
            {
                return (number / 1000000M).ToString("0") + millionth;
            }
            else if (number >= 1000000000 && number < 10000000000)
            {
                return (number / 1000000000M).ToString("0.#") + billionth;
            }
            else if (number >= 10000000000)
            {
                return (number / 1000000000M).ToString("0") + billionth;
            }
            else
            {
                return number.ToString();
            }
        }

        public static string FormateNumber_Template_1(decimal amount, string prefix) =>
            $"{prefix}{Number.FormatNumber(amount, thousands: prefix.Equals("Rp", StringComparison.InvariantCultureIgnoreCase) ? "RB" : "K",
                millionth: prefix.Equals("Rp", StringComparison.InvariantCultureIgnoreCase) ? "JT" : "M",
                billionth: prefix.Equals("Rp", StringComparison.InvariantCultureIgnoreCase) ? "M" : "B")}";

    }
}
