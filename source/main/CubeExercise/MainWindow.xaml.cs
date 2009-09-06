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
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media.Imaging;
    using System.Windows.Threading;
    using System.Xml.Linq;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static string fileVersion;
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

        private Formula rootFormula;

        private List<Formula> plainListFormulas;

        //public List<Formula> Formulas { get { return this.formulas; } }

        private List<Formula> exercising;

        private Formula currentFormula;

        public MainWindow()
        {
            this.timer = new DispatcherTimer();
            this.timer.Tick += new EventHandler(timer_Tick);
            this.ExerciseConfiguration = new ExerciseConfiguration();
            this.plainListFormulas = new List<Formula>();
            this.InitializeFormulas();
            InitializeComponent();
            this.InitializeTree();
        }

        private void InitializeTree()
        {
            List<Formula> formulas = this.rootFormula.SubNodes;
            TreeViewItem root = this.GetSubTreeItems(this.rootFormula);

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

        private TreeViewItem GetSubTreeItems(Formula group)
        {
            TreeViewItem item = new TreeViewItem();

            foreach (Formula f in group.SubNodes)
            {
                if (f.SubNodes == null)
                {
                    StackPanel panel = new StackPanel();
                    panel.HorizontalAlignment = HorizontalAlignment.Left;
                    panel.VerticalAlignment = VerticalAlignment.Center;
                    panel.Orientation = Orientation.Horizontal;
                    CheckBox cb = new CheckBox() { Tag = f, VerticalAlignment = VerticalAlignment.Center };
                    cb.DataContext = f;
                    cb.SetBinding(CheckBox.IsCheckedProperty, "Enabled");
                    panel.Children.Add(cb);
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
                    Button itemButton = new Button();
                    itemButton.Tag = f;
                    itemButton.Click += new RoutedEventHandler(Button_Click);
                    itemButton.Content = panel;

                    TreeViewItem subItem = new TreeViewItem();
                    subItem.Header = itemButton;
                    subItem.Tag = f;
                    item.Items.Add(subItem);
                }
                else
                {
                    WrapPanel panel = new WrapPanel();
                    panel.HorizontalAlignment = HorizontalAlignment.Left;
                    panel.VerticalAlignment = VerticalAlignment.Center;
                    panel.Orientation = Orientation.Horizontal;
                    CheckBox cb = new CheckBox() { Tag = f, VerticalAlignment = VerticalAlignment.Center };
                    cb.DataContext = f;
                    cb.SetBinding(CheckBox.IsCheckedProperty, "Enabled");
                    panel.Children.Add(cb);
                    TextBlock tb = new TextBlock() { VerticalAlignment = VerticalAlignment.Center };
                    tb.DataContext = f;
                    tb.SetBinding(TextBlock.TextProperty, "Name");
                    panel.Children.Add(tb);
                    TreeViewItem subItem = GetSubTreeItems(f);
                    subItem.IsExpanded = true;
                    subItem.Header = panel;
                    item.Items.Add(subItem);
                }
            }

            return item;
        }

        private void InitializeFormulas()
        {
            string xmlPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            xmlPath = System.IO.Path.Combine(xmlPath, "Formulas.xml");
            if (System.IO.File.Exists(xmlPath))
            {
                XDocument doc = XDocument.Load(xmlPath);
                this.rootFormula = this.ReadGroup(doc.Root, doc.Root.GetDefaultNamespace());
                this.rootFormula.Name = "Root - Do not display";
            }
        }

        private Formula ReadGroup(XElement group, XNamespace defaultNamespace)
        {
            Formula formulaGroup = new Formula();

            formulaGroup.Name = group.Attribute("Name") != null ? group.Attribute("Name").Value : string.Empty;

            if (group.Attribute("Enabled") != null)
            {
                bool enabled;
                if (Boolean.TryParse(group.Attribute("Enabled").Value, out enabled))
                {
                    formulaGroup.Enabled = enabled;
                }
            }

            formulaGroup.SubNodes = new List<Formula>();

            foreach (XElement element in group.Elements())
            {
                Formula formula = null;
                if (element.Name == defaultNamespace + "Formula")
                {
                    formula = new Formula()
                    {
                        Name = element.Attribute("Name") != null ? element.Attribute("Name").Value : string.Empty,
                        Demo = element.Attribute("Demo") != null ? element.Attribute("Demo").Value : string.Empty,
                        Image = element.Attribute("Image") != null ? element.Attribute("Image").Value : string.Empty,
                        Script = element.Attribute("Script") != null ? element.Attribute("Script").Value : string.Empty,
                        PreScript = element.Attribute("PreScript") != null ? element.Attribute("PreScript").Value : string.Empty,
                        PostScript = element.Attribute("PostScript") != null ? element.Attribute("PostScript").Value : string.Empty,
                        Enabled = true
                    };

                    if (element.Attribute("Enabled") != null)
                    {
                        bool enabled;
                        if (Boolean.TryParse(element.Attribute("Enabled").Value, out enabled))
                        {
                            formula.Enabled = enabled;
                        }
                    }

                    if (element.Attribute("PracticeTimes") != null)
                    {
                        int practiceTimes = 0;
                        if (Int32.TryParse(element.Attribute("PracticeTimes").Value, out practiceTimes))
                        {
                            formula.PracticeTimes = practiceTimes;
                        }
                    }

                    this.plainListFormulas.Add(formula);
                }
                else
                {
                    formula = this.ReadGroup(element, defaultNamespace);
                }

                formulaGroup.SubNodes.Add(formula);
            }

            return formulaGroup;
        }

        private void SaveFormulas()
        {
            if (this.rootFormula == null || this.rootFormula.SubNodes == null)
            {
                return;
            }

            XNamespace ns = "http://schemas.fanrui.net/cubeexercise/2009/08/FormulasSchema.xsd";
            XDocument doc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), new XElement(ns + "Formulas"));

            foreach (Formula f in this.rootFormula.SubNodes)
            {
                XElement e = this.GetElement(f);
                doc.Root.Add(e);
            }

            string xmlPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            xmlPath = System.IO.Path.Combine(xmlPath, "Formulas.xml");
            try
            {
                doc.Save(xmlPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("无法保存公式文件Formulas.xml文件！错误信息:" + ex.ToString(), "错误", MessageBoxButton.OK);
            }
        }

        private XElement GetElement(Formula parent)
        {
            XNamespace ns = "http://schemas.fanrui.net/cubeexercise/2009/08/FormulasSchema.xsd";
            XElement e = null;
            if (parent.SubNodes != null)
            {
                e = new XElement(
                    ns + "Group",
                    new XAttribute("Name", parent.Name),
                    new XAttribute("Enabled", parent.Enabled));

                foreach (Formula f in parent.SubNodes)
                {
                    e.Add(GetElement(f));
                }
            }
            else
            {
                e = new XElement(
                        ns + "Formula",
                        new XAttribute("Name", parent.Name),
                        new XAttribute("Enabled", parent.Enabled),
                        new XAttribute("Image", parent.Image),
                        new XAttribute("Script", parent.Script),
                        new XAttribute("PreScript", parent.PreScript),
                        new XAttribute("PostScript", parent.PostScript),
                        new XAttribute("Demo", parent.Demo),
                        new XAttribute("PracticeTimes", parent.PracticeTimes));
            }


            return e;
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
            if (Keyboard.Modifiers != ModifierKeys.None)
            {
                return;
            }

            switch (e.Key)
            {
                case Key.Space:
                case Key.Enter:
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
