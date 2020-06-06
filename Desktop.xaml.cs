using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace 高主动性的todo清单
{
    /// <summary>
    /// desktop.xaml 的交互逻辑
    /// </summary>
    public partial class Desktop : Window
    {
        public static Desktop desktop = new Desktop();
        private TaskService taskService = new TaskService();

        private Desktop()
        {
            InitializeComponent();
        }

        private void updateTaskList()
        {
            //清空原列表
            taskList.Children.RemoveRange(0, taskList.Children.Count);
            //更新新列表
            List<Task> list = taskService.Tasks;
            for (int i = 0; i < list.Count; i++)
            {
                Task task = list.ElementAt(i);
                showTask(task, false);
            }
        }

        private void showTask(Task task, bool isExpand)
        {
            int priority = task.Priority;
            string taskName = task.TaskName;
            string description = task.TaskDescription;
            DateTime dateTime = task.TaskDate;

            //整体卡片
            Card card = new Card();
            card.Tag = task;
            card.Width = 406;
            card.Margin = new Thickness(0, 16, 0, 0);
            taskList.Children.Add(card);

            //收缩框
            Expander expander = new Expander();
            expander.HorizontalAlignment = HorizontalAlignment.Stretch;
            expander.Header = taskName;
            card.Content = expander;
            //展开任务面板
            if (isExpand)
            {
                expander.IsExpanded = true;
            }

            //stack面板
            StackPanel stackPanel = new StackPanel();
            stackPanel.Margin = new Thickness(24, 8, 24, 16);
            expander.Content = stackPanel;

            //任务的描述文本框
            TextBlock textBlock = new TextBlock();
            textBlock.Opacity = 0.68;
            textBlock.Text = description;
            textBlock.TextWrapping = TextWrapping.Wrap;
            stackPanel.Children.Add(textBlock);
            textBlock.Tag = task.TaskId;
            textBlock.DataContextChanged += Description_Changed;

            //优先级选择栏
            ComboBox comboBox = new ComboBox();
            comboBox.Style = FindResource("MaterialDesignFilledComboBox") as Style;
            HintAssist.SetHint(comboBox, "优先级");
            comboBox.Margin = new Thickness(0, 8, 0, 0);
            comboBox.Width = 230;
            comboBox.HorizontalAlignment = HorizontalAlignment.Left;
            comboBox.Items.Add("十万火急");
            comboBox.Items.Add("很急");
            comboBox.Items.Add("一般");
            comboBox.Items.Add("不急");
            comboBox.SelectedIndex = priority;
            comboBox.Tag = task.TaskId;
            comboBox.SelectionChanged += Priority_Changed;
            stackPanel.Children.Add(comboBox);

            //时间日期面板
            StackPanel stackPanel1 = new StackPanel();
            stackPanel1.HorizontalAlignment = HorizontalAlignment.Left;
            stackPanel1.Orientation = Orientation.Horizontal;
            stackPanel1.Margin = new Thickness(0, 8, 0, 0);
            stackPanel.Children.Add(stackPanel1);
            //日期
            DatePicker datePicker = new DatePicker();
            datePicker.HorizontalAlignment = HorizontalAlignment.Left;
            datePicker.Width = 82;
            datePicker.Style = FindResource("MaterialDesignFloatingHintDatePicker") as Style;
            HintAssist.SetHint(datePicker, "任务日期");
            datePicker.Margin = new Thickness(16, 0, 16, 0);
            datePicker.SelectedDate = dateTime;
            datePicker.Tag = task.TaskId;
            datePicker.SelectedDateChanged += Date_Changed; ;
            stackPanel1.Children.Add(datePicker);
            //时间
            TimePicker timePicker = new TimePicker();
            timePicker.HorizontalAlignment = HorizontalAlignment.Left;
            timePicker.Width = 82;
            timePicker.Style = FindResource("MaterialDesignFloatingHintTimePicker") as Style;
            HintAssist.SetHint(timePicker, "时间");
            timePicker.SelectedTime = dateTime;
            timePicker.Tag = task.TaskId;
            timePicker.SelectedTimeChanged += Time_Changed;
            stackPanel1.Children.Add(timePicker);

            //子任务树
            TreeView subTaskTree = new TreeView();
            subTaskTree.Margin = new Thickness(0, 16, 0, 0);
            subTaskTree.MinWidth = 220;
            subTaskTree.Tag = task.TaskId;

            List<SubTask> subTasks = task.SubTasks;
            if (subTasks != null)
            {
                for (int j = 0; j < subTasks.Count; j++)
                {
                    SubTask subTask = subTasks.ElementAt(j);
                    List<SubTask> subSonTasks = subTask.SubTasks;
                    TreeViewItem treeViewItem = createTreeViewItem(subTask);
                    //二级子任务
                    for (int k = 0; k < subSonTasks.Count; k++)
                    {
                        TreeViewItem sonTreeViewItem = createTreeViewItem(subSonTasks.ElementAt(k));
                        treeViewItem.Items.Add(sonTreeViewItem);
                    }
                    //新增二级子任务按钮
                    TreeViewItem addSon = new TreeViewItem();
                    addSon.Header = "+";
                    addSon.MouseUp += AddSon_MouseUp;
                    addSon.Tag = treeViewItem;
                    treeViewItem.Items.Add(addSon);

                    subTaskTree.Items.Add(treeViewItem);
                }
            }
            //新增子任务按钮
            TreeViewItem addParent = new TreeViewItem();
            addParent.Header = "+";
            addParent.Tag = subTaskTree;
            addParent.MouseUp += AddParent_MouseUp;
            subTaskTree.Items.Add(addParent);
            stackPanel.Children.Add(subTaskTree);
        }

        private void Time_Changed(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            TimePicker time_picker = (TimePicker)sender;
            taskService.changeTime((int)time_picker.Tag, time_picker.SelectedTime);
        }

        private void Date_Changed(object sender, SelectionChangedEventArgs e)
        {
            DatePicker date_picker = (DatePicker)sender;
            taskService.changeDate((int)date_picker.Tag, date_picker.SelectedDate);
        }

        /**
         * 任务优先级变更时
         * */
        private void Priority_Changed(object sender, SelectionChangedEventArgs e)
        {
            ComboBox priority_box = (ComboBox)sender;
            taskService.changePriority((int)priority_box.Tag, priority_box.SelectedIndex);
        }

        /**
         * 任务描述变更时
         */
        private void Description_Changed(object sender, DependencyPropertyChangedEventArgs e)
        {
            TextBlock description_Text = (TextBlock)sender;
            taskService.changeDescription((int)description_Text.Tag, description_Text.Text);
        }

        /**
         * 创建文本框-双按钮的子任务树节点
         */
        private TreeViewItem createTreeViewItem(SubTask subTask)
        {
            TreeViewItem result = new TreeViewItem();
            result.IsExpanded = true;
            StackPanel stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Horizontal;
            stackPanel.HorizontalAlignment = HorizontalAlignment.Left;

            TextBox textBox = new TextBox();
            textBox.HorizontalAlignment = HorizontalAlignment.Left;
            textBox.Height = 30;
            textBox.Width = 180;
            textBox.Text = subTask.SubTaskName;
            textBox.Tag = subTask.Id;
            textBox.LostFocus += subTaskName_LostFocus;
            stackPanel.Children.Add(textBox);

            Button link = new Button();
            link.Style = FindResource("MaterialDesignFloatingActionMiniLightButton") as Style;
            link.Height = 20;
            link.Width = 20;
            link.HorizontalAlignment = HorizontalAlignment.Right;
            PackIcon linkIcon = new PackIcon();
            linkIcon.Kind = PackIconKind.Link;
            linkIcon.Height = 12;
            linkIcon.Width = 12;
            link.Content = linkIcon;
            stackPanel.Children.Add(link);

            Button edit = new Button();
            edit.Style = FindResource("MaterialDesignFloatingActionMiniLightButton") as Style;
            edit.Height = 20;
            edit.Width = 20;
            edit.HorizontalAlignment = HorizontalAlignment.Right;
            PackIcon editIcon = new PackIcon();
            editIcon.Kind = PackIconKind.Pen;
            editIcon.Height = 12;
            editIcon.Width = 12;
            edit.Content = editIcon;
            stackPanel.Children.Add(edit);

            result.Header = stackPanel;
            return result;
        }

        /**
         * 更新子任务名
         * */
        private void subTaskName_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox nameTextBox = (TextBox)sender;
            String newName = nameTextBox.Text;

        }

        /**
         * 添加一级子任务节点
         */
        private void AddParent_MouseUp(object sender, MouseButtonEventArgs e)
        {
            TreeViewItem addParent = (TreeViewItem)sender;
            TreeView subTaskTree = (TreeView)addParent.Tag;
            TreeViewItem treeViewItem = createTreeViewItem(taskService.addParentSubTask((int)subTaskTree.Tag));
            TreeViewItem addSon = new TreeViewItem();
            addSon.Header = "+";
            addSon.MouseUp += AddSon_MouseUp;
            addSon.Tag = treeViewItem;
            treeViewItem.Items.Add(addSon);
            //移除“+”
            subTaskTree.Items.Remove(addParent);
            //添加新子任务
            subTaskTree.Items.Add(treeViewItem);
            //还原“+”
            subTaskTree.Items.Add(addParent);
        }

        private void AddSon_MouseUp(object sender, MouseButtonEventArgs e)
        {
            TreeViewItem addSon = (TreeViewItem)sender;
            TreeViewItem sonTree = (TreeViewItem)addSon.Tag;
            TreeViewItem son = new TreeViewItem();
            son.Header = "新的一步";
            //移除“+”
            sonTree.Items.Remove(addSon);
            //添加新子任务
            sonTree.Items.Add(son);
            //还原“+”
            sonTree.Items.Add(addSon);
        }

        private void getAllTask()
        {
            taskService.getAllTasks();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox searchBox = (TextBox)sender;
            taskService.search(searchBox.Text);
            updateTaskList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //插入新任务
            Task newTask = taskService.addNewTask();
            //在最底部添加新任务卡片控件,并展开
            showTask(newTask, true);
            //滚动到最底部
            scrollView.ScrollToVerticalOffset(scrollView.ScrollableHeight);

        }
    }
}
