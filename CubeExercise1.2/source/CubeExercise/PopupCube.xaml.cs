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
using System.Windows.Shapes;

namespace CubeExercise
{
    /// <summary>
    /// Interaction logic for PopupCube.xaml
    /// </summary>
    public partial class PopupCube : Window
    {
        public PopupCube()
        {
            InitializeComponent();
        }

        public bool ParentClosing { get; set; }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!this.ParentClosing)
            {
                this.Hide();
                e.Cancel = true;
            }
        }
    }
}
