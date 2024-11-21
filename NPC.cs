using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

/*
 
 */

namespace KR
{
    abstract internal class NPC
    {
        public string Name { get; set; }
        public int pos;
        public List<string> images;
        public int HP { get; set; }
        public int Damage { get; set; }
        public int v { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        List<string> dialog;
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
        public virtual void Set_Position(int x, int y)
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
    internal class Hero : NPC
    {
        
    }
}
