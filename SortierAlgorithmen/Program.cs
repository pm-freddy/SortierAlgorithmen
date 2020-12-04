using System;
using System.Collections.Generic;
using System.Linq;

namespace SortierAlgorithmen
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random(); // befüllen mit zufälligen werten
            //byte[] arrayDataA = new byte[10000000]; // erstellen des Ausgangsarrays
            //int[] arrayDataB = new int[50000];
            //arrayRandomFill(arrayDataB);
            //rnd.NextBytes(arrayDataA);
            byte[] arrayDataC = new byte[100];
            rnd.NextBytes(arrayDataC);

            // DateTime start = DateTime.Now;
            // MergeSort(arrayDataA);
            // Console.WriteLine("Array sortiert nach {0} millisekunden", (DateTime.Now - start).TotalMilliseconds);

            #region MergeSort
            for (int counter = 0; counter < arrayDataC.Length; counter++)
            {
                Console.Write(arrayDataC[counter] + " ");
            }

            Console.WriteLine();
            Console.WriteLine();

            DateTime start2 = DateTime.Now;
            byte[] buffer = MergeSortWithoutRecursion(arrayDataC);
            Console.Write("Array sortiert nach {0} millisekunden", (DateTime.Now - start2).TotalMilliseconds);

            Console.WriteLine();
            Console.WriteLine();

            for (int counter = 0; counter < buffer.Length; counter++)
            {
                Console.Write(buffer[counter] + " ");
            }
            Console.WriteLine();
            #endregion
        }
        

        static bool GetNextPow(int ArrayLength, out int NewArrayLength)
        {
            NewArrayLength = 1;

            while (NewArrayLength < ArrayLength) NewArrayLength <<= 1;

            return NewArrayLength != ArrayLength ? true : false;
        }

        static byte[] MergeSortWithoutRecursion(byte[] ArrayToSort)
        {
            int methodCount = 1;
            int Schrittlänge;
            int linkerCounter;
            int rechterCounter;
            int gesamtCounter;
            int OriginalLength = ArrayToSort.Length;                            //speichern der Original-Länge des übergebenen Arrays

            byte[] MergedArray;

            if (GetNextPow(OriginalLength, out int NewLength))
            {
                Array.Resize(ref ArrayToSort, NewLength);                       //die Länge des übergebenen Arrays auf neue Länge setzen
                //da das alte Array mit der neuen Länge hinten mit 0 aufgefüllt wird...
                for (int counter = OriginalLength; counter < ArrayToSort.Length; counter++)
                {
                    ArrayToSort[counter] = byte.MaxValue;                       //...alle hinzugefügten Nullen mit 255 ersetzen
                }
            }


            MergedArray = new byte[ArrayToSort.Length];
            ArrayToSort.CopyTo(MergedArray, 0);                                 //Kopie des Arrays in ein Zwischenlager



            while (methodCount < ArrayToSort.Length)
            {
                gesamtCounter = 0;
                Schrittlänge = 2 * methodCount;
                for (int ArrayPosition = 0; ArrayPosition < ArrayToSort.Length; ArrayPosition += Schrittlänge)
                {
                    linkerCounter = ArrayPosition;
                    rechterCounter = ArrayPosition + Schrittlänge / 2;

                    while (linkerCounter != (ArrayPosition + Schrittlänge / 2)
                        && rechterCounter != (ArrayPosition + Schrittlänge))
                    {
                        if (ArrayToSort[linkerCounter] < ArrayToSort[rechterCounter])
                        {
                            MergedArray[gesamtCounter++] = ArrayToSort[linkerCounter++];
                        }
                        else
                        {
                            MergedArray[gesamtCounter++] = ArrayToSort[rechterCounter++];
                        }
                    }

                    while (linkerCounter < ArrayPosition + Schrittlänge / 2)
                    {
                        MergedArray[gesamtCounter++] = ArrayToSort[linkerCounter++];
                    }

                    while (rechterCounter < ArrayPosition + Schrittlänge)
                    {
                        MergedArray[gesamtCounter++] = ArrayToSort[rechterCounter++];
                    }
                }

                methodCount <<= 1;
                MergedArray.CopyTo(ArrayToSort, 0);
            }

            Array.Resize(ref ArrayToSort, OriginalLength);
            return ArrayToSort;
        }



        static byte[] MergeSort(byte[] ArrayToSort)
        {
            if (ArrayToSort.Length == 1)
            {
                return ArrayToSort;
            }

            byte[] ArrayMerge = new byte[ArrayToSort.Length];
            byte[] ArrayLeft = MergeSort(ArrayToSort.Take(ArrayToSort.Length / 2).ToArray<byte>());
            byte[] ArrayRight = MergeSort(ArrayToSort.Skip(ArrayToSort.Length / 2).ToArray<byte>());
            int CounterArray1 = 0;
            int CounterArray2 = 0;
            int CounterMerge = 0;
            while (CounterArray1 != ArrayLeft.Length && CounterArray2 != ArrayRight.Length)
            {
                if (ArrayLeft[CounterArray1] < ArrayRight[CounterArray2])
                {
                    ArrayMerge[CounterMerge] = ArrayLeft[CounterArray1];
                    CounterMerge++;
                    CounterArray1++;
                }
                else
                {
                    ArrayMerge[CounterMerge] = ArrayRight[CounterArray2];
                    CounterMerge++;
                    CounterArray2++;
                }
            }

            for (; CounterArray1 < ArrayLeft.Length; CounterArray1++)
            {
                ArrayMerge[CounterMerge] = ArrayLeft[CounterArray1];
                CounterMerge++;
            }
            for (; CounterArray2 < ArrayRight.Length; CounterArray2++)
            {
                ArrayMerge[CounterMerge] = ArrayRight[CounterArray2];
                CounterMerge++;
            }

            return ArrayMerge;
        }
    }
}
