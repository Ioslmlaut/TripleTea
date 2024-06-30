using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripleTea.Tables
{
    class records
    {
        public int id { get; set; }
        public DateTime start_time { get; set; }
        public DateTime end_time { get; set; }
        public TimeSpan duration { get; set; }
        public string activity { get; set; } = "";
        public int computer_id { get; set; }
        public int user_id { get; set; }
        public required computers computer { get; set; }
        public required users user { get; set; }
    }
    class users
    {
        public int id {  get; set; }
        public string username { get; set; } = "";
        public string? password { get; set; }
        public ICollection<computers> computers { get; set; } = new List<computers>();
        public ICollection<records> records { get; set; } = new List<records>();
        public settings? setting { get; set; }
    }
    class computers
    {
        public int id { get; set; }
        public string name { get; set; } = "";
        public ICollection<records> records { get; set; } = new List<records>();
        public ICollection<users> users { get; set; } = new List<users>();
    }
    class settings
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public string theme { get; set; } = "";
        public required users user { get; set; }
    }
}
