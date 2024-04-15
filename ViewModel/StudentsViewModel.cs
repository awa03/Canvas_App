using Library.Models;
using Library.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Canvas.ViewModel
{
    internal class StudentsViewModel : INotifyPropertyChanged
    {
        private StudentService studentSvc;
        public Person? SelectedStudent { get; set; }

        public string Query { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<Person> Students {
            get {
                return new ObservableCollection<Person>(
                    studentSvc.Roster.ToList()
                    .Where(s => s?.Name.ToLower()?
                    .Contains(Query?.ToLower() ?? string.Empty) ?? false)) ;
            } 
        }

        public StudentsViewModel() {
            studentSvc = StudentService.Current;
            SelectedStudent = new Person();
            Query = "";
        }

        public void ClearSearch()
        {
            Query = string.Empty;
            NotifyPropertyChanged(nameof(Students));
            NotifyPropertyChanged(nameof(Query));
        }

        public void RemoveStudent() {
            if (SelectedStudent == null)
            {
                return;
            }
            StudentService.Current.Delete(SelectedStudent);
            Refresh();
        }

         public void AddStudent() {
            Shell.Current.GoToAsync($"//StudentDetail?personId={new Person().Id}");
            Refresh();
        }


       
        public void Refresh()
        {
            NotifyPropertyChanged(nameof(Students));
        }

    }
}
