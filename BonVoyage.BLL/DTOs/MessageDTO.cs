using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonVoyage.BLL.DTOs
{
    public class MessageDTO
    {
        public int ID { get; set; }
        public string? Text { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public string? UserName { get; set; }
    }
}
