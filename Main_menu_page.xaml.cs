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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace KR
{
    /// <summary>
    /// Логика взаимодействия для Main_menu_page.xaml
    /// </summary>
    public partial class Main_menu_page : Page
    {
        public Main_menu_page()
        {
            InitializeComponent();
            //Загрузка ресурсов
            ResourceDictionary resourceDictionary = new ResourceDictionary();
            resourceDictionary.Source = new Uri("Styles/Main_menu.xaml", UriKind.RelativeOrAbsolute);
            this.Resources.MergedDictionaries.Add(resourceDictionary);
            //Установка размеров окна
            Main_menu_can.Width = 1920;
            Main_menu_can.Height = 1080;
            Main_menu_can.Children.Add(Set_Image("Images/Main_menu/Main_Bg_lvl1.png", 1920, 1080)); //Установка заднего фона(небо)
            List<Image> clouds = new List<Image>();//Настройка облаков
            for (int i = 1; i < 4; i++)
            {
                clouds.Add(Set_Image("Images/Main_menu/cloud_" + Convert.ToString(i) + "_lvl1.png", 800, 250));
                Canvas.SetLeft(clouds[i - 1], -800 * i);
                Canvas.SetTop(clouds[i - 1], new Random().Next(-200, 512));
                Main_menu_can.Children.Add(clouds[i - 1]);
                MoveImage(clouds[i - 1], -800 * i, 1920, (800 * i + 1920) / 54.4);
            }
            Main_menu_can.Children.Add(Set_Image("Images/Main_menu/hills_lvl1.png", 1920, 1080)); //Установка заднего фона(земля)
            //Настройки кнопок 
            Button button;
            button = Set_Button("Play", (Style)this.Resources["lvl1"]);
            Canvas.SetLeft(button, (Main_menu_can.Width - button.Width) / 2);
            Canvas.SetTop(button, (Main_menu_can.Height - button.Height) / 2);
            Main_menu_can.Children.Add(button);
            button = Set_Button("Redactor", (Style)this.Resources["lvl1"]);
            Canvas.SetLeft(button, (Main_menu_can.Width - button.Width) / 2);
            Canvas.SetTop(button, (Main_menu_can.Height - button.Height * 4) / 2);
            button.Click += Redactor_Click;
            Main_menu_can.Children.Add(button);
            button = Set_Button("Exit", (Style)this.Resources["lvl1"]);
            Canvas.SetLeft(button, (Main_menu_can.Width - button.Width) / 2);
            Canvas.SetTop(button, (Main_menu_can.Height + button.Height * 2) / 2);
            Main_menu_can.Children.Add(button);
        }
        public Image Set_Image(string image_name, double Widht, double Height) //Функция создания изображения
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
        public Button Set_Button(string content, Style st) //Функция создания кнопки
        {
            Button button = new Button();
            button.Content = content;
            button.Name = content;
            button.Style = st;
            return button;
        }
        private void MoveImage(Image image, double fromX, double toX, double duraction)//Анимация движения облаков
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
            Storyboard.SetTarget(moveXAnimation, image);
            Storyboard.SetTargetProperty(moveXAnimation, new PropertyPath("(Canvas.Left)"));
            moveXAnimation.Completed += (s, e) =>
            {
                Canvas.SetLeft(image, 0);
                Canvas.SetTop(image, new Random().Next(-200, 512));
                storyboard.Begin();
            };
            storyboard.Begin();
        }
        private void Redactor_Click(object sender, RoutedEventArgs e)//Перенос в редактор
        {
            NavigationService.Navigate(new Redactor_page());
        }
    }
}

