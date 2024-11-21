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
    public struct Cols
    {
        public Button Button;
        public string way;
        public int col;
    }
    public partial class Redactor_page : Page
    {
        Map map = new Map();
        Button button_SHAB = new Button();
        List<Tiless> Titles = new List<Tiless>();
        List<OBJJ> Objects = new List<OBJJ>();
        List<Cols> col = new List<Cols>();
        Image onCurs = new Image();
        NPC npc;
        string onCursb = "";
        string[] Col_ways = { "pack://application:,,,/Images/Col/col_1_.png", "pack://application:,,,/Images/Col/col_2_.png", "pack://application:,,,/Images/Col/col_3_.png", "pack://application:,,,/Images/Col/col_4_.png" };
        public Redactor_page()
        {
            InitializeComponent();
            ResourceDictionary resourceDictionary = new ResourceDictionary();
            resourceDictionary.Source = new Uri("pack://application:,,,/Styles/Main_menu.xaml", UriKind.RelativeOrAbsolute);
            this.Resources.MergedDictionaries.Add(resourceDictionary);
            set_tiles();
            set_objects();
            set_col();
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
                Redactor_map_col.ColumnDefinitions.Add(new ColumnDefinition());
                Redactor_map_col.ColumnDefinitions[j].Width = GridLength.Auto;
            }
            map.add_Map_tile(24 * 12);
            for (int i = 0; i < 12; i++)
            {
                Redactor_map.RowDefinitions.Add(new RowDefinition());
                Redactor_map.RowDefinitions[i].Height = GridLength.Auto;
                Redactor_map_col.RowDefinitions.Add(new RowDefinition());
                Redactor_map_col.RowDefinitions[i].Height = GridLength.Auto;
                for (int j = 0; j < 24; j++)
                {
                    button_SHAB = Set_Button("", (Style)this.Resources["Tiles"]);
                    button_SHAB.Click += Button_tile_Click;
                    Redactor_map.Children.Add(button_SHAB);
                    Grid.SetRow(button_SHAB, i);
                    Grid.SetColumn(button_SHAB, j);
                    button_SHAB = Set_Button("", (Style)this.Resources["Col"]);
                    button_SHAB.Content = new Canvas { Width = 80, Height = 80};
                    button_SHAB.Click += Button_Col_Click;
                    Redactor_map_col.Children.Add(button_SHAB);
                    Grid.SetRow(button_SHAB, i);
                    Grid.SetColumn(button_SHAB, j);
                }
            }
        }
        // Заполнение списков выбора
        private void set_tiles() //Заполнение списка Titles
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
        private void set_objects() //Заполнение списка Objects
        {
            Objects.Add(Set_OBJJ("pack://application:,,,/Images/Objects/House/House_Door_Close_lvl1.png", (Style)this.Resources["Tiles"]));
            Objects.Add(Set_OBJJ("pack://application:,,,/Images/Objects/House/House_Door_Open_lvl1.png", (Style)this.Resources["Tiles"]));
            Objects.Add(Set_OBJJ("pack://application:,,,/Images/Objects/House/House_lvl1.png", (Style)this.Resources["Tiles"]));
            for (int i = 0; i < Objects.Count(); i++)
            {
                Objects[i].Objection.Click += Button_Obj_click;
            }
        }
        private void set_col() //Заполнение списка col
        {
            col.Add(Set_Col("pack://application:,,,/Images/Col/col_1_.png", (Style)this.Resources["Col"],0));
            col[col.Count() - 1].Button.Click += Button_vib_Click;
            col.Add(Set_Col("pack://application:,,,/Images/Col/col_2_.png", (Style)this.Resources["Col"], 1));
            col[col.Count() - 1].Button.Click += Button_vib_Click;
            col.Add(Set_Col("pack://application:,,,/Images/Col/col_3_.png", (Style)this.Resources["Col"], 2));
            col[col.Count() - 1].Button.Click += Button_vib_Click;
            col.Add(Set_Col("pack://application:,,,/Images/Col/col_4_.png", (Style)this.Resources["Col"], 3));
            col[col.Count() - 1].Button.Click += Button_vib_Click;
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
        public Button Set_Button(string content, Style st) //Функция создания кнопки без фото
        {
            Button button = new Button();
            button.Content = content;
            button.Style = st;
            return button;
        }
        public Button Set_Button(string way, float width, float height, Style st) //Функция создания кнопки с фото
        {
            Button button = new Button();
            BitmapImage bitmap = new BitmapImage();
            if (way != "" && way != null)
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
        public Tiless set_Tiless(string way, Style st) //Функция создания объекта структуры Tiless
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
        public OBJJ Set_OBJJ(string way, Style st) //Функция создания объекта структуры OBJJ
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
            obj.bitmapImage = bitmap;
            obj.way = way;
            return obj;
        }
        public Cols Set_Col(string way, Style st,int col)//Функция создания объекта структуры Cols
        {
            Cols obj = new Cols();
            Button button = new Button();
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(way, UriKind.RelativeOrAbsolute);
            bitmap.EndInit();
            ImageBrush imageBrush = new ImageBrush(bitmap);
            button.Background = imageBrush;
            button.Style = st;
            obj.way = way;
            obj.Button = button;
            obj.col = col;
            return obj;
        }
        //Функции обработки событий нажатия кнопки
        private void Button_vib_Click(object sender, EventArgs e) //Функция сохранения тайла или col из выборочного списка
        {
            button_SHAB = (Button)sender;
            //onCursb = Titles.FirstOrDefault(p => p.Button == (sender)).way;
        }
        private void Button_tile_Click(object sender, EventArgs e) //Функция сохранения тайла
        {
            ((Button)sender).Background = button_SHAB.Background;
            map.red_tiless(onCursb, Grid.GetRow((UIElement)sender) * 24 + Grid.GetColumn((UIElement)sender));
        }
        private void Button_Col_Click(Object sender, EventArgs e) //Функция сохранения колизии
        {
            Canvas canvas = (Canvas)((Button)sender).Content;
            int num = Grid.GetRow((UIElement)sender) * 24 + Grid.GetColumn((UIElement)sender);
            List<int> colds;
            colds = map.col[num];
            bool ch = true;
            Cols cold = new Cols();
            cold = col.FirstOrDefault(p => p.Button == (button_SHAB));
            foreach (UIElement child in canvas.Children)
            {
                if (Canvas.GetZIndex(child) == cold.col)
                {
                    colds.Remove(cold.col);
                    canvas.Children.Remove(child);
                    ch = false;
                    break;
                }
            }
            if(ch)
            {
                canvas.Children.Add(Set_Image(cold.way, 80, 80));
                Canvas.SetZIndex(canvas.Children[canvas.Children.Count - 1], cold.col);
                colds.Add(cold.col);
            }
            ((Button)sender).Content = canvas;
            map.red_col(colds, num);
        }
        private void Button_Obj_click(object sender, EventArgs e) //Функция сохранения объекта из выборного списка
        {
            onCurs.Source = Objects.FirstOrDefault(p => p.Objection == (sender)).bitmapImage;
            onCursb = Objects.FirstOrDefault(p => p.Objection == (sender)).way;
            Canvas.SetZIndex(onCurs, 1);
        }
        //Обработчики события меню
        private void Menu_back_Click(object sender, EventArgs e) //Возврат на предыдущую страницу
        {
            NavigationService.GoBack();
        }
        private void Menu_Object_Click(object sender, EventArgs e) //Изменнеие выборочного списка на режим объектов
        {
            Redactor_map_col.Visibility = Visibility.Collapsed;
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
        private void Menu_Tiles_Click(object sender, EventArgs e) //Изменение выборочного списка на режим Тайлов
        {
            Redactor_map_col.Visibility = Visibility.Collapsed;
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
        private void Menu_Save_Click(object sender, EventArgs e) //Сохранение карты
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
        private void Menu_Load_Click(object sender, EventArgs e) //Загрузка карты
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
                Redactor_map_col.Children.Clear();
                for (int i = 0; i < 12; i++)
                {
                    for (int j = 0; j < 24; j++)
                    {
                        button_SHAB = Set_Button(map.tiless[i * 24 + j], 80, 80, (Style)this.Resources["Tiles"]);
                        //button_SHAB.Content = map.tiless[i * 12 + j];
                        button_SHAB.Click += Button_tile_Click;

                        Redactor_map.Children.Add(button_SHAB);
                        Grid.SetRow(button_SHAB, i);
                        Grid.SetColumn(button_SHAB, j);
                        button_SHAB = Set_Button("", (Style)this.Resources["Col"]);
                        Canvas canvas = new Canvas {Width = 80,Height = 80 };
                        for (int k = 0; k < map.col[i * 24 + j].Count; k++)
                        {
                            canvas.Children.Add(Set_Image(Col_ways[map.col[i * 24 + j][k]], 80, 80));
                            Canvas.SetZIndex(canvas.Children[canvas.Children.Count - 1], map.col[i * 24 + j][k]);
                        }
                        button_SHAB.Content = canvas;
                        button_SHAB.Click += Button_Col_Click;
                        Redactor_map_col.Children.Add(button_SHAB);
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
                    image.MouseDown += Mouse_click_del_obj;
                    Redactor_map_can.Children.Add(image);
                    Canvas.SetLeft(image, map.objj[i].x);
                    Canvas.SetTop(image, map.objj[i].y);
                }
            }
        }
        private void Menu_Col_Click(object sender, EventArgs e) //Изменение выборочного списка на режим col
        {
            Redactor_map_col.Visibility = Visibility.Visible;
            Redactor_ScrView_grid.Children.Clear();
            for (int i = 0; i < col.Count(); i++)
            {
                Redactor_ScrView_grid.Children.Add(col[i].Button);
                if (i % 2 == 0)
                {
                    Redactor_ScrView_grid.RowDefinitions.Add(new RowDefinition());
                }
                Grid.SetRow(col[i].Button, i / 2);
                Grid.SetColumn(col[i].Button, i % 2);
            }
        }
        //Обработчики событий мыши
        private void Mouse_move_obj(object sender, MouseEventArgs e) //Мышка двигается c объектом
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
                    Canvas.SetLeft(onCurs, (int)(Mouse.GetPosition(Redactor_map_can).X / 80) * 80);
                    Canvas.SetTop(onCurs, (int)(Mouse.GetPosition(Redactor_map_can).Y / 80) * 80);
                }
            }
            catch
            {
                MessageBox.Show("Ошибка!!!");
            }
        }
        private void Mouse_click_add_obj(object sender, EventArgs e)//Добовление объекта на карту
        {
            if (onCurs.Visibility == Visibility.Visible)
            {
                Image image = new Image();
                image.Source = onCurs.Source;
                image.Width = onCurs.Width;
                image.Height = onCurs.Height;
                image.MouseDown += Mouse_click_del_obj;
                Redactor_map_can.Children.Add(image);
                Canvas.SetLeft(image, Mouse.GetPosition(Redactor_map_can).X);
                Canvas.SetTop(image, Mouse.GetPosition(Redactor_map_can).Y);
                map.add_Map_obj(Mouse.GetPosition(Redactor_map_can).X, Mouse.GetPosition(Redactor_map_can).Y, onCursb);
            }
        }
        private void Mouse_click_del_obj(object sender, EventArgs e) //Удаление объекта с карты
        {
            Redactor_map_can.Children.Remove((UIElement)sender);
            map.remove_Map_obj(Canvas.GetLeft((UIElement)sender), Canvas.GetTop((UIElement)sender));
        }
    }
}
