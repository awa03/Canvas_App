using Canvas.ViewModel;
using Library.Models;
using Library.Services;
namespace Canvas.View;

[QueryProperty(nameof(PersonId), "personId")]
[QueryProperty(nameof(CourseId), "courseId")]
public partial class SelectedCourseView : ContentPage
{
	public int PersonId
	{
        get;
        set;
    }
	public int CourseId
	{
        get;
        set;
    }
	public SelectedCourseView()
	{
		InitializeComponent();
	}

    public void SearchClicked(object sender, EventArgs e)
    {
        (BindingContext as SelectedCourseViewModel)?.Search();
    }

    void BackClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync($"//SelectedInstructor?personId={PersonId}");
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {

		BindingContext = new SelectedCourseViewModel(PersonId, CourseId);
    }

    public void ViewAssignmentsClicked(object sender, EventArgs e)
    {
        (BindingContext as SelectedCourseViewModel)?.ViewAssignments();
    }

    void AddAssignmentClicked(object sender, EventArgs e)
    {
        (BindingContext as SelectedCourseViewModel)?.AddAssignment(Shell.Current);
    }

    void EditAssignmentClicked(object sender, EventArgs e)
    {
        (BindingContext as SelectedCourseViewModel)?.EditAssignment(Shell.Current);
    }

    void DeleteAssignmentClicked(object sender, EventArgs e)
    {
        (BindingContext as SelectedCourseViewModel)?.DeleteAssignment();
    }

    private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {

    }
}
