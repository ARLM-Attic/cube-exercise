using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;

namespace CubeExercise
{
    /// <summary>
    /// Interaction logic for CubeControl.xaml
    /// </summary>
    public partial class CubeControl : UserControl
    {
        private Cube<Brush> cube;

        public CubeControl()
        {
            InitializeComponent();
            InitializeCube();
        }

        public void InitializeCube()
        {
            this.cube = new Cube<Brush>(
                new SolidColorBrush(Colors.Orange),
                new SolidColorBrush(Colors.Blue),
                new SolidColorBrush(Colors.Red),
                new SolidColorBrush(Colors.Green),
                new SolidColorBrush(Colors.Yellow),
                new SolidColorBrush(Colors.White));
            string script = ConfigurationManager.AppSettings["CubeInitializationAlgorithm"];
            if (!string.IsNullOrEmpty(script))
            {
                this.cube.DoAlgorithm(script);
            }

            this.UpdateColors();
        }

        public bool IsRecovered()
        {
            return this.cube.IsRecovered();
        }

        public void UpdateColors()
        {
            Brush[, ,] brushes = this.cube.Status;
            Brush[] left = new Brush[9]
            {
                brushes[0, 0, 0],
                brushes[0, 0, 1],
                brushes[0, 0, 2],
                brushes[0, 1, 0],
                brushes[0, 1, 1],
                brushes[0, 1, 2],
                brushes[0, 2, 0],
                brushes[0, 2, 1],
                brushes[0, 2, 2],
            };
            Brush[] front = new Brush[9]
            {
                brushes[1, 0, 0],
                brushes[1, 0, 1],
                brushes[1, 0, 2],
                brushes[1, 1, 0],
                brushes[1, 1, 1],
                brushes[1, 1, 2],
                brushes[1, 2, 0],
                brushes[1, 2, 1],
                brushes[1, 2, 2],
            };
            Brush[] right = new Brush[9]
            {
                brushes[2, 0, 0],
                brushes[2, 0, 1],
                brushes[2, 0, 2],
                brushes[2, 1, 0],
                brushes[2, 1, 1],
                brushes[2, 1, 2],
                brushes[2, 2, 0],
                brushes[2, 2, 1],
                brushes[2, 2, 2],
            };
            Brush[] back = new Brush[9]
            {
                brushes[3, 0, 0],
                brushes[3, 0, 1],
                brushes[3, 0, 2],
                brushes[3, 1, 0],
                brushes[3, 1, 1],
                brushes[3, 1, 2],
                brushes[3, 2, 0],
                brushes[3, 2, 1],
                brushes[3, 2, 2],
            };
            Brush[] up = new Brush[9]
            {
                brushes[4, 0, 0],
                brushes[4, 0, 1],
                brushes[4, 0, 2],
                brushes[4, 1, 0],
                brushes[4, 1, 1],
                brushes[4, 1, 2],
                brushes[4, 2, 0],
                brushes[4, 2, 1],
                brushes[4, 2, 2],
            };
            Brush[] down = new Brush[9]
            {
                brushes[5, 0, 0],
                brushes[5, 0, 1],
                brushes[5, 0, 2],
                brushes[5, 1, 0],
                brushes[5, 1, 1],
                brushes[5, 1, 2],
                brushes[5, 2, 0],
                brushes[5, 2, 1],
                brushes[5, 2, 2],
            };


            this.Left.Colors = left;
            this.Front.Colors = front;
            this.Right.Colors = right;
            this.Back.Colors = back;
            this.Up.Colors = up;
            this.Down.Colors = down;
        }

        public void Transform(string script)
        {
            this.cube.DoAlgorithm(script);
            this.UpdateColors();
        }
    }
}
