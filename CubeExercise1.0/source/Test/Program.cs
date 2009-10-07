using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CubeExercise;
using System.Diagnostics;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //GenerateTransform();
            Cube<char> cube = new Cube<char>('O', 'B', 'R', 'G', 'Y', 'W');
            for (int i = 0; i < 1; i++)
            {
                cube.DoFormula("b");
            }
            cube.Print();

            Debug.Assert(Cube<char>.ExpandFormula("") == "");
            Debug.Assert(Cube<char>.ExpandFormula("()") == "");
            Debug.Assert(Cube<char>.ExpandFormula("RLRU") == "RLRU");
            Debug.Assert(Cube<char>.ExpandFormula("(RLRU)") == "RLRU");
            Debug.Assert(Cube<char>.ExpandFormula("RL(RU)") == "RLRU");
            Debug.Assert(Cube<char>.ExpandFormula("RL(RU)L") == "RLRUL");
            Debug.Assert(Cube<char>.ExpandFormula("RL(RU)2L") == "RLRURUL");
            Debug.Assert(Cube<char>.ExpandFormula("(RL)RUL") == "RLRUL");
            Debug.Assert(Cube<char>.ExpandFormula("(RL)2RUL") == "RLRLRUL");
        }

        //static void GenerateTransform()
        //{
        //    Cube<byte[]> cube = new Cube<byte[]>(
        //        new byte[] { 0, 0, 0 },
        //        new byte[] { 1, 1, 1 },
        //        new byte[] { 2, 2, 2 },
        //        new byte[] { 3, 3, 3 },
        //        new byte[] { 4, 4, 4 },
        //        new byte[] { 5, 5, 5 });
        //    cube.cube = Transformation.O;
        //    string script = "E'";
        //    cube.DoFormula(script);

        //    Console.WindowWidth = 100;
        //    Console.WindowHeight = 36;
        //    Console.WriteLine("        public static byte[, ,][] " + script.Replace('\'', '_') + " = new byte[6, 3, 3][] {");
        //    for (int i = 0; i < 6; i++)
        //    {
        //        Console.WriteLine("            {");
        //        for (int j = 0; j < 3; j++)
        //        {
        //            Console.Write("                {");
        //            for (int k = 0; k < 3; k++)
        //            {
        //                string c0 = cube.Status[i, j, k][0].ToString(), c1 = cube.Status[i, j, k][1].ToString(), c2 = cube.Status[i, j, k][2].ToString();
        //                if (cube.Status[i, j, k][0] == Transformation.O[i, j, k][0]
        //                    && cube.Status[i, j, k][1] == Transformation.O[i, j, k][1]
        //                    && cube.Status[i, j, k][2] == Transformation.O[i, j, k][2])
        //                {
        //                    Console.Write("null, ");
        //                    continue;
        //                }
        //                string format = "new byte[]{{{0}, {1}, {2}}}, ";

        //                Console.Write(string.Format(format, c0, c1, c2));
        //            }

        //            Console.WriteLine("},");
        //        }

        //        Console.WriteLine("            },");
        //    }
        //    Console.WriteLine("        };");
        //}
    }
}
