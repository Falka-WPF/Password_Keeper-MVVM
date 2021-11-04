using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using PaswordKeeperMVVM.Data;
using PaswordKeeperMVVM.Models;
using PaswordKeeperMVVM.Commands;

namespace PaswordKeeperMVVM.ViewModel
{
    public class AppViewModel : INotifyPropertyChanged
    {
        private DataModel _dm;
        public ObservableCollection<CategoryViewModel> Categories_VM { get; set; }
        public ObservableCollection<MyNotesViewModel> MyNotes_VM { get; set; }
        private MyNotesViewModel currentNote;
        public MyNotesViewModel CurrentNote
        {
            get { return currentNote; }
            set { currentNote = value; OnPropertyChanged("CurrentNote"); }
        }
        private CategoryViewModel currentCategory;
        public CategoryViewModel CurrentCategory{
            get{return currentCategory;}
            set{currentCategory = value; OnPropertyChanged("CurrentCategory");}
        }

        public AppViewModel()
        {
            _dm = new DataModel();
            Categories_VM = new ObservableCollection<CategoryViewModel>();
            MyNotes_VM = new ObservableCollection<MyNotesViewModel>();
            LoadCategories();
            LoadMyNotes();
        }
        private void LoadCategories()
        {
            Categories_VM.Clear();
            var categories = _dm.Categories.ToList();
            foreach (var item in categories)
                Categories_VM.Add(new CategoryViewModel(item));
        }
        private void LoadMyNotes()
        {
            MyNotes_VM.Clear();
            var notes = _dm.MyNotes.ToList();
            foreach (var item in notes)
                MyNotes_VM.Add(new MyNotesViewModel(item));
        }

        private RelayCommand addMyNote;
        public RelayCommand AddMyNote
        {
            get
            {
                return addMyNote ?? (addMyNote = new RelayCommand(
                    (obj) =>
                    {
                        Category ct = new Category();
                        if (CurrentCategory != null)
                        {
                            ct.Id = CurrentCategory.Id;
                            ct.Name = CurrentCategory.Name;
                        }
                        MyNote myNote = new MyNote()
                        {
                            Login = "NewNote",
                            Password = "",
                            site_url = "",
                            NoteCategory = ct
                        };
                        MyNotesViewModel mnvm = new MyNotesViewModel(myNote);
                        MyNotes_VM.Add(mnvm);
                        CurrentNote = mnvm;
                    }));
            }
        }

        private RelayCommand updateNote;
        public RelayCommand UpdateNote
        {
            get { return updateNote ?? (updateNote = new RelayCommand(
                (obj) =>
                {
                    var notes = MyNotes_VM.Where(c => c.Id == 0).ToList();
                    foreach (var note in notes)
                    {
                        _dm.MyNotes.Add(new MyNote() { NoteCategory = note.NoteCategory, Login= note.Login,Password = note.Password, site_url = note.site_url});
                    }
                    _dm.SaveChanges();
                    LoadMyNotes();
                    System.Windows.MessageBox.Show("Sucessfully saved!", "Informarion", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
                )); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop="")
        {
            if(PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(prop));
            }
        }
    }
}
