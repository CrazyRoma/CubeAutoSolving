﻿using System;
using System.Drawing;
using System.Reflection;

namespace CubeAutoSolving
{
    class Moves 
    {
        // Переменная для свапа элементов char
        char cache;
        // Одномерный массив для ликвидация последовательности в формуле внутренних блоков
        int[] denie_i = new int[8];

        //  Инициализация
        // Инициализация массива
        public static char[][,] cube = new char[6][,]{
            new char[3,3],
            new char[3,3],
            new char[3,3],
            new char[3,3],
            new char[3,3],
            new char[3,3]
        };

        public void ColorsSet()
        {
            Form1 form = new Form1();

            // Одномерный массив чаров для инициализации массива куба
            char[] ColorsStart = new char[6]
            {
                'b', 
                'r', 
                'y',
                'o', 
                'w', 
                'g' 
            };

            // Заполнения куба начальными данными
            for (int k = 0; k < 6; k++)
                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 3; j++)
                        cube[k][i, j] = ColorsStart[k];

        }
        
        // Вызов методов по строковой формуле (рефлексия)
        public void DoMovesByFormula(string formula)
        {
            string[] moves = formula.Split(' ');
            //foreach (string move in moves)
                //InvokeMethodByName(move);
        }

        public static void InvokeMethodByName(object sender, string methodName)
        {
            MethodInfo moveMethod = typeof(Moves).GetMethod(methodName);
            moveMethod.Invoke(sender, null);
        }

        //   Методы для движения граней
        #region Повороты

        //  Стандартные движения
        // Поворот задней грани по часовой стрелки
        public void B()
        {
            // Вызов мтетода для внутренних блоков
            DoMoveInside(
                0, // Сторона
                true // Поворот по/против часовой стрелки (true/false)
            ); 
            // Вызов метода для внешних блоков
            DoMoveOutside(
                new int[]
                {
                    2, // Сторона 1 для внешнего поворота
                    3, // Сторона 2 для внешнего поворота
                    4, // Сторона 3 для внешнего поворота
                    1  // Сторона 4 для внешнего поворота
                }, // Одномерный массив для передачи адресов сторон
                new int[] // 8 элементов
                {
                    0, 0,
                    0, 0,
                    0, 0,
                    0, 0
                }, // Одномерный массив для: фиксирования адреса / обратной последовательности адреса
                new bool[] // 8 элементов
                { 
                    false, true,
                    false, true,
                    false, true,
                    false, true
                } // Одномерный bool-массив для инициализации массива ликвидации последовательности
            );
        }

        // Поворот задней грани против часовой стрелки
        public void Bi()
        {
            DoMoveInside(
                0,
                false
            );
            DoMoveOutside(
                new int[]
                {
                    2, 
                    1, 
                    4, 
                    3
                },
                new int[]
                {
                    0, 0,
                    0, 0,
                    0, 0,
                    0, 0
                },
                new bool[] 
                { 
                    false, true,
                    false, true,
                    false, true,
                    false, true
                }
            );
        }

        // Поворот левой грани по часовой стрелке
        public void L()
        {
            DoMoveInside(
                1,
                true
            );
            DoMoveOutside(
                new int[]
                {
                    5,
                    2,
                    0,
                    4
                },
                new int[] 
                {
                    0, 2,
                    0, 2,
                    0, 2,
                    2, 0
                },
                new bool[]
                {
                    true, false,
                    true, false,
                    true, false,
                    true, false
                }
            );
        }

        // Поворот левой грани против часовой стрелки
        public void Li()
        {
            DoMoveInside(
                1,
                false
            );
            DoMoveOutside(
                new int[]
                {
                    5,
                    4,
                    0,
                    2
                },
                new int[]
                {
                    0, 2,
                    2, 0,
                    0, 2,
                    0, 2
                },
                new bool[]
                {
                    true, false,
                    true, false,
                    true, false,
                    true, false
                }
            );
        }

        // Поворот верхней грани по часовой стрелке
        public void U()
        {
            DoMoveInside(
                2,
                true
            );
            DoMoveOutside(
                new int[]
                {
                    5, 
                    3, 
                    0, 
                    1
                },
                new int[]
                {
                    0, 0,
                    0, 2,
                    2, 2,
                    2, 0
                },
                new bool[] 
                { 
                    false, true,
                    true, false,
                    false, true,
                    true, false
                }
            );
        }

