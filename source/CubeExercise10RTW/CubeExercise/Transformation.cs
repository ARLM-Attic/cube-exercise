using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CubeExercise
{
    public class Transformation
    {
        /// <summary>
        /// 不转（0变换）
        /// </summary>
        public static byte[, ,][] O = new byte[6, 3, 3][] {
            {
                {new byte[]{0, 0, 0}, new byte[]{0, 0, 1}, new byte[]{0, 0, 2}},
                {new byte[]{0, 1, 0}, new byte[]{0, 1, 1}, new byte[]{0, 1, 2}},
                {new byte[]{0, 2, 0}, new byte[]{0, 2, 1}, new byte[]{0, 2, 2}}},
            {
                {new byte[]{1, 0, 0}, new byte[]{1, 0, 1}, new byte[]{1, 0, 2}},
                {new byte[]{1, 1, 0}, new byte[]{1, 1, 1}, new byte[]{1, 1, 2}},
                {new byte[]{1, 2, 0}, new byte[]{1, 2, 1}, new byte[]{1, 2, 2}}},
            {
                {new byte[]{2, 0, 0}, new byte[]{2, 0, 1}, new byte[]{2, 0, 2}},
                {new byte[]{2, 1, 0}, new byte[]{2, 1, 1}, new byte[]{2, 1, 2}},
                {new byte[]{2, 2, 0}, new byte[]{2, 2, 1}, new byte[]{2, 2, 2}}},
            {
                {new byte[]{3, 0, 0}, new byte[]{3, 0, 1}, new byte[]{3, 0, 2}},
                {new byte[]{3, 1, 0}, new byte[]{3, 1, 1}, new byte[]{3, 1, 2}},
                {new byte[]{3, 2, 0}, new byte[]{3, 2, 1}, new byte[]{3, 2, 2}}},
            {
                {new byte[]{4, 0, 0}, new byte[]{4, 0, 1}, new byte[]{4, 0, 2}},
                {new byte[]{4, 1, 0}, new byte[]{4, 1, 1}, new byte[]{4, 1, 2}},
                {new byte[]{4, 2, 0}, new byte[]{4, 2, 1}, new byte[]{4, 2, 2}}},
            {
                {new byte[]{5, 0, 0}, new byte[]{5, 0, 1}, new byte[]{5, 0, 2}},
                {new byte[]{5, 1, 0}, new byte[]{5, 1, 1}, new byte[]{5, 1, 2}},
                {new byte[]{5, 2, 0}, new byte[]{5, 2, 1}, new byte[]{5, 2, 2}}}
        };

        /// <summary>
        /// R : 右面单层顺时针转90度
        /// </summary>
        public static byte[, ,][] R = new byte[6, 3, 3][] {
            {
                {null, null, null, },
                {null, null, null, },
                {null, null, null, },
            },
            {
                {null, null, new byte[]{5, 0, 2}, },
                {null, null, new byte[]{5, 1, 2}, },
                {null, null, new byte[]{5, 2, 2}, },
            },
            {
                {new byte[]{2, 2, 0}, new byte[]{2, 1, 0}, new byte[]{2, 0, 0}, },
                {new byte[]{2, 2, 1}, null, new byte[]{2, 0, 1}, },
                {new byte[]{2, 2, 2}, new byte[]{2, 1, 2}, new byte[]{2, 0, 2}, },
            },
            {
                {new byte[]{4, 2, 2}, null, null, },
                {new byte[]{4, 1, 2}, null, null, },
                {new byte[]{4, 0, 2}, null, null, },
            },
            {
                {null, null, new byte[]{1, 0, 2}, },
                {null, null, new byte[]{1, 1, 2}, },
                {null, null, new byte[]{1, 2, 2}, },
            },
            {
                {null, null, new byte[]{3, 2, 0}, },
                {null, null, new byte[]{3, 1, 0}, },
                {null, null, new byte[]{3, 0, 0}, },
            },
        };

        public static byte[, ,][] R_ = new byte[6, 3, 3][] {
            {
                {null, null, null, },
                {null, null, null, },
                {null, null, null, },
            },
            {
                {null, null, new byte[]{4, 0, 2}, },
                {null, null, new byte[]{4, 1, 2}, },
                {null, null, new byte[]{4, 2, 2}, },
            },
            {
                {new byte[]{2, 0, 2}, new byte[]{2, 1, 2}, new byte[]{2, 2, 2}, },
                {new byte[]{2, 0, 1}, null, new byte[]{2, 2, 1}, },
                {new byte[]{2, 0, 0}, new byte[]{2, 1, 0}, new byte[]{2, 2, 0}, },
            },
            {
                {new byte[]{5, 2, 2}, null, null, },
                {new byte[]{5, 1, 2}, null, null, },
                {new byte[]{5, 0, 2}, null, null, },
            },
            {
                {null, null, new byte[]{3, 2, 0}, },
                {null, null, new byte[]{3, 1, 0}, },
                {null, null, new byte[]{3, 0, 0}, },
            },
            {
                {null, null, new byte[]{1, 0, 2}, },
                {null, null, new byte[]{1, 1, 2}, },
                {null, null, new byte[]{1, 2, 2}, },
            },
        };

        public static byte[, ,][] r = new byte[6, 3, 3][]
        {
            {
                {null, null, null, },
                {null, null, null, },
                {null, null, null, },
            },
            {
                {null, new byte[]{5, 0, 1}, new byte[]{5, 0, 2}, },
                {null, new byte[]{5, 1, 1}, new byte[]{5, 1, 2}, },
                {null, new byte[]{5, 2, 1}, new byte[]{5, 2, 2}, },
            },
            {
                {new byte[]{2, 2, 0}, new byte[]{2, 1, 0}, new byte[]{2, 0, 0}, },
                {new byte[]{2, 2, 1}, null, new byte[]{2, 0, 1}, },
                {new byte[]{2, 2, 2}, new byte[]{2, 1, 2}, new byte[]{2, 0, 2}, },
            },
            {
                {new byte[]{4, 2, 2}, new byte[]{4, 2, 1}, null, },
                {new byte[]{4, 1, 2}, new byte[]{4, 1, 1}, null, },
                {new byte[]{4, 0, 2}, new byte[]{4, 0, 1}, null, },
            },
            {
                {null, new byte[]{1, 0, 1}, new byte[]{1, 0, 2}, },
                {null, new byte[]{1, 1, 1}, new byte[]{1, 1, 2}, },
                {null, new byte[]{1, 2, 1}, new byte[]{1, 2, 2}, },
            },
            {
                {null, new byte[]{3, 2, 1}, new byte[]{3, 2, 0}, },
                {null, new byte[]{3, 1, 1}, new byte[]{3, 1, 0}, },
                {null, new byte[]{3, 0, 1}, new byte[]{3, 0, 0}, },
            },
        };

        public static byte[, ,][] r_ = new byte[6, 3, 3][] {
            {
                {null, null, null, },
                {null, null, null, },
                {null, null, null, },
            },
            {
                {null, new byte[]{4, 0, 1}, new byte[]{4, 0, 2}, },
                {null, new byte[]{4, 1, 1}, new byte[]{4, 1, 2}, },
                {null, new byte[]{4, 2, 1}, new byte[]{4, 2, 2}, },
            },
            {
                {new byte[]{2, 0, 2}, new byte[]{2, 1, 2}, new byte[]{2, 2, 2}, },
                {new byte[]{2, 0, 1}, null, new byte[]{2, 2, 1}, },
                {new byte[]{2, 0, 0}, new byte[]{2, 1, 0}, new byte[]{2, 2, 0}, },
            },
            {
                {new byte[]{5, 2, 2}, new byte[]{5, 2, 1}, null, },
                {new byte[]{5, 1, 2}, new byte[]{5, 1, 1}, null, },
                {new byte[]{5, 0, 2}, new byte[]{5, 0, 1}, null, },
            },
            {
                {null, new byte[]{3, 2, 1}, new byte[]{3, 2, 0}, },
                {null, new byte[]{3, 1, 1}, new byte[]{3, 1, 0}, },
                {null, new byte[]{3, 0, 1}, new byte[]{3, 0, 0}, },
            },
            {
                {null, new byte[]{1, 0, 1}, new byte[]{1, 0, 2}, },
                {null, new byte[]{1, 1, 1}, new byte[]{1, 1, 2}, },
                {null, new byte[]{1, 2, 1}, new byte[]{1, 2, 2}, },
            },
        };

        /// <summary>
        /// L : 左面单层顺时针转90度
        /// </summary>
        public static byte[, ,][] L = new byte[6, 3, 3][] {
            {
                {new byte[]{0, 2, 0}, new byte[]{0, 1, 0}, new byte[]{0, 0, 0}},
                {new byte[]{0, 2, 1}, null,                new byte[]{0, 0, 1}},
                {new byte[]{0, 2, 2}, new byte[]{0, 1, 2}, new byte[]{0, 0, 2}}},
            {
                {new byte[]{4, 0, 0}, null, null},
                {new byte[]{4, 1, 0}, null, null},
                {new byte[]{4, 2, 0}, null, null}},
            {
                {null, null, null},
                {null, null, null},
                {null, null, null}},
            {
                {null, null, new byte[]{5, 2, 0}},
                {null, null, new byte[]{5, 1, 0}},
                {null, null, new byte[]{5, 0, 0}}},
            {
                {new byte[]{3, 2, 2}, null, null},
                {new byte[]{3, 1, 2}, null, null},
                {new byte[]{3, 0, 2}, null, null}},
            {
                {new byte[]{1, 0, 0}, null, null},
                {new byte[]{1, 1, 0}, null, null},
                {new byte[]{1, 2, 0}, null, null}}
        };

        public static byte[, ,][] L_ = new byte[6, 3, 3][] {
            {
                {new byte[]{0, 0, 2}, new byte[]{0, 1, 2}, new byte[]{0, 2, 2}, },
                {new byte[]{0, 0, 1}, null, new byte[]{0, 2, 1}, },
                {new byte[]{0, 0, 0}, new byte[]{0, 1, 0}, new byte[]{0, 2, 0}, },
            },
            {
                {new byte[]{5, 0, 0}, null, null, },
                {new byte[]{5, 1, 0}, null, null, },
                {new byte[]{5, 2, 0}, null, null, },
            },
            {
                {null, null, null, },
                {null, null, null, },
                {null, null, null, },
            },
            {
                {null, null, new byte[]{4, 2, 0}, },
                {null, null, new byte[]{4, 1, 0}, },
                {null, null, new byte[]{4, 0, 0}, },
            },
            {
                {new byte[]{1, 0, 0}, null, null, },
                {new byte[]{1, 1, 0}, null, null, },
                {new byte[]{1, 2, 0}, null, null, },
            },
            {
                {new byte[]{3, 2, 2}, null, null, },
                {new byte[]{3, 1, 2}, null, null, },
                {new byte[]{3, 0, 2}, null, null, },
            },
        };

        public static byte[, ,][] l = new byte[6, 3, 3][] {
            {
                {new byte[]{0, 2, 0}, new byte[]{0, 1, 0}, new byte[]{0, 0, 0}, },
                {new byte[]{0, 2, 1}, null, new byte[]{0, 0, 1}, },
                {new byte[]{0, 2, 2}, new byte[]{0, 1, 2}, new byte[]{0, 0, 2}, },
            },
            {
                {new byte[]{4, 0, 0}, new byte[]{4, 0, 1}, null, },
                {new byte[]{4, 1, 0}, new byte[]{4, 1, 1}, null, },
                {new byte[]{4, 2, 0}, new byte[]{4, 2, 1}, null, },
            },
            {
                {null, null, null, },
                {null, null, null, },
                {null, null, null, },
            },
            {
                {null, new byte[]{5, 2, 1}, new byte[]{5, 2, 0}, },
                {null, new byte[]{5, 1, 1}, new byte[]{5, 1, 0}, },
                {null, new byte[]{5, 0, 1}, new byte[]{5, 0, 0}, },
            },
            {
                {new byte[]{3, 2, 2}, new byte[]{3, 2, 1}, null, },
                {new byte[]{3, 1, 2}, new byte[]{3, 1, 1}, null, },
                {new byte[]{3, 0, 2}, new byte[]{3, 0, 1}, null, },
            },
            {
                {new byte[]{1, 0, 0}, new byte[]{1, 0, 1}, null, },
                {new byte[]{1, 1, 0}, new byte[]{1, 1, 1}, null, },
                {new byte[]{1, 2, 0}, new byte[]{1, 2, 1}, null, },
            },
        };

        public static byte[, ,][] l_ = new byte[6, 3, 3][] {
            {
                {new byte[]{0, 0, 2}, new byte[]{0, 1, 2}, new byte[]{0, 2, 2}, },
                {new byte[]{0, 0, 1}, null, new byte[]{0, 2, 1}, },
                {new byte[]{0, 0, 0}, new byte[]{0, 1, 0}, new byte[]{0, 2, 0}, },
            },
            {
                {new byte[]{5, 0, 0}, new byte[]{5, 0, 1}, null, },
                {new byte[]{5, 1, 0}, new byte[]{5, 1, 1}, null, },
                {new byte[]{5, 2, 0}, new byte[]{5, 2, 1}, null, },
            },
            {
                {null, null, null, },
                {null, null, null, },
                {null, null, null, },
            },
            {
                {null, new byte[]{4, 2, 1}, new byte[]{4, 2, 0}, },
                {null, new byte[]{4, 1, 1}, new byte[]{4, 1, 0}, },
                {null, new byte[]{4, 0, 1}, new byte[]{4, 0, 0}, },
            },
            {
                {new byte[]{1, 0, 0}, new byte[]{1, 0, 1}, null, },
                {new byte[]{1, 1, 0}, new byte[]{1, 1, 1}, null, },
                {new byte[]{1, 2, 0}, new byte[]{1, 2, 1}, null, },
            },
            {
                {new byte[]{3, 2, 2}, new byte[]{3, 2, 1}, null, },
                {new byte[]{3, 1, 2}, new byte[]{3, 1, 1}, null, },
                {new byte[]{3, 0, 2}, new byte[]{3, 0, 1}, null, },
            },
        };

        /// <summary>
        /// U : 上面单层顺时针转90度
        /// </summary>
        public static byte[, ,][] U = new byte[6, 3, 3][] {
            {
                {new byte[]{1, 0, 0}, new byte[]{1, 0, 1}, new byte[]{1, 0, 2}},
                {null, null, null},
                {null, null, null}},
            {
                {new byte[]{2, 0, 0}, new byte[]{2, 0, 1}, new byte[]{2, 0, 2}},
                {null, null, null},
                {null, null, null}},
            {
                {new byte[]{3, 0, 0}, new byte[]{3, 0, 1}, new byte[]{3, 0, 2}},
                {null, null, null},
                {null, null, null}},
            {
                {new byte[]{0, 0, 0}, new byte[]{0, 0, 1}, new byte[]{0, 0, 2}},
                {null, null, null},
                {null, null, null}},
            {
                {new byte[]{4, 2, 0}, new byte[]{4, 1, 0}, new byte[]{4, 0, 0}},
                {new byte[]{4, 2, 1}, null,                new byte[]{4, 0, 1}},
                {new byte[]{4, 2, 2}, new byte[]{4, 1, 2}, new byte[]{4, 0, 2}}},
            {
                {null, null, null},
                {null, null, null},
                {null, null, null}}
        };

        public static byte[, ,][] U_ = new byte[6, 3, 3][] {
            {
                {new byte[]{3, 0, 0}, new byte[]{3, 0, 1}, new byte[]{3, 0, 2}, },
                {null, null, null, },
                {null, null, null, },
            },
            {
                {new byte[]{0, 0, 0}, new byte[]{0, 0, 1}, new byte[]{0, 0, 2}, },
                {null, null, null, },
                {null, null, null, },
            },
            {
                {new byte[]{1, 0, 0}, new byte[]{1, 0, 1}, new byte[]{1, 0, 2}, },
                {null, null, null, },
                {null, null, null, },
            },
            {
                {new byte[]{2, 0, 0}, new byte[]{2, 0, 1}, new byte[]{2, 0, 2}, },
                {null, null, null, },
                {null, null, null, },
            },
            {
                {new byte[]{4, 0, 2}, new byte[]{4, 1, 2}, new byte[]{4, 2, 2}, },
                {new byte[]{4, 0, 1}, null, new byte[]{4, 2, 1}, },
                {new byte[]{4, 0, 0}, new byte[]{4, 1, 0}, new byte[]{4, 2, 0}, },
            },
            {
                {null, null, null, },
                {null, null, null, },
                {null, null, null, },
            },
        };

        public static byte[, ,][] u = new byte[6, 3, 3][]{
            {
                {new byte[]{1, 0, 0}, new byte[]{1, 0, 1}, new byte[]{1, 0, 2}, },
                {new byte[]{1, 1, 0}, new byte[]{1, 1, 1}, new byte[]{1, 1, 2}, },
                {null, null, null, },
            },
            {
                {new byte[]{2, 0, 0}, new byte[]{2, 0, 1}, new byte[]{2, 0, 2}, },
                {new byte[]{2, 1, 0}, new byte[]{2, 1, 1}, new byte[]{2, 1, 2}, },
                {null, null, null, },
            },
            {
                {new byte[]{3, 0, 0}, new byte[]{3, 0, 1}, new byte[]{3, 0, 2}, },
                {new byte[]{3, 1, 0}, new byte[]{3, 1, 1}, new byte[]{3, 1, 2}, },
                {null, null, null, },
            },
            {
                {new byte[]{0, 0, 0}, new byte[]{0, 0, 1}, new byte[]{0, 0, 2}, },
                {new byte[]{0, 1, 0}, new byte[]{0, 1, 1}, new byte[]{0, 1, 2}, },
                {null, null, null, },
            },
            {
                {new byte[]{4, 2, 0}, new byte[]{4, 1, 0}, new byte[]{4, 0, 0}, },
                {new byte[]{4, 2, 1}, null, new byte[]{4, 0, 1}, },
                {new byte[]{4, 2, 2}, new byte[]{4, 1, 2}, new byte[]{4, 0, 2}, },
            },
            {
                {null, null, null, },
                {null, null, null, },
                {null, null, null, },
            },
        };

        public static byte[, ,][] u_ = new byte[6, 3, 3][] {
            {
                {new byte[]{3, 0, 0}, new byte[]{3, 0, 1}, new byte[]{3, 0, 2}, },
                {new byte[]{3, 1, 0}, new byte[]{3, 1, 1}, new byte[]{3, 1, 2}, },
                {null, null, null, },
            },
            {
                {new byte[]{0, 0, 0}, new byte[]{0, 0, 1}, new byte[]{0, 0, 2}, },
                {new byte[]{0, 1, 0}, new byte[]{0, 1, 1}, new byte[]{0, 1, 2}, },
                {null, null, null, },
            },
            {
                {new byte[]{1, 0, 0}, new byte[]{1, 0, 1}, new byte[]{1, 0, 2}, },
                {new byte[]{1, 1, 0}, new byte[]{1, 1, 1}, new byte[]{1, 1, 2}, },
                {null, null, null, },
            },
            {
                {new byte[]{2, 0, 0}, new byte[]{2, 0, 1}, new byte[]{2, 0, 2}, },
                {new byte[]{2, 1, 0}, new byte[]{2, 1, 1}, new byte[]{2, 1, 2}, },
                {null, null, null, },
            },
            {
                {new byte[]{4, 0, 2}, new byte[]{4, 1, 2}, new byte[]{4, 2, 2}, },
                {new byte[]{4, 0, 1}, null, new byte[]{4, 2, 1}, },
                {new byte[]{4, 0, 0}, new byte[]{4, 1, 0}, new byte[]{4, 2, 0}, },
            },
            {
                {null, null, null, },
                {null, null, null, },
                {null, null, null, },
            },
        };

        /// <summary>
        /// D : 下面单层顺时针转90度
        /// </summary>
        public static byte[, ,][] D = new byte[6, 3, 3][] {
            {
                {null, null, null},
                {null, null, null},
                {new byte[]{3, 2, 0}, new byte[]{3, 2, 1}, new byte[]{3, 2, 2}}},
            {
                {null, null, null},
                {null, null, null},
                {new byte[]{0, 2, 0}, new byte[]{0, 2, 1}, new byte[]{0, 2, 2}}},
            {
                {null, null, null},
                {null, null, null},
                {new byte[]{1, 2, 0}, new byte[]{1, 2, 1}, new byte[]{1, 2, 2}}},
            {
                {null, null, null},
                {null, null, null},
                {new byte[]{2, 2, 0}, new byte[]{2, 2, 1}, new byte[]{2, 2, 2}}},
            {
                {null, null, null},
                {null, null, null},
                {null, null, null}},
            {
                {new byte[]{5, 2, 0}, new byte[]{5, 1, 0}, new byte[]{5, 0, 0}},
                {new byte[]{5, 2, 1}, null,                new byte[]{5, 0, 1}},
                {new byte[]{5, 2, 2}, new byte[]{5, 1, 2}, new byte[]{5, 0, 2}}}
        };

        public static byte[, ,][] D_ = new byte[6, 3, 3][] {
            {
                {null, null, null, },
                {null, null, null, },
                {new byte[]{1, 2, 0}, new byte[]{1, 2, 1}, new byte[]{1, 2, 2}, },
            },
            {
                {null, null, null, },
                {null, null, null, },
                {new byte[]{2, 2, 0}, new byte[]{2, 2, 1}, new byte[]{2, 2, 2}, },
            },
            {
                {null, null, null, },
                {null, null, null, },
                {new byte[]{3, 2, 0}, new byte[]{3, 2, 1}, new byte[]{3, 2, 2}, },
            },
            {
                {null, null, null, },
                {null, null, null, },
                {new byte[]{0, 2, 0}, new byte[]{0, 2, 1}, new byte[]{0, 2, 2}, },
            },
            {
                {null, null, null, },
                {null, null, null, },
                {null, null, null, },
            },
            {
                {new byte[]{5, 0, 2}, new byte[]{5, 1, 2}, new byte[]{5, 2, 2}, },
                {new byte[]{5, 0, 1}, null, new byte[]{5, 2, 1}, },
                {new byte[]{5, 0, 0}, new byte[]{5, 1, 0}, new byte[]{5, 2, 0}, },
            },
        };

        public static byte[, ,][] d = new byte[6, 3, 3][] {
            {
                {null, null, null, },
                {new byte[]{3, 1, 0}, new byte[]{3, 1, 1}, new byte[]{3, 1, 2}, },
                {new byte[]{3, 2, 0}, new byte[]{3, 2, 1}, new byte[]{3, 2, 2}, },
            },
            {
                {null, null, null, },
                {new byte[]{0, 1, 0}, new byte[]{0, 1, 1}, new byte[]{0, 1, 2}, },
                {new byte[]{0, 2, 0}, new byte[]{0, 2, 1}, new byte[]{0, 2, 2}, },
            },
            {
                {null, null, null, },
                {new byte[]{1, 1, 0}, new byte[]{1, 1, 1}, new byte[]{1, 1, 2}, },
                {new byte[]{1, 2, 0}, new byte[]{1, 2, 1}, new byte[]{1, 2, 2}, },
            },
            {
                {null, null, null, },
                {new byte[]{2, 1, 0}, new byte[]{2, 1, 1}, new byte[]{2, 1, 2}, },
                {new byte[]{2, 2, 0}, new byte[]{2, 2, 1}, new byte[]{2, 2, 2}, },
            },
            {
                {null, null, null, },
                {null, null, null, },
                {null, null, null, },
            },
            {
                {new byte[]{5, 2, 0}, new byte[]{5, 1, 0}, new byte[]{5, 0, 0}, },
                {new byte[]{5, 2, 1}, null, new byte[]{5, 0, 1}, },
                {new byte[]{5, 2, 2}, new byte[]{5, 1, 2}, new byte[]{5, 0, 2}, },
            },
        };

        public static byte[, ,][] d_ = new byte[6, 3, 3][] {
            {
                {null, null, null, },
                {new byte[]{1, 1, 0}, new byte[]{1, 1, 1}, new byte[]{1, 1, 2}, },
                {new byte[]{1, 2, 0}, new byte[]{1, 2, 1}, new byte[]{1, 2, 2}, },
            },
            {
                {null, null, null, },
                {new byte[]{2, 1, 0}, new byte[]{2, 1, 1}, new byte[]{2, 1, 2}, },
                {new byte[]{2, 2, 0}, new byte[]{2, 2, 1}, new byte[]{2, 2, 2}, },
            },
            {
                {null, null, null, },
                {new byte[]{3, 1, 0}, new byte[]{3, 1, 1}, new byte[]{3, 1, 2}, },
                {new byte[]{3, 2, 0}, new byte[]{3, 2, 1}, new byte[]{3, 2, 2}, },
            },
            {
                {null, null, null, },
                {new byte[]{0, 1, 0}, new byte[]{0, 1, 1}, new byte[]{0, 1, 2}, },
                {new byte[]{0, 2, 0}, new byte[]{0, 2, 1}, new byte[]{0, 2, 2}, },
            },
            {
                {null, null, null, },
                {null, null, null, },
                {null, null, null, },
            },
            {
                {new byte[]{5, 0, 2}, new byte[]{5, 1, 2}, new byte[]{5, 2, 2}, },
                {new byte[]{5, 0, 1}, null, new byte[]{5, 2, 1}, },
                {new byte[]{5, 0, 0}, new byte[]{5, 1, 0}, new byte[]{5, 2, 0}, },
            },
        };

        /// <summary>
        /// F : 前面单层顺时针转90度
        /// </summary>
        public static byte[, ,][] F = new byte[6, 3, 3][] {
            {
                {null, null, new byte[]{5, 0, 0}},
                {null, null, new byte[]{5, 0, 1}},
                {null, null, new byte[]{5, 0, 2}}},
            {
                {new byte[]{1, 2, 0}, new byte[]{1, 1, 0}, new byte[]{1, 0, 0}},
                {new byte[]{1, 2, 1}, null,                new byte[]{1, 0, 1}},
                {new byte[]{1, 2, 2}, new byte[]{1, 1, 2}, new byte[]{1, 0, 2}}},
            {
                {new byte[]{4, 2, 0}, null, null,},
                {new byte[]{4, 2, 1}, null, null,},
                {new byte[]{4, 2, 2}, null, null,}},
            {
                {null, null, null},
                {null, null, null},
                {null, null, null}},
            {
                {null, null, null},
                {null, null, null},
                {new byte[]{0, 2, 2}, new byte[]{0, 1, 2}, new byte[]{0, 0, 2}}},
            {
                {new byte[]{2, 2, 0}, new byte[]{2, 1, 0}, new byte[]{2, 0, 0}},
                {null, null, null},
                {null, null, null}}
        };

        public static byte[, ,][] F_ = new byte[6, 3, 3][] {
            {
                {null, null, new byte[]{4, 2, 2}, },
                {null, null, new byte[]{4, 2, 1}, },
                {null, null, new byte[]{4, 2, 0}, },
            },
            {
                {new byte[]{1, 0, 2}, new byte[]{1, 1, 2}, new byte[]{1, 2, 2}, },
                {new byte[]{1, 0, 1}, null, new byte[]{1, 2, 1}, },
                {new byte[]{1, 0, 0}, new byte[]{1, 1, 0}, new byte[]{1, 2, 0}, },
            },
            {
                {new byte[]{5, 0, 2}, null, null, },
                {new byte[]{5, 0, 1}, null, null, },
                {new byte[]{5, 0, 0}, null, null, },
            },
            {
                {null, null, null, },
                {null, null, null, },
                {null, null, null, },
            },
            {
                {null, null, null, },
                {null, null, null, },
                {new byte[]{2, 0, 0}, new byte[]{2, 1, 0}, new byte[]{2, 2, 0}, },
            },
            {
                {new byte[]{0, 0, 2}, new byte[]{0, 1, 2}, new byte[]{0, 2, 2}, },
                {null, null, null, },
                {null, null, null, },
            },
        };

        public static byte[, ,][] f = new byte[6, 3, 3][] {
            {
                {null, new byte[]{5, 1, 0}, new byte[]{5, 0, 0}, },
                {null, new byte[]{5, 1, 1}, new byte[]{5, 0, 1}, },
                {null, new byte[]{5, 1, 2}, new byte[]{5, 0, 2}, },
            },
            {
                {new byte[]{1, 2, 0}, new byte[]{1, 1, 0}, new byte[]{1, 0, 0}, },
                {new byte[]{1, 2, 1}, null, new byte[]{1, 0, 1}, },
                {new byte[]{1, 2, 2}, new byte[]{1, 1, 2}, new byte[]{1, 0, 2}, },
            },
            {
                {new byte[]{4, 2, 0}, new byte[]{4, 1, 0}, null, },
                {new byte[]{4, 2, 1}, new byte[]{4, 1, 1}, null, },
                {new byte[]{4, 2, 2}, new byte[]{4, 1, 2}, null, },
            },
            {
                {null, null, null, },
                {null, null, null, },
                {null, null, null, },
            },
            {
                {null, null, null, },
                {new byte[]{0, 2, 1}, new byte[]{0, 1, 1}, new byte[]{0, 0, 1}, },
                {new byte[]{0, 2, 2}, new byte[]{0, 1, 2}, new byte[]{0, 0, 2}, },
            },
            {
                {new byte[]{2, 2, 0}, new byte[]{2, 1, 0}, new byte[]{2, 0, 0}, },
                {new byte[]{2, 2, 1}, new byte[]{2, 1, 1}, new byte[]{2, 0, 1}, },
                {null, null, null, },
            },
        };

        public static byte[, ,][] f_ = new byte[6, 3, 3][] {
            {
                {null, new byte[]{4, 1, 2}, new byte[]{4, 2, 2}, },
                {null, new byte[]{4, 1, 1}, new byte[]{4, 2, 1}, },
                {null, new byte[]{4, 1, 0}, new byte[]{4, 2, 0}, },
            },
            {
                {new byte[]{1, 0, 2}, new byte[]{1, 1, 2}, new byte[]{1, 2, 2}, },
                {new byte[]{1, 0, 1}, null, new byte[]{1, 2, 1}, },
                {new byte[]{1, 0, 0}, new byte[]{1, 1, 0}, new byte[]{1, 2, 0}, },
            },
            {
                {new byte[]{5, 0, 2}, new byte[]{5, 1, 2}, null, },
                {new byte[]{5, 0, 1}, new byte[]{5, 1, 1}, null, },
                {new byte[]{5, 0, 0}, new byte[]{5, 1, 0}, null, },
            },
            {
                {null, null, null, },
                {null, null, null, },
                {null, null, null, },
            },
            {
                {null, null, null, },
                {new byte[]{2, 0, 1}, new byte[]{2, 1, 1}, new byte[]{2, 2, 1}, },
                {new byte[]{2, 0, 0}, new byte[]{2, 1, 0}, new byte[]{2, 2, 0}, },
            },
            {
                {new byte[]{0, 0, 2}, new byte[]{0, 1, 2}, new byte[]{0, 2, 2}, },
                {new byte[]{0, 0, 1}, new byte[]{0, 1, 1}, new byte[]{0, 2, 1}, },
                {null, null, null, },
            },
        };

        /// <summary>
        /// B : 后面单层顺时针转90度
        /// </summary>
        public static byte[, ,][] B = new byte[6, 3, 3][] {
            {
                {new byte[]{4, 0, 2}, null, null},
                {new byte[]{4, 0, 1}, null, null},
                {new byte[]{4, 0, 0}, null, null}},
            {
                {null, null, null},
                {null, null, null},
                {null, null, null}},
            {
                {null, null, new byte[]{5, 2, 2}},
                {null, null, new byte[]{5, 2, 1}},
                {null, null, new byte[]{5, 2, 0}}},
            {
                {new byte[]{3, 2, 0}, new byte[]{3, 1, 0}, new byte[]{3, 0, 0}},
                {new byte[]{3, 2, 1}, null,                new byte[]{3, 0, 1}},
                {new byte[]{3, 2, 2}, new byte[]{3, 1, 2}, new byte[]{3, 0, 2}}},
            {
                {new byte[]{2, 0, 2}, new byte[]{2, 1, 2}, new byte[]{2, 2, 2}},
                {null, null, null},
                {null, null, null}},
            {
                {null, null, null},
                {null, null, null},
                {new byte[]{0, 0, 0}, new byte[]{0, 1, 0}, new byte[]{0, 2, 0}}}
        };

        public static byte[, ,][] B_ = new byte[6, 3, 3][] {
            {
                {new byte[]{5, 2, 0}, null, null, },
                {new byte[]{5, 2, 1}, null, null, },
                {new byte[]{5, 2, 2}, null, null, },
            },
            {
                {null, null, null, },
                {null, null, null, },
                {null, null, null, },
            },
            {
                {null, null, new byte[]{4, 0, 0}, },
                {null, null, new byte[]{4, 0, 1}, },
                {null, null, new byte[]{4, 0, 2}, },
            },
            {
                {new byte[]{3, 0, 2}, new byte[]{3, 1, 2}, new byte[]{3, 2, 2}, },
                {new byte[]{3, 0, 1}, null, new byte[]{3, 2, 1}, },
                {new byte[]{3, 0, 0}, new byte[]{3, 1, 0}, new byte[]{3, 2, 0}, },
            },
            {
                {new byte[]{0, 2, 0}, new byte[]{0, 1, 0}, new byte[]{0, 0, 0}, },
                {null, null, null, },
                {null, null, null, },
            },
            {
                {null, null, null, },
                {null, null, null, },
                {new byte[]{2, 2, 2}, new byte[]{2, 1, 2}, new byte[]{2, 0, 2}, },
            },
        };

        public static byte[, ,][] b = new byte[6, 3, 3][] {
            {
                {new byte[]{4, 0, 2}, new byte[]{4, 1, 2}, null, },
                {new byte[]{4, 0, 1}, new byte[]{4, 1, 1}, null, },
                {new byte[]{4, 0, 0}, new byte[]{4, 1, 0}, null, },
            },
            {
                {null, null, null, },
                {null, null, null, },
                {null, null, null, },
            },
            {
                {null, new byte[]{5, 1, 2}, new byte[]{5, 2, 2}, },
                {null, new byte[]{5, 1, 1}, new byte[]{5, 2, 1}, },
                {null, new byte[]{5, 1, 0}, new byte[]{5, 2, 0}, },
            },
            {
                {new byte[]{3, 2, 0}, new byte[]{3, 1, 0}, new byte[]{3, 0, 0}, },
                {new byte[]{3, 2, 1}, null, new byte[]{3, 0, 1}, },
                {new byte[]{3, 2, 2}, new byte[]{3, 1, 2}, new byte[]{3, 0, 2}, },
            },
            {
                {new byte[]{2, 0, 2}, new byte[]{2, 1, 2}, new byte[]{2, 2, 2}, },
                {new byte[]{2, 0, 1}, new byte[]{2, 1, 1}, new byte[]{2, 2, 1}, },
                {null, null, null, },
            },
            {
                {null, null, null, },
                {new byte[]{0, 0, 1}, new byte[]{0, 1, 1}, new byte[]{0, 2, 1}, },
                {new byte[]{0, 0, 0}, new byte[]{0, 1, 0}, new byte[]{0, 2, 0}, },
            },
        };

        public static byte[, ,][] b_ = new byte[6, 3, 3][] {
            {
                {new byte[]{5, 2, 0}, new byte[]{5, 1, 0}, null, },
                {new byte[]{5, 2, 1}, new byte[]{5, 1, 1}, null, },
                {new byte[]{5, 2, 2}, new byte[]{5, 1, 2}, null, },
            },
            {
                {null, null, null, },
                {null, null, null, },
                {null, null, null, },
            },
            {
                {null, new byte[]{4, 1, 0}, new byte[]{4, 0, 0}, },
                {null, new byte[]{4, 1, 1}, new byte[]{4, 0, 1}, },
                {null, new byte[]{4, 1, 2}, new byte[]{4, 0, 2}, },
            },
            {
                {new byte[]{3, 0, 2}, new byte[]{3, 1, 2}, new byte[]{3, 2, 2}, },
                {new byte[]{3, 0, 1}, null, new byte[]{3, 2, 1}, },
                {new byte[]{3, 0, 0}, new byte[]{3, 1, 0}, new byte[]{3, 2, 0}, },
            },
            {
                {new byte[]{0, 2, 0}, new byte[]{0, 1, 0}, new byte[]{0, 0, 0}, },
                {new byte[]{0, 2, 1}, new byte[]{0, 1, 1}, new byte[]{0, 0, 1}, },
                {null, null, null, },
            },
            {
                {null, null, null, },
                {new byte[]{2, 2, 1}, new byte[]{2, 1, 1}, new byte[]{2, 0, 1}, },
                {new byte[]{2, 2, 2}, new byte[]{2, 1, 2}, new byte[]{2, 0, 2}, },
            },
        };

        /// <summary>
        /// X :整体向R方向转
        /// </summary>
        public static byte[, ,][] X = new byte[6, 3, 3][] {
            {
                {new byte[]{0, 0, 2}, new byte[]{0, 1, 2}, new byte[]{0, 2, 2}},
                {new byte[]{0, 0, 1}, null,                new byte[]{0, 2, 1}},
                {new byte[]{0, 0, 0}, new byte[]{0, 1, 0}, new byte[]{0, 2, 0}}},
            {
                {new byte[]{5, 0, 0}, new byte[]{5, 0, 1}, new byte[]{5, 0, 2}},
                {new byte[]{5, 1, 0}, new byte[]{5, 1, 1}, new byte[]{5, 1, 2}},
                {new byte[]{5, 2, 0}, new byte[]{5, 2, 1}, new byte[]{5, 2, 2}}},
            {
                {new byte[]{2, 2, 0}, new byte[]{2, 1, 0}, new byte[]{2, 0, 0}},
                {new byte[]{2, 2, 1}, null,                new byte[]{2, 0, 1}},
                {new byte[]{2, 2, 2}, new byte[]{2, 1, 2}, new byte[]{2, 0, 2}}},
            {
                {new byte[]{4, 2, 2}, new byte[]{4, 2, 1}, new byte[]{4, 2, 0}},
                {new byte[]{4, 1, 2}, new byte[]{4, 1, 1}, new byte[]{4, 1, 0}},
                {new byte[]{4, 0, 2}, new byte[]{4, 0, 1}, new byte[]{4, 0, 0}}},
            {
                {new byte[]{1, 0, 0}, new byte[]{1, 0, 1}, new byte[]{1, 0, 2}},
                {new byte[]{1, 1, 0}, new byte[]{1, 1, 1}, new byte[]{1, 1, 2}},
                {new byte[]{1, 2, 0}, new byte[]{1, 2, 1}, new byte[]{1, 2, 2}}},
            {
                {new byte[]{3, 2, 2}, new byte[]{3, 2, 1}, new byte[]{3, 2, 0}},
                {new byte[]{3, 1, 2}, new byte[]{3, 1, 1}, new byte[]{3, 1, 0}},
                {new byte[]{3, 0, 2}, new byte[]{3, 0, 1}, new byte[]{3, 0, 0}}}
        };

        public static byte[, ,][] X_ = new byte[6, 3, 3][] {
            {
                {new byte[]{0, 2, 0}, new byte[]{0, 1, 0}, new byte[]{0, 0, 0}, },
                {new byte[]{0, 2, 1}, null, new byte[]{0, 0, 1}, },
                {new byte[]{0, 2, 2}, new byte[]{0, 1, 2}, new byte[]{0, 0, 2}, },
            },
            {
                {new byte[]{4, 0, 0}, new byte[]{4, 0, 1}, new byte[]{4, 0, 2}, },
                {new byte[]{4, 1, 0}, new byte[]{4, 1, 1}, new byte[]{4, 1, 2}, },
                {new byte[]{4, 2, 0}, new byte[]{4, 2, 1}, new byte[]{4, 2, 2}, },
            },
            {
                {new byte[]{2, 0, 2}, new byte[]{2, 1, 2}, new byte[]{2, 2, 2}, },
                {new byte[]{2, 0, 1}, null, new byte[]{2, 2, 1}, },
                {new byte[]{2, 0, 0}, new byte[]{2, 1, 0}, new byte[]{2, 2, 0}, },
            },
            {
                {new byte[]{5, 2, 2}, new byte[]{5, 2, 1}, new byte[]{5, 2, 0}, },
                {new byte[]{5, 1, 2}, new byte[]{5, 1, 1}, new byte[]{5, 1, 0}, },
                {new byte[]{5, 0, 2}, new byte[]{5, 0, 1}, new byte[]{5, 0, 0}, },
            },
            {
                {new byte[]{3, 2, 2}, new byte[]{3, 2, 1}, new byte[]{3, 2, 0}, },
                {new byte[]{3, 1, 2}, new byte[]{3, 1, 1}, new byte[]{3, 1, 0}, },
                {new byte[]{3, 0, 2}, new byte[]{3, 0, 1}, new byte[]{3, 0, 0}, },
            },
            {
                {new byte[]{1, 0, 0}, new byte[]{1, 0, 1}, new byte[]{1, 0, 2}, },
                {new byte[]{1, 1, 0}, new byte[]{1, 1, 1}, new byte[]{1, 1, 2}, },
                {new byte[]{1, 2, 0}, new byte[]{1, 2, 1}, new byte[]{1, 2, 2}, },
            },
        };

        /// <summary>
        /// Y :整体向U方向转
        /// </summary>
        public static byte[, ,][] Y = new byte[6, 3, 3][] {
            {
                {new byte[]{1, 0, 0}, new byte[]{1, 0, 1}, new byte[]{1, 0, 2}},
                {new byte[]{1, 1, 0}, new byte[]{1, 1, 1}, new byte[]{1, 1, 2}},
                {new byte[]{1, 2, 0}, new byte[]{1, 2, 1}, new byte[]{1, 2, 2}}},
            {
                {new byte[]{2, 0, 0}, new byte[]{2, 0, 1}, new byte[]{2, 0, 2}},
                {new byte[]{2, 1, 0}, new byte[]{2, 1, 1}, new byte[]{2, 1, 2}},
                {new byte[]{2, 2, 0}, new byte[]{2, 2, 1}, new byte[]{2, 2, 2}}},
            {
                {new byte[]{3, 0, 0}, new byte[]{3, 0, 1}, new byte[]{3, 0, 2}},
                {new byte[]{3, 1, 0}, new byte[]{3, 1, 1}, new byte[]{3, 1, 2}},
                {new byte[]{3, 2, 0}, new byte[]{3, 2, 1}, new byte[]{3, 2, 2}}},
            {
                {new byte[]{0, 0, 0}, new byte[]{0, 0, 1}, new byte[]{0, 0, 2}},
                {new byte[]{0, 1, 0}, new byte[]{0, 1, 1}, new byte[]{0, 1, 2}},
                {new byte[]{0, 2, 0}, new byte[]{0, 2, 1}, new byte[]{0, 2, 2}}},
            {
                // Clockwize 90 degrees
                {new byte[]{4, 2, 0}, new byte[]{4, 1, 0}, new byte[]{4, 0, 0}},
                {new byte[]{4, 2, 1}, null,                new byte[]{4, 0, 1}},
                {new byte[]{4, 2, 2}, new byte[]{4, 1, 2}, new byte[]{4, 0, 2}}},
            {
                // Counter-Clockwize 90 degrees
                {new byte[]{5, 0, 2}, new byte[]{5, 1, 2}, new byte[]{5, 2, 2}},
                {new byte[]{5, 0, 1}, null,                new byte[]{5, 2, 1}},
                {new byte[]{5, 0, 0}, new byte[]{5, 1, 0}, new byte[]{5, 2, 0}}}
        };

        public static byte[, ,][] Y_ = new byte[6, 3, 3][] {
            {
                {new byte[]{3, 0, 0}, new byte[]{3, 0, 1}, new byte[]{3, 0, 2}, },
                {new byte[]{3, 1, 0}, new byte[]{3, 1, 1}, new byte[]{3, 1, 2}, },
                {new byte[]{3, 2, 0}, new byte[]{3, 2, 1}, new byte[]{3, 2, 2}, },
            },
            {
                {new byte[]{0, 0, 0}, new byte[]{0, 0, 1}, new byte[]{0, 0, 2}, },
                {new byte[]{0, 1, 0}, new byte[]{0, 1, 1}, new byte[]{0, 1, 2}, },
                {new byte[]{0, 2, 0}, new byte[]{0, 2, 1}, new byte[]{0, 2, 2}, },
            },
            {
                {new byte[]{1, 0, 0}, new byte[]{1, 0, 1}, new byte[]{1, 0, 2}, },
                {new byte[]{1, 1, 0}, new byte[]{1, 1, 1}, new byte[]{1, 1, 2}, },
                {new byte[]{1, 2, 0}, new byte[]{1, 2, 1}, new byte[]{1, 2, 2}, },
            },
            {
                {new byte[]{2, 0, 0}, new byte[]{2, 0, 1}, new byte[]{2, 0, 2}, },
                {new byte[]{2, 1, 0}, new byte[]{2, 1, 1}, new byte[]{2, 1, 2}, },
                {new byte[]{2, 2, 0}, new byte[]{2, 2, 1}, new byte[]{2, 2, 2}, },
            },
            {
                {new byte[]{4, 0, 2}, new byte[]{4, 1, 2}, new byte[]{4, 2, 2}, },
                {new byte[]{4, 0, 1}, null, new byte[]{4, 2, 1}, },
                {new byte[]{4, 0, 0}, new byte[]{4, 1, 0}, new byte[]{4, 2, 0}, },
            },
            {
                {new byte[]{5, 2, 0}, new byte[]{5, 1, 0}, new byte[]{5, 0, 0}, },
                {new byte[]{5, 2, 1}, null, new byte[]{5, 0, 1}, },
                {new byte[]{5, 2, 2}, new byte[]{5, 1, 2}, new byte[]{5, 0, 2}, },
            },
        };

        /// <summary>
        /// Z :整体向F方向转
        /// </summary>
        public static byte[, ,][] Z = new byte[6, 3, 3][] {
            {
                {new byte[]{5, 2, 0}, new byte[]{5, 1, 0}, new byte[]{5, 0, 0}},
                {new byte[]{5, 2, 1}, new byte[]{5, 1, 1}, new byte[]{5, 0, 1}},
                {new byte[]{5, 2, 2}, new byte[]{5, 1, 2}, new byte[]{5, 0, 2}}},
            {
                // Clockwize 90 degrees
                {new byte[]{1, 2, 0}, new byte[]{1, 1, 0}, new byte[]{1, 0, 0}},
                {new byte[]{1, 2, 1}, null,                new byte[]{1, 0, 1}},
                {new byte[]{1, 2, 2}, new byte[]{1, 1, 2}, new byte[]{1, 0, 2}}},
            {
                {new byte[]{4, 2, 0}, new byte[]{4, 1, 0}, new byte[]{4, 0, 0}},
                {new byte[]{4, 2, 1}, new byte[]{4, 1, 1}, new byte[]{4, 0, 1}},
                {new byte[]{4, 2, 2}, new byte[]{4, 1, 2}, new byte[]{4, 0, 2}}},
            {
                // Counter-Clockwize 90 degrees
                {new byte[]{3, 0, 2}, new byte[]{3, 1, 2}, new byte[]{3, 2, 2}},
                {new byte[]{3, 0, 1}, null,                new byte[]{3, 2, 1}},
                {new byte[]{3, 0, 0}, new byte[]{3, 1, 0}, new byte[]{3, 2, 0}}},
            {
                {new byte[]{0, 2, 0}, new byte[]{0, 1, 0}, new byte[]{0, 0, 0}},
                {new byte[]{0, 2, 1}, new byte[]{0, 1, 1}, new byte[]{0, 0, 1}},
                {new byte[]{0, 2, 2}, new byte[]{0, 1, 2}, new byte[]{0, 0, 2}}},
            {
                {new byte[]{2, 2, 0}, new byte[]{2, 1, 0}, new byte[]{2, 0, 0}},
                {new byte[]{2, 2, 1}, new byte[]{2, 1, 1}, new byte[]{2, 0, 1}},
                {new byte[]{2, 2, 2}, new byte[]{2, 1, 2}, new byte[]{2, 0, 2}}}
        };

        public static byte[, ,][] Z_ = new byte[6, 3, 3][] {
            {
                {new byte[]{4, 0, 2}, new byte[]{4, 1, 2}, new byte[]{4, 2, 2}, },
                {new byte[]{4, 0, 1}, new byte[]{4, 1, 1}, new byte[]{4, 2, 1}, },
                {new byte[]{4, 0, 0}, new byte[]{4, 1, 0}, new byte[]{4, 2, 0}, },
            },
            {
                {new byte[]{1, 0, 2}, new byte[]{1, 1, 2}, new byte[]{1, 2, 2}, },
                {new byte[]{1, 0, 1}, null, new byte[]{1, 2, 1}, },
                {new byte[]{1, 0, 0}, new byte[]{1, 1, 0}, new byte[]{1, 2, 0}, },
            },
            {
                {new byte[]{5, 0, 2}, new byte[]{5, 1, 2}, new byte[]{5, 2, 2}, },
                {new byte[]{5, 0, 1}, new byte[]{5, 1, 1}, new byte[]{5, 2, 1}, },
                {new byte[]{5, 0, 0}, new byte[]{5, 1, 0}, new byte[]{5, 2, 0}, },
            },
            {
                {new byte[]{3, 2, 0}, new byte[]{3, 1, 0}, new byte[]{3, 0, 0}, },
                {new byte[]{3, 2, 1}, null, new byte[]{3, 0, 1}, },
                {new byte[]{3, 2, 2}, new byte[]{3, 1, 2}, new byte[]{3, 0, 2}, },
            },
            {
                {new byte[]{2, 0, 2}, new byte[]{2, 1, 2}, new byte[]{2, 2, 2}, },
                {new byte[]{2, 0, 1}, new byte[]{2, 1, 1}, new byte[]{2, 2, 1}, },
                {new byte[]{2, 0, 0}, new byte[]{2, 1, 0}, new byte[]{2, 2, 0}, },
            },
            {
                {new byte[]{0, 0, 2}, new byte[]{0, 1, 2}, new byte[]{0, 2, 2}, },
                {new byte[]{0, 0, 1}, new byte[]{0, 1, 1}, new byte[]{0, 2, 1}, },
                {new byte[]{0, 0, 0}, new byte[]{0, 1, 0}, new byte[]{0, 2, 0}, },
            },
        };

        /// <summary>
        /// S : BF夹层向F方向转
        /// </summary>
        public static byte[, ,][] S = new byte[6, 3, 3][] {
            {
                {null, new byte[]{5, 1, 0}, null},
                {null, new byte[]{5, 1, 1}, null},
                {null, new byte[]{5, 1, 2}, null}},
            {
                {null, null, null},
                {null, null, null},
                {null, null, null}},
            {
                {null, new byte[]{4, 1, 0}, null},
                {null, new byte[]{4, 1, 1}, null},
                {null, new byte[]{4, 1, 2}, null}},
            {
                {null, null, null},
                {null, null, null},
                {null, null, null}},
            {
                {null, null, null},
                {new byte[]{0, 0, 1}, new byte[]{0, 1, 1}, new byte[]{0, 2, 1}},
                {null, null, null}},
            {
                {null, null, null},
                {new byte[]{2, 2, 1}, new byte[]{2, 1, 1}, new byte[]{2, 0, 1}},
                {null, null, null}}
        };

        public static byte[, ,][] S_ = new byte[6, 3, 3][] {
            {
                {null, new byte[]{4, 1, 2}, null, },
                {null, new byte[]{4, 1, 1}, null, },
                {null, new byte[]{4, 1, 0}, null, },
            },
            {
                {null, null, null, },
                {null, null, null, },
                {null, null, null, },
            },
            {
                {null, new byte[]{5, 1, 0}, null, },
                {null, new byte[]{5, 1, 1}, null, },
                {null, new byte[]{5, 1, 2}, null, },
            },
            {
                {null, null, null, },
                {null, null, null, },
                {null, null, null, },
            },
            {
                {null, null, null, },
                {new byte[]{2, 2, 1}, new byte[]{2, 1, 1}, new byte[]{2, 0, 1}, },
                {null, null, null, },
            },
            {
                {null, null, null, },
                {new byte[]{0, 2, 1}, new byte[]{0, 1, 1}, new byte[]{0, 0, 1}, },
                {null, null, null, },
            },
        };

        /// <summary>
        /// M : LR夹层向L方向转
        /// </summary>
        public static byte[, ,][] M = new byte[6, 3, 3][] {
            {
                {null, null, null},
                {null, null, null},
                {null, null, null}},
            {
                {null, new byte[]{4, 0, 1}, null},
                {null, new byte[]{4, 1, 1}, null},
                {null, new byte[]{4, 2, 1}, null}},
            {
                {null, null, null},
                {null, null, null},
                {null, null, null}},
            {
                {null, new byte[]{5, 2, 1}, null},
                {null, new byte[]{5, 1, 1}, null},
                {null, new byte[]{5, 0, 1}, null}},
            {
                {null, new byte[]{3, 2, 1}, null},
                {null, new byte[]{3, 1, 1}, null},
                {null, new byte[]{3, 0, 1}, null}},
            {
                {null, new byte[]{1, 0, 1}, null},
                {null, new byte[]{1, 1, 1}, null},
                {null, new byte[]{1, 2, 1}, null}}
        };

        public static byte[, ,][] M_ = new byte[6, 3, 3][] {
            {
                {null, null, null, },
                {null, null, null, },
                {null, null, null, },
            },
            {
                {null, new byte[]{5, 0, 1}, null, },
                {null, new byte[]{5, 1, 1}, null, },
                {null, new byte[]{5, 2, 1}, null, },
            },
            {
                {null, null, null, },
                {null, null, null, },
                {null, null, null, },
            },
            {
                {null, new byte[]{4, 2, 1}, null, },
                {null, new byte[]{4, 1, 1}, null, },
                {null, new byte[]{4, 0, 1}, null, },
            },
            {
                {null, new byte[]{1, 0, 1}, null, },
                {null, new byte[]{1, 1, 1}, null, },
                {null, new byte[]{1, 2, 1}, null, },
            },
            {
                {null, new byte[]{3, 2, 1}, null, },
                {null, new byte[]{3, 1, 1}, null, },
                {null, new byte[]{3, 0, 1}, null, },
            },
        };

        /// <summary>
        /// E : UD夹层向U'方向转
        /// </summary>
        public static byte[, ,][] E = new byte[6, 3, 3][] {
            {
                {null, null, null},
                {new byte[]{3, 1, 0}, new byte[]{3, 1, 1}, new byte[]{3, 1, 2}},
                {null, null, null}},
            {
                {null, null, null},
                {new byte[]{0, 1, 0}, new byte[]{0, 1, 1}, new byte[]{0, 1, 2}},
                {null, null, null}},
            {
                {null, null, null},
                {new byte[]{1, 1, 0}, new byte[]{1, 1, 1}, new byte[]{1, 1, 2}},
                {null, null, null}},
            {
                {null, null, null},
                {new byte[]{2, 1, 0}, new byte[]{2, 1, 1}, new byte[]{2, 1, 2}},
                {null, null, null}},
            {
                {null, null, null},
                {null, null, null},
                {null, null, null}},
            {
                {null, null, null},
                {null, null, null},
                {null, null, null}}
        };

        public static byte[, ,][] E_ = new byte[6, 3, 3][] {
            {
                {null, null, null, },
                {new byte[]{1, 1, 0}, new byte[]{1, 1, 1}, new byte[]{1, 1, 2}, },
                {null, null, null, },
            },
            {
                {null, null, null, },
                {new byte[]{2, 1, 0}, new byte[]{2, 1, 1}, new byte[]{2, 1, 2}, },
                {null, null, null, },
            },
            {
                {null, null, null, },
                {new byte[]{3, 1, 0}, new byte[]{3, 1, 1}, new byte[]{3, 1, 2}, },
                {null, null, null, },
            },
            {
                {null, null, null, },
                {new byte[]{0, 1, 0}, new byte[]{0, 1, 1}, new byte[]{0, 1, 2}, },
                {null, null, null, },
            },
            {
                {null, null, null, },
                {null, null, null, },
                {null, null, null, },
            },
            {
                {null, null, null, },
                {null, null, null, },
                {null, null, null, },
            },
        };
    }
}
