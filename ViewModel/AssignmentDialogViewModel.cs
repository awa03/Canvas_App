using Library.Models;
using Library.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canvas.ViewModel
{
    public class AssignmentDialogViewModel
    {

        private Course _course { get; set; }
        public Course course {
            get {
                return _course; 
            }
            set {
                _course = value;  
            } 
        }

        private Person _student { get; set; }
        public Person student {
            get {
                return _student; 
            }
            set {
                _student = value; 
            } 
        }

        public string Name {
            get { 
                return course.Name; }            
                
        }

        private StudentService studentSrv { get; set; } 
        private CourseService courseSrv { get; set; }
        public Assignment LoadAssignment { get; set; }

        public bool AddToAll { get; set; }

        public AssignmentDialogViewModel(int PersonId, int CourseId, int AssignmentId)
        {

            if (PersonId == SelectedCourseViewModel.DefaultStudentId)
            {
                AddToAll = true;
            }
            else {
                AddToAll = false; 
            }
            studentSrv = StudentService.Current;
            courseSrv = CourseService.Current;
            course = courseSrv.GetCourseId(CourseId);
            student = studentSrv.GetStudent(PersonId);
            if (AssignmentId == SelectedCourseViewModel.DefaultAssignmentId) {
                LoadAssignment = new Assignment(); 
            }
            else
            {
                LoadAssignment = course.GetAssignment(AssignmentId);
            }
        }

        public string AssignmentName {
            get {
                return LoadAssignment.Name; 
            }
            set {
                LoadAssignment.Name = value; 
            }
        }

        public string AssignmentDescription
        {
            get
            {
                return LoadAssignment.Desc; 
            }
            set
            {
                LoadAssignment.Desc = value; 
            }
        }

        public int AssignmentPoints
        {
            get
            {
                return LoadAssignment.Points; 
            }
            set
            {
                LoadAssignment.Points = value; 
            }
        }

        public int AssignmentPossiblePoints
        {
            get
            {
                return LoadAssignment.PossiblePoints; 
            }
            set
            {
                LoadAssignment.PossiblePoints = value; 
            }
        }

        public DateTime AssignmentDueDate
        {
            get
            {
                return LoadAssignment.DueDate; 
            }
            set
            {
                LoadAssignment.DueDate = value; 
            }
        }

        public void DeleteAssignment()
        {
            if (AddToAll)
            {
                course.DeleteAssignmentFromAllStudents(LoadAssignment);
            }
            else
            {
                course.DeleteAssignmentFromSelectStudent(student.Id, LoadAssignment);
            }
        }
        

        public void Save()
        {
            if (AddToAll)
            {
                course.AddAssignmentToAllStudents(LoadAssignment);
            }
            else
            {
               course.AddAssignmentToSelectStudent(student.Id, LoadAssignment);
            }
        }

    }
}