        // Поворот верхней грани против часовой стрелки 
        public void Ui()
        {
            DoMoveInside(
                2,
                false
            );
            DoMoveOutside(
                new int[]
                {
                    5,
                    1, 
                    0, 
                    3
                },
                new int[]
                {
                    0, 0,
                    2, 0,
                    2, 2,
                    0, 2
                },
                new bool[] 
                { 
                    false, true,
                    true, false,
                    false, true,
                    true, false
                }
            );
        }

        // Поворот правой грани по часовой стрелке        
        public void R()
        {
            DoMoveInside(
                3,
                true
            );
            DoMoveOutside(                
                new int[]
                {                   
                    5,
                    4, 
                    0, 
                    2                 
                }, 
                new int[] 
                {
                    2, 0,
                    0, 2,
                    2, 0,
                    2, 0
                },
                new bool[]
                {
                    true, false,
                    true, false,
                    true, false,
                    true, false
                }
            );
        }

        // Поворот правой грани против часовой стрелки
        public void Ri()
        {
            DoMoveInside(
                3,
                false
            );
            DoMoveOutside(                
                new int[] 
                {
                    5,
                    2,
                    0, 
                    4
                },
                new int[]
                {
                    2, 0,
                    2, 0,
                    2, 0,
                    0, 2
                },
                new bool[] 
                { 
                    true, false,
                    true, false,
                    true, false,
                    true, false
                }
            );
        }

        // Поворот нижней грани по часовой стрелки
        public void D()
        {
            DoMoveInside(
                4,
                true
            );
            DoMoveOutside(
                new int[]
                {
                    5,
                    1, 
                    0, 
                    3
                },
                new int[]
                {
                    2, 2,
                    0, 2,
                    0, 0,
                    2, 0
                },
                new bool[] 
                { 
                    false, true,
                    true, false,
                    false, true,
                    true, false
                }
            );
        }

        // Поворот нижней грани против часовой стрелки
        public void Di()
        {
            DoMoveInside(
                4,
                false
            );
            DoMoveOutside(
                new int[]
                {
                    5,
                    3,
                    0, 
                    1
                },
                new int[]
                {
                    2, 2,
                    2, 0,
                    0, 0,
                    0, 2
                },
                new bool[] 
                { 
                    false, true,
                    true, false,
                    false, true,
                    true, false
                }
            );
        }

        // Поворот фронтовой грани по часовой стрелки
        public void F()
        {
            DoMoveInside(
                5,
                true
            );
            DoMoveOutside(
                new int[]
                {
                    4,
                    3,
                    2, 
                    1
                },
                new int[]
                {
                    2, 2,
                    2, 2,
                    2, 2,
                    2, 2
                },
                new bool[] 
                { 
                    false, true,
                    false, true,
                    false, true,
                    false, true
                }
            );
        }

        // Поворот фронтовой грани против часовой стрелки
        public void Fi()
        {
            DoMoveInside(
                5,
                false
            );
            DoMoveOutside(
                new int[]
                {
                    4, 
                    1, 
                    2, 
                    3
                },
                new int[]
                {
                    2, 2,
                    2, 2,
                    2, 2,
                    2, 2
                },
                new bool[] 
                { 
                    false, true,
                    false, true,                
                    false, true,
                    false, true
                }
            );
        }
        
        //   Нестандартные движения
        //  Повороты центральных слоев
        // Движение центральной стороны относительно левой стороны по часовой стрелке
        public void M()
        {
            DoMoveOutside(
                new int[]
                {
                    4,
                    5,
                    2,
                    0
                },
                new int[]
                {
                    1, 0,
                    1, 2,
                    1, 2,
                    1, 2
                },
                new bool[] 
                { 
                    true, false,
                    true, false,
                    true, false,
                    true, false
                }
            );
        }

        // Движение центральной стороны относительно левой стороны против часовой стрелки
        public void Mi()
        {
            DoMoveOutside(
                new int[]
                {
                    4,
                    0,
                    2,
                    5
                },
                new int[]
                {
                    1, 0,
                    1, 2,
                    1, 2,
                    1, 2
                },
                new bool[] 
                { 
                    true, false,
                    true, false,
                    true, false,
                    true, false
                }
            );
        }

