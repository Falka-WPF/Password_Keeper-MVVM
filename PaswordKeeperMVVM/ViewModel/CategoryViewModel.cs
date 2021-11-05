using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.ComponentModel;
using PaswordKeeperMVVM.Models;

namespace PaswordKeeperMVVM.ViewModel
{
    public class CategoryViewModel : INotifyPropertyChanged
    {
        private Category _category;
        public CategoryViewModel(Category category)
        {
            this._category = category;
        }
        public Category getCategory()
        {
            return this._category;
        }
        public int Id
        {
            get { return _category.Id; }
            set { _category.Id = value; OnProperyChanged("Id"); }
        }
        public string Name
        {
            get { return _category.Name; }
            set { _category.Name = value; OnProperyChanged("Name"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnProperyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
