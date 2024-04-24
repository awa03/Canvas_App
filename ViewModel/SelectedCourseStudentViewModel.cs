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
    public class SelectedCourseStudentViewModel
    {
        // Private feilds needed for the view model
        private Person _student { get; set; }
        private Course _course { get; set; }

        // public services for the view model
        public StudentService studentSrv { get; set; }
        public CourseService courseSrv { get; set; }

        public Assignment SelectedAssignment { get; set; }
        

        // Constructor for the view model -- Only one that should be used

        public SelectedCourseStudentViewModel(int personId, int courseId) {

            // Error checking for invalid Id's
            if (personId <= 0 || courseId <= 0) {
                throw new Exception("Error Invalid Id's");
            }      

            studentSrv = StudentService.Current;
            courseSrv = CourseService.Current;
            
            _student = studentSrv.GetStudent(personId);
            _course = _student.GetStudentsCourse(courseId);

            if (_course == null || _student == null) {
                throw new Exception("student or course is null buddy"); 
            }
        }

        public void GoToSubmissionPage(Shell s)
        {
            // Go to the submission page
            if (SelectedAssignment == null)
            {
                return;
            }
            else { 
                 s.GoToAsync($"//Submit?personId={student.Id}&courseId={course.Id}&assignmentId={SelectedAssignment.Id}");
            }
        }

        // Public accessors for the private fields
        public Person student
        {
            get
            {
                return _student; 
            }
            set
            {
                _student = value; 
            }
        }

        public string WelcomeMsg
        {
            get
            {
                return $"Welcome {_student.Name}";    
            } 
        }

        public ObservableCollection<Assignment> Assignments {
            get {
                return new ObservableCollection<Assignment>(
                   course.GetStudentsAssignments(_student.Id)
                    ); 
            } 
        }

        public Course course
        {
            get
            {
                return _course; 
            }
            set
            {
                _course = value; 
            }
        }
    }
}
