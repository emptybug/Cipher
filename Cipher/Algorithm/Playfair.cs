
using System.Text;
using System.Windows;

namespace Cipher.Algorithm
{
    public static class Playfair
    {
        private static char[,] _key = new char[5, 5];     //  经过处理的5×5矩阵
        private static Point[] _location = new Point[26]; //  26个字母在key中的位置
        private static string _group;   //  分组后的字符串
        private static char _ch = 'Q';    //  无效字母，如Q， K， X

        public static string Encrypt(string input)
        {
            StringBuilder sb = new StringBuilder();
            string str = group(input);
            for(int i = 0; i < str.Length; i += 2)
            {
                int r1 = (int)(_location[str[i] - 'A'].X);
                int r2 = (int)(_location[str[i + 1] - 'A'].X);
                int c1 = (int)(_location[str[i] - 'A'].Y);
                int c2 = (int)(_location[str[i + 1] - 'A'].Y);
                //  字母同行
                if (r1 == r2)
                {
                    sb.Append(_key[r1, (c1 + 1) % 5]).Append(_key[r1, (c2 + 1) % 5]);
                }
                //  字母同列
                else if (c1 == c2)
                {
                    sb.Append(_key[(r1 + 1) % 5, c1]).Append(_key[(r2 + 1) % 5, c1]);
                }
                else
                {
                    if (r1 > r2 && c1 > c2)
                    {
                        sb.Append(_key[r1, c2]).Append(_key[r2, c1]);
                    }
                    else if (r1 < r2 && c1 > c2)
                    {
                        sb.Append(_key[r2, c1]).Append(_key[r1, c2]);
                    }
                    else if (r1 > r2 && c1 < c2)
                    {
                        sb.Append(_key[r1, c2]).Append(_key[r2, c1]);
                    }
                    else
                    {
                        sb.Append(_key[r2, c1]).Append(_key[r1, c2]);
                    }
                }
            }
            return sb.ToString();
        }

        public static string Decrypt(string input)
        {
            StringBuilder sb = new StringBuilder();
            string str = (string)input.ToUpper();
            if (str.Length % 2 == 1 || str.Length == 0 || str.IndexOf(' ') != -1) return "";
            for (int i = 0; i < str.Length; i += 2)
            {
                int r1 = (int)(_location[str[i] - 'A'].X);
                int r2 = (int)(_location[str[i + 1] - 'A'].X);
                int c1 = (int)(_location[str[i] - 'A'].Y);
                int c2 = (int)(_location[str[i + 1] - 'A'].Y);
                //  字母同行
                if (r1 == r2)
                {
                    sb.Append(_key[r1, (c1 - 1 + 5) % 5]).Append(_key[r1, (c2 - 1 + 5) % 5]);
                }
                //  字母同列
                else if (c1 == c2)
                {
                    sb.Append(_key[(r1 - 1 + 5) % 5, c1]).Append(_key[(r2 - 1 + 5) % 5, c1]);
                }
                else
                {
                    if (r1 > r2 && c1 > c2)
                    {
                        sb.Append(_key[r1, c2]).Append(_key[r2, c1]);
                    }
                    else if (r1 < r2 && c1 > c2)
                    {
                        sb.Append(_key[r2, c1]).Append(_key[r1, c2]);
                    }
                    else if (r1 > r2 && c1 < c2)
                    {
                        sb.Append(_key[r1, c2]).Append(_key[r2, c1]);
                    }
                    else
                    {
                        sb.Append(_key[r2, c1]).Append(_key[r1, c2]);
                    }
                }
            }
            for(int i = 2; i < sb.Length; ++i)
            {
                if(sb[i].Equals(sb[i - 2]) && sb[i - 1].Equals(_ch))
                {
                    sb.Remove(i - 1, 1);
                }
            }
            if (sb[sb.Length - 1].Equals(_ch)) sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }

        public static char[, ] Key(string word)
        {
            string temp = word.ToUpper();
            StringBuilder sb = new StringBuilder();
            bool[] flag = new bool[26];
                for(int i = 0; i < temp.Length; ++i)
                {
                    //  该字母未出现过
                    if (flag[temp[i] - 'A'] == false)
                    {
                        sb.Append(temp[i]);
                    }
                    flag[temp[i] - 'A'] = true;
                }
                for(int i = 0; i < 26; ++i)
                {
                    if (i == 'J' - 'A')
                    {
                        continue;
                    }
                    if (flag[i] == false)
                    {
                        sb.Append((char)(i + 'A'));
                    }
                }
                for (int i = 0; i < 5; ++i)
                {
                    for(int j = 0; j < 5; ++j)
                    {
                        _key[i, j] = sb[i * 5 + j];
                        Point insert = new Point(i, j);
                        _location[_key[i, j] - 'A'] = insert;
                    }
                }
            return _key;
        }

        private static string group(string input)
        {
            StringBuilder sb = new StringBuilder();
            string temp = input.ToUpper();
            for(int i = 0; i < temp.Length; )
            {
                if (0 != i && sb.Length > 0 && temp[i] == sb[sb.Length - 1])
                {
                    sb.Append(_ch);
                }
                else if ('A' <= temp[i] && temp[i] <= 'Z')
                {
                    sb.Append(temp[i]);
                    ++i;
                }
                else
                {
                    ++i;
                }
            }
            if (sb.Length % 2 == 1)
            {
                sb.Append(_ch);
            }
            _group = sb.ToString();
            return sb.ToString();
        }
    }
}
