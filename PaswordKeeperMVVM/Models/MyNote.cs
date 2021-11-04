using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaswordKeeperMVVM.Models
{
    public class MyNote
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string site_url { get; set; }
        public Category NoteCategory { get; set; }
    }
}