        // Движение центральной стороны относительно фронтальной стороны по часовой стрелке
        public void S()
        {
            DoMoveOutside(
                new int[]
                {
                    4,
                    1,
                    2,
                    3
                },
                new int[]
                {
                    0, 1,
                    0, 1,
                    0, 1,
                    0, 1
                },
                new bool[] 
                { 
                    false, true,
                    false, true,
                    false, true,
                    false, true
                }
            );
        }

        // Движение центральной стороны относительно фронтальной стороны против часовой стрелки
        public void Si()
        {
            DoMoveOutside(
                new int[]
                {
                    4,
                    3,
                    2,
                    1
                },
                new int[]
                {
                    0, 1,
                    0, 1,
                    0, 1,
                    0, 1
                },
                new bool[] 
                { 
                    false, true,
                    false, true,
                    false, true,
                    false, true
                }
            );
        }

        // Движение центральной стороны относительно нижней стороны по часовой стрелке
        public void E()
        {
            DoMoveOutside(
                new int[]
                {
                    5,
                    1,
                    0,
                    3
                },
                new int[]
                {
                    0, 1,
                    1, 0,
                    0, 1,
                    1, 0
                },
                new bool[] 
                { 
                    false, true,
                    true, false,
                    false, true,
                    true, false
                }
            );
        }

        // Движение центральной стороны относительно нижней стороны против часовой стрелки
        public void Ei()
        {
            DoMoveOutside(
                new int[]
                {
                    5,
                    3,
                    0,
                    1
                },
                new int[]
                {
                    0, 1,
                    1, 0,
                    0, 1,
                    1, 0
                },
                new bool[] 
                { 
                    false, true,
                    true, false,
                    false, true,
                    true, false
                }
            );
        }

        // Поворот + буква 'w'
        // Поворот правой стороны + среднего слоя по часовой стрелке
        public void Rw()
        {
            DoMoveInside(
                3,
                true
            );
            DoMoveOutside(
                new int[]
                {                   
                    5,
                    4, 
                    0, 
                    2                 
                },
                new int[] 
                {
                    2, 0,
                    0, 2,
                    2, 0,
                    2, 0
                },
                new bool[]
                {
                    true, false,
                    true, false,
                    true, false,
                    true, false
                }
            );
            DoMoveOutside(
                new int[]
                {
                    4,
                    0,
                    2,
                    5
                },
                new int[]
                {
                    1, 0,
                    1, 2,
                    1, 2,
                    1, 2
                },
                new bool[] 
                { 
                    true, false,
                    true, false,
                    true, false,
                    true, false
                }
            );
        }

        // Поворот правой стороны + среднего слоя против часовой стрелки
        public void Rwi()
        {
            DoMoveInside(
                3,
                false
            );
            DoMoveOutside(
                new int[] 
                {
                    5,
                    2,
                    0, 
                    4
                },
                new int[]
                {
                    2, 0,
                    2, 0,
                    2, 0,
                    0, 2
                },
                new bool[] 
                { 
                    true, false,
                    true, false,
                    true, false,
                    true, false
                }
            );
            DoMoveOutside(
                new int[]
                {
                    4,
                    5,
                    2,
                    0
                },
                new int[]
                {
                    1, 0,
                    1, 2,
                    1, 2,
                    1, 2
                },
                new bool[] 
                { 
                    true, false,
                    true, false,
                    true, false,
                    true, false
                }
            );
        }

        // Поворот левой стороны + среднего слоя по часовой стрелке
        public void Lw()
        {
            DoMoveInside(
                1,
                true
            );
            DoMoveOutside(
                new int[]
                {
                    5,
                    2,
                    0,
                    4
                },
                new int[] 
                {
                    0, 2,
                    0, 2,
                    0, 2,
                    2, 0
                },
                new bool[]
                {
                    true, false,
                    true, false,
                    true, false,
                    true, false
                }
            );
            DoMoveOutside(
                new int[]
                {
                    4,
                    5,
                    2,
                    0
                },
                new int[]
                {
                    1, 0,
                    1, 2,
                    1, 2,
                    1, 2
                },
                new bool[] 
                { 
                    true, false,
                    true, false,
                    true, false,
                    true, false
                }
            );
        }

