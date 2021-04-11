using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement
{
    [Serializable]
    public class Client
    {
        public int cid;
        public string cname;

        public Client()
        {
        }
        public Client(int id, string name)
        {
            cid = id;
            cname = name;
        }

        public override string ToString()
        {
            return ("ClientID: "+ cid+ "  Name: " + cname);
        }
    }
}
