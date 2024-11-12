using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;
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
using System.Xml.Serialization;

namespace KR
{
    /// <summary>
    /// Логика взаимодействия для Redactor_page.xaml
    /// </summary>
    /// 
    public struct OBJJ
    {
        public Button Objection;
        public BitmapImage bitmapImage;
        public string way;
    }
    public struct Tiless
    {
        public Button Button;
        public BitmapImage bitmapImage;
        public string way;
    }
    public partial class Redactor_page : Page
    {
        Map map = new Map();
        Button button_SHAB = new Button();
        List<Tiless> Titles = new List<Tiless>();
        List<OBJJ> Objects = new List<OBJJ>();
        Image onCurs = new Image();
        string onCursb = "";
        public Redactor_page()
        {
            InitializeComponent();
            ResourceDictionary resourceDictionary = new ResourceDictionary();
            resourceDictionary.Source = new Uri("pack://application:,,,/Styles/Main_menu.xaml", UriKind.RelativeOrAbsolute);
            this.Resources.MergedDictionaries.Add(resourceDictionary);
            set_tiles();
            set_objects();
            Redactor_map_can.Children.Add(onCurs);
            for (int i = 0; i < Titles.Count(); i++)
            {
                Redactor_ScrView_grid.Children.Add(Titles[i].Button);
                if (i % 2 == 0)
                {
                    Redactor_ScrView_grid.RowDefinitions.Add(new RowDefinition());
                }
                Grid.SetRow(Titles[i].Button, i / 2);
                Grid.SetColumn(Titles[i].Button, i % 2);
            }
            for (int j = 0; j < 24; j++)
            {
                Redactor_map.ColumnDefinitions.Add(new ColumnDefinition());
                Redactor_map.ColumnDefinitions[j].Width = GridLength.Auto;
            }
            map.add_Map_tile(24 * 12);
            for (int i = 0; i < 12; i++)
            {
                Redactor_map.RowDefinitions.Add(new RowDefinition());
                Redactor_map.RowDefinitions[i].Height = GridLength.Auto;
                for(int j = 0;j < 24; j++)
                {
                    button_SHAB = Set_Button("", (Style)this.Resources["Tiles"]);
                    button_SHAB.Click += Button_map_Click;

                    Redactor_map.Children.Add(button_SHAB);
                    Grid.SetRow(button_SHAB, i);
                    Grid.SetColumn(button_SHAB, j);
                }
            }
        }
        private void set_tiles()
        {
            for (int i = 1; i < 14; i++)
            {
                Titles.Add(set_Tiless("pack://application:,,,/Images/Tiles/Tile_road_" + Convert.ToString(i) + "_lvl1.png", (Style)this.Resources["Tiles"]));
                Titles[Titles.Count() - 1].Button.Click += Button_vib_Click;
            }
            for (int i = 1; i < 5; i++)
            {
                Titles.Add(set_Tiless("pack://application:,,,/Images/Tiles/Tile_grass_" + Convert.ToString(i) + "_lvl1.png", (Style)this.Resources["Tiles"]));
                Titles[Titles.Count() - 1].Button.Click += Button_vib_Click;
            }
        }
        private void set_objects()
        {
            Objects.Add(Set_OBJJ("pack://application:,,,/Images/Objects/House/House_Door_Close_lvl1.png",(Style)this.Resources["Tiles"]));
            Objects.Add(Set_OBJJ("pack://application:,,,/Images/Objects/House/House_Door_Open_lvl1.png", (Style)this.Resources["Tiles"]));
            Objects.Add(Set_OBJJ("pack://application:,,,/Images/Objects/House/House_lvl1.png", (Style)this.Resources["Tiles"]));
            for (int i = 0; i < Objects.Count(); i++)
            {
                Objects[i].Objection.Click += Button_Obj_click;
            }
        }
        public Button Set_Button(string content, Style st)
        {
            Button button = new Button();
            button.Content = content;
            button.Style = st;
            return button;
        }
        public Button Set_Button(string way,float width,float height,Style st)
        {
            Button button = new Button();
            BitmapImage bitmap = new BitmapImage();
            if (way != "")
            {
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(way, UriKind.RelativeOrAbsolute);
                bitmap.EndInit();
                ImageBrush imageBrush = new ImageBrush(bitmap);
                button.Background = imageBrush;
            }
            button.Style = st;
            return button;
        }
        public Tiless set_Tiless(string way,Style st)
        {
            Tiless obj = new Tiless();
            Button button = new Button();
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(way, UriKind.RelativeOrAbsolute);
            bitmap.EndInit();
            ImageBrush imageBrush = new ImageBrush(bitmap);
            button.Background = imageBrush;
            button.Style = st;
            obj.Button = button;
            obj.bitmapImage = bitmap;
            obj.way = way;
            return obj;
        }
        public OBJJ Set_OBJJ(string way, Style st)
        {
            OBJJ obj = new OBJJ();
            Button button = new Button();
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(way, UriKind.RelativeOrAbsolute);
            bitmap.EndInit();
            ImageBrush imageBrush = new ImageBrush(bitmap);
            button.Background = imageBrush;
            button.Style = st;
            obj.Objection = button;
            obj.bitmapImage = bitmap;
            obj.way = way;
            return obj;
        }
        private void Button_vib_Click(object sender, EventArgs e)
        {
            button_SHAB = (Button)sender;
            onCursb = Titles.FirstOrDefault(p => p.Button == (sender)).way;
        }
        private void Button_map_Click(object sender, EventArgs e)
        {
            ((Button)sender).Background = button_SHAB.Background;
            map.red_tiless(onCursb, Grid.GetRow((UIElement)sender)*24 + Grid.GetColumn((UIElement)sender));
        }
        private void Menu_back_Click(object sender, EventArgs e)
        {
            NavigationService.GoBack();
        }
        private void Menu_Object_Click(object sender, EventArgs e)
        {
            onCurs.Visibility = Visibility.Visible;
            Redactor_ScrView_grid.Children.Clear();
            for (int i = 0; i < Objects.Count(); i++)
            {
                Redactor_ScrView_grid.Children.Add(Objects[i].Objection);
                if (i % 2 == 0)
                {
                    Redactor_ScrView_grid.RowDefinitions.Add(new RowDefinition());
                }
                Grid.SetRow(Objects[i].Objection, i / 2);
                Grid.SetColumn(Objects[i].Objection, i % 2);
            }
        }
        private void Menu_Tiles_Click(object sender, EventArgs e)
        {
            onCurs.Visibility = Visibility.Collapsed;
            Redactor_ScrView_grid.Children.Clear();
            for (int i = 0; i < Titles.Count(); i++)
            {
                Redactor_ScrView_grid.Children.Add(Titles[i].Button);
                if (i % 2 == 0)
                {
                    Redactor_ScrView_grid.RowDefinitions.Add(new RowDefinition());
                }
                Grid.SetRow(Titles[i].Button, i / 2);
                Grid.SetColumn(Titles[i].Button, i % 2);
            }
        }
        private void Menu_Save_Click(object sender, EventArgs e)
        {
            string folder_path = System.IO.Path.Combine(Environment.CurrentDirectory, "Maps");
            Text_window text_Window = new Text_window();
            if (text_Window.ShowDialog() == true)
            {
                string file_path = System.IO.Path.Combine(folder_path, text_Window.InputText);
                if (!Directory.Exists(folder_path))
                {
                    Directory.CreateDirectory(folder_path);
                }
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Map));
                using (FileStream fs = new FileStream(file_path, FileMode.Create))
                {
                    xmlSerializer.Serialize(fs, map);
                }
                //Map maps;
                //using (FileStream fs = new FileStream(file_path, FileMode.OpenOrCreate))
                //{
                //   maps =  (Map)xmlSerializer.Deserialize(fs);
                //}
            }
        }
        private void Menu_Load_Click(object sender, EventArgs e)
        {
            string folder_path = System.IO.Path.Combine(Environment.CurrentDirectory, "Maps");
            Text_window text_Window = new Text_window();
            if (text_Window.ShowDialog() == true)
            {
                string file_path = System.IO.Path.Combine(folder_path, text_Window.InputText);
                if (!Directory.Exists(folder_path))
                {
                    Directory.CreateDirectory(folder_path);
                }
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Map));
                using (FileStream fs = new FileStream(file_path, FileMode.OpenOrCreate))
                {
                    map = (Map)xmlSerializer.Deserialize(fs);
                }
                Redactor_map.Children.Clear();
                for (int i = 0; i < 12; i++)
                {
                    for (int j = 0; j < 24; j++)
                    {
                        button_SHAB = Set_Button(map.tiless[i*24+j],80,80, (Style)this.Resources["Tiles"]);
                        //button_SHAB.Content = map.tiless[i * 12 + j];
                        button_SHAB.Click += Button_map_Click;

                        Redactor_map.Children.Add(button_SHAB);
                        Grid.SetRow(button_SHAB, i);
                        Grid.SetColumn(button_SHAB, j);
                    }
                }
                while (Redactor_map_can.Children.Count > 2)
                {
                    Redactor_map_can.Children.RemoveAt(2);
                }
                for (int i = 0; i < map.objj.Count; i++)
                {
                    Image image = new Image();
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(map.objj[i].image, UriKind.RelativeOrAbsolute);
                    bitmap.EndInit();
                    image.Source = bitmap;
                    image.MouseDown += Mouse_click_obj;
                    Redactor_map_can.Children.Add(image);
                    Canvas.SetLeft(image, map.objj[i].x);
                    Canvas.SetTop(image, map.objj[i].y);
                }
            }
        }
        private void Button_Obj_click(object sender, EventArgs e)
        {
            onCurs.Source = Objects.FirstOrDefault(p => p.Objection == (sender)).bitmapImage;
            onCursb = Objects.FirstOrDefault(p => p.Objection == (sender)).way;
            Canvas.SetZIndex(onCurs,1);
        }
        private void Mouse_move(object sender, MouseEventArgs e)
        {
            try
            {
                if (!Keyboard.IsKeyDown(Key.LeftShift))
                {
                    Canvas.SetLeft(onCurs, Mouse.GetPosition(Redactor_map_can).X);
                    Canvas.SetTop(onCurs, Mouse.GetPosition(Redactor_map_can).Y);
                }
                else
                {
                    Canvas.SetLeft(onCurs, (int)(Mouse.GetPosition(Redactor_map_can).X/80)*80);
                    Canvas.SetTop(onCurs, (int)(Mouse.GetPosition(Redactor_map_can).Y/80)*80);
                }
            }
            catch
            {
                MessageBox.Show("Ошибка!!!");
            }
        }
        private void Mouse_click(object sender, EventArgs e)
        {
            if (onCurs.Visibility == Visibility.Visible)
            {
                Image image = new Image();
                image.Source = onCurs.Source;
                image.Width = onCurs.Width;
                image.Height = onCurs.Height;
                image.MouseDown += Mouse_click_obj;
                Redactor_map_can.Children.Add(image);
                Canvas.SetLeft(image, Mouse.GetPosition(Redactor_map_can).X);
                Canvas.SetTop(image, Mouse.GetPosition(Redactor_map_can).Y);
                map.add_Map_obj(Mouse.GetPosition(Redactor_map_can).X, Mouse.GetPosition(Redactor_map_can).Y, onCursb);
            }
        }
        private void Mouse_click_obj(object sender, EventArgs e)
        {
            Redactor_map_can.Children.Remove((UIElement)sender);
            map.remove_Map_obj(Canvas.GetLeft((UIElement)sender), Canvas.GetTop((UIElement)sender));
        }
    }
}
