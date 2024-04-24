using Library.Services;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using Microsoft.Maui;

namespace Canvas.ViewModel
{
    public partial class SelectedInstructorViewModel : INotifyPropertyChanged
    {
        public string Name {
            get {
                return instructor.Name; 
            }
            set {
                instructor.Name = value; 
            }
        }

        public int Id {
            get {
                return instructor.Id; 
            }
            set {
                instructor.Id = value;
            }
        }

        public string Email {
            get {
                return instructor.Email;
            }
            set {
                instructor.Email = value; 
            }
        }

        private Person instructor { get; set; }
        public Person Instructor {
            get {
                return instructor; 
            }
            set {
                instructor = value;
            } 
        }

        public Course SelectedCourse { get; set; }

        public ObservableCollection<Course> Courses {
            get {
                return new ObservableCollection<Course>(
                  instructor.courseList.ToList());
            } 
        }

        public void EnterCourse(Shell s)
        {
            if (SelectedCourse == null) { return; }
            s.GoToAsync($"//SelectedCourse?personId={instructor.Id}&courseId={SelectedCourse.Id}");
        }

        public SelectedInstructorViewModel()
        {
            instructor = new Person();
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public SelectedInstructorViewModel(int id = -1) {

            if (id > -1) {
                LoadById(id);
            }

        }

        public static int Default = -1; 

        public void AddCourseClicked(Shell shell)
        {
            shell.GoToAsync($"//CourseDetail?personId={Id}&courseId={Default}");
        }

        public void EditCourseClicked(Shell shell)
        {
            if (SelectedCourse == null) { return; }
            shell.GoToAsync($"//CourseDetail?personId={Id}&courseId={SelectedCourse.Id}");
        }

        // Courses are being managed horribly but im somehow bandaiding it together
        // but thats all cs is right!
        public void DeleteCourseClicked() {
            if (SelectedCourse == null) { return; }
            Instructor.courseList.Remove(SelectedCourse);
            CourseService.Current.CourseList.Remove(SelectedCourse);
             NotifyPropertyChanged(nameof(Instructor));
            NotifyPropertyChanged(nameof(Courses));

        }



      public void LoadById(int id = 0)
        {
            if (id == 0) return;
            var person = InstructorService.Current.GetInstructor(id) as Person;
            if (person != null)
            {
                instructor = person;
            }
            
            NotifyPropertyChanged(nameof(Instructor));
            NotifyPropertyChanged(nameof(Courses));
        }
    }
}
