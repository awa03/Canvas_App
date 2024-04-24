using Canvas.ViewModel;
using Library.Models;
namespace Canvas.View;

public partial class StudentsView : ContentPage
{
	public StudentsView()
	{
		InitializeComponent();
		BindingContext = new StudentsViewModel();
	}

	void AddStudentClicked(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync($"//StudentDetail?personId={new Person().Id}");
    }

	void RemoveStudentClicked(object sender, EventArgs e)
	{
        (BindingContext as StudentsViewModel)?.RemoveStudent();
    }

	void SelectStudentClicked(object sender, EventArgs e)
	{
		(BindingContext as StudentsViewModel)?.EnterStudent();
    }

	void BackClicked(object sender, EventArgs e)
	{
        Shell.Current.GoToAsync("//MainPage");
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
		(BindingContext as StudentsViewModel)?.Refresh();
    }

    void SearchClicked(object sender, EventArgs e)
    {
		(BindingContext as StudentsViewModel)?.Refresh();
    }

    void ClearSearchClicked(object sender, EventArgs e) {
		(BindingContext as StudentsViewModel)?.ClearSearch();	
	}
}