using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;

/*
 
 */

namespace KR
{
    [XmlInclude(typeof(Vilag))]
    abstract public class NPC
    {
        [XmlElement]
        public int number_dialog {  get; set; }
        [XmlElement]
        public string Name { get; set; }
        [XmlElement]
        public int pos;
        [XmlArray]
        public List<string> images;
        [XmlElement]
        public int HP { get; set; }
        [XmlElement]
        public int Damage { get; set; }
        [XmlElement]
        public double v { get; set; }
        [XmlElement]
        public double x { get; set; }
        [XmlElement]
        public double y { get; set; }
        [XmlArray]
        public List<string> dialog;
        [XmlElement]
        public int attitude {  get; set; }
        public virtual void Set_pos(int pos)
        {
            this.pos = pos;
        }
        public virtual void Set_Images(List<string> images_way)
        {
            this.images = images_way;
        }
        public virtual void Set_Dialog(string[] dialog_way)
        {
            for(int i = 0;i < dialog_way.Length; i++)
            {
                dialog.Add(dialog_way[i]);
            }
        }
        public virtual void Set_Position(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
        public virtual void Set_HP(int hp)
        {
            this.HP = hp;
        }
        public virtual void Set_Damage(int damage)
        {
            this.Damage = damage;
        }
        public virtual string Get_Dialog(int number_dialog)
        {
            return number_dialog < dialog.Count ? dialog[number_dialog] : "";
        }
        public virtual List<string> Get_Images()
        {
            return this.images;
        }
        public virtual void Damaged(int damage)
        {
            this.HP -= damage;
        }
        public virtual int Get_Damage()
        {
            return this.Damage;
        }
    }
    [XmlType]
    public class Vilag : NPC
    {
        public Vilag()
        {
            this.attitude = 0;
            this.Damage = 5;
            this.HP = 20;
            this.v = 30;
            this.number_dialog = 0;
            this.pos = 0;
            this.images = new List<string>();
            this.dialog = new List<string>();
        }
    }
    internal class Hero : NPC
    {
        
    }
}
