using System;
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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Workshop02
{
    /// <summary>
    /// Interaction logic for QuizWindow.xaml
    /// </summary>
    /// 
    
    public partial class QuizWindow : Window
    {
        private Content Content;
        private int Tick = 60;
        private bool CloseWithCloseButton = true;

        public QuizWindow(Content c)
        {
            InitializeComponent();
            this.Content = c;
            lb_question.Content = c.Question;

            foreach (var item in c.Answers)
            {
                Label label = new Label();
                label.Content = item;
                label.Background = Brushes.LightGray;
                label.Margin = new Thickness(20);
                label.MouseLeftButtonDown += L_MouseLeftButtonDown;
                wp_answers.Children.Add(label);
            }

            DispatcherTimer dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromSeconds(1);
            dt.Tick += dt_Ticker;
            dt.Start();
        }

        private void dt_Ticker(object sender, EventArgs args)
        {
            Tick--;
            lb_time.Content = Tick.ToString();
            if (Tick == 0)
            {
                this.DialogResult = false;
            }
        }

        private void L_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Label label = (sender as Label);
            string answer = label.Content.ToString();
            this.CloseWithCloseButton = false;
            if (answer == Content.RightAnswer)
            {
                this.DialogResult = true;
            }
            else
            {
                this.DialogResult = false;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (CloseWithCloseButton)
            {
                MessageBoxResult result = MessageBox.Show("Biztos be szeretnéd zárni?", "hahi", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.No)
                {
                    e.Cancel = true;
                }
                else
                {
                    this.DialogResult = false;
                }
            }
        }
    }
}
