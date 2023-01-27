using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace exam2022
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Complement9(4690));
            int[] arr = GenerateArray();
            //WriteArray(arr);
            //EX2(arr, 6);
            //WriteArray(arr);
            //EX3();

            //int[,] mat = GenerateMatrix();
            //WriteMatrix(mat);
            //Console.WriteLine(SumaPrimeSubDiagonala(mat));
            Console.WriteLine(EX5(16));
            //EX6(100);
            Console.WriteLine(CifraPara(126438535));
        }

        static int Complement9(int input)
        {
            int aux = input; 
            int digits = 0;         
            while(aux > 0)
            {
                digits++;
                aux /= 10;
            }
            int mask = 0;
            for(int i = 0; i < digits; i++)
            {
                mask += (int)Math.Pow(10, i) * 9; // 9 + 90 + 900 + 9000 +... 
            }
            return mask - input;
        }
        static int Complement10(int input)
        {
            return Complement9(input) + 1;
        }        
        static int[] GenerateArray()
        {
            Random rnd = new Random();
            int[] array = new int[10];
            for(int i = 0; i < array.Length; i++)
            {
                array[i] = rnd.Next(0, 10);
            }
            return array;
        }
        static void WriteArray(int[] v)
        {
            Console.WriteLine("~~");
            for (int i = 0; i < v.Length; i++)
            {
                Console.Write(v[i] + " ");
         
            }
            Console.WriteLine();
            Console.WriteLine("~~");
        }
        static void WriteMatrix(int[,] v)
        {
            Console.WriteLine("~~");
            for (int i = 0; i < v.GetLength(1); i++)
            {
                for (int j = 0; j < v.GetLength(0); j++)
                {
                    Console.Write(v[i,j] + " ");

                }
                Console.WriteLine();

            }
            Console.WriteLine();
            Console.WriteLine("~~");
        }
        /// <summary>
        /// Scrieți o funcție care primește ca argument un vector de numere întregi și o valoare t.
        //          Funcția determină și afișează indicii a două elemente din vector a căror sumă este egală cu t.
        //          Se poate presupune că pentru fiecare input problema are o singură soluție iar un element nu
        //          poate fi folosit de două ori în sumă(cei doi indici sunt diferiți). Pentru punctaj maxim creați o
        //          soluție eficientă.
        //          [3,5,3,2,32,4,23,342,234,5,3,2]
        /// </summary>
        /// <param name="v"></param>
        /// <param name="t"></param>
        static void EX2(int[] v, int t)
        {
            // solutie banala cu complexitate n^2
            //for (int i = 0; i < v.Length; i++)
            //{
            //    for (int j = i + 1; j < v.Length; j++)
            //    {
            //        if (v[i] + v[j] == t)
            //        {
            //            Console.WriteLine($"{i}, {j}");
            //            return;
            //        }
            //    }
            //}

            Array.Sort(v);  // ar trebui implementat un algoritm de sortare

            int left = 0;
            int right = v.Length - 1;
            while (left < right)   // solutie liniara pentru un vector sortat
            {
                if (v[left] + v[right] > t)
                {
                    right--;
                    continue;
                }
                if (v[left] + v[right] < t)
                {
                    left++;
                }
                if (v[left] + v[right] == t)
                {
                    Console.WriteLine($"{left}, {right}");
                    break;
                }
            }

        }
        static void EX3()
        {
            /*
             * Scrieți o funcție care citește de la tastatura o listă de numere până se introduce valoarea
                zero. Funcția va determina și va afișa cele mai mari trei numere din listă. Valoarea zero care
                termină lista nu se consideră că face parte din lista de numere care trebuie prelucrate. Lista
                conține cel puțin 3 numere. În rezolvarea acestei probleme nu veți folosi tablouri.*/
            int nr1 = Int32.MinValue;
            int nr2 = Int32.MinValue;
            int nr3 = Int32.MinValue;
            string s = Console.ReadLine();
            while (s != "0")
            {
                int input = int.Parse(s);
                if(input >= nr1)
                {                                      
                    nr3 = nr2;
                    nr2 = nr1;
                    nr1 = input;                   
                }
                else if(input >= nr2)
                {
                    nr3 = nr2;
                    nr2 = input;                   
                }
                else if(input >= nr3)
                {
                    nr3 = input;                   
                }
                s = Console.ReadLine();
            }
            Console.WriteLine($"{nr1}, {nr2}, {nr3}");
        }
        /*Scrieți o funcție care citește de la tastatura un număr natural n și o matrice pătratică cu n
            linii și n coloane formată din numere naturale mai mari sau egale decât 2, după care
            determină suma numerelor prime de sub diagonala secundară.
        */
        static int SumaPrimeSubDiagonala(int[,] m)
        {
            int sum = 0;
            int n = m.GetLength(0);
            for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < n; j++)
                {
                    if(i + j >= n)
                    {
                        if (IsPrim(m[i, j]))
                        {
                            sum += m[i, j];
                            Console.Write($"p:{m[i,j]} ");
                        }
                    }
                }
            }
            Console.WriteLine();
            return sum;
        }
        static int[,] GenerateMatrix()
        {
            Random rnd = new Random();
            int n = int.Parse(Console.ReadLine());
            int[,] m = new int[n, n];
            for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < n; j++)
                {
                    m[i, j] = rnd.Next(2, 100);
                }
            }
            return m;
        }
        static bool IsPrim(int input)
        {
            if(input % 2 == 0)
            {
                return false;
            }
            for(int i = 3; i < input/2; i+= 2)
            {
                if(input % i == 0)
                {
                    return false;
                }
            }
            return true;
        }
        /* Incape pisica intre ecuatorul curent si un ecuator imaginar cu 1m mai lung decat cel curent? 
         * ecuator = r1 * 2pi => r1 = ecuator / 2pi
         * ecuator + 1 = r2 * 2pi => r2 = ecuator + 1 / 2pi 
         * 
         * r2 - r1 = 1 / 2pi 
         * 
         * 
         */
        static bool EX5(double inaltime)
        {
            double result = 1 / (2 * Math.PI);
            result = result * 100; // result e in metri, trebuie convertit in cm
            if (inaltime < result)
            {
                return true;
            }
            else return false;            
        }
        //Scrieți o funcție care primește ca parametru un număr natural nenul n și afișează primele n
        //elemente ale șirului 1,1,1,2,2,1,1,1,2,2,2,3,3,3,...
        // 1
        // 11 22
        // 111 222 333
        // 1111 2222 3333 4444
        static void EX6(int n)
        {
            int contor = 0;
            int i = 1;
            while(contor < n)
            {
                for(int j = 1; j <= i; j++)
                {
                    for(int k = 1; k <= i; k++)
                    {
                        Console.Write(j + " ");
                        contor++;
                        if(contor == n)
                        {
                            return;
                        }
                    }
                }
                i++;
            }
        }
        static int CifraPara(int n)     // abcdefg           max(max(max(max(max(max(a,b), c) ,d) ,e) ,f) ,g)    a b c d e f g ii inlocuit cu -1 daca nu este par
        {
            if(n < 10)
            {
                if (n % 2 == 0)
                {
                    return n;
                }
                else return -1;
            }
            if ((n % 10) % 2 == 0)
            {
                return Math.Max(CifraPara(n / 10), n % 10);
            }
            else return CifraPara(n / 10);
        }
    }
}
