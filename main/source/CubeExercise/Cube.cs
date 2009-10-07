using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Reflection;

namespace CubeExercise
{
    public class Cube<T>
    {
        private T[, ,] cube = new T[6, 3, 3];
        //{
        //    {
        //        {'O', 'O', 'O'},
        //        {'O', 'O', 'O'},
        //        {'O', 'O', 'O'}
        //    },
        //    {
        //        {'B', 'B', 'B'},
        //        {'B', 'B', 'B'},
        //        {'B', 'B', 'B'}
        //    },
        //    {
        //        {'R', 'R', 'R'},
        //        {'R', 'R', 'R'},
        //        {'R', 'R', 'R'}
        //    },
        //    {
        //        {'G', 'G', 'G'},
        //        {'G', 'G', 'G'},
        //        {'G', 'G', 'G'}
        //    },
        //    {
        //        {'Y', 'Y', 'Y'},
        //        {'Y', 'Y', 'Y'},
        //        {'Y', 'Y', 'Y'}
        //    },
        //    {
        //        {'W', 'W', 'W'},
        //        {'W', 'W', 'W'},
        //        {'W', 'W', 'W'}
        //    }
        //};

        public Cube(T l, T f, T r, T b, T u, T d)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    this.cube[0, i, j] = l;
                }
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    this.cube[1, i, j] = f;
                }
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    this.cube[2, i, j] = r;
                }
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    this.cube[3, i, j] = b;
                }
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    this.cube[4, i, j] = u;
                }
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    this.cube[5, i, j] = d;
                }
            }
        }

        public T[, ,] Status
        {
            get
            {
                return this.cube;
            }
        }

        public void R()
        {
            this.Move(Transformation.R);
        }

        public void r()
        {
            this.Move(Transformation.r);
        }

        public void L()
        {
            this.Move(Transformation.L);
        }

        public void U()
        {
            this.Move(Transformation.U);
        }

        public void D()
        {
            this.Move(Transformation.D);
        }

        public void F()
        {
            this.Move(Transformation.F);
        }

        public void B()
        {
            this.Move(Transformation.B);
        }

        /// <summary>
        /// X :整体向R方向转
        /// </summary>
        public void X()
        {
            this.Move(Transformation.X);
        }

        /// <summary>
        /// Y :整体向U方向转
        /// </summary>
        public void Y()
        {
            this.Move(Transformation.Y);
        }

        /// <summary>
        /// Z :整体向F方向转
        /// </summary>
        public void Z()
        {
            this.Move(Transformation.Z);
        }

        /// <summary>
        /// S : BF夹层向F方向转
        /// </summary>
        public void S()
        {
            this.Move(Transformation.S);
        }

        /// <summary>
        /// M : LR夹层向L方向转
        /// </summary>
        public void M()
        {
            this.Move(Transformation.M);
        }

        /// <summary>
        /// E : UD夹层向U'方向转
        /// </summary>
        public void E()
        {
            this.Move(Transformation.E);
        }

        public T[, ,] Move(string singleAction)
        {
            switch (singleAction)
            {
                case "R":
                    this.R();
                    break;

                case "R'":
                    this.Move(Transformation.R_);
                    break;

                case "r":
                    this.r();
                    break;

                case "r'":
                    this.Move(Transformation.r_);
                    break;

                case "L":
                    this.L();
                    break;

                case "L'":
                    this.Move(Transformation.L_);
                    break;

                case "l":
                    this.Move(Transformation.l);
                    break;

                case "l'":
                    this.Move(Transformation.l_);
                    break;

                case "U":
                    this.U();
                    break;

                case "U'":
                    this.Move(Transformation.U_);
                    break;

                case "u":
                    this.Move(Transformation.u);
                    break;

                case "u'":
                    this.Move(Transformation.u_);
                    break;

                case "D":
                    this.D();
                    break;

                case "D'":
                    this.Move(Transformation.D_);
                    break;

                case "d":
                    this.Move(Transformation.d);
                    break;

                case "d'":
                    this.Move(Transformation.d_);
                    break;

                case "F":
                    this.F();
                    break;

                case "F'":
                    this.Move(Transformation.F_);
                    break;

                case "f":
                    this.Move(Transformation.f);
                    break;

                case "f'":
                    this.Move(Transformation.f_);
                    break;

                case "B":
                    this.B();
                    break;

                case "B'":
                    this.Move(Transformation.B_);
                    break;

                case "b":
                    this.Move(Transformation.b);
                    break;

                case "b'":
                    this.Move(Transformation.b_);
                    break;

                case "X":
                case "x":
                    this.X();
                    break;

                case "X'":
                case "x'":
                    this.Move(Transformation.X_);
                    break;

                case "Y":
                case "y":
                    this.Y();
                    break;

                case "Y'":
                case "y'":
                    this.Move(Transformation.Y_);
                    break;

                case "Z":
                case "z":
                    this.Z();
                    break;

                case "Z'":
                case "z'":
                    this.Move(Transformation.Z_);
                    break;

                case "S":
                case "s":
                    this.S();
                    break;

                case "S'":
                case "s'":
                    this.Move(Transformation.S_);
                    break;

                case "M":
                case "m":
                    this.M();
                    break;

                case "M'":
                case "m'":
                    this.Move(Transformation.M_);
                    break;

                case "E":
                case "e":
                    this.E();
                    break;

                case "E'":
                case "e'":
                    this.Move(Transformation.E_);
                    break;

                default:
                    throw new ArgumentException("未知的符号: " + singleAction + "!", "singleAction");
            }

            return this.cube;
        }

        public T[, ,] DoAlgorithm(string algorithm)
        {
            return this.DoAlgorithm(algorithm, false);
        }

        public T[,,] DoAlgorithm(string algorithm, bool reverse)
        {
            if (!string.IsNullOrEmpty(algorithm))
            {
                // Remove the C++ style comments like /*abc*/
                Regex removeCommentRegex = new Regex(@"/\*(.*?)\*/", RegexOptions.Singleline | RegexOptions.Compiled);
                algorithm = removeCommentRegex.Replace(algorithm, string.Empty);
                algorithm = algorithm.Replace('’', '\'').Replace('‘', '\'').Replace('[', '(').Replace(']', ')');
                Regex removeRegex = new Regex(@"[^a-zA-Z0-9'\(\)]", RegexOptions.Singleline | RegexOptions.Compiled);
                algorithm = removeRegex.Replace(algorithm, string.Empty);
                algorithm = ExpandAlgorithm(algorithm);

                // Replace the form of "R2'" to "R'2"
                Regex swapRegex = new Regex(@"([0-9]+)(['])", RegexOptions.Singleline | RegexOptions.Compiled);
                algorithm = swapRegex.Replace(algorithm, "$2$1");
            }

            // i: Pointer to the next unprocessed character.
            int i = 0;
            List<string> actionList = new List<string>();
            while (i < algorithm.Length)
            {
                int repeat = 1;

                // Get the next one-character token.
                string token = algorithm[i].ToString();
                i++;

                // Parse the suffix of the token (reverse or not)
                if ((i < algorithm.Length) && algorithm[i] == '\'')
                {
                    token += '\'';
                    i++;
                }

                // Parse the suffix of the token (the number of repeats)
                Regex number = new Regex("^[0-9]+", RegexOptions.Singleline | RegexOptions.Compiled);
                string subAlgorithm = algorithm.Substring(i);
                Match numberMatch = number.Match(subAlgorithm);

                if (numberMatch.Success)
                {
                    Int32.TryParse(numberMatch.Value, out repeat);
                    i += numberMatch.Value.Length;
                }

                // Perform the token and its suffix.
                for (int j = 0; j < repeat; j++)
                {
                    actionList.Add(token);
                }
            }

            if (!reverse)
            {
                for (i = 0; i < actionList.Count; i++)
                {
                    this.Move(actionList[i]);
                }
            }
            else
            {
                for (i = actionList.Count - 1; i >= 0; i--)
                {
                    string action = actionList[i];
                    if (action.EndsWith("'"))
                    {
                        action = action.TrimEnd('\'');
                    }
                    else
                    {
                        action += "'";
                    }

                    this.Move(action);
                }
            }

            return this.cube;
        }

        public static string ExpandAlgorithm(string algorithm)
        {
            StringBuilder expanded = new StringBuilder(algorithm.Length * 2);
            int m = 0, n = 0;

            if (algorithm.IndexOf('(') < 0)
            {
                expanded.Append(algorithm);
                return expanded.ToString();
            }

            while ((m = algorithm.IndexOf('(', m)) >= 0)
            {
                expanded.Append(algorithm.Substring(n, m - n));
                n = algorithm.IndexOf(')', m + 1);
                if (n < 0)
                {
                    throw new ArgumentException("公式括号不匹配", "algorithm");
                }

                // Suppress the ')'
                n++;

                Regex number = new Regex("^[0-9]+", RegexOptions.Singleline | RegexOptions.Compiled);
                string subAlgorithm = algorithm.Substring(n);
                Match numberMatch = number.Match(subAlgorithm);
                int repeat = 1;
                if (numberMatch.Success)
                {
                    Int32.TryParse(numberMatch.Value, out repeat);

                    for (int i = 0; i < repeat; i++)
                    {
                        expanded.Append(algorithm.Substring(m + 1, n - 1 - m - 1));
                    }

                    n += numberMatch.Value.Length;
                }
                else
                {
                    expanded.Append(algorithm.Substring(m + 1, n - 1 - m - 1));
                }

                m = n;
            }

            if (n > 0 && n < algorithm.Length)
            {
                expanded.Append(algorithm.Substring(n));
            }

            return expanded.ToString();
        }

        private T[, ,] Move(byte[, ,][] function)
        {
            T[, ,] result = new T[6, 3, 3];
            Array.Copy(this.cube, result, 6 * 3 * 3);
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        byte[] position = function[i, j, k];
                        if (position != null)
                        {
                            result[i, j, k] = this.cube[position[0], position[1], position[2]];
                        }
                    }
                }
            }

            this.cube = result;
            return this.cube;
        }

        public bool IsRecovered()
        {
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        Type genericType = typeof(T);
                        Type[] myInterfaces = genericType.FindInterfaces(
                            delegate(Type m, object filterCriteria) { return m.ToString() == filterCriteria.ToString(); },
                            "System.IComparable");
                        if (myInterfaces.Length > 0)
                        {
                            Comparer<T> comparer = Comparer<T>.Default;
                            if (comparer.Compare(this.cube[i, j, k], this.cube[i, 1, 1]) != 0)
                            {
                                return false;
                            }
                        }
                        else if (!Object.ReferenceEquals(this.cube[i, j, k], this.cube[i, 1, 1]))
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        public void Print()
        {
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        Console.Write(this.cube[i, j, k] + " ");
                    }

                    Console.WriteLine();
                }

                Console.WriteLine();
            }
        }
    }
}