        // Поворот левой стороны + среднего слоя против часовой стрелки
        public void Lwi()
        {
            DoMoveInside(
                1,
                false
            );
            DoMoveOutside(
                new int[]
                {
                    5,
                    4,
                    0,
                    2
                },
                new int[]
                {
                    0, 2,
                    2, 0,
                    0, 2,
                    0, 2
                },
                new bool[]
                {
                    true, false,
                    true, false,
                    true, false,
                    true, false
                }
            );
            DoMoveOutside(
                new int[]
                {
                    4,
                    0,
                    2,
                    5
                },
                new int[]
                {
                    1, 0,
                    1, 2,
                    1, 2,
                    1, 2
                },
                new bool[] 
                { 
                    true, false,
                    true, false,
                    true, false,
                    true, false
                }
            );
        }

        // Поворот фронтовой стороны + среднего слоя по часовой стрелке
        public void Fw()
        {
            DoMoveInside(
                5,
                true
            );
            DoMoveOutside(
                new int[]
                {
                    4,
                    3,
                    2, 
                    1
                },
                new int[]
                {
                    2, 2,
                    2, 2,
                    2, 2,
                    2, 2
                },
                new bool[] 
                { 
                    false, true,
                    false, true,
                    false, true,
                    false, true
                }
            );
            DoMoveOutside(
                new int[]
                {
                    4,
                    1,
                    2,
                    3
                },
                new int[]
                {
                    0, 1,
                    0, 1,
                    0, 1,
                    0, 1
                },
                new bool[] 
                { 
                    false, true,
                    false, true,
                    false, true,
                    false, true
                }
            );
        }

        // Поворот фронтовой стороны + среднего слоя против часовой стрелки
        public void Fwi()
        {
            DoMoveInside(
                5,
                false
            );
            DoMoveOutside(
                new int[]
                {
                    4, 
                    1, 
                    2, 
                    3
                },
                new int[]
                {
                    2, 2,
                    2, 2,
                    2, 2,
                    2, 2
                },
                new bool[] 
                { 
                    false, true,
                    false, true,                
                    false, true,
                    false, true
                }
            );
            DoMoveOutside(
                new int[]
                {
                    4,
                    3,
                    2,
                    1
                },
                new int[]
                {
                    0, 1,
                    0, 1,
                    0, 1,
                    0, 1
                },
                new bool[] 
                { 
                    false, true,
                    false, true,
                    false, true,
                    false, true
                }
            );
        }

        // Поворот задней стороны + среднего слоя по часовой стрелке
        public void Bw()
        {
            DoMoveInside(
                0, 
                true 
            );
            DoMoveOutside(
                new int[]
                {
                    2, 
                    3, 
                    4, 
                    1  
                }, 
                new int[] 
                {
                    0, 0,
                    0, 0,
                    0, 0,
                    0, 0
                }, 
                new bool[] 
                { 
                    false, true,
                    false, true,
                    false, true,
                    false, true
                } 
            );
            DoMoveOutside(
                new int[]
                {
                    4,
                    3,
                    2,
                    1
                },
                new int[]
                {
                    0, 1,
                    0, 1,
                    0, 1,
                    0, 1
                },
                new bool[] 
                { 
                    false, true,
                    false, true,
                    false, true,
                    false, true
                }
            );
        }

        // Поворот задней стороны + среднего слоя против часовой стрелки
        public void Bwi()
        {
            DoMoveInside(
                0,
                false
            );
            DoMoveOutside(
                new int[]
                {
                    2, 
                    1, 
                    4, 
                    3
                },
                new int[]
                {
                    0, 0,
                    0, 0,
                    0, 0,
                    0, 0
                },
                new bool[] 
                { 
                    false, true,
                    false, true,
                    false, true,
                    false, true
                }
            );
            DoMoveOutside(
                new int[]
                {
                    4,
                    1,
                    2,
                    3
                },
                new int[]
                {
                    0, 1,
                    0, 1,
                    0, 1,
                    0, 1
                },
                new bool[] 
                { 
                    false, true,
                    false, true,
                    false, true,
                    false, true
                }
            );
        }

        // Поворот верхней стороны + среднего слоя по часовой стрелке
        public void Uw()
        {
            DoMoveInside(
                2,
                true
            );
            DoMoveOutside(
                new int[]
                {
                    5, 
                    3, 
                    0, 
                    1
                },
                new int[]
                {
                    0, 0,
                    0, 2,
                    2, 2,
                    2, 0
                },
                new bool[] 
                { 
                    false, true,
                    true, false,
                    false, true,
                    true, false
                }
            );
            DoMoveOutside(
                new int[]
                {
                    5,
                    3,
                    0,
                    1
                },
                new int[]
                {
                    0, 1,
                    1, 0,
                    0, 1,
                    1, 0
                },
                new bool[] 
                { 
                    false, true,
                    true, false,
                    false, true,
                    true, false
                }
            );
        }

