﻿using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace CubeAutoSolving
{
	class LayerByLayer : SolvingMethod
	{
		private int position = 0;
		private bool stopAlg = false;

		public char[][,] duplicateCube = new char[6][,]
		{
			new char[3,3],
			new char[3,3],
			new char[3,3],
			new char[3,3],
			new char[3,3],
			new char[3,3]
		};

		public LayerByLayer()
		{
			Task console = Task.Factory.StartNew(MyConsole);

			Form diagnosticForm = new Form();
		}

		public override void SolveCube()
		{
			SolveFirstLayer();
			/*SolveCross();
			SolveSecondLayer();
			OrientateLastLayerCross();
			PermutateLastLayerEdges();
			OrientateLastLayerCorners();
			PermutateLastLayerCorners();*/
		}
		
		private void SolveFirstLayer()
		{
			stopAlg = false;
			position = 0;
		}

		private static Stopwatch watch = null;

		public void MyConsole()
		{
			if (AllocConsole())
			{
				string[] moves =
				{
					"R", "R'", "R2",
					"U", "U'", "U2",
					"F", "F'", "F2",
					"L", "L'", "L2",
					"D", "D'", "D2",
					"B", "B'", "B2",
				};

				for (int k = 0; k < 6; k++)
				{
					for (int i = 0; i < 3; i++)
					{
						for (int j = 0; j < 3; j++)
						{
							duplicateCube[k][i, j] = Moves.cube[k][i, j];
						}
					}
				}

				watch = new Stopwatch();
				watch.Start();

				for (position = 0; position < 20; position++)
				{
					if (!stopAlg)
					{
						Generate(0, "");

						if (watch.ElapsedMilliseconds >= 600000)
						{
							break;
						}
					}
				}

				Console.ReadKey();
				FreeConsole();
			}
		}
		
		private static int numberAttempts = 0;
		private static int formulaCount = 1;
		private static string[][] moves =
		{
			new string[] { "R", "R'", "R2" },
			new string[] { "L", "L'", "L2" },
			new string[] { "U", "U'", "U2" },
			new string[] { "D", "D'", "D2" },
			new string[] { "F", "F'", "F2" },
			new string[] { "B", "B'", "B2" }
		};

		// Метод рекурсивной генерации формулы для кубика
		private void Generate(int position, string pattern)
		{
			if (watch.ElapsedMilliseconds >= 600000)
			{
				return;
			}

			int last = position - 1;
			int penultimate = position - 2;
			string[] elements = pattern.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
			string formula = "";

			// Цикл по типу поворота
			for (int turn = 0; turn < moves.Length; turn++)
			{
				// Для формулы больше одного движения
				if (elements.Length >= 1)
				{
					// Сверка последнего элемента формулы со следующим предпологаемым типом поворота
					if (Array.IndexOf(moves[turn], elements[last]) != -1)
					{
						if (turn != moves.Length - 1)
						{
							turn++;
						}
						else
						{
							continue;
						}
					}
					// Для формулы больше двух движений
					if (elements.Length >= 2)
					{
						// Сверка предпоследнего элемента формулы со следующим предпологаемым типом поворота
						if (Array.IndexOf(moves[turn], elements[penultimate]) != -1)
						{
							if (turn != moves.Length - 1)
							{
								turn++;
								// Сверка последнего элемента формулы со следующим предпологаемым типом поворота
								if (Array.IndexOf(moves[turn], elements[last]) != -1)
								{
									if (turn != moves.Length - 1)
									{
										turn++;
									}
									else
									{
										continue;
									}
								}
							}
							else
							{
								continue;
							}
						}
					}
				}

				// Цикл по варианту поворота
				for (int variant = 0; variant < moves[turn].Length; variant++)
				{
					// Вывод результата
					Console.WriteLine(
						"Formula count: {0}; time: {1}; attempts: {2}; formula: {3}",
						formulaCount < position ? position + 1 : formulaCount, 
						watch.Elapsed, 
						++numberAttempts,
						pattern + moves[turn][variant]
						);
					formula = pattern + moves[turn][variant];
					Moves.DoMovesByFormula(formula); // Выполнение формулы

					// Создания новой ветки рекурсии
					if (position < this.position && !stopAlg)
					{
						Generate(position + 1, pattern + moves[turn][variant] + " ");
					}
				}
			}
		}

		private void SolveSecondLayer()
		{
			
		}

		private void OrientateLastLayerCross()
		{

		}

		private void PermutateLastLayerEdges()
		{

		}

		private void OrientateLastLayerCorners()
		{

		}

		private void PermutateLastLayerCorners()
		{

		}

		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool AllocConsole();

		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool FreeConsole();
	}
}
