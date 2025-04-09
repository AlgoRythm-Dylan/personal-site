using System.Security.Cryptography;

namespace Web.Lib
{
    public class Crypto
    {
        private const string CHARSET = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        public static string RandomToken(int length = 32)
        {
            if (length <= 0)
                throw new ArgumentOutOfRangeException(nameof(length), "Length must be greater than 0.");

            char[] randomChars = new char[length];

            for (int i = 0; i < length; i++)
            {
                randomChars[i] = CHARSET[RandomNumberGenerator.GetInt32(0, CHARSET.Length)];
            }

            return new string(randomChars);
        }
    }
}
