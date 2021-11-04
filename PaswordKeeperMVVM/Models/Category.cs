using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaswordKeeperMVVM.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        List<MyNote> MyTasks { get; set; }
    }
}
