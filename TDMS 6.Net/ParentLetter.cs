using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TDMS
{
    public class ParentLetter
    {
        public int Id { get; set; }
        public string? Type { get; set; }//сопроводительное или исходящее, оставлю как есть, могут быть еще заказными и т.п
        public int Number { get; set; }
        public DateTime DateTime { get; set; }
        public int ProjectId { get; set; }
        public virtual Project? Project { get; set; }
        public virtual User? From { get; set; }
        public virtual User? To { get; set; }

        //public File f { get; set; }
        //public List<ParentLetter> parentLetters { get; set; }
    }

    public class Project
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int CompanyId { get; set; }
        public virtual Company? Company { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }

    public class Company
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public virtual List<User> Users { get; set; } = new(); // сотрудники компании
        
        public override string ToString()
        {
            return Name;
        }
    }

    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int CompanyId { get; set; }
        public virtual Company? Company { get; set; }  // компания пользователя
        public override string ToString()
        {
            return Name;
        }
    }
}
