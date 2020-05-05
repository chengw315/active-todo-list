using MaterialDesignThemes.Wpf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace 高主动性的todo清单
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //用 所有任务 初始化任务列表
            initTaskList(getAllTask());
        }

        private void initTaskList(List<Task> list)
        {
            int i = 1;
            int priority = 1;
            string taskName = "代码任务";
            string description = "这是代码生成的任务";
            DateTime dateTime = new DateTime();

            //整体卡片
            Card card = new Card();
            card.Name = "card" + i.ToString();
            card.Width = 406;
            card.Margin = new Thickness(0, 16, 0, 0);
            taskList.Children.Add(card);

            //收缩框
            Expander expander = new Expander();
            expander.HorizontalAlignment = HorizontalAlignment.Stretch;
            expander.Header = taskName;
            card.Content = expander;

            //stack面板
            StackPanel stackPanel = new StackPanel();
            stackPanel.Margin = new Thickness(24,8,24,16);
            expander.Content = stackPanel;

            //任务的描述文本框
            TextBlock textBlock = new TextBlock();
            textBlock.Opacity = 0.68;
            textBlock.Text = description;
            textBlock.TextWrapping = TextWrapping.Wrap;
            stackPanel.Children.Add(textBlock);

            //优先级选择栏
            ComboBox comboBox = new ComboBox();
            comboBox.Style = FindResource("MaterialDesignFilledComboBox") as Style;
            comboBox.Name = "priority" + i.ToString();
            HintAssist.SetHint(comboBox, "优先级");
            comboBox.Margin = new Thickness(0, 8, 0, 0);
            comboBox.Width = 230;
            comboBox.HorizontalAlignment = HorizontalAlignment.Left;
            comboBox.Items.Add("十万火急");
            comboBox.Items.Add("很急");
            comboBox.Items.Add("一般");
            comboBox.Items.Add("不急");
            comboBox.SelectedIndex = priority;
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
            stackPanel1.Children.Add(datePicker);
            //时间
            TimePicker timePicker = new TimePicker();
            timePicker.HorizontalAlignment = HorizontalAlignment.Left;
            timePicker.Width = 82;
            timePicker.Style = FindResource("MaterialDesignFloatingHintTimePicker") as Style;
            HintAssist.SetHint(timePicker, "时间");
            timePicker.SelectedTime = dateTime;
            stackPanel1.Children.Add(timePicker);

            //子任务树

        }

        private List<Task> getAllTask()
        {
            List<Task> result = new List<Task>();
            result.Add(new Task(1, "代码定义的任务", "这是代码定义的任务", new DateTime(), 1, 20, new List<SubTask>()));
            return result;
        }


        private void Image_KeyUp(object sender, KeyEventArgs e)
        {
          

        }

        private void Card_KeyUp(object sender, KeyEventArgs e)
        {
            
        }

        private void Card_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void Card_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Card card = (Card)sender;
            //扩展
            //card.
            
        }
    }
}
