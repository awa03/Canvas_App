using System;
using Library.Models;
using Library.Services;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Canvas.ViewModel
{
    internal class InstructorViewModel : INotifyPropertyChanged
    {
        private InstructorService instructorSvc;
        private StudentService studentSvc;
        private Person? NewInstructor;
        public Person? SelectedStudent { get; set; } 
        public Person? SelectedInstructor { get; set; } 

        public Person? newInstructor
        {
            get
            {
                return NewInstructor;
            }
            set
            {
                NewInstructor = value;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string InstructorQuery { get; set; }
        public string StudentQuery { get; set; }
        
        public ObservableCollection<Person> Instructors {
            get {
                return new ObservableCollection<Person>(instructorSvc.Roster.ToList()
                    .Where(s => s?.Name.ToLower()?
                    .Contains(InstructorQuery?.ToLower() ?? string.Empty) ?? false)) ;
            } 
        }
        public ObservableCollection<Person> Students
        {
            get
            {
                 return new ObservableCollection<Person>(studentSvc.Roster.ToList()
                    .Where(s => s?.Name.ToLower()?
                    .Contains(StudentQuery?.ToLower() ?? string.Empty) ?? false)) ;

            }
        }

        public void ClearInstructorSearch()
        {
            InstructorQuery = string.Empty;
            NotifyPropertyChanged(nameof(Instructors));
            NotifyPropertyChanged(nameof(InstructorQuery));
        }

        public void ClearStudentSearch()
        {
            StudentQuery = string.Empty;
            NotifyPropertyChanged(nameof(Students));
            NotifyPropertyChanged(nameof(StudentQuery));
        }
 



        public InstructorViewModel()
        {
            instructorSvc = InstructorService.Current;
            studentSvc = StudentService.Current;
            SelectedInstructor = null;
            SelectedStudent = null;
            NewInstructor = new Person();
        }

        public void AddInstructor() {
            instructorSvc.AddInstructor(new Person { Name = "This is a new Instructor" });
            NotifyPropertyChanged(nameof(Instructors));
        }

        public void DeleteStudent()
        {
            if (SelectedStudent == null)
            {
                return;
            }
            StudentService.Current.Delete(SelectedStudent);
            Refresh();
        }

        public void DeleteInstructor() { 
            if(SelectedInstructor == null)
            {
                return;
            }
            InstructorService.Current.Delete(SelectedInstructor);
            Refresh();
        }

        public void EnterInstructor(Shell s)
        {
            if (SelectedInstructor == null) {
                return; 
            }
            s.GoToAsync($"//SelectedInstructor?personId={SelectedInstructor?.Id}");
        }

        public void EditStudentClicked(Shell s) {
            var idParam = SelectedStudent?.Id ?? 0; 
            s.GoToAsync($"//StudentDetail?personId={idParam}");
        }

        public void EditInstructorClicked(Shell s)
        {
            var idParam = SelectedInstructor?.Id ?? 0; 
            s.GoToAsync($"//InstructorDetail?personId={idParam}");
        }

        internal void Refresh()
        {
            NotifyPropertyChanged(nameof(Instructors));
            NotifyPropertyChanged(nameof(Students));
        }

        public void RefreshStudents()
        {
            NotifyPropertyChanged(nameof(Students));
        }

        public void RefreshInstructors()
        {
            NotifyPropertyChanged(nameof(Instructors));
        }
    }
}
