using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OrderForm
{
    [Serializable]
    public class Client
    {
        [Key]
        public int ClientId { get; set; }
        public string cname { get; set; }

        public Client()
        {
        }
        public Client(int id, string name)
        {
            ClientId = id;
            cname = name;
        }

        public override string ToString()
        {
            //return ("ClientID: "+ cid+ "  Name: " + cname);
            return cname;
        }
    }
}
