using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Workshop01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Note> notes { get; set; }

        private int CurrentIndex { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            notes = new List<Note>();


            if (File.Exists("notes.json"))
            {
                notes = JsonConvert.DeserializeObject<Note[]>(File.ReadAllText("notes.json")).ToList();
            }


            if (notes.Count == 0)
            {
                tb_text.IsEnabled = false;
            } 
            else
            {
                notes.ForEach(note => { addButtonToPanel(note, sp_buttons); });

                SetCurrentIndex(0);
            }
        }

        private void btn_newnote_Click(object sender, RoutedEventArgs e)
        {
            Note note = new Note() { Title = tb_title.Text };
            tb_title.Clear();
            
            notes.Add(note);
            addButtonToPanel(note, sp_buttons);
            SetCurrentIndex(notes.Count - 1);

            if (notes.Count > 0)
            {
                tb_text.IsEnabled = true;
            }
        }

        public void SetCurrentIndex(int index)
        {
            CurrentIndex = index;
            tb_text.Text = notes[index].Text;
        }

        private void addButtonToPanel(Note note, Panel panel)
        {
            Button button = new Button();
            button.Name = note.Title;
            button.Content = note.Title;
            button.FontSize = 16;
            button.Margin = new Thickness(5);
            button.Click += Button_Click;
            panel.Children.Add(button);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (e.Source is Button)
            {
                string content = (e.Source as Button).Content.ToString();
                int index = notes.FindIndex(n => n.Title == content);
                SetCurrentIndex(index);
            }
        }

        private void tb_text_TextChanged(object sender, TextChangedEventArgs e)
        {
            notes[CurrentIndex].Text = (e.Source as TextBox).Text;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (notes.Count > 0)
            {
                string jsonData = JsonConvert.SerializeObject(notes);
                File.WriteAllText("notes.json", jsonData);
            }
        }

        class Note
        {
            public string Title { get; set; }
            public string Text { get; set; }
        }
    }
}