        // Поворот верхней стороны + среднего слоя против часовой стрелки
        public void Uwi()
        {
            DoMoveInside(
                2,
                false
            );
            DoMoveOutside(
                new int[]
                {
                    5,
                    1, 
                    0, 
                    3
                },
                new int[]
                {
                    0, 0,
                    2, 0,
                    2, 2,
                    0, 2
                },
                new bool[] 
                { 
                    false, true,
                    true, false,
                    false, true,
                    true, false
                }
            );
            DoMoveOutside(
                new int[]
                {
                    5,
                    1,
                    0,
                    3
                },
                new int[]
                {
                    0, 1,
                    1, 0,
                    0, 1,
                    1, 0
                },
                new bool[] 
                { 
                    false, true,
                    true, false,
                    false, true,
                    true, false
                }
            );
        }

        // Поворот нижней стороны + среднего слоя по часовой стрелке
        public void Dw()
        {
            DoMoveInside(
                4,
                true
            );
            DoMoveOutside(
                new int[]
                {
                    5,
                    1, 
                    0, 
                    3
                },
                new int[]
                {
                    2, 2,
                    0, 2,
                    0, 0,
                    2, 0
                },
                new bool[] 
                { 
                    false, true,
                    true, false,
                    false, true,
                    true, false
                }
            );
            DoMoveOutside(
                new int[]
                {
                    5,
                    1,
                    0,
                    3
                },
                new int[]
                {
                    0, 1,
                    1, 0,
                    0, 1,
                    1, 0
                },
                new bool[] 
                { 
                    false, true,
                    true, false,
                    false, true,
                    true, false
                }
            );
        }

        // Поворот нижней стороны + среднего слоя против часовой стрелки
        public void Dwi()
        {
            DoMoveInside(
                4,
                true
            );
            DoMoveOutside(
                new int[]
                {
                    5,
                    1, 
                    0, 
                    3
                },
                new int[]
                {
                    2, 2,
                    0, 2,
                    0, 0,
                    2, 0
                },
                new bool[] 
                { 
                    false, true,
                    true, false,
                    false, true,
                    true, false
                }
            );
            DoMoveOutside(
                new int[]
                {
                    5,
                    3,
                    0,
                    1
                },
                new int[]
                {
                    0, 1,
                    1, 0,
                    0, 1,
                    1, 0
                },
                new bool[] 
                { 
                    false, true,
                    true, false,
                    false, true,
                    true, false
                }
            );
        }

        //  Повороты всего куба
        // Поворот куба относительно правой стороны по часовой стрелке
        public void x()
        {
            DoMoveInside(
                3,
                true
            );
            DoMoveOutside(
                new int[]
                {                   
                    5,
                    4, 
                    0, 
                    2                 
                },
                new int[] 
                {
                    2, 0,
                    0, 2,
                    2, 0,
                    2, 0
                },
                new bool[]
                {
                    true, false,
                    true, false,
                    true, false,
                    true, false
                }
            );
            DoMoveOutside(
                new int[]
                {
                    4,
                    0,
                    2,
                    5
                },
                new int[]
                {
                    1, 0,
                    1, 2,
                    1, 2,
                    1, 2
                },
                new bool[] 
                { 
                    true, false,
                    true, false,
                    true, false,
                    true, false
                }
            );
            DoMoveInside(
                1,
                false
            );
            DoMoveOutside(
                new int[]
                {
                    5,
                    4,
                    0,
                    2
                },
                new int[]
                {
                    0, 2,
                    2, 0,
                    0, 2,
                    0, 2
                },
                new bool[]
                {
                    true, false,
                    true, false,
                    true, false,
                    true, false
                }
            );
        }

