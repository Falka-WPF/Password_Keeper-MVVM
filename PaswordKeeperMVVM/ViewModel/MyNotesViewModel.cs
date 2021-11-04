using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using PaswordKeeperMVVM.Models;

namespace PaswordKeeperMVVM.ViewModel
{
    public class MyNotesViewModel : INotifyPropertyChanged
    {
        private MyNote _myNote;
        public MyNotesViewModel(MyNote note)
        {
            this._myNote = note;
        }
        public int Id
        {
            get { return _myNote.Id; }
            set { _myNote.Id = value; OnProperyChanged("Id"); }
        }
        public string Login
        {
            get { return _myNote.Login; }
            set { _myNote.Login = value; OnProperyChanged("Login"); }
        }
        public string Password
        {
            get { return _myNote.Password; }
            set { _myNote.Password = value; OnProperyChanged("Password"); }
        }
        public string site_url
        {
            get { return _myNote.site_url; }
            set { _myNote.site_url = value; OnProperyChanged("site_url"); }
        }
        public Category NoteCategory
        {
            get { return _myNote.NoteCategory; }
            set { _myNote.NoteCategory = value; OnProperyChanged("NoteCategory"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnProperyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
