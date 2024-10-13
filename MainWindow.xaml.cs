using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

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
            ResourceDictionary resourceDictionary = new ResourceDictionary();
            resourceDictionary.Source = new Uri("Styles/Main_menu.xaml",UriKind.RelativeOrAbsolute);
            this.Resources.MergedDictionaries.Add(resourceDictionary);
            Main_menu_can.Width = 1920;
            Main_menu_can.Height = 1080;
            Main_menu_can.Children.Add(Set_Image("Images/Main_Bg_lvl1.png",1920,1080));
            List<Image> clouds = new List<Image>();
            for (int i = 1; i < 4; i++)
            {
                clouds.Add(Set_Image("Images/cloud_" + Convert.ToString(i) + "_lvl1.png", 800, 250));
                Canvas.SetLeft(clouds[i - 1], -800*i);
                Canvas.SetTop(clouds[i - 1], new Random().Next(-200, 512));
                Main_menu_can.Children.Add(clouds[i - 1]);
                MoveImage(clouds[i - 1],-800*i, 1920, (800*i + 1920)/54.4);          
            }
            Main_menu_can.Children.Add(Set_Image("Images/hills_lvl1.png",1920,1080));
            Button button;
            button = Set_Button("Play", (Style)this.Resources["lvl1"]);
            Canvas.SetLeft(button,(Main_menu_can.Width - button.Width) / 2);
            Canvas.SetTop(button,(Main_menu_can.Height - button.Height) / 2);
            Main_menu_can.Children.Add(button);
            button = Set_Button("Redactor", (Style)this.Resources["lvl1"]);
            Canvas.SetLeft(button, (Main_menu_can.Width - button.Width) / 2);
            Canvas.SetTop(button, (Main_menu_can.Height - button.Height*4) / 2);
            Main_menu_can.Children.Add(button);
            button = Set_Button("Exit", (Style)this.Resources["lvl1"]);
            Canvas.SetLeft(button, (Main_menu_can.Width - button.Width) / 2);
            Canvas.SetTop(button, (Main_menu_can.Height + button.Height * 2) / 2);
            Main_menu_can.Children.Add(button);
            DispatcherTimer _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(50);
            //_timer.Tick += MoveButton;
            _timer.Start();
            //Main_menu.Children.Add(Set_Image("C:\\Users\\ПК\\Desktop\\Kyrs\\KR\\Images\\Light_lvl2.png", 1920, 1080));
        }
        public Image Set_Image(string image_name,double Widht,double Height)
        {
            Image image = new Image();
            image.Width = Widht;
            image.Height = Height;
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(image_name, UriKind.RelativeOrAbsolute);
            bitmap.EndInit();
            image.Source = bitmap;
            return image;
        }
        public Button Set_Button(string content,Style st)
        {
            Button button = new Button();
            button.Content = content;
            button.Name = content;
            button.Style = st;
            return button;
        }
        private void MoveImage(Image image,double fromX,double toX,double duraction)
        {
            DoubleAnimation moveXAnimation = new DoubleAnimation()
            {
                From = fromX,
                To = toX,
                Duration = TimeSpan.FromSeconds(duraction),
                FillBehavior = FillBehavior.HoldEnd,
            };
            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(moveXAnimation);
            Storyboard.SetTarget(moveXAnimation,image);
            Storyboard.SetTargetProperty(moveXAnimation, new PropertyPath("(Canvas.Left)"));
            moveXAnimation.Completed += (s, e) =>
            {
                Canvas.SetLeft(image, 0);
                Canvas.SetTop(image, new Random().Next(-200, 512));
                storyboard.Begin();
            };
            storyboard.Begin();
        }
    }
}