using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TDMS
{
    class ParentLetter
    {
        public int ID { get; set; }
        public string type { get; set; }//сопроводительное или исходящее
        public int number { get; set; }
        public DateTime DateTime { get; set; }
        public string nameObgect { get; set; }
        public string from { get; set; }
        public string to { get; set; } 
        
        //public File f { get; set; }
    }
    class Company
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<User> Users { get; set; } = new();
    }
    class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public int CompanyId { get; set; }
        public Company? Company { get; set; }
    }

    class OutgoingLetter:ParentLetter 
    { 

    }

    class IncomingLetter : ParentLetter 
    { 

    }

}
