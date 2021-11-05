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
            if (categories.Count == 0)
            {
                _dm.Categories.Add(new Category() { Name = "Social account" });
                _dm.Categories.Add(new Category() { Name = "E - mail account" });
                _dm.SaveChanges();
                categories = _dm.Categories.ToList();
            }
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
                        MyNote myNote = new MyNote()
                        {
                            Login = "NewNote",
                            Password = "",
                            site_url = "",
                            NoteCategory = currentCategory.getCategory()
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

        private RelayCommand delNote;
        public RelayCommand DelNote
        {
            get {
                return delNote ?? (delNote = new RelayCommand(
               (obj) =>
               {
                   if (CurrentNote != null)
                   {
                       //MyNote toDelete = _dm.MyNotes.Where(c => c.Id == CurrentNote.Id && c.Login == CurrentNote.Login && c.Password == CurrentNote.Password && c.site_url == CurrentNote.site_url).FirstOrDefault();
                       MyNote toDelete = _dm.MyNotes.Where(c => c.Id == CurrentNote.Id).FirstOrDefault();
                       if (toDelete != null)
                       {
                           _dm.MyNotes.Remove(toDelete);
                           _dm.SaveChanges();
                           LoadMyNotes();
                           System.Windows.MessageBox.Show("Sucessfully deleted!", "Done", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                       }
                   }
               }
               )); }
        }

        private RelayCommand filterNotes { get; set; }
        public RelayCommand FilterNotes
        {
            get
            {
                return filterNotes ?? (filterNotes = new RelayCommand(
                    (obj) =>
                    {
                        LoadMyNotes();
                        if (CurrentCategory != null)
                        {
                            var notes = MyNotes_VM.Where(note => note.NoteCategory?.Id == CurrentCategory.Id).ToList();
                            MyNotes_VM.Clear();
                            if (notes.Count > 0)
                            {
                                foreach (MyNotesViewModel mnvm in notes)
                                    MyNotes_VM.Add(mnvm);
                            }
                        }
                    }));
            }
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
