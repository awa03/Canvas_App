using Canvas.ViewModel;
using Library.Models;
using Library.Services;
namespace Canvas.View;

public partial class InstructorView : ContentPage
{
	public InstructorView()
	{
		InitializeComponent();
		BindingContext = new InstructorViewModel();
	}

	void AddStudentClicked(object sender, EventArgs e)
	{
        Shell.Current.GoToAsync($"//StudentDetail?personId={new Person().Id}");
    }

	void DeleteStudentClicked(object sender, EventArgs e)
	{

		(BindingContext as InstructorViewModel)?.EditStudentClicked(Shell.Current);
    }

	void DeleteInstructorClicked(object sender, EventArgs e)
	{
		(BindingContext as InstructorViewModel)?.DeleteInstructor();
    }

	void EditStudentClicked(object sender, EventArgs e)
	{
		(BindingContext as InstructorViewModel)?.EditStudentClicked(Shell.Current);
    }

	void AddInstructorClicked(object sender, EventArgs e) {
		// safer than using casting to avoid null reference exceptions
		Shell.Current.GoToAsync($"//InstructorDetail?personId={new Person().Id}");
	}

	void EditInstructorClicked(object sender, EventArgs e) {
		(BindingContext as InstructorViewModel)?.EditInstructorClicked(Shell.Current);
	}

    void BackClicked(object sender, EventArgs e)
	{
        Shell.Current.GoToAsync("//MainPage");
    }

	void SearchStudentClicked(object sender, EventArgs e)
	{
        (BindingContext as InstructorViewModel)?.RefreshStudents();
    }
	void SearchInstructorClicked(object sender, EventArgs e) { 
		(BindingContext as InstructorViewModel)?.RefreshInstructors();
	}

    void ClearInstructorSearch(object sender, EventArgs e) { 
		(BindingContext as InstructorViewModel)?.ClearInstructorSearch();
	}

	void ClearStudentSearch(object sender, EventArgs e) { 
		(BindingContext as InstructorViewModel)?.ClearStudentSearch();
	}

	void EnterInstructorClicked(object sender, EventArgs e) { 
		(BindingContext as InstructorViewModel)?.EnterInstructor(Shell.Current);
	}

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
		(BindingContext as InstructorViewModel)?.Refresh();
    }
}