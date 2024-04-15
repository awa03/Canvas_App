using Canvas.ViewModel;
using Library.Models;

namespace Canvas.View;


[QueryProperty(nameof(PersonId), "personId")]
public partial class SelectedInstructorView : ContentPage
{
	public int PersonId {
		get;
		set;
	}

	public SelectedInstructorView()
	{
        InitializeComponent();
	}

	public void EnterCourseClicked(object sender, EventArgs e)
	{
        (BindingContext as SelectedInstructorViewModel)?.EnterCourse(Shell.Current);
    }

	public void AddCourseClicked(object sender, EventArgs e)
	{
		(BindingContext as SelectedInstructorViewModel)?.AddCourseClicked(Shell.Current);
    }

	public void EditCourseClicked(object sender, EventArgs e)
	{
        (BindingContext as SelectedInstructorViewModel)?.EditCourseClicked(Shell.Current);
    }

	public void DeleteCourseClicked(object sender, EventArgs e) {
		(BindingContext as SelectedInstructorViewModel)?.DeleteCourseClicked();	
	}

	public void BackClicked(object sender, EventArgs e) { 
		Shell.Current.GoToAsync("//Instructor");	
	}


    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
		BindingContext = new SelectedInstructorViewModel(PersonId);
    }
}