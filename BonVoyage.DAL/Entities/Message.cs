using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonVoyage.DAL.Entities
{
    public class Message
    {
        public int ID { get; set; }
        public string? Text { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public virtual User? User { get; set; }
    }

}
