namespace TechGen_fundamentals_hw_1
{
    internal class Program
    {
        public static void toBinary(int value, ref string result)
        {
            while (value > 0)
            {
                result = (value % 2) + result;
                value /= 2;
            }
        }

        public static string floatToBinary(float value, bool pretty = false)
        {
            if (value == 0) { return "0"; }

            // sign bit
            int sign = value < 0 ? 1 : 0;
            value = Math.Abs(value);

            // take the integer part and convert it to binary
            int intPart = (int)value;
            string intBinary = "";
            toBinary(intPart, ref intBinary);

            // taking the fraction part
            // converting to binary with 23bit accuracy
            float fracPart = value - intPart;
            string fracBinary = "";
            while (fracBinary.Length < 24 && fracPart != 0)
            {
                fracPart *= 2;
                if (fracPart >= 1)
                {
                    fracBinary += "1";
                    fracPart -= 1;
                }
                else
                {
                    fracBinary += "0";
                }
            }

            // normalization
            // calculate and convert exponent
            // get the mantissa in parallel
            int exponent;
            string mantissa;
            if (intBinary != "")
            {
                // 1.00101.... case
                exponent = intBinary.Length - 1;
                mantissa = intBinary.Substring(1) + fracBinary;
            }
            else
            {
                // 0.00101.... case
                int firstOne = fracBinary.IndexOf('1');
                exponent = -(firstOne + 1);
                mantissa = fracBinary.Substring(firstOne + 1);
            }

            exponent += 127;
            string expBinary = "";
            toBinary(exponent, ref expBinary);

            mantissa = mantissa.PadRight(23, '0').Substring(0, 23);

            string result = sign + expBinary + mantissa;

            if (!pretty)
            {
                return result;
            }

            return $"{result[0]} | {result.Substring(1, 8)} | {result.Substring(9)}";
        }

        public static float binaryToFloat(string str)
        {
            str = str.Replace(" ", "").Replace("|", "");

            if (str.Length != 32)
            {
                throw new ArgumentException("Invalid length for float bit string");
            }

            int sign = str[0] - '0';

            // back convert the binary exponent to decimal
            int exponent = 0;
            for (int i = 1; i <= 8; i++)
            {
                exponent = exponent * 2 + (str[i] - '0');
            }
            exponent -= 127;

            // mantissa
            float mantissa = 1.0f;
            float fraction = 0.5f;

            for (int i = 9; i < 32; i++)
            {
                if (str[i] == '1')
                {
                    mantissa += fraction;
                }
                fraction /= 2;
            }

            float result = mantissa * (float)Math.Pow(2, exponent);

            if (sign == 1) result = -result;

            return result;
        }

        public static string doubleToBinary(double value, bool pretty = false)
        {
            if (value == 0) { return new string('0', 64); }

            int sign = value < 0 ? 1 : 0;
            value = Math.Abs(value);

            long intPart = (long)value;
            string intBinary = "";
            toBinary((int)intPart, ref intBinary);

            double fracPart = value - intPart;
            string fracBinary = "";

            while (fracBinary.Length < 54 && fracPart != 0)
            {
                fracPart *= 2;
                if (fracPart >= 1)
                {
                    fracBinary += "1";
                    fracPart -= 1;
                }
                else
                {
                    fracBinary += "0";
                }
            }

            int exponent;
            string mantissa;

            if (intBinary != "")
            {
                exponent = intBinary.Length - 1;
                mantissa = intBinary.Substring(1) + fracBinary;
            }
            else
            {
                int firstOne = fracBinary.IndexOf('1');
                exponent = -(firstOne + 1);
                mantissa = fracBinary.Substring(firstOne + 1);
            }

            exponent += 1023;

            string expBinary = "";
            toBinary(exponent, ref expBinary);

            mantissa = mantissa.PadRight(52, '0').Substring(0, 52);

            string result = sign + expBinary + mantissa;

            if (!pretty)
                return result;

            return $"{result[0]} | {result.Substring(1, 11)} | {result.Substring(12)}";
        }

        public static double binaryToDouble(string str)
        {
            str = str.Replace(" ", "").Replace("|", "");

            if (str.Length != 64)
            {
                throw new ArgumentException("Invalid length for double bit string");
            }

            int sign = str[0] - '0';

            // exponent (11 bits)
            int exponent = 0;
            for (int i = 1; i <= 11; i++)
            {
                exponent = exponent * 2 + (str[i] - '0');
            }
            exponent -= 1023;

            // mantissa (52 bits)
            double mantissa = 1.0;
            double fraction = 0.5;

            for (int i = 12; i < 64; i++)
            {
                if (str[i] == '1')
                {
                    mantissa += fraction;
                }
                fraction /= 2;
            }

            double result = mantissa * Math.Pow(2, exponent);

            if (sign == 1)
                result = -result;

            return result;
        }

        static void Main(string[] args)
        {
            // float test case
            float x = 12.375f;

            string bin = floatToBinary(x, true);
            Console.WriteLine(bin);
            float restored = binaryToFloat(bin);
            Console.WriteLine(restored);


            // double test case
            double y = -12.375;

            string binDouble = doubleToBinary(y, true);
            Console.WriteLine(binDouble);
            double restoredDouble = binaryToDouble(binDouble);
            Console.WriteLine(restoredDouble);
        }
    }
}