        // Поворот куба относительно правой стороны против часовой стрелки
        public void xi()
        {
            DoMoveInside(
                3,
                false
            );
            DoMoveOutside(
                new int[] 
                {
                    5,
                    2,
                    0, 
                    4
                },
                new int[]
                {
                    2, 0,
                    2, 0,
                    2, 0,
                    0, 2
                },
                new bool[] 
                { 
                    true, false,
                    true, false,
                    true, false,
                    true, false
                }
            );
            DoMoveOutside(
                new int[]
                {
                    4,
                    5,
                    2,
                    0
                },
                new int[]
                {
                    1, 0,
                    1, 2,
                    1, 2,
                    1, 2
                },
                new bool[] 
                { 
                    true, false,
                    true, false,
                    true, false,
                    true, false
                }
            );
            DoMoveInside(
                1,
                true
            );
            DoMoveOutside(
                new int[]
                {
                    5,
                    2,
                    0,
                    4
                },
                new int[] 
                {
                    0, 2,
                    0, 2,
                    0, 2,
                    2, 0
                },
                new bool[]
                {
                    true, false,
                    true, false,
                    true, false,
                    true, false
                }
            );
        }

        // Поворот куба относительно верхней стороны по часовой стрелке
        public void y()
        {
            DoMoveInside(
                2,
                true
            );
            DoMoveOutside(
                new int[]
                {
                    5, 
                    3, 
                    0, 
                    1
                },
                new int[]
                {
                    0, 0,
                    0, 2,
                    2, 2,
                    2, 0
                },
                new bool[] 
                { 
                    false, true,
                    true, false,
                    false, true,
                    true, false
                }
            );
            DoMoveOutside(
                new int[]
                {
                    5,
                    3,
                    0,
                    1
                },
                new int[]
                {
                    0, 1,
                    1, 0,
                    0, 1,
                    1, 0
                },
                new bool[] 
                { 
                    false, true,
                    true, false,
                    false, true,
                    true, false
                }
            );
            DoMoveInside(
                4,
                false
            );
            DoMoveOutside(
                new int[]
                {
                    5,
                    3,
                    0, 
                    1
                },
                new int[]
                {
                    2, 2,
                    2, 0,
                    0, 0,
                    0, 2
                },
                new bool[] 
                { 
                    false, true,
                    true, false,
                    false, true,
                    true, false
                }
            );
        }

        // Поворот куба относительно верхней стороны против часовой стрелки
        public void yi()
        {
            DoMoveInside(
                2,
                false
            );
            DoMoveOutside(
                new int[]
                {
                    5,
                    1, 
                    0, 
                    3
                },
                new int[]
                {
                    0, 0,
                    2, 0,
                    2, 2,
                    0, 2
                },
                new bool[] 
                { 
                    false, true,
                    true, false,
                    false, true,
                    true, false
                }
            );
            DoMoveOutside(
                new int[]
                {
                    5,
                    1,
                    0,
                    3
                },
                new int[]
                {
                    0, 1,
                    1, 0,
                    0, 1,
                    1, 0
                },
                new bool[] 
                { 
                    false, true,
                    true, false,
                    false, true,
                    true, false
                }
            );
            DoMoveInside(
                4,
                true
            );
            DoMoveOutside(
                new int[]
                {
                    5,
                    1, 
                    0, 
                    3
                },
                new int[]
                {
                    2, 2,
                    0, 2,
                    0, 0,
                    2, 0
                },
                new bool[] 
                { 
                    false, true,
                    true, false,
                    false, true,
                    true, false
                }
            );
        }

        // Поворот куба относительно фронтальной стороны по часовой стрелке
        public void z()
        {
            DoMoveInside(
                5,
                true
            );
            DoMoveOutside(
                new int[]
                {
                    4,
                    3,
                    2, 
                    1
                },
                new int[]
                {
                    2, 2,
                    2, 2,
                    2, 2,
                    2, 2
                },
                new bool[] 
                { 
                    false, true,
                    false, true,
                    false, true,
                    false, true
                }
            );
            DoMoveOutside(
                new int[]
                {
                    4,
                    1,
                    2,
                    3
                },
                new int[]
                {
                    0, 1,
                    0, 1,
                    0, 1,
                    0, 1
                },
                new bool[] 
                { 
                    false, true,
                    false, true,
                    false, true,
                    false, true
                }
            );
            DoMoveInside(
                0,
                false
            );
            DoMoveOutside(
                new int[]
                {
                    2, 
                    1, 
                    4, 
                    3
                },
                new int[]
                {
                    0, 0,
                    0, 0,
                    0, 0,
                    0, 0
                },
                new bool[] 
                { 
                    false, true,
                    false, true,
                    false, true,
                    false, true
                }
            );
        }

