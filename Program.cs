using System;
using System.Diagnostics;
using System.Linq;

namespace MyFirstProgram {
	class Program {
		static Random random = new Random();

		static void Main(string[] args) {
			Console.Write("Enter the length of the list: ");
			int length_list = int.Parse(Console.ReadLine());

			Console.WriteLine();
			
			int[] a = new int[length_list];
			int[] b = new int[a.Length];
			int[] c = new int[a.Length];
			int[] d = new int[a.Length];
			int[] e = new int[a.Length];

			for (int i = 0; i < a.Length; i++) {
				a[i] = random.Next(a.Length);
				b[i] = random.Next(a.Length);
				c[i] = random.Next(a.Length);
				d[i] = random.Next(a.Length);
				e[i] = random.Next(a.Length);
			}

			DoTest(a, SelectionSort);
			DoTest(b, InsertionSort);
			DoTest(c, BobbleSort);
			DoTest(d, QuickSort);
			DoTest(e, LinqSort);

			Console.ReadKey();
		}

		static ulong Steps;
		delegate void SortingMethod(int[] a);

		static void DoTest(int[] a, SortingMethod method) {
			Console.WriteLine(method.Method.Name);
			Steps = 0;
			Stopwatch sw = new Stopwatch();
			sw.Start();
			method(a);
			double cas = sw.Elapsed.TotalMilliseconds;
			sw.Stop();
			Console.WriteLine("Steps: {0:N0}", Steps);
			Console.WriteLine("Time: {0:N5}ms", cas);
			Console.WriteLine();
		}

		static void LinqSort(int[] a) {
			a = a.OrderBy(x => x).ToArray();
		}

		static void QuickSort(int[] a) {
			QuickSort(a, 0, a.Length - 1);
		}

		static void QuickSort(int[] a, int iFrom, int iTo) {
			if (iFrom >= iTo) return;

			int iMez = iFrom;
			for (int i = iFrom + 1; i <= iTo; i++) {
				if (a[i] < a[iFrom]) {
					++iMez;
					int temp = a[i];
					a[i] = a[iMez];
					a[iMez] = temp;
				}
				Steps++;
			}
			int temp_2 = a[iFrom];
			a[iFrom] = a[iMez];
			a[iMez] = temp_2;

			QuickSort(a, iFrom, iMez - 1);
			QuickSort(a, iMez + 1, iTo);
		}

		static void BobbleSort(int[] a) {
			for (int i = 0; i < a.Length - 1; i++) {
				bool switch_1 = false;
				for (int j = 0; j < a.Length - 1 - i; j++) {
					if (a[j + 1] < a[j]) {
						int temp = a[j];
						a[j] = a[j + 1];
						a[j + 1] = temp;
						switch_1 = true;
					}
					Steps++;
				}
				if (!switch_1) return;
			}
		}

		static void InsertionSort(int[] a) {
			for (int i = 0; i < a.Length - 1; i++) {
				int j = i + 1;
				int temp = a[j];
				while (j > 0 && temp < a[j - 1]) {
					a[j] = a[j - 1];
					j--;
					Steps++;
				}
				a[j] = temp;
			}
		}

		static void SelectionSort(int[] a) {
			for (int j = 0; j < a.Length - 1; j++) {
				int min = j;
				for (int i = j + 1; i < a.Length; i++) {
					if (a[i] < a[min])
						min = i;
					Steps++;
				}
				if (j != min) {
					int temp = a[j];
					a[j] = a[min];
					a[min] = temp;
				}
			}
		}
	}
}