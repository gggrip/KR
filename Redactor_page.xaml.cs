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

namespace KR
{
    /// <summary>
    /// Логика взаимодействия для Redactor_page.xaml
    /// </summary>
    public partial class Redactor_page : Page
    {
        Button button_SHAB = new Button();
        public Redactor_page()
        {
            InitializeComponent();
            ResourceDictionary resourceDictionary = new ResourceDictionary();
            resourceDictionary.Source = new Uri("Styles/Main_menu.xaml", UriKind.RelativeOrAbsolute);
            this.Resources.MergedDictionaries.Add(resourceDictionary);
            List<Button> Titles = new List<Button>();
            for (int i = 1; i < 14; i++)
            {
                Titles.Add(Set_Button("pack://application:,,,/Images/Tiles/Tile_road_" +Convert.ToString(i)+ "_lvl1.png", 80, 80, (Style)this.Resources["Tiles"]));
                Titles[Titles.Count() - 1].Click += Button_vib_Click;
            }
            for (int i = 1; i < 5; i++)
            {
                Titles.Add(Set_Button("pack://application:,,,/Images/Tiles/Tile_grass_" + Convert.ToString(i) + "_lvl1.png", 80, 80, (Style)this.Resources["Tiles"]));
                Titles[Titles.Count() - 1].Click += Button_vib_Click;
            }
            for(int i = 0; i < Titles.Count(); i++)
            {
                Redactor_ScrView_grid.Children.Add(Titles[i]);
                if(i % 2 == 0)
                {
                    Redactor_ScrView_grid.RowDefinitions.Add(new RowDefinition());
                }
                Grid.SetRow(Titles[i], i/2);
                Grid.SetColumn(Titles[i], i%2); 
            }
            for (int j = 0; j < 24; j++)
            {
                Redactor_map.ColumnDefinitions.Add(new ColumnDefinition());
                Redactor_map.ColumnDefinitions[j].Width = GridLength.Auto;
            }
            for (int i = 0; i < 14; i++)
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
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(way, UriKind.RelativeOrAbsolute);
            bitmap.EndInit();
            ImageBrush imageBrush = new ImageBrush(bitmap);
            button.Background = imageBrush;
            button.Style = st;
            return button;
        }
        private void Button_vib_Click(object sender, EventArgs e)
        {
            button_SHAB = (Button)sender;
        }
        private void Button_map_Click(object sender, EventArgs e)
        {
            ((Button)sender).Background = button_SHAB.Background;
        }
    }
}