        // Поворот куба относительно фронтальной стороны против часовой стрелки
        public void zi()
        {
            DoMoveInside(
                5,
                false
            );
            DoMoveOutside(
                new int[]
                {
                    4, 
                    1, 
                    2, 
                    3
                },
                new int[]
                {
                    2, 2,
                    2, 2,
                    2, 2,
                    2, 2
                },
                new bool[] 
                { 
                    false, true,
                    false, true,                
                    false, true,
                    false, true
                }
            );
            DoMoveOutside(
                new int[]
                {
                    4,
                    3,
                    2,
                    1
                },
                new int[]
                {
                    0, 1,
                    0, 1,
                    0, 1,
                    0, 1
                },
                new bool[] 
                { 
                    false, true,
                    false, true,
                    false, true,
                    false, true
                }
            );
            DoMoveInside(
                0,
                true
            );
            DoMoveOutside(
                new int[]
                {
                    2,
                    3,
                    4,
                    1
                },
                new int[]
                {
                    0, 0,
                    0, 0,
                    0, 0,
                    0, 0
                },
                new bool[]
                { 
                    false, true,
                    false, true,
                    false, true,
                    false, true
                }
            );
        }

        #endregion

        // Универсальный метод для поворота блоков грани
        public void DoMoveOutside(
            int[] cubeFace,
            int[] fixedNumber, 
            bool[] denie
        )
        {
            // Поворот внешних блоков
            for (int k = 0; k < 3; k++)
            {
                // Определение фиксирования
                for (int i = 0; i < 8; i++)
                    denie_i[i] = (denie[i] ? k : 0);

                // cache = cube[0]
                this.cache =  cube[cubeFace[0]]
                [
                    Math.Abs(fixedNumber[0] - k + denie_i[0]),
                    Math.Abs(fixedNumber[1] - k + denie_i[1])
                ];
                // cube[0] = cube[1]                        
                cube[cubeFace[0]]
                [
                    Math.Abs(fixedNumber[0] - k + denie_i[0]),                     
                    Math.Abs(fixedNumber[1] - k + denie_i[1])
                ] = cube[cubeFace[1]]
                [
                    Math.Abs(fixedNumber[2] - k + denie_i[2]),
                    Math.Abs(fixedNumber[3] - k + denie_i[3])
                ];
                // cube[1] = cube[2]
                cube[cubeFace[1]]
                [
                    Math.Abs(fixedNumber[2] - k + denie_i[2]),
                    Math.Abs(fixedNumber[3] - k + denie_i[3])
                ] = cube[cubeFace[2]]
                [
                    Math.Abs(fixedNumber[4] - k + denie_i[4]),
                    Math.Abs(fixedNumber[5] - k + denie_i[5])
                ];
                // cube[2] = cube[3]
                cube[cubeFace[2]]
                [
                    Math.Abs(fixedNumber[4] - k + denie_i[4]),
                    Math.Abs(fixedNumber[5] - k + denie_i[5])
                ] = cube[cubeFace[3]]
                [
                    Math.Abs(fixedNumber[6] - k + denie_i[6]),
                    Math.Abs(fixedNumber[7] - k + denie_i[7])
                ];
                // cube[3] = cache
                cube[cubeFace[3]]
                [
                    Math.Abs(fixedNumber[6] - k + denie_i[6]),
                    Math.Abs(fixedNumber[7] - k + denie_i[7])
                ] = cache;
            }
        }

        public void DoMoveInside(int Face, bool invert)
        {
            // Определение, в какую сторону прокручивать внутренние блоки            
            int invertOne = (invert ? 0 : 2); // По часовой
            int invertTwo = (invert ? 2 : 0); // Против часовой

            // Поворот внутренних блоков
            for (int i = 0; i < 2; i++)
            {
                this.cache = 
                    cube[Face][i,0];
                cube[Face][i,0] = 
                    cube[Face]
                [
                    invertOne, 
                    Math.Abs(invertTwo - i)
                ];
                cube[Face]
                [
                    invertOne, 
                    Math.Abs(invertTwo - i)
                ] = cube[Face]
                [
                    2 - i,
                    2
                ];
                cube[Face]
                [
                    2 - i,
                    2
                ] = cube[Face]
                [
                    invertTwo,
                    Math.Abs(invertOne - i)
                ];
                cube[Face]
                [
                    invertTwo,
                    Math.Abs(invertOne - i)
                ] = this.cache;
            }
        }
    }
}