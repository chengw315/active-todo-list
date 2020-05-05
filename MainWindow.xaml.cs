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
            string taskName = "代码任务";
            string description = "这是代码生成的任务";

            //卡片
            Card card = new Card();
            card.Name = "card" + i.ToString();
            card.Margin = new Thickness(0, 16, 0, 0);

            //收缩框
            Expander expander = new Expander();
            expander.HorizontalAlignment = HorizontalAlignment.Stretch;
            expander.Header = taskName;
            card.Content = expander;

            //stack面板
            StackPanel stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Horizontal;
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
            //comboBox.
            stackPanel.Children.Add(comboBox);
  

            taskList.Children.Add(card); 
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
