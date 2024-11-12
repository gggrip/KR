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

namespace KR
{
    /// <summary>
    /// Логика взаимодействия для Text_window.xaml
    /// </summary>
    public partial class Text_window : Window
    {
        public string InputText { get; private set; }
        public Text_window()
        {
            InitializeComponent();
        }
        public void But_click(object sender, RoutedEventArgs e)
        {
            InputText = Input_Box.Text;
            DialogResult = true;
            this.Close();
        }
    }
}
