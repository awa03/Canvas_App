using Library.Models;
using Library.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canvas.ViewModel
{
    public class SelectedStudentViewModel
    {
        public StudentService studentSrv { get; set; }
        public CourseService courseSrv { get; set; }
        private Person _student { get; set; }

        public Course SelectedCourse { get; set; }

        public string WelcomeMsg {
            get {
                return $"Welcome {_student.Name}";    
            } 
        }

        public Person student {
            get {
                return _student; 
            }
            set {
                _student = value; 
            }
        }

        public List<Course> courses
        {
            get
            {
                return student.courseList;
            }
        }

        public ObservableCollection<Course> DisplayCourseList {
            get {
                return new ObservableCollection<Course>(courses); 
            } 
        }


        public SelectedStudentViewModel(int PersonId = -1)
        {
            studentSrv = StudentService.Current;
            courseSrv = CourseService.Current; 
            if (PersonId <= 0) {
                throw new Exception("Error Loading Selected Student");
            } 
            _student = studentSrv.GetStudent(PersonId);
        }

        public void OpenSelectedCourse(Shell s)
        {
            if (SelectedCourse == null) { return;  }
            s.GoToAsync($"//SelectedStudentCourse?personId={student.Id}&courseId={SelectedCourse.Id}");

        }
    }
}
