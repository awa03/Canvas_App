using Library.Models;
using Library.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Graphics;

namespace Canvas.ViewModel
{
    public class EnrollmentViewModel : INotifyPropertyChanged
    {
        public StudentService studentSrv { get; set; }
        public CourseService courseSrv { get; set; }
        public Person? student { get; set; }
        public ObservableCollection<Course> AllCourses {
            get {
                return new ObservableCollection<Course>(
                    courseSrv.CourseList.ToList()
                    ); 
            }
        }

        public Course? SelectedCourse { get; set; }
        
        public EnrollmentViewModel(int PersonId = -1)
        {
            studentSrv = StudentService.Current;
            courseSrv = CourseService.Current;
            if (PersonId <= 0)
            {
                throw new Exception("Error Loading Enrollment");
            }
            student = studentSrv.GetStudent(PersonId);
            SelectedCourse = null;
        }

        public void Enroll()
        {
            if (SelectedCourse == null)
            {
                isAdded = false;
                isEnrolledMsg = $"Please Select A Course";
            }
            else
            {
                isAdded = studentSrv.Enroll(student, SelectedCourse);
                isEnrolledMsg = $"Enrolled in {SelectedCourse.Name}";
            }
            RefreshAdded();
         }

        public void Unenroll()
        {
            if (SelectedCourse == null)
            {
                isAdded = false;
                isEnrolledMsg = $"Please Select A Course";
            }
            else
            {
                isAdded = studentSrv.Unenroll(student, SelectedCourse);
                isEnrolledMsg = $"Unenrolled from {SelectedCourse.Name}";

            }
            RefreshAdded();
        }

        // Enrollment Messaging stuff ---------------------------------
        
        void RefreshAdded()
        {
            NotifyPropertyChanged(nameof(isAdded));
            NotifyPropertyChanged(nameof(isEnrolledMsg));
        }


        public string isEnrolledMsg { get; set; }


        public bool isAdded
        {
            get; set;
        }

        
        // NotifyPropertyChanged stuff ---------------------------------
        // Basically just used to refresh the UI when course added / not added
        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
