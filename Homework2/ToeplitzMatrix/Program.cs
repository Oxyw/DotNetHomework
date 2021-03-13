using System;
using System.Text.RegularExpressions;

namespace ToeplitzMatrix
{
    class Program
    {
        static void Main(string[] args)
        {
            int m = 0, n = 0;
            try
            {
                Console.Write("Please enter the number of rows: ");
                m = int.Parse(Console.ReadLine());
                Console.Write("Please enter the number of columns: ");
                n = int.Parse(Console.ReadLine());
            }
            catch (Exception e)
            {
                Console.WriteLine("解析错误" + e.Message);
            }
            int[,] a = new int[m, n];
            Console.WriteLine("Please enter the matrix: ");
            int i, j;
            string stemp;
            for(i=0; i<m; i++)
            {
                stemp = Console.ReadLine();
                string[] num = Regex.Split(stemp, " ");
                for (j=0; j<n; j++)
                {
                    a[i, j] = int.Parse(num[j]);
                }
            }
            Console.Write(isTplzMatrix(a));
        }

        public static bool isTplzMatrix(int[,] matrix)
        {
            bool flag = true;
            int m = matrix.GetLength(0), n = matrix.GetLength(1);
            int i, j, temp;
            //以[row, 0]为起始斜线扫描
            i = m - 2;
            while (flag == true && i > 0)
            {
                j = 0;
                temp = i;
                while (flag == true && j < n && temp < m)
                {
                    if (matrix[temp, j]!=matrix[i,0])
                        flag = false;
                    temp++; j++;
                }
                i--;
            }
            //以[0,col]为起始斜线扫描，i=0
            j = 0;
            while (flag == true && j < n)
            {
                i = 0;
                temp = j;
                while (flag == true && i < m && temp < n)
                {
                    if (matrix[i, temp] != matrix[0, j])
                        flag = false;
                    temp++; i++;
                }
                j++;
            }
            return flag;
        }
    }
}
