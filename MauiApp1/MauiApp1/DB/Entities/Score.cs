using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.DB.Entities
{
    public class Score
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public int Value { get; set; }
        public int Level { get; set; }
        public DateTime Date { get; set; }
    }
}
