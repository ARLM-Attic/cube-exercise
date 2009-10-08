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
// <history date="09/15/2009" Author="Rui Fan">
//     Reorganize algorithms management.
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
        private const string DefaultNameSpace = "http://schemas.fanrui.net/CubeExercise/2009/09/AlgorithmFile";

        /// <summary>
        /// Cache the file version.
        /// </summary>
        private static string fileVersion;

        private List<AlgorithmFile> algorithmFiles;

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

        /// <summary>
        /// The timer which is used to decide when to show the algorithm script.
        /// </summary>
        private DispatcherTimer timerShowAlgorithm;

        /// <summary>
        /// The timer which is used to decide when to switch to next algorithm automatically.
        /// </summary>
        private DispatcherTimer timerAlgorithmLimit;

        private Group root;

        /// <summary>
        /// A plain list is helpful for the iteration of the algorithms
        // because we don't have to find sub nodes recursivly.
        /// </summary>
        private List<Algorithm> plainListAlgorithms;

        private List<AlgorithmReference> plainListAlgorithmReferences;

        private SortedDictionary<int, Algorithm> algorithmDict;

        private List<Algorithm> exercising;

        private Algorithm currentAlgorithm;

        public MainWindow()
        {
            this.algorithmFiles = new List<AlgorithmFile>();
            this.algorithmDict = new SortedDictionary<int, Algorithm>();
            this.root = new Group();
            this.plainListAlgorithms = new List<Algorithm>();
            this.plainListAlgorithmReferences = new List<AlgorithmReference>();
            this.timerShowAlgorithm = new DispatcherTimer();
            this.timerAlgorithmLimit = new DispatcherTimer();
            this.timerShowAlgorithm.Tick += new EventHandler(timerShowAlgorithm_Tick);
            this.timerAlgorithmLimit.Tick += new EventHandler(timerAlgorithmLimit_Tick);
            this.ExerciseConfiguration = new ExerciseConfiguration();
            InitializeComponent();
            this.InitializeAlgorithms();
            this.InitializeTree();
        }

        private void InitializeTree()
        {
            TreeViewItem tempRoot = new TreeViewItem();
            this.isInitializing = true;
            this.GetSubTreeItems(this.root, tempRoot);
            this.isInitializing = false;

            // Transfer the sub items from the TreeViewItem to the TreeView.
            while (tempRoot.Items.Count > 0)
            {
                // One TreeViewItem can have only one logical parent. So the current
                // parent must remove it from its children.
                object subItem = tempRoot.Items[0];
                tempRoot.Items.RemoveAt(0);
                this.tvAlgorithms.Items.Add(subItem);
            }
        }

        private TreeViewItem GetSubTreeItems(Group group, TreeViewItem tvItem)
        {
            {
                // Initialize the node itself.
                StackPanel panel = new StackPanel();
                panel.BeginInit();
                // This binding is OneWay by default. So use a explicit binding configuration.
                Binding expandedBinding = new Binding("Expanded") { Source = group, Mode = BindingMode.TwoWay };
                tvItem.SetBinding(TreeViewItem.IsExpandedProperty, expandedBinding);
                tvItem.Header = panel;
                tvItem.Tag = group;
                tvItem.ContextMenu = (ContextMenu)this.tvAlgorithms.FindResource("cmGroup");
                tvItem.ContextMenuOpening += new ContextMenuEventHandler(group_ContextMenuOpening);

                panel.HorizontalAlignment = HorizontalAlignment.Left;
                panel.VerticalAlignment = VerticalAlignment.Center;
                panel.Orientation = Orientation.Horizontal;
                CheckBox cb = new CheckBox() { VerticalAlignment = VerticalAlignment.Center, IsChecked = null };
                panel.Children.Add(cb);
                cb.Checked += new RoutedEventHandler(cb_CheckedChanged);
                cb.Unchecked += new RoutedEventHandler(cb_CheckedChanged);
                cb.DataContext = group;
                cb.IsChecked = group.Enabled;
                //Binding enabledBinding = new Binding("Enabled") { Mode = BindingMode.OneWayToSource };
                //cb.SetBinding(CheckBox.IsCheckedProperty, enabledBinding);

                TextBlock tb = new TextBlock() { VerticalAlignment = VerticalAlignment.Center };
                tb.DataContext = group;
                tb.SetBinding(TextBlock.TextProperty, "Name");
                panel.Children.Add(tb);
                panel.EndInit();
            }

            if (group.Items == null || group.Items.Count() <= 0)
            {
                return tvItem;
            }

            // TODO: Tag, DataContext, Algorithm, AlgorithmReference... It is a little
            // cluttered here. Refector it later.
            // Initialize the subnodes.
            foreach (object item in group.Items)
            {
                if (item.GetType() == typeof(AlgorithmReference))
                {
                    AlgorithmReference r = (AlgorithmReference)item;
                    if (group.Enabled == false)
                    {
                        r.Enabled = false;
                    }

                    Algorithm a = this.ResolveReference(r);
                    r.Algorithm = a;

                    this.plainListAlgorithmReferences.Add(r);

                    Button itemButton = new Button();
                    itemButton.BeginInit();
                    itemButton.Tag = a;
                    itemButton.Click += new RoutedEventHandler(Button_Click);

                    TreeViewItem subItem = new TreeViewItem();
                    subItem.Header = itemButton;
                    subItem.Tag = r;
                    subItem.ContextMenu = (ContextMenu)this.tvAlgorithms.FindResource("cmAlgorithm");
                    subItem.ContextMenuOpening += new ContextMenuEventHandler(algorithm_ContextMenuOpening);
                    tvItem.Items.Add(subItem);

                    StackPanel subPanel = new StackPanel();
                    itemButton.Content = subPanel;
                    subPanel.HorizontalAlignment = HorizontalAlignment.Left;
                    subPanel.VerticalAlignment = VerticalAlignment.Center;
                    subPanel.Orientation = Orientation.Horizontal;
                    CheckBox subcb = new CheckBox() { VerticalAlignment = VerticalAlignment.Center, IsChecked = null };
                    subPanel.Children.Add(subcb);
                    subcb.Checked += new RoutedEventHandler(cb_CheckedChanged);
                    subcb.Unchecked += new RoutedEventHandler(cb_CheckedChanged);
                    subcb.DataContext = r;
                    subcb.SetBinding(CheckBox.IsCheckedProperty, "Enabled");

                    if (!string.IsNullOrEmpty(a.Image) && System.IO.File.Exists(a.Image))
                    {
                        string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                        path = System.IO.Path.Combine(path, a.Image);
                        subPanel.Children.Add(new Image() { Source = new BitmapImage(new Uri(path, UriKind.Absolute)) });
                    }
                    TextBlock subtb = new TextBlock() { VerticalAlignment = VerticalAlignment.Center };
                    subtb.DataContext = r;
                    subtb.SetBinding(TextBlock.TextProperty, "Name");
                    subPanel.Children.Add(subtb);
                    itemButton.EndInit();
                }
                else
                {
                    Group g = (Group)item;
                    if (group.Enabled == false)
                    {
                        g.Enabled = false;
                    }

                    g.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(g_PropertyChanged);
                    TreeViewItem subItem = new TreeViewItem();
                    tvItem.Items.Add(subItem);
                    this.GetSubTreeItems(g, subItem);
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
                    if (item.GetType() == typeof(AlgorithmReference))
                    {
                        ((AlgorithmReference)item).Enabled = g.Enabled;
                    }
                    else if (item.GetType() == typeof(Group))
                    {
                        ((Group)item).Enabled = g.Enabled;
                    }
                }
            }
        }

        /// <summary>
        /// The binding should be one-way when initializing the tree for the first time.
        /// After it is initialized, updates from the UI should sync to the algorithms and groups.
        /// </summary>
        private bool isInitializing = true;

        private void cb_CheckedChanged(object sender, RoutedEventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            if (!this.isInitializing)
            {
                if (cb.DataContext is Group && cb.IsChecked.HasValue)
                {
                    ((Group)cb.DataContext).Enabled = cb.IsChecked.Value;
                }
            }
            TreeViewItem tvItem = this.FindParentTreeViewItem(cb);
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

        private Algorithm ResolveReference(AlgorithmReference reference)
        {
            if (this.algorithmDict.ContainsKey(reference.Id))
            {
                return this.algorithmDict[reference.Id];
            }

            return null;
        }

        private void InitializeAlgorithms()
        {
            string xmlPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            xmlPath = System.IO.Path.Combine(xmlPath, "Algorithms.xml");
            if (System.IO.File.Exists(xmlPath))
            {
                this.algorithmFiles.Clear();
                this.algorithmDict.Clear();
                this.plainListAlgorithms.Clear();

                XmlSerializer s = new XmlSerializer(typeof(AlgorithmFile), DefaultNameSpace);
                AlgorithmFile file = (AlgorithmFile)s.Deserialize(XmlReader.Create(xmlPath));
                this.algorithmFiles.Add(file);
                file.Path = xmlPath;
                this.root.Items = file.Group;

                foreach (Algorithm a in file.Algorithms)
                {
                    if (!this.algorithmDict.ContainsKey(a.Id))
                    {
                        this.algorithmDict.Add(a.Id, a);
                        this.plainListAlgorithms.Add(a);
                    }

                    // TODO: Duplicate Id found. Show a warning message here.
                }
            }
        }

        private void SaveAlgorithms()
        {
            if (this.root == null || this.root.Items == null)
            {
                return;
            }

            try
            {
                foreach (AlgorithmFile file in this.algorithmFiles)
                {
                    XmlSerializer s = new XmlSerializer(typeof(AlgorithmFile));
                    using (TextWriter textWriter = new StreamWriter(file.Path, false, Encoding.UTF8))
                    {
                        s.Serialize(textWriter, file);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("无法保存公式文件！错误信息:" + ex.ToString(), "错误", MessageBoxButton.OK);
            }
        }

        private void Transform(bool reverse)
        {
            try
            {
                string todo = this.txtAlgorithm.Text;
                if (this.txtAlgorithm.SelectionLength != 0)
                {
                    todo = this.txtAlgorithm.SelectedText;
                }

                this.cube.Transform(todo, reverse);
            }
            catch (ArgumentException ex)
            {
                string msg = string.Format("公式不正确！错误信息：{0}", ex.ToString());
                MessageBox.Show(msg, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnInitialize_Click(object sender, RoutedEventArgs e)
        {
            this.cube.InitializeCube();
        }

        private void btnTransform_Click(object sender, RoutedEventArgs e)
        {
            this.Transform(false);
        }

        private void btnReverseTransform_Click(object sender, RoutedEventArgs e)
        {
            this.Transform(true);
        }

        private void btnFind_Click(object sender, RoutedEventArgs e)
        {
            string todo = this.txtAlgorithm.Text;
            if (this.txtAlgorithm.SelectionLength != 0)
            {
                todo = this.txtAlgorithm.SelectedText;
            }

            if (string.IsNullOrEmpty(todo))
            {
                return;
            }

            int i = 0;
            Cube<int> tempCube = new Cube<int>(0, 1, 2, 3, 4, 5);
            do
            {
                tempCube.DoAlgorithm(todo);
                i++;
            } while (!tempCube.IsRecovered());

            MessageBox.Show("如果重复做公式" + todo + "，将在" + i + "次后回到初始状态。");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                Algorithm f = btn.Tag as Algorithm;
                if (f != null)
                {
                    this.txtAlgorithm.Text = f.Script;
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
                this.exercising = new List<Algorithm>();
            }
            else
            {
                this.exercising.Clear();
            }

            if (this.ExerciseConfiguration.Mode == ExerciseMode.Descending)
            {
                for (int i = this.plainListAlgorithmReferences.Count - 1; i >= 0; i--)
                {
                    if (this.plainListAlgorithmReferences[i].Enabled)
                    {
                        Algorithm a = this.ResolveReference(this.plainListAlgorithmReferences[i]);
                        if (this.exercising.Find((alg) => { return alg.Id == a.Id; }) == null)
                        {
                            this.exercising.Add(a);
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < this.plainListAlgorithmReferences.Count; i++)
                {
                    if (this.plainListAlgorithmReferences[i].Enabled)
                    {
                        Algorithm a = this.ResolveReference(this.plainListAlgorithmReferences[i]);
                        if (this.exercising.Find((alg) => { return alg.Id == a.Id; }) == null)
                        {
                            this.exercising.Add(a);
                        }
                    }
                }
            }

            this.exercising.Sort((alg1, alg2) => { return alg1.Id - alg2.Id; });

            if (this.ExerciseConfiguration.Mode != ExerciseMode.RepeatableRandom)
            {
                if (this.ExerciseConfiguration.NumberOfAlgorithms > this.exercising.Count || this.ExerciseConfiguration.NumberOfAlgorithms <= 0)
                {
                    this.ExerciseConfiguration.NumberOfAlgorithms = this.exercising.Count;
                }
            }
            else
            {
                this.ExerciseConfiguration.NumberOfAlgorithms = 999;
            }

            if (this.chkRecordScript.IsChecked.Value == true)
            {
                this.txtAlgorithm.Clear();
            }

            this.stopWatch.Reset();
            this.ToggleControlStatus(ExerciseStatus.Started);
            this.ShowNextAlgorithm();
        }

        private void ShowNextAlgorithm()
        {
            if (this.exercising.Count > 0 && this.ExerciseConfiguration.NumberOfAlgorithms > 0)
            {
                this.tbAlgorithmPrompt.Text = string.Empty;
                this.timerShowAlgorithm.Stop();
                this.timerAlgorithmLimit.Stop();

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

                this.currentAlgorithm = this.exercising.ElementAt(index);
                this.currentAlgorithm.PracticeTimes++;
                this.ExerciseConfiguration.NumberOfAlgorithms--;
                if (this.ExerciseConfiguration.Mode != ExerciseMode.RepeatableRandom)
                {
                    this.exercising.RemoveAt(index);
                }

                string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                path = System.IO.Path.Combine(path, this.currentAlgorithm.Image);
                this.imgExercise.Source = new BitmapImage(new Uri(path, UriKind.Absolute));
                string fullScript = "";
                fullScript += "/*" + this.currentAlgorithm.Name + "*/ ";
                if (!string.IsNullOrEmpty(this.currentAlgorithm.PreScript))
                {
                    fullScript += "[" + this.currentAlgorithm.PreScript + "]";
                }

                fullScript += this.currentAlgorithm.Script;
                if (!string.IsNullOrEmpty(this.currentAlgorithm.PostScript))
                {
                    fullScript += "[" + this.currentAlgorithm.PostScript + "]";
                }

                fullScript += Environment.NewLine;

                if (this.chkRecordScript.IsChecked.HasValue && this.chkRecordScript.IsChecked.Value == true)
                {
                    this.txtAlgorithm.AppendText(fullScript);
                }

                this.cubeStatus.cube.Transform(fullScript);

                if (this.ExerciseConfiguration.ShowScriptDelay == 0)
                {
                    this.ShowAlgorithm(this.currentAlgorithm);
                }
                else if (this.ExerciseConfiguration.ShowScriptDelay > 0)
                {
                    this.timerShowAlgorithm.Interval = TimeSpan.FromSeconds(this.ExerciseConfiguration.ShowScriptDelay);
                    this.timerShowAlgorithm.Start();
                }

                if (this.ExerciseConfiguration.AlgorithmTimeLimit > 0)
                {
                    this.timerAlgorithmLimit.Interval = TimeSpan.FromSeconds(this.ExerciseConfiguration.AlgorithmTimeLimit);
                    this.timerAlgorithmLimit.Start();
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
                        ShowNextAlgorithm();
                    }

                    e.Handled = true;
                    break;

                case Key.N:
                    if (this.stopWatch.IsRunning)
                    {
                        Algorithm f = this.currentAlgorithm;
                        if (f != null)
                        {
                            this.exercising.Add(f);
                            this.ExerciseConfiguration.NumberOfAlgorithms++;
                        }

                        ShowNextAlgorithm();
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

                case Key.R:
                    this.cubeStatus.cube.InitializeCube();
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
            this.txtAlgorithm.Clear();
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
            this.SaveAlgorithms();
        }

        void timerShowAlgorithm_Tick(object sender, EventArgs e)
        {
            this.ShowAlgorithm(this.currentAlgorithm);
            this.timerShowAlgorithm.Stop();
        }

        void timerAlgorithmLimit_Tick(object sender, EventArgs e)
        {
            this.timerAlgorithmLimit.Stop();
            this.ShowNextAlgorithm();
        }

        private void ShowAlgorithm(Algorithm algorithm)
        {
            if (algorithm != null)
            {
                string format = "名称：{1}{0}公式：{2}{0}练习次数：{3}";
                this.tbAlgorithmPrompt.Text = string.Format(format, Environment.NewLine, algorithm.Name, algorithm.Script, algorithm.PracticeTimes.ToString());
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
                    this.timerShowAlgorithm.Stop();
                    this.timerAlgorithmLimit.Stop();
                    this.exercising.Clear();
                    this.tbAlgorithmPrompt.Text = string.Empty;
                    break;
                case ExerciseStatus.Paused:
                    this.btnStartExercise.IsEnabled = false;
                    this.btnStopExercise.IsEnabled = true;
                    this.btnPauseExercise.IsEnabled = true;
                    this.txtRepeatTimes.IsEnabled = false;
                    this.cmbRandomMode.IsEnabled = false;
                    this.stopWatch.Stop();
                    this.timerShowAlgorithm.Stop();
                    this.timerAlgorithmLimit.Stop();
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
                        this.timerShowAlgorithm.Start();
                    }

                    if (this.ExerciseConfiguration.AlgorithmTimeLimit > 0)
                    {
                        this.timerAlgorithmLimit.Start();
                    }
                    break;
            }
        }

        private void mainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.SaveAlgorithms();
            this.cubeStatus.ParentClosing = true;
            this.cubeStatus.Close();
        }

        PopupCube cubeStatus = new PopupCube();

        private void chkShowRealtimeStatus_Checked(object sender, RoutedEventArgs e)
        {
            this.cubeStatus.Show();
        }

        private void chkShowRealtimeStatus_Unchecked(object sender, RoutedEventArgs e)
        {
            this.cubeStatus.Hide();
        }

        private void btnResetRealtimeStatus_Click(object sender, RoutedEventArgs e)
        {
            this.cubeStatus.cube.InitializeCube();
            this.imgExercise.Focus();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            e.ToString();
        }

        private void group_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            //e.Handled = true;
        }

        private void algorithm_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            e.Handled = true;
            FrameworkElement fe = sender as FrameworkElement;
            fe.ContextMenu.IsOpen = true;
        }
    }
}
