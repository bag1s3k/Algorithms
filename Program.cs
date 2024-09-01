using System;
using System.Diagnostics;
using System.Linq;

namespace MyFirstProgram
{
	class Program
	{
		static Random random = new Random();

		static void Main(string[] args)
		{
			int[] a = new int[100];
			int[] b = new int[a.Length];
			int[] c = new int[a.Length];
			int[] d = new int[a.Length];
			int[] e = new int[a.Length];

			for (int i = 0; i < a.Length; i++)
			{
				a[i] = random.Next(a.Length);
				b[i] = random.Next(a.Length);
				c[i] = random.Next(a.Length);
				d[i] = random.Next(a.Length);
				e[i] = random.Next(a.Length);
			}

			// for (int i = 0; i < a.Length; i++)
			// 	Console.Write($"{a[i]} ");
			// Console.WriteLine();

			Serad(a, SelectionSort);
			Serad(b, InsertionSort);
			Serad(c, BoubbleSort);
			Serad(d, QuickSort);
			Serad(e, LinqSort);

			// for (int i = 0; i < a.Length; i++)
			//	 Console.Write($"{a[i]} ");

			Console.ReadKey();
		}

		static ulong PocetKroku;
		delegate void RadiciMetoda(int[] a);

		static void Serad(int[] a, RadiciMetoda metoda)
		{
			Console.WriteLine(metoda.Method.Name);
			PocetKroku = 0;
			Stopwatch sw = new Stopwatch();
			sw.Start();
			metoda(a);
			double cas = sw.Elapsed.TotalMilliseconds;
			sw.Stop();
			Console.WriteLine("Kroků: {0:N0}", PocetKroku);
			Console.WriteLine("Čas: {0:N5}ms", cas);
			Console.WriteLine();
		}

		static void LinqSort(int[] a)
		{
			a = a.OrderBy(x => x).ToArray(); // OrderByDescending
		}

		static void QuickSort(int[] a)
		{
			QuickSort(a, 0, a.Length - 1);
		}
		static void QuickSort(int[] a, int iFrom, int iTo)
		{
			if (iFrom >= iTo) return;

			int iMez = iFrom;
			for (int i = iFrom + 1; i <= iTo; i++)
			{
				if (a[i] < a[iFrom])
				{
					++iMez;
					int temp = a[i];
					a[i] = a[iMez];
					a[iMez] = temp;
				}
				PocetKroku++;
			}
			int temp_2 = a[iFrom];
			a[iFrom] = a[iMez];
			a[iMez] = temp_2;

			QuickSort(a, iFrom, iMez - 1);
			QuickSort(a, iMez + 1, iTo);
		}

		static void BoubbleSort(int[] a)
		{
			for (int i = 0; i < a.Length - 1; i++)
			{
				bool switch_1 = false;
				for (int j = 0; j < a.Length - 1 - i; j++)
				{
					if (a[j + 1] < a[j]) // zde staci otocit
					{
						int temp = a[j];
						a[j] = a[j + 1];
						a[j + 1] = temp;
						switch_1 = true;
					}
					PocetKroku++;
				}
				if (!switch_1) return;
			}
		}

		static void InsertionSort(int[] a)
		{
			for (int i = 0; i < a.Length - 1; i++)
			{
				int j = i + 1; // index zařazovaného prvku
				int temp = a[j]; // hodnota zařazovaného prvku
				while (j > 0 && temp < a[j - 1])
				{
					a[j] = a[j - 1]; // posun většího prvku doprava
					j--;
					PocetKroku++;
				}
				a[j] = temp; // zařazení prvku do setříděné části posloupnosti
			}
		}

		static void SelectionSort(int[] a)
		{
			for (int j = 0; j < a.Length - 1; j++)
			{
				int min = j;
				for (int i = j + 1; i < a.Length; i++)
				{
					if (a[i] < a[min]) // zde stačí prohodit znaménko pro opačný chod
						min = i;
					PocetKroku++;
				}
				if (j != min)
				{
					int temp = a[j];
					a[j] = a[min];
					a[min] = temp;
				}
			}
		}
	}
}