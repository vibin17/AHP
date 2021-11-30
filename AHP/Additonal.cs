using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AHP
{
    static class Additonal
    {
        public static double MatrixMultiply(double[] a, double[] b)
        {
            double result = 0;
            for (int i = 0; i < a.Length; i++)
            {
                result += a[i] * b[i];
            }
            return result;
        }
        public static double[] GetRow(double[,] matrix, int rowNumber)
        {
            return Enumerable.Range(0, matrix.GetLength(1))
                    .Select(x => matrix[rowNumber, x])
                    .ToArray();
        }
        public static double[] GetColumn(double[,] matrix, int columnNumber)
        {
            return Enumerable.Range(0, matrix.GetLength(0))
                    .Select(x => matrix[x, columnNumber])
                    .ToArray();
        }
        public static void PrintMatrix(double[,] matrix)
        {
            int n = matrix.GetUpperBound(0) + 1;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (j == i)
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.Write("M-");
                        Console.Write($"{string.Format("{0:0.##}", matrix[i, j])}");
                        Console.ResetColor();
                        Console.Write("\t");
                    }
                    else 
                        Console.Write($"{string.Format("{0:0.##}", matrix[i, j])} \t");

                }
                Console.WriteLine();
            }
        }
        public static double[,] NormalizeMatrix(double[,] wMatrix)
        {
            int n = wMatrix.GetUpperBound(0) + 1;
            double[,] normalizedMatrix = (double[,])wMatrix.Clone();
            double[] columnSums = new double[n];
            for (int j = 0; j < n; j++)
            {
                double columnSum = 0;
                for (int i = 0; i < n; i++)
                {
                    columnSum += wMatrix[i, j];
                }
                columnSums[j] = columnSum;
            }
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    normalizedMatrix[i, j] /= columnSums[j];
                }
            }
            return normalizedMatrix;
        }
    }
}
