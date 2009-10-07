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

namespace CubeExercise
{
    /// <summary>
    /// Interaction logic for CubeSurface.xaml
    /// </summary>
    public partial class CubeSurface : UserControl
    {
        public Brush[] Colors
        {
            set
            {
                this.block00.Fill = value[0];
                this.block01.Fill = value[1];
                this.block02.Fill = value[2];
                this.block10.Fill = value[3];
                this.block11.Fill = value[4];
                this.block12.Fill = value[5];
                this.block20.Fill = value[6];
                this.block21.Fill = value[7];
                this.block22.Fill = value[8];
            }
        }

        public CubeSurface()
        {
            InitializeComponent();
        }
    }
}
