namespace TechGen_fundamentals_hw_1
{
    internal class Program
    {
        // validation method
        static string Normalize(string s)
        {
            if (string.IsNullOrEmpty(s))
                return "0";

            foreach (char c in s)
            {
                if (c < '0' || c > '9')
                    return "0";
            }

            int i = 0;
            while (i < s.Length - 1 && s[i] == '0')
                i++;

            return s.Substring(i);
        }

        static string Add(string a, string b)
        {
            a = Normalize(a);
            b = Normalize(b);

            int i = a.Length - 1;
            int j = b.Length - 1;
            int carry = 0;

            string result = "";

            while (i >= 0 || j >= 0 || carry > 0)
            {
                int da = i >= 0 ? a[i] - '0' : 0;
                int db = j >= 0 ? b[j] - '0' : 0;

                int sum = da + db + carry;

                result = (sum % 10) + result;
                carry = sum / 10;

                i--;
                j--;
            }

            return result;
        }

        static string Subtract(string a, string b)
        {
            a = Normalize(a);
            b = Normalize(b);

            int i = a.Length - 1;
            int j = b.Length - 1;

            int borrow = 0;
            string result = "";

            while (i >= 0)
            {
                int da = (a[i] - '0') - borrow;
                int db = j >= 0 ? b[j] - '0' : 0;

                if (da < db)
                {
                    da += 10;
                    borrow = 1;
                }
                else
                {
                    borrow = 0;
                }

                result = (da - db) + result;

                i--;
                j--;
            }

            return Normalize(result);
        }

        static string Multiply(string a, string b)
        {
            a = Normalize(a);
            b = Normalize(b);

            string result = "0";

            int shift = 0;

            for (int i = b.Length - 1; i >= 0; i--)
            {
                int db = b[i] - '0';

                int carry = 0;
                string temp = "";

                for (int j = a.Length - 1; j >= 0; j--)
                {
                    int da = a[j] - '0';

                    int mul = da * db + carry;

                    temp = (mul % 10) + temp;
                    carry = mul / 10;
                }

                if (carry > 0)
                    temp = carry + temp;

                temp = temp + new string('0', shift);

                result = Add(result, temp);

                shift++;
            }

            return Normalize(result);
        }

        static void Main(string[] args)
        {
            int a = int.MaxValue;
            Console.WriteLine(unchecked(a + 1)); // -2147483648
            long b = long.MaxValue;
            Console.WriteLine(unchecked(b + 1)); // -9223372036854775808

            Console.WriteLine(Add("2147483647", "100")); // 2147483747
            Console.WriteLine(Subtract("10000", "1"));   // 9999
            Console.WriteLine(Multiply("123", "456"));   // 56088
        }
    }
}
