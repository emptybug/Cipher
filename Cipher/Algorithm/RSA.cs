using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace Cipher.Algorithm
{
    class RSA
    {
        //  已保存的素数集
        protected int[] primes = { 307, 311, 313, 317, 331, 337, 347, 349, 353, 359, 367, 373, 379, 383, 389 };

        protected BigInteger rsa_e;
        protected BigInteger rsa_d;
        protected BigInteger rsa_n;

        protected BigInteger rsa_p;
        protected BigInteger rsa_q;

        #region Properties
        public string P
        {
            get
            {
                return rsa_p.ToString();
            }
        }
        public string Q
        {
            get
            {
                return rsa_q.ToString();
            }
        }

        public string E
        {
            get
            {
                return rsa_e.ToString();
            }
        }

        public string D
        {
            get
            {
                return rsa_d.ToString();
            }
        }

        public string N
        {
            get
            {
                return rsa_n.ToString();
            }
        }
        #endregion

        public RSA()
        {
            BigInteger p, q;
            p = getRandomPrime();
            q = getRandomPrime();
            while (p == q)
            {
                //  确保p与q不相等
                q = getRandomPrime();
            }
            BigInteger n = p * q;
            BigInteger fi_n = (p - 1) * (q - 1);
            BigInteger e = getRandomPrime();
            while (GCD(fi_n, e) != 1)
            {
                e = getRandomPrime();
            }
            BigInteger d = getInverseElement(e, fi_n);

            rsa_e = e;
            rsa_d = d;
            rsa_n = n;
            rsa_p = p;
            rsa_q = q;
        }

        public RSA(BigInteger p, BigInteger q, BigInteger e)
        {
            rsa_p = p;
            rsa_q = q;
            rsa_e = e;
            BigInteger n = p * q;
            BigInteger fi_n = (p - 1) * (q - 1);
            if (GCD(fi_n, e) != 1) return;
            BigInteger d = getInverseElement(e, fi_n);

            rsa_d = d;
            rsa_n = n;
        }

        public BigInteger[] Encrypt(string input)
        {
            List<BigInteger> res = new List<BigInteger>();
            char[] c = input.ToArray();
            for (int i = 0; i < c.Length; ++i)
            {
                res.Add(EncryptSingle(c[i], rsa_e));
            }
            return res.ToArray();
        }

        public char[] Decrypt(BigInteger[] input)
        {
            List<char> res = new List<char>();
            for (int i = 0; i < input.Length; ++i)
            {
                int ch = Int32.Parse(EncryptSingle(input[i], rsa_d).ToString());
                res.Add((char)ch);
            }
            return res.ToArray();
        }

        /// <summary>
        /// 对单个字符进行幂运算加密
        /// </summary>
        /// <param name="input"></param>
        /// <param name="m"></param>
        /// <returns></returns>
        protected BigInteger EncryptSingle(BigInteger input, BigInteger m)
        {
            BigInteger res = 1;
            for (int i = 0; i < m; ++i)
            {
                res = (res * input) % rsa_n;
            }
            return res;
        }

        protected BigInteger getRandomPrime()
        {
            Random rd = new Random(Rd.GetRandomSeed());
            BigInteger res = new BigInteger(primes[rd.Next(0, primes.Length)]);
            return res;
        }

        protected BigInteger GCD(BigInteger a, BigInteger b)
        {
            if (b == BigInteger.Zero) return a;
            return GCD(b, a % b);
        }

        /// <summary>
        /// 求a关于m的乘法逆元
        /// </summary>
        /// <param name="a">原数</param>
        /// <param name="m">被MOD的数</param>
        /// <returns>逆元</returns>
        protected BigInteger getInverseElement(BigInteger a, BigInteger m)
        {
            BigInteger x = 0, y = 0;
            BigInteger gcd = E_GCD(a, m, ref x, ref y);
            if (1 % gcd != 0) return -1;
            x *= 1 / gcd;
            m = BigInteger.Abs(m);
            BigInteger res = x % m;
            if (res <= 0) res += m;
            return res;
        }

        /// <summary>
        /// 拓展欧几里德算法
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>GCD（a， b）</returns>
        protected BigInteger E_GCD(BigInteger a, BigInteger b, ref BigInteger x, ref BigInteger y)
        {
            if (0 == b)
            {
                x = 1;
                y = 0;
                return a;
            }
            BigInteger res = E_GCD(b, a % b, ref x, ref y);
            BigInteger temp = x;
            x = y;
            y = temp - a / b * y;
            return res;
        }
    }
}
