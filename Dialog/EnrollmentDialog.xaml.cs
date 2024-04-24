using Canvas.ViewModel;

namespace Canvas.Dialog;


[QueryProperty(nameof(PersonId), "personId")]
public partial class EnrollmentDialog : ContentPage
{
	public int PersonId { get; set; }
	public EnrollmentDialog()
	{
		InitializeComponent();
	}

	public void EnrollClicked(object sender, EventArgs e)
	{
		(BindingContext as EnrollmentViewModel)?.Enroll();
    }

	public void UnenrollClicked(object sender, EventArgs e)
	{
        (BindingContext as EnrollmentViewModel)?.Unenroll();
    }

	public void BackClicked(object sender, EventArgs e)
	{
        Shell.Current.GoToAsync($"//SelectedStudentHome?personId={PersonId}");

	}

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
		BindingContext = new EnrollmentViewModel(PersonId);
    }
}