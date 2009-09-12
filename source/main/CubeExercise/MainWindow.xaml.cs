//-----------------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="Rui Fan">
//     Copyright (c) Rui Fan.  All rights reserved.
// </copyright>
//
// <author email="albert@fanrui.net">
//     Rui Fan
// </author>
//
// <summary>
//     This class is the main window.
// </summary>
//
// <remarks/>
//
// <disclaimer/>
//
// <history date="08/01/2009" Author="Rui Fan">
//     Class Created.
// </history>
// <history date="09/06/2009" Author="Rui Fan">
//     Add formulas grouping feature.
// </history>
//-----------------------------------------------------------------------------

namespace CubeExercise
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Input;
    using System.Windows.Media.Imaging;
    using System.Windows.Threading;
    using System.Xml;
    using System.Xml.Serialization;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Cache the file version.
        /// </summary>
        private static string fileVersion;

        /// <summary>
        /// Gets the file version. If the version is not cached, get it from the assembly attribute.
        /// </summary>
        public static string FileVersion
        {
            get
            {
                if (string.IsNullOrEmpty(fileVersion))
                {
                    FileVersionInfo version = FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location);
                    fileVersion = version.FileVersion;
                }

                return fileVersion;
            }
        }

        public ExerciseConfiguration ExerciseConfiguration { get; set; }

        private DispatcherTimer timer;

        private Group root;

        private List<Formula> plainListFormulas;

        //public List<Formula> Formulas { get { return this.formulas; } }

        private List<Formula> exercising;

        private Formula currentFormula;

        public MainWindow()
        {
            this.root = new Group();
            this.plainListFormulas = new List<Formula>();
            this.timer = new DispatcherTimer();
            this.timer.Tick += new EventHandler(timer_Tick);
            this.ExerciseConfiguration = new ExerciseConfiguration();
            InitializeComponent();
            this.InitializeFormulas();
            this.InitializeTree();
        }

        private void InitializeTree()
        {
            TreeViewItem root = this.GetSubTreeItems(this.root);

            // Transfer the sub items from the TreeViewItem to the TreeView.
            while (root.Items.Count > 0)
            {
                // One TreeViewItem can have only one logical parent. So the current
                // parent must remove it from its children.
                object subItem = root.Items[0];
                root.Items.RemoveAt(0);
                this.FormulasTree.Items.Add(subItem);
            }
        }

        private TreeViewItem GetSubTreeItems(Group group)
        {
            TreeViewItem tvItem = new TreeViewItem();

            foreach (object item in group.Items)
            {
                if (item.GetType() == typeof(Formula))
                {
                    Formula f = (Formula)item;

                    // A plain list is helpful for the iteration of the formulas
                    // because we don't have to find sub nodes recursivly.
                    this.plainListFormulas.Add(f);
                    Button itemButton = new Button();
                    itemButton.Tag = f;
                    itemButton.Click += new RoutedEventHandler(Button_Click);

                    TreeViewItem subItem = new TreeViewItem();
                    subItem.Header = itemButton;
                    subItem.Tag = f;
                    tvItem.Items.Add(subItem);

                    StackPanel panel = new StackPanel();
                    itemButton.Content = panel;
                    panel.HorizontalAlignment = HorizontalAlignment.Left;
                    panel.VerticalAlignment = VerticalAlignment.Center;
                    panel.Orientation = Orientation.Horizontal;
                    CheckBox cb = new CheckBox() { VerticalAlignment = VerticalAlignment.Center };
                    panel.Children.Add(cb);
                    cb.Checked += new RoutedEventHandler(cb_CheckedChanged);
                    cb.Unchecked += new RoutedEventHandler(cb_CheckedChanged);
                    cb.DataContext = f;
                    cb.SetBinding(CheckBox.IsCheckedProperty, "Enabled");

                    if (!string.IsNullOrEmpty(f.Image) && System.IO.File.Exists(f.Image))
                    {
                        string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                        path = System.IO.Path.Combine(path, f.Image);
                        panel.Children.Add(new Image() { Source = new BitmapImage(new Uri(path, UriKind.Absolute)) });
                    }
                    TextBlock tb = new TextBlock() { VerticalAlignment = VerticalAlignment.Center };
                    tb.DataContext = f;
                    tb.SetBinding(TextBlock.TextProperty, "Name");
                    panel.Children.Add(tb);
                }
                else
                {
                    Group g = (Group)item;
                    g.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(g_PropertyChanged);
                    StackPanel panel = new StackPanel();
                    TreeViewItem subItem = GetSubTreeItems(g);

                    // This binding is OneWay by default. So use a explicit binding configuration.
                    Binding binding = new Binding("Expanded") { Source = g, Mode = BindingMode.TwoWay };
                    subItem.SetBinding(TreeViewItem.IsExpandedProperty, binding);
                    subItem.Header = panel;
                    subItem.Tag = g;
                    tvItem.Items.Add(subItem);

                    panel.HorizontalAlignment = HorizontalAlignment.Left;
                    panel.VerticalAlignment = VerticalAlignment.Center;
                    panel.Orientation = Orientation.Horizontal;
                    CheckBox cb = new CheckBox() { VerticalAlignment = VerticalAlignment.Center };
                    panel.Children.Add(cb);
                    cb.Checked += new RoutedEventHandler(cb_CheckedChanged);
                    cb.Unchecked += new RoutedEventHandler(cb_CheckedChanged);
                    cb.DataContext = g;
                    cb.SetBinding(CheckBox.IsCheckedProperty, "Enabled");

                    TextBlock tb = new TextBlock() { VerticalAlignment = VerticalAlignment.Center };
                    tb.DataContext = g;
                    tb.SetBinding(TextBlock.TextProperty, "Name");
                    panel.Children.Add(tb);
                }
            }

            return tvItem;
        }

        void g_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Group g = (Group)sender;
            if (e.PropertyName == "Enabled")
            {
                foreach (object item in g.Items)
                {
                    if (item.GetType() == typeof(Formula))
                    {
                        ((Formula)item).Enabled = g.Enabled;
                    }
                    else if (item.GetType() == typeof(Group))
                    {
                        ((Group)item).Enabled = g.Enabled;
                    }
                }
            }
        }

        private void cb_CheckedChanged(object sender, RoutedEventArgs e)
        {
            TreeViewItem tvItem = this.FindParentTreeViewItem((CheckBox)sender);
            while (tvItem != null)
            {
                this.ResetParentStatus(tvItem);
                tvItem = this.FindParentTreeViewItem(tvItem);
            }
        }

        /// <summary>
        /// Find the nearest TreeViewItem in its parents.
        /// </summary>
        /// <param name="child"></param>
        /// <returns></returns>
        private TreeViewItem FindParentTreeViewItem(FrameworkElement child)
        {
            if (child.Parent == null)
            {
                return null;
            }

            if (child.Parent is TreeViewItem)
            {
                return (TreeViewItem)child.Parent;
            }
            else
            {
                return this.FindParentTreeViewItem((FrameworkElement)child.Parent);
            }
        }

        private CheckBox FindChildCheckBox(TreeViewItem item)
        {
            CheckBox cb = null;

            Button btn = item.Header as Button;
            Panel p;
            if (btn == null)
            {
                p = item.Header as Panel;
            }
            else
            {
                p = btn.Content as Panel;
            }

            if (p == null)
            {
                return null;
            }

            cb = (from UIElement c in p.Children
                  where c.GetType() == typeof(CheckBox)
                  select c as CheckBox).ElementAtOrDefault(0);

            return cb;
        }

        /// <summary>
        /// Reset the status of the CheckBox of the parent of the 'item'.
        /// </summary>
        /// <param name="item">The element whose parent's status will be reset.</param>
        private void ResetParentStatus(TreeViewItem item)
        {
            if (item == null)
            {
                return;
            }

            if (item.Parent == null || !(item.Parent is TreeViewItem))
            {
                return;
            }

            TreeViewItem parent = item.Parent as TreeViewItem;
            CheckBox parentCheckBox = this.FindChildCheckBox(parent);
            if (parentCheckBox == null)
            {
                return;
            }

            int numOfUncheckedChildren = 0;
            for (int i = 0; i < parent.Items.Count; i++)
            {
                TreeViewItem childItem = parent.Items[i] as TreeViewItem;
                CheckBox childCheckBox = this.FindChildCheckBox(childItem);
                if (childCheckBox == null)
                {
                    continue;
                }

                if (childCheckBox.IsChecked == null)
                {
                    parentCheckBox.IsChecked = null;
                    return;
                }

                if (childCheckBox.IsChecked == false)
                {
                    numOfUncheckedChildren++;
                }
            }

            if (numOfUncheckedChildren == parent.Items.Count)
            {
                parentCheckBox.IsChecked = false;
            }
            else if (numOfUncheckedChildren == 0)
            {
                parentCheckBox.IsChecked = true;
            }
            else
            {
                parentCheckBox.IsChecked = null;
            }
        }

        private void InitializeFormulas()
        {
            string xmlPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            xmlPath = System.IO.Path.Combine(xmlPath, "Formulas.xml");
            if (System.IO.File.Exists(xmlPath))
            {
                XmlSerializer s = new XmlSerializer(typeof(Group), "http://schemas.fanrui.net/cubeexercise/2009/08/FormulasSchema.xsd");
                Group g = (Group)s.Deserialize(XmlReader.Create(xmlPath));
                g.FilePath = xmlPath;
                this.root.Items = new Group[] { g };
            }
        }

        private void SaveFormulas()
        {
            if (this.root == null || this.root.Items == null)
            {
                return;
            }

            try
            {
                foreach (object item in this.root.Items)
                {
                    if (item.GetType() != typeof(Group))
                    {
                        continue;
                    }

                    Group g = (Group)item;
                    XmlSerializer s = new XmlSerializer(typeof(Group));
                    using (TextWriter textWriter = new StreamWriter(g.FilePath, false, Encoding.UTF8))
                    {
                        s.Serialize(textWriter, g);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("无法保存公式文件！错误信息:" + ex.ToString(), "错误", MessageBoxButton.OK);
            }
        }

        private void btnInitialize_Click(object sender, RoutedEventArgs e)
        {
            this.cube.InitializeCube();
        }

        private void btnTransform_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string todo = this.txtFormula.Text;
                if (this.txtFormula.SelectionLength != 0)
                {
                    todo = this.txtFormula.SelectedText;
                }

                this.cube.Transform(todo);
                this.cube.UpdateColors();
            }
            catch (ArgumentException ex)
            {
                string msg = string.Format("公式不正确！错误信息：{0}", ex.ToString());
                MessageBox.Show(msg, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnFind_Click(object sender, RoutedEventArgs e)
        {
            string todo = this.txtFormula.Text;
            if (this.txtFormula.SelectionLength != 0)
            {
                todo = this.txtFormula.SelectedText;
            }

            if (string.IsNullOrEmpty(todo))
            {
                return;
            }

            int i = 0;
            Cube<int> tempCube = new Cube<int>(0, 1, 2, 3, 4, 5);
            do
            {
                tempCube.DoFormula(todo);
                i++;
            } while (!tempCube.IsRecovered());

            MessageBox.Show("如果重复做公式" + todo + "，将在" + i + "次后回到初始状态。");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                Formula f = btn.Tag as Formula;
                if (f != null)
                {
                    this.txtFormula.Text = f.Script;
                }
            }
        }

        private void btnStartExercise_Click(object sender, RoutedEventArgs e)
        {
            this.StartExercise();
        }

        private void StartExercise()
        {
            this.ExerciseConfiguration.Mode = (ExerciseMode)this.cmbRandomMode.SelectedIndex;

            if (this.exercising == null)
            {
                this.exercising = new List<Formula>();
            }
            else
            {
                this.exercising.Clear();
            }

            if (this.ExerciseConfiguration.Mode == ExerciseMode.Descending)
            {
                for (int i = this.plainListFormulas.Count - 1; i >= 0; i--)
                {
                    if (this.plainListFormulas[i].Enabled)
                    {
                        this.exercising.Add(this.plainListFormulas[i]);
                    }
                }
            }
            else
            {
                for (int i = 0; i < this.plainListFormulas.Count; i++)
                {
                    if (this.plainListFormulas[i].Enabled)
                    {
                        this.exercising.Add(this.plainListFormulas[i]);
                    }
                }
            }

            if (this.ExerciseConfiguration.Mode != ExerciseMode.RepeatableRandom)
            {
                if (this.ExerciseConfiguration.NumberOfFormulas > this.exercising.Count || this.ExerciseConfiguration.NumberOfFormulas <= 0)
                {
                    this.ExerciseConfiguration.NumberOfFormulas = this.exercising.Count;
                }
            }
            else
            {
                this.ExerciseConfiguration.NumberOfFormulas = 999;
            }

            this.stopWatch.Reset();
            this.ToggleControlStatus(ExerciseStatus.Started);
            this.ShowNextFormula();
        }

        private void ShowNextFormula()
        {
            if (this.exercising.Count > 0 && this.ExerciseConfiguration.NumberOfFormulas > 0)
            {
                this.tbFormulaPrompt.Text = string.Empty;
                this.timer.Stop();

                int index = 0;
                switch (this.ExerciseConfiguration.Mode)
                {
                    case ExerciseMode.NoRepeatRandom:
                    case ExerciseMode.RepeatableRandom:
                    default:
                        Random rnd = new Random(Environment.TickCount);
                        index = rnd.Next(0, this.exercising.Count);
                        break;

                    case ExerciseMode.Ascending:
                    case ExerciseMode.Descending:
                        index = 0;
                        break;
                }

                this.currentFormula = this.exercising.ElementAt(index);
                this.ExerciseConfiguration.NumberOfFormulas--;
                if (this.ExerciseConfiguration.Mode != ExerciseMode.RepeatableRandom)
                {
                    this.exercising.RemoveAt(index);
                }

                string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                path = System.IO.Path.Combine(path, this.currentFormula.Image);
                this.imgExercise.Source = new BitmapImage(new Uri(path, UriKind.Absolute));
                if (this.chkRecordScript.IsChecked.HasValue && this.chkRecordScript.IsChecked.Value == true)
                {
                    this.txtFormula.AppendText("/*" + this.currentFormula.Name + "*/ ");

                    if (!string.IsNullOrEmpty(this.currentFormula.PreScript))
                    {
                        this.txtFormula.AppendText("[" + this.currentFormula.PreScript + "]");
                    }

                    this.txtFormula.AppendText(this.currentFormula.Script);

                    if (!string.IsNullOrEmpty(this.currentFormula.PostScript))
                    {
                        this.txtFormula.AppendText("[" + this.currentFormula.PostScript + "]");
                    }

                    this.txtFormula.AppendText(Environment.NewLine);
                }

                if (this.ExerciseConfiguration.ShowScriptDelay == 0)
                {
                    this.ShowFormula(this.currentFormula);
                }
                else if (this.ExerciseConfiguration.ShowScriptDelay > 0)
                {
                    this.timer.Interval = TimeSpan.FromSeconds(this.ExerciseConfiguration.ShowScriptDelay);
                    this.timer.Start();
                }
            }
            else
            {
                this.ToggleControlStatus(ExerciseStatus.Stopped);
            }
        }

        private void imgExercise_KeyDown(object sender, KeyEventArgs e)
        {
            //if (Keyboard.Modifiers != ModifierKeys.None)
            //{
            //    return;
            //}

            switch (e.Key)
            {
                case Key.Space:
                case Key.Enter:
                case Key.RightAlt:
                case Key.LeftAlt:
                case Key.RWin:
                case Key.LWin:
                case Key.RightCtrl:
                case Key.LeftCtrl:
                case Key.Apps:
                case Key.System:
                    // This include all the keys in the bottom line of the main keyboard.
                    // Helpful for preventing pressing the wrong key.
                    if (this.stopWatch.IsRunning)
                    {
                        if (this.currentFormula != null)
                        {
                            this.currentFormula.PracticeTimes++;
                        }
                        ShowNextFormula();
                    }

                    e.Handled = true;
                    break;

                case Key.N:
                    if (this.stopWatch.IsRunning)
                    {
                        Formula f = this.currentFormula;
                        if (f != null)
                        {
                            f.PracticeTimes++;
                            this.exercising.Add(f);
                            this.ExerciseConfiguration.NumberOfFormulas++;
                        }

                        ShowNextFormula();
                    }

                    e.Handled = true;
                    break;

                // This is also handled by the shortcut key.
                case Key.P:
                    if (this.stopWatch.IsRunning)
                    {
                        this.ToggleControlStatus(ExerciseStatus.Paused);
                    }
                    else
                    {
                        this.ToggleControlStatus(ExerciseStatus.Started);
                    }

                    e.Handled = true;
                    break;

                case Key.Escape:
                    this.ToggleControlStatus(ExerciseStatus.Stopped);
                    e.Handled = true;
                    break;
                default:
                    break;
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            this.txtFormula.Clear();
        }

        private void imgExercise_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.imgExercise.Focus();
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock tb = sender as TextBlock;
            if (tb != null)
            {
                if (tb.Text.IndexOf('@') >= 0)
                {
                    System.Diagnostics.Process.Start("mailto:" + tb.Text);
                }
                else
                {
                    System.Diagnostics.Process.Start(tb.Text);
                }
            }
        }

        /// <summary>
        /// Filter out the non-numeric keys.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxNumeric_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.Modifiers != ModifierKeys.None)
            {
                return;
            }

            switch (e.Key)
            {
                case Key.D0:
                case Key.D1:
                case Key.D2:
                case Key.D3:
                case Key.D4:
                case Key.D5:
                case Key.D6:
                case Key.D7:
                case Key.D8:
                case Key.D9:
                case Key.NumPad0:
                case Key.NumPad1:
                case Key.NumPad2:
                case Key.NumPad3:
                case Key.NumPad4:
                case Key.NumPad5:
                case Key.NumPad6:
                case Key.NumPad7:
                case Key.NumPad8:
                case Key.NumPad9:
                case Key.Tab:
                case Key.LWin:
                case Key.RWin:
                case Key.Left:
                case Key.Up:
                case Key.Right:
                case Key.Down:
                case Key.Insert:
                case Key.Delete:
                case Key.Home:
                case Key.End:
                case Key.Back:
                case Key.CapsLock:
                case Key.NumLock:
                case Key.Subtract:
                    break;

                case Key.Enter:
                case Key.Space:
                    this.StartExercise();
                    break;
                default:
                    e.Handled = true;
                    break;
            }
        }

        private void btnPauseExercise_Click(object sender, RoutedEventArgs e)
        {
            if (this.stopWatch.IsRunning)
            {
                this.ToggleControlStatus(ExerciseStatus.Paused);
            }
            else
            {
                this.ToggleControlStatus(ExerciseStatus.Started);
            }
        }

        private void btnStopExercise_Click(object sender, RoutedEventArgs e)
        {
            this.ToggleControlStatus(ExerciseStatus.Stopped);
            this.SaveFormulas();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            this.ShowFormula(this.currentFormula);
            this.timer.Stop();
        }

        private void ShowFormula(Formula formula)
        {
            if (formula != null)
            {
                string format = "名称：{1}{0}公式：{2}{0}练习次数：{3}";
                this.tbFormulaPrompt.Text = string.Format(format, Environment.NewLine, formula.Name, formula.Script, formula.PracticeTimes.ToString());
            }
        }

        private enum ExerciseStatus
        {
            Stopped,
            Paused,
            Started
        }

        private void ToggleControlStatus(ExerciseStatus status)
        {
            switch (status)
            {
                case ExerciseStatus.Stopped:
                default:
                    this.btnStartExercise.IsEnabled = true;
                    this.btnStopExercise.IsEnabled = false;
                    this.btnPauseExercise.IsEnabled = false;
                    this.txtRepeatTimes.IsEnabled = true;
                    this.cmbRandomMode.IsEnabled = true;
                    this.imgExercise.Source = null;
                    this.btnPauseExercise.Content = "暂停练习(_P)";
                    this.stopWatch.Stop();
                    this.timer.Stop();
                    this.exercising.Clear();
                    this.tbFormulaPrompt.Text = string.Empty;
                    break;
                case ExerciseStatus.Paused:
                    this.btnStartExercise.IsEnabled = false;
                    this.btnStopExercise.IsEnabled = true;
                    this.btnPauseExercise.IsEnabled = true;
                    this.txtRepeatTimes.IsEnabled = false;
                    this.cmbRandomMode.IsEnabled = false;
                    this.stopWatch.Stop();
                    this.timer.Stop();
                    this.btnPauseExercise.Content = "继续练习(_P)";
                    break;
                case ExerciseStatus.Started:
                    this.btnStartExercise.IsEnabled = false;
                    this.btnStopExercise.IsEnabled = true;
                    this.btnPauseExercise.IsEnabled = true;
                    this.txtRepeatTimes.IsEnabled = false;
                    this.cmbRandomMode.IsEnabled = false;
                    this.stopWatch.Start();
                    this.btnPauseExercise.Content = "暂停练习(_P)";
                    this.imgExercise.Focus();
                    if (this.ExerciseConfiguration.ShowScriptDelay > 0)
                    {
                        this.timer.Start();
                    }
                    break;
            }
        }

        private void mainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.SaveFormulas();
        }
    }
}
