using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KR
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Main_menu.Children.Add(Set_Image("Images/Main_Bg_lvl1.png",1920,1080));
            Main_menu.Children.Add(Set_Image("Images/hills_lvl1.png",1920,1080));
            //Main_menu.Children.Add(Set_Image("C:\\Users\\ПК\\Desktop\\Kyrs\\KR\\Images\\Light_lvl2.png", 1920, 1080));
        }
        public Image Set_Image(string image_name,double Widht,double Height)
        {
            Image image = new Image();
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(image_name, UriKind.RelativeOrAbsolute);
            bitmap.EndInit();
            image.Source = bitmap;
            image.Width =Widht;
            image.Height =Height;
            return image;
        }
        public Button Set_Button(string content)
        {
            Button button = new Button();
            button.Content = content;
            return button;
        }
    }
}