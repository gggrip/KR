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
        public Button Button;
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
    public struct Npcs
    {
        public Button Button;
        public string way;
    }
    public partial class Redactor_page : Page
    {
        NPC item_npc;
        Map map = new Map();
        Button button_SHAB = new Button();
        List<Tiless> Titles = new List<Tiless>();
        List<OBJJ> Objects = new List<OBJJ>();
        List<Cols> col = new List<Cols>();
        List<OBJJ> npcs = new List<OBJJ>();
        Image onCurs_0bj = new Image();
        Image onCurs_npc = new Image();
        string onCursb = "";
        string[] Col_ways = { "pack://application:,,,/Images/Col/col_1_.png", "pack://application:,,,/Images/Col/col_2_.png", "pack://application:,,,/Images/Col/col_3_.png", "pack://application:,,,/Images/Col/col_4_.png" };
        string[] Npc_ways = { "pack://application:,,,/Images/Npc/Test_NPC.png" };
        public Redactor_page()
        {
            InitializeComponent();
            ResourceDictionary resourceDictionary = new ResourceDictionary();
            resourceDictionary.Source = new Uri("pack://application:,,,/Styles/Main_menu.xaml", UriKind.RelativeOrAbsolute);
            this.Resources.MergedDictionaries.Add(resourceDictionary);
            set_tiles();
            set_objects();
            set_col();
            set_npcs();
            Redactor_map_can.Children.Add(onCurs_0bj);
            Redactor_map_can.Children.Add(onCurs_npc);
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
                Objects[i].Button.Click += Button_Obj_click;
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
        private void set_npcs()
        {
            npcs.Add(Set_OBJJ("pack://application:,,,/Images/Npc/Test_NPC.png", (Style)this.Resources["Tiles"]));
            npcs[npcs.Count() - 1].Button.Click += Button_Npcs_click;
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
            obj.Button = button;
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
        //public Npcs Set_Npcs(string way, Style st)
        //{
        //    Npcs obj = new Npcs();
        //    Button button = new Button();
        //    BitmapImage bitmap = new BitmapImage();
        //    bitmap.BeginInit();
        //    bitmap.UriSource = new Uri(way, UriKind.RelativeOrAbsolute);
        //    bitmap.EndInit();
        //    ImageBrush imageBrush = new ImageBrush( bitmap);
        //    button.Background = imageBrush;
        //    button.Style = st;
        //    obj.way = way;
        //    obj.Button = button;
        //    return obj;
        //}
        //Функции обработки событий нажатия кнопки
        private void Button_vib_Click(object sender, EventArgs e) //Функция сохранения тайла или col из выборочного списка
        {
            button_SHAB = (Button)sender;
            onCursb = Titles.FirstOrDefault(p => p.Button == (sender)).way;
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
            onCurs_0bj.Source = Objects.FirstOrDefault(p => p.Button == (sender)).bitmapImage;
            onCursb = Objects.FirstOrDefault(p => p.Button == (sender)).way;
            Canvas.SetZIndex(onCurs_0bj, 1);
        }
        private void Button_Npcs_click(object sender, EventArgs e)
        {
            onCurs_npc.Source = npcs.FirstOrDefault(p => p.Button == (sender)).bitmapImage;
            onCursb = npcs.FirstOrDefault(p => p.Button == (sender)).way;
            Canvas.SetZIndex(onCurs_npc, 1);
        }
        //Обработчики события меню
        private void Menu_back_Click(object sender, EventArgs e) //Возврат на предыдущую страницу
        {
            NavigationService.GoBack();
        }
        private void Menu_Object_Click(object sender, EventArgs e) //Изменнеие выборочного списка на режим объектов
        {
            Redactor_map_col.Visibility = Visibility.Collapsed;
            onCurs_0bj.Visibility = Visibility.Visible;
            onCurs_npc.Visibility = Visibility.Collapsed;
            Redactor_ScrView_grid.Children.Clear();
            for (int i = 0; i < Objects.Count(); i++)
            {
                Redactor_ScrView_grid.Children.Add(Objects[i].Button);
                if (i % 2 == 0)
                {
                    Redactor_ScrView_grid.RowDefinitions.Add(new RowDefinition());
                }
                Grid.SetRow(Objects[i].Button, i / 2);
                Grid.SetColumn(Objects[i].Button, i % 2);
            }
        }
        private void Menu_Tiles_Click(object sender, EventArgs e) //Изменение выборочного списка на режим Тайлов
        {
            Redactor_map_col.Visibility = Visibility.Collapsed;
            Redactor_map.IsEnabled = true;
            onCurs_0bj.Visibility = Visibility.Collapsed;
            onCurs_npc.Visibility = Visibility.Collapsed;
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
                while (Redactor_map_can.Children.Count > 4)
                {
                    Redactor_map_can.Children.RemoveAt(4);
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
                for(int i = 0; i < map.vilags.Count; i++)
                {
                    Image image = new Image();
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(map.vilags[i].images[map.vilags[i].pos], UriKind.RelativeOrAbsolute);
                    bitmap.EndInit();
                    image.Source = bitmap;
                    image.MouseDown += Mouse_click_npc;
                    Redactor_map_can.Children.Add(image);
                    Canvas.SetLeft(image, map.vilags[i].x);
                    Canvas.SetTop(image, map.vilags[i].y);
                }
                Menu_Tiles_Click(sender,e);
            }
        }
        private void Menu_Col_Click(object sender, EventArgs e) //Изменение выборочного списка на режим col
        {
            Redactor_map_col.Visibility = Visibility.Visible;
            onCurs_0bj.Visibility = Visibility.Collapsed;
            onCurs_npc.Visibility = Visibility.Collapsed;
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
        private void Menu_NPC_Click(object sender, EventArgs e)
        {
            Redactor_map.IsEnabled = false;
            Redactor_map_col.Visibility = Visibility.Collapsed;
            onCurs_0bj.Visibility = Visibility.Collapsed;
            onCurs_npc.Visibility = Visibility.Visible;
            Redactor_ScrView_grid.Children.Clear();
            for (int i = 0; i < npcs.Count(); i++)
            {
                Redactor_ScrView_grid.Children.Add(npcs[i].Button);
                if (i % 2 == 0)
                {
                    Redactor_ScrView_grid.RowDefinitions.Add(new RowDefinition());
                }
                Grid.SetRow(npcs[i].Button, i / 2);
                Grid.SetColumn(npcs[i].Button, i % 2);
            }
        }
        //Обработчики событий мыши
        private void Mouse_move_obj(object sender, MouseEventArgs e) //Мышка двигается c объектом
        {
            if (onCurs_0bj.Visibility == Visibility.Visible)
            {
                try
                {
                    if (!Keyboard.IsKeyDown(Key.LeftShift))
                    {
                        Canvas.SetLeft(onCurs_0bj, Mouse.GetPosition(Redactor_map_can).X);
                        Canvas.SetTop(onCurs_0bj, Mouse.GetPosition(Redactor_map_can).Y);
                    }
                    else
                    {
                        Canvas.SetLeft(onCurs_0bj, (int)(Mouse.GetPosition(Redactor_map_can).X / 80) * 80);
                        Canvas.SetTop(onCurs_0bj, (int)(Mouse.GetPosition(Redactor_map_can).Y / 80) * 80);
                    }
                }
                catch
                {
                    MessageBox.Show("Ошибка!!!");
                }
            }
            else if(onCurs_npc.Visibility == Visibility.Visible)
            {
                try
                {
                    if (!Keyboard.IsKeyDown(Key.LeftShift))
                    {
                        Canvas.SetLeft(onCurs_npc, Mouse.GetPosition(Redactor_map_can).X);
                        Canvas.SetTop(onCurs_npc, Mouse.GetPosition(Redactor_map_can).Y);
                    }
                    else
                    {
                        Canvas.SetLeft(onCurs_npc, (int)(Mouse.GetPosition(Redactor_map_can).X / 80) * 80);
                        Canvas.SetTop(onCurs_npc, (int)(Mouse.GetPosition(Redactor_map_can).Y / 80) * 80 - 40);
                    }
                }
                catch
                {
                    MessageBox.Show("Ошибка!!!");
                }
            }
        }
        private void Mouse_click_add_obj(object sender, EventArgs e)//Добовление объекта на карту
        {
            if (onCurs_0bj.Visibility == Visibility.Visible)
            {
                Image image = new Image();
                image.Source = onCurs_0bj.Source;
                image.Width = onCurs_0bj.Width;
                image.Height = onCurs_0bj.Height;
                image.MouseDown += Mouse_click_del_obj;
                Redactor_map_can.Children.Add(image);
                Canvas.SetLeft(image, Canvas.GetLeft(onCurs_0bj));
                Canvas.SetTop(image, Canvas.GetTop(onCurs_0bj));
                map.add_Map_obj(Mouse.GetPosition(Redactor_map_can).X, Mouse.GetPosition(Redactor_map_can).Y, onCursb);
            }
            if (onCurs_npc.Visibility == Visibility.Visible)
            {
                Image image = new Image();
                image.Source = onCurs_npc.Source;
                image.Width = onCurs_npc.Width;
                image.Height = onCurs_npc.Height;
                image.MouseDown += Mouse_click_npc;
                Redactor_map_can.Children.Add(image);
                Canvas.SetLeft(image, Canvas.GetLeft(onCurs_npc));
                Canvas.SetTop(image, Canvas.GetTop(onCurs_npc));
                switch (Array.IndexOf(Npc_ways, onCursb))
                {
                    case 0:
                        Vilag vilag = new Vilag();
                        vilag.images.Add(onCursb);
                        vilag.x = Canvas.GetLeft(onCurs_npc);
                        vilag.y = Canvas.GetTop(onCurs_npc);
                        map.add_Npc(vilag);
                        break;
                    default:
                        Redactor_map_can.Children.Remove(image);
                        MessageBox.Show("Произошла ошибка");
                        break;
                }
            }
        }
        private void Mouse_click_del_obj(object sender, EventArgs e) //Удаление объекта с карты
        {
            Redactor_map_can.Children.Remove((UIElement)sender);
            map.remove_Map_obj(Canvas.GetLeft((UIElement)sender), Canvas.GetTop((UIElement)sender));
        }
        private void Mouse_click_npc(object sender, EventArgs e)
        {
            Npc_ScrView.Visibility = Visibility.Visible;
            Npc_gridsplit.Visibility = Visibility.Visible;
            item_npc = map.vilags.FirstOrDefault(p => p.x == Canvas.GetLeft((UIElement)sender) && p.y == Canvas.GetTop((UIElement)sender));
            ((TextBox)Npc_ScrView_grid.Children[1]).Text = item_npc.Name;
            ((TextBox)((Grid)Npc_ScrView_grid.Children[3]).Children[1]).Text = Convert.ToString(item_npc.x);
            ((TextBox)((Grid)Npc_ScrView_grid.Children[3]).Children[3]).Text = Convert.ToString(item_npc.y);
            for (int i = 0; i < item_npc.images.Count;i++)
            {
                ((TextBox)((Grid)Npc_ScrView_grid.Children[5]).Children[i]).Text = item_npc.images[i];
            }
            ((TextBox)Npc_ScrView_grid.Children[7]).Text = Convert.ToString(item_npc.HP);
            ((TextBox)Npc_ScrView_grid.Children[9]).Text = Convert.ToString(item_npc.Damage);
            ((TextBox)Npc_ScrView_grid.Children[11]).Text = Convert.ToString(item_npc.v);
            ((TextBox)Npc_ScrView_grid.Children[13]).Text = Convert.ToString(item_npc.attitude);
            for (int i = 0; i < item_npc.dialog.Count; i++)
            {
                ((TextBox)((Grid)Npc_ScrView_grid.Children[15]).Children[i]).Text = item_npc.dialog[i];
                TextBox textBox = new TextBox();
                textBox.TextInput += New_Row_Dialog;
                ((Grid)Npc_ScrView_grid.Children[15]).Children.Add(textBox);
                ((Grid)Npc_ScrView_grid.Children[15]).RowDefinitions.Add(new RowDefinition());
                Grid.SetColumn(textBox, 1);
                Grid.SetRow(textBox, i);
            }
        }
        private void Button_NPC_SCR_Click(object sender, EventArgs e)
        {
            item_npc.Name = ((TextBox)Npc_ScrView_grid.Children[1]).Text;
            item_npc.x = Convert.ToDouble(((TextBox)((Grid)Npc_ScrView_grid.Children[3]).Children[1]).Text);
            item_npc.y = Convert.ToDouble(((TextBox)((Grid)Npc_ScrView_grid.Children[3]).Children[3]).Text);
            for (int i = 0;i < ((Grid)Npc_ScrView_grid.Children[5]).Children.Count; i++)
            {
                if((((TextBox)((Grid)Npc_ScrView_grid.Children[5]).Children[i]).Text).Length < 4)
                {
                    break;
                }
                try
                {
                    item_npc.images[i] = ((TextBox)((Grid)Npc_ScrView_grid.Children[5]).Children[i]).Text;
                }
                catch
                {
                    item_npc.images.Add(((TextBox)((Grid)Npc_ScrView_grid.Children[5]).Children[i]).Text);
                }
            }
            item_npc.HP = Convert.ToInt32(((TextBox)Npc_ScrView_grid.Children[7]).Text);
            item_npc.Damage = Convert.ToInt32(((TextBox)Npc_ScrView_grid.Children[9]).Text);
            item_npc.v = Convert.ToInt32(((TextBox)Npc_ScrView_grid.Children[11]).Text);
            item_npc.attitude = Convert.ToInt32(((TextBox)Npc_ScrView_grid.Children[13]).Text);
            for (int i = 0; i < ((Grid)Npc_ScrView_grid.Children[15]).Children.Count;i++)
            {
                if (((TextBox)((Grid)Npc_ScrView_grid.Children[15]).Children[i]).Text.Length < 4)
                {
                    break;
                }
                try
                {
                    item_npc.dialog[i] = ((TextBox)((Grid)Npc_ScrView_grid.Children[15]).Children[i]).Text;
                }
                catch
                {
                    item_npc.dialog.Add(((TextBox)((Grid)Npc_ScrView_grid.Children[15]).Children[i]).Text);
                }
            }
            ((Grid)Npc_ScrView_grid.Children[15]).RowDefinitions.Clear();
            ((Grid)Npc_ScrView_grid.Children[15]).RowDefinitions.Add(new RowDefinition());
            TextBox textBox = new TextBox();
            textBox.TextInput += New_Row_Dialog;
            ((Grid)Npc_ScrView_grid.Children[15]).Children.Add(textBox);
            Grid.SetRow(textBox, 0);
            Grid.SetColumn(textBox, 1);
            Npc_ScrView.Visibility = Visibility.Collapsed;
            Npc_gridsplit.Visibility = Visibility.Collapsed;
        }
        private void New_Row_Dialog(object sender, EventArgs e)
        {
            if (((TextBox)((Grid)Npc_ScrView_grid.Children[11]).Children[((Grid)Npc_ScrView_grid.Children[11]).Children.Count - 1]).Text.Length > 0)
            {
                TextBox textBox = new TextBox();
                textBox.TextInput += New_Row_Dialog;
                ((Grid)Npc_ScrView_grid.Children[11]).Children.Add(textBox);
                ((Grid)Npc_ScrView_grid.Children[11]).RowDefinitions.Add(new RowDefinition());
                Grid.SetColumn(textBox, 1);
                Grid.SetRow(textBox, ((Grid)Npc_ScrView_grid.Children[11]).RowDefinitions.Count-1);
            }
        }
    }
}
