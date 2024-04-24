using Canvas.ViewModel;

namespace Canvas.View;

[QueryProperty(nameof(PersonId), "personId")]
public partial class SelectedStudentView : ContentPage
{
	public int PersonId { get; set; }
	public SelectedStudentView()
	{
		InitializeComponent();
	}

	public void SelectClicked(object sender, EventArgs e)
	{
		(BindingContext as SelectedStudentViewModel)?.OpenSelectedCourse(Shell.Current);
    }

	public void EnrollClicked(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync($"//Enroll?personId={PersonId}");
    }

	public void BackClicked(object sender, EventArgs e) {
		Shell.Current.GoToAsync("//Student");	
	}

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
		BindingContext = new SelectedStudentViewModel(PersonId);
    }

}