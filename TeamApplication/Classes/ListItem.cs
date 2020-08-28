using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamApplication.Classes
{
    public class ListItem
    {
        public int ID { get; set; }
        public string TEXT { get; set; }

        public ListItem(int iD, string tEXT)
        {
            ID = iD;
            TEXT = tEXT;
        }
    }
}
