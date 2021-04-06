using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace RPS
{
    class Program
    {
        private static byte[] GenerateKey(int size)
        {
            using (var generator = RandomNumberGenerator.Create())
            {
                var key = new byte[size];
                generator.GetBytes(key);
                return key;
            }
        }
        static void Main(string[] args)
        {
            int len = args.Length;
            if (len % 2 == 0 || len < 2)
            {
                Console.WriteLine("Number of arguments must be odd and more than one.");
                return;
            }
            foreach (string arg in args)
            {
                int count = 0;
                for (int i = 0; i < len; i++)
                {
                    if (args[i] == arg)
                    {
                        count++;
                    }
                }
                if (count > 1)
                {
                    Console.WriteLine("Arguments must be unique.");
                    return;
                }
            }
            int min = 1, max = len;
            int compTurn = RandomNumberGenerator.GetInt32(min, max);
            
            byte[] key = GenerateKey(16);
            using (var hmacsha256 = new HMACSHA256(key))
            {
                hmacsha256.ComputeHash(Encoding.Default.GetBytes(compTurn.ToString()));
                Console.WriteLine("HMAC: {0}", BitConverter.ToString(hmacsha256.Hash).Replace("-", string.Empty)); 
            }
            int userTurn;
            while (true)
            {
                Console.WriteLine("Available moves:");
                for (int i = 0; i < len; i++)
                {
                    Console.WriteLine("{0} - {1}", i + 1, args[i]);
                }
                Console.WriteLine("0 - exit");
                Console.Write("Enter your move: ");
                if (!Int32.TryParse(Console.ReadLine(), out userTurn))
                {
                    Console.WriteLine("Wrong input. Enter only one digit. For example: {0}", len - 1);
                    continue;
                }
                else
                {
                    if (userTurn > len || userTurn < 0)
                    {
                        Console.WriteLine("Wrong input. Enter a digit that more than -1 and less than {0}. For example: {1}", len + 1, len - 1);
                        continue;
                    }
                    break;
                }
            }
            if (userTurn == 0)
            {
                return;
            }
            Console.WriteLine("Your move: {0}\nComputer move: {1}", args[userTurn - 1], args[compTurn - 1]);
            int half = len / 2;
            if (userTurn > compTurn && userTurn - compTurn <= half || userTurn < compTurn && compTurn - userTurn >= half)
            {
                Console.WriteLine("You Win!");
            }      
            else if (userTurn == compTurn)
            {
                Console.WriteLine("Draw!");
            }
            else
            {
                Console.WriteLine("You Lose!");
            }
            Console.WriteLine("HMAC key: {0}", BitConverter.ToString(key).Replace("-", string.Empty));
        }
    }
}
