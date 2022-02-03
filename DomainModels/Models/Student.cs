using DomainModels.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels.Models
{
    public class Student : Entity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
