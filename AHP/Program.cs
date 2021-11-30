using System;
using System.Collections.Generic;
using System.Linq;

namespace AHP
{
    class Program
    {
        static void Main(string[] args)
        {
            var example = new TestValues();
            var alts = example.GetAlternativesRated();
            double[] critPriors = example.GetCrits();
            double[,] altsCritPriors = example.GetAlternativesRated();

            double[,] critWeights = BuildWeightMatrixFromPriorities(critPriors);
            List<double[,]> itemsWeightsForEachCrit = new List<double[,]>();
            for (int i = 0; i < critPriors.Length; i++)
            {
                itemsWeightsForEachCrit.Add(BuildWeightMatrixFromPriorities(Additonal.GetRow(altsCritPriors, i)));
            }
            int critsAmount = critPriors.Length;
            int itemsAmount = Additonal.GetRow(altsCritPriors, 0).Length;

            double[] critFreeTerms = BuildFreeTermsMatrix(critWeights);
            double[,] itemsFreeTermsForEachCrit = new double[itemsAmount, critsAmount];
            for (int i = 0; i < critsAmount; i++)
            {
                var curCritAllItemsFreeTerms = BuildFreeTermsMatrix(itemsWeightsForEachCrit[i]);
                for (int j = 0; j < itemsAmount; j++)
                {
                    itemsFreeTermsForEachCrit[j, i] = curCritAllItemsFreeTerms[j];
                }
            }
            double[] results = new double[itemsAmount];
            for (int i = 0; i < itemsAmount; i++)
            {
                results[i] = Additonal.MatrixMultiply(Additonal.GetRow(itemsFreeTermsForEachCrit, i), critFreeTerms);
            }
            var bestChoice = example.Alternatives[Array.IndexOf(results, results.Max())];
            Console.WriteLine($"{bestChoice.Name}");
            ////foreach (var a in itemsWeightsForEachCrit)
            ////{
            ////    Additonal.PrintMatrix(a);
            ////    Console.WriteLine();
            ////}
            //for (int i = 0; i < 10; i++)
            //{
            //    for (int j = 0; j < 9; j++)
            //    {
            //        Console.Write($"{altsCritPriors[j, i]} ");
            //    }
            //    Console.WriteLine();
            //}
            return;
        }
        static double[,] BuildWeightMatrixFromPriorities(double[] items)
        {
            int n = items.Length;
            double[,] weightMatrix = new double[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (j == i)
                    {
                        weightMatrix[i, j] = 1;
                    } else
                    {
                        var priorirtyDiff = items[i] - items[j];
                        if (priorirtyDiff <= 0)
                            weightMatrix[i, j] = -priorirtyDiff * 2 + 1;
                        else
                            weightMatrix[i, j] = 1 / (priorirtyDiff * 2 + 1f);
                    }
                }
            }
            return weightMatrix;
        }
        static double[] BuildFreeTermsMatrixWithNorm(double[,] wMatrix)
        {
            int n = wMatrix.GetUpperBound(0) + 1;
            double[,] normalizedMatrix = Additonal.NormalizeMatrix(wMatrix);
            double[] freeTerms = new double[n];
            for (int i = 0; i < n; i++)
            {
                freeTerms[i] = Additonal.GetRow(normalizedMatrix, i).Sum() / n;
            }
            double[] multiplyRes = new double[n];
            for (int i = 0; i < n; i++)
            {
                multiplyRes[i] = Additonal.MatrixMultiply(Additonal.GetRow(wMatrix, i), freeTerms);
            }
            double[] RI = { 0, 0, 0.58, 0.9, 1.12, 1.24, 1.32, 1.41, 1.45, 1.49 };
            double CR = ((multiplyRes.Sum() - n) / (n - 1)) / RI[n - 1];
            if (CR > 0.1)
                throw new Exception("CR greater than 0.1");

            return freeTerms;
        }
        public static double[] BuildFreeTermsMatrix(double[,] wMatrix)
        {
            int n = wMatrix.GetUpperBound(0) + 1;
            double[] mults = new double[n];
            for (int i = 0; i < n; i++)
            {
                double rowProduct = 1;
                for (int j = 0; j < n; j++)
                {
                    rowProduct *= wMatrix[i, j];
                }
                mults[i] = Math.Pow(rowProduct, 1f / n);
            }
            double powersSum = mults.Sum();
            double[] freeTerms = new double[n];
            for (int i = 0; i < n; i++)
            {
                freeTerms[i] = mults[i] / powersSum;
            }
            double[] multiplyRes = new double[n];
            for (int i = 0; i < n; i++)
            {
                multiplyRes[i] = Additonal.MatrixMultiply(Additonal.GetRow(wMatrix, i), freeTerms);
            }
            double[] RI = { 0, 0, 0.58, 0.9, 1.12, 1.24, 1.32, 1.41, 1.45, 1.49 };
            double CR = ((multiplyRes.Sum() - n) / (n - 1)) / RI[n - 1];
            if (CR > 0.1)
                throw new Exception("CR greater than 0.1");

            return freeTerms;
        }

    }
}
