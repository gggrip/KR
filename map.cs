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
        public List<List<int>> col;
        [XmlArray]
        public List<Map_obj> objj;
        [XmlArray]
        public List<string> tiless;
        [XmlArray]
        public List<NPC> vilags;
        public Map()
        {
            col = new List<List<int>>();
            objj = new List<Map_obj>();
            tiless = new List<string>();
            vilags = new List<NPC>();
        }
        public Map(List<List<int>> col, List<Map_obj> objj, List<string> tiless, List<NPC> vilags)
        {
            this.col = col;
            this.objj = objj;
            this.tiless = tiless;
            this.vilags = vilags;
        }

        public void add_Map_tile(int n)
        {
            for (int i = 0; i < n; i++)
            {
                col.Add([]);
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
        public void red_col(List<int> col,int pos)
        {
            this.col[pos] = col;
        }
        public void remove_Map_obj(double x, double y)
        {
            objj.Remove(objj.FirstOrDefault(p => p.x == x && p.y == y));
        }
        public void add_Npc(Vilag vilag)
        {
            this.vilags.Add(vilag);
        }
        public void red_Npc(Vilag vilag,int pos)
        {
            this.vilags[pos] = vilag;
        }
    }
}
