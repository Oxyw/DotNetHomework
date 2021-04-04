using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement
{
    class Client
    {
        public uint cid;
        public string cname;

        public Client() { }
        public Client(uint id, string name)
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
