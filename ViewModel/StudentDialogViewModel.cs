using Library.Models;
using Library.Services;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Canvas.ViewModel
{
    public class StudentDialogViewModel : INotifyPropertyChanged
    {
        private Person student;
        public Person Student {
            get {
                return student; 
            } 
        }

        public List<string> YearOptions { get; } = new List<string> { "Freshman", "Sophomore", "Junior", "Senior" };
        public string SelectedYear {
            set {
                switch (value) { 
                    case "Freshman":
                        student.year = Person.Classification.Freshman;
                        break;
                    case "Sophomore":
                        student.year = Person.Classification.Sophomore;
                        break;
                    case "Junior":
                        student.year = Person.Classification.Junior;
                        break;
                    case "Senior":
                        student.year = Person.Classification.Senior;
                        break;
                    default:
                        student.year = Person.Classification.None;
                        break;
                }
            }
        }
       public string Name {
            get {
                return student?.Name ?? string.Empty; 
            }
            set {
                if (student == null) student = new Person();
                student.Name = value;
            }
       } 

        public string Email {
            get { 
                return student?.Email ?? string.Empty;
            }
            set { 
                if(student == null)  student = new Person();
                student.Email = value;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        


        public StudentDialogViewModel()
        {
            student = new Person();
        }

        public void AddStudent()
        {
            if (student != null) StudentService.Current.AddStudent(student);
            student = new Person();
        }

        public StudentDialogViewModel(int id = 0) {

            if (id > 0) {
                LoadById(id);
            }

        }

        public void LoadById(int id)
        {
            if (id == 0) return;
            var person = StudentService.Current.GetStudent(id) as Person;
            if (person != null) {
                Name = person.Name; 
                SelectedYear = Enum.GetName(typeof(Person.Classification), person.year) ?? "Freshman";
                student.Id = person.Id;
                student.Email = person.Email;
            }
            NotifyPropertyChanged(nameof(Name));
            NotifyPropertyChanged(nameof(Email));
            NotifyPropertyChanged(nameof(SelectedYear));
            
        }
    }
}
