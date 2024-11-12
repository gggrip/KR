using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;


namespace KR
{
    [XmlType]
    public struct Map_obj
    {
        [XmlElement]
        public double x;
        [XmlElement]
        public double y;
        [XmlElement]
        public string image;
    }
    [XmlType]
    public class Map
    {
        [XmlArray]
        public List<bool> col;
        [XmlArray]
        public List<Map_obj> objj;
        [XmlArray]
        public List<string> tiless;
        public Map()
        {
            col = new List<bool>();
            objj = new List<Map_obj>();
            tiless = new List<string>();

        }
        public Map(List<bool> col, List<Map_obj> objj, List<string> tiless)
        {
            this.col = col;
            this.objj = objj;
            this.tiless = tiless;
        }

        public void add_Map_tile(int n)
        {
            for (int i = 0; i < n; i++)
            {
                col.Add(true);
                tiless.Add("");
            }
        }
        public void add_Map_obj(double x, double y, string bitmapImage)
        {
            Map_obj obj = new Map_obj();
            obj.x = x;
            obj.y = y;
            obj.image = bitmapImage;
            this.objj.Add(obj);
        }
        public void red_tiless(string bitmapImage, int pos)
        {
            try
            {
                this.tiless[pos] = bitmapImage;
            }
            catch 
            {
                MessageBox.Show(Convert.ToString(pos));
            }
        }
        public void red_col(bool col,int pos)
        {
            this.col[pos] = col;
        }
        public void remove_Map_obj(double x, double y)
        {
            objj.Remove(objj.FirstOrDefault(p => p.x == x && p.y == y));
        }
    }
}
