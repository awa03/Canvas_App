using Library.Models;
using Library.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Canvas.ViewModel
{
    internal class InstructorDialogViewModel : INotifyPropertyChanged
    {


        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Person instructor { get; set; }

        public string Name {
            get {
                return instructor?.Name ?? string.Empty;
            }
            set {
                if (instructor == null) instructor = new Person();
                instructor.Name = value;
            }
        }

        public string Email {
            get {
                return instructor?.Email ?? string.Empty;
            }
            set {
                if (instructor == null) instructor = new Person();
                instructor.Email = value;
            }
        }

       

        public InstructorDialogViewModel()
        {
            instructor = new Person();
        }

        public void AddInstructor()
        {
            if (instructor != null) InstructorService.Current.AddInstructor(instructor);
            instructor = new Person();
        }

        public InstructorDialogViewModel(int id = 0) {
            if (id > 0) {
                LoadById(id);
            }
        }

        public void LoadById(int id)
        {
            if (id == 0) return;
            var person = InstructorService.Current.GetInstructor(id) as Person;
            if (person != null) {
                instructor = person;
            }
            NotifyPropertyChanged(nameof(Name));
            NotifyPropertyChanged(nameof(Email));
        }



    }

}

