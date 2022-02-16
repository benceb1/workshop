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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Workshop02
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Content[] contents = new Content[]
            {
                new Content()
                {
                    Touched = false,
                    Question = "Melyik varázsigével űzte el Harry Potter a dementorokat?",
                    Answers = new string[] { "Capitulatus", "Expecto Patronum", "Vingardium leviosa", "Lumos" },
                    RightAnswer = "Expecto Patronum"
                },
                new Content()
                {
                    Touched = false,
                    Question = "Melyik találós kérdéssel nyerte el Zsákos Bilbó a gyűrűt Gollamtól?",
                    Answers = new string[] { "Mennyi egy töketlen fecske maximális repülési sebessége?", "Miért égnek a boszorkányok?", "Mi van a zsebemben?", "Nem is játszottak ilyet." },
                    RightAnswer = "Mi van a zsebemben?"
                },
            };

            foreach (Content content in contents)
            {
                Label label = new Label();
                label.Tag = content;
                label.Background = Brushes.LightBlue;
                label.Width = this.ActualWidth / 4;
                label.Margin = new Thickness(20);
                label.Height = this.ActualHeight / 4;
                label.Content = "⚡";
                label.FontSize = 72;
                label.HorizontalContentAlignment = HorizontalAlignment.Center;
                label.MouseLeftButtonDown += Label_MouseLeftButtonDown;
                wp_cards.Children.Add(label);
            }
        }

        private void Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Label label = sender as Label;
            Content content = (Content)label.Tag;

            if (!content.Touched)
            {
                QuizWindow quizWindow = new QuizWindow(content);
                if (quizWindow.ShowDialog() == true)
                {
                    label.BorderBrush = Brushes.LightGreen;
                }
                else
                {
                    label.BorderBrush = Brushes.LightPink;
                }
                label.BorderThickness = new Thickness(3);
                content.Touched = true;
            }
        }
    }
}
