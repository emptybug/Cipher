
using System.Text;

namespace Cipher.Algorithm
{
    static class Caesar
    {
        static public string Encrypt(string input, int key)
        {
            StringBuilder sb = new StringBuilder();
            for(int i = 0; i < input.Length; ++i)
            {
                if ('a' <= input[i] && input[i] <= 'z')
                {
                    sb.Append((char)((input[i] - 'a' + key + 26) % 26 + 'a'));
                }
                else if ('A' <= input[i] && input[i] <= 'Z')
                {
                    sb.Append((char)((input[i] - 'A' + key + 26) % 26 + 'A'));
                }
                else
                {
                    sb.Append(input[i]);
                }
            }
            return sb.ToString();
        }

        static public string Decrypt(string input, int key)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < input.Length; ++i)
            {
                if ('a' <= input[i] && input[i] <= 'z')
                {
                    sb.Append((char)((input[i] - 'a' - key + 26) % 26 + 'a'));
                }
                else if ('A' <= input[i] && input[i] <= 'Z')
                {
                    sb.Append((char)((input[i] - 'A' - key + 26) % 26 + 'A'));
                }
                else
                {
                    sb.Append(input[i]);
                }
            }
            return sb.ToString();
        }
    }
}
