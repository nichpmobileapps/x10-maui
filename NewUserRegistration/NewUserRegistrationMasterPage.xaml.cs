using X10Card.Models;
using X10Card.Models.NewUserRegistration;

namespace X10Card.NewUserRegistration;

public partial class NewUserRegistrationMasterPage : FlyoutPage
{
    string RegNo="";
    public NewUserRegistrationMasterPage(int id)
	{
        try
        {
            System.Diagnostics.Debug.WriteLine("=== NewUserRegistrationMasterPage: Starting InitializeComponent ===");
            Console.WriteLine("=== NewUserRegistrationMasterPage: Starting InitializeComponent ===");
            
            InitializeComponent();
            
            System.Diagnostics.Debug.WriteLine("=== NewUserRegistrationMasterPage: InitializeComponent completed ===");
            Console.WriteLine("=== NewUserRegistrationMasterPage: InitializeComponent completed ===");
            
            IsPresented = false;
            
            System.Diagnostics.Debug.WriteLine($"=== NewUserRegistrationMasterPage: Calling MyDetailPage({id}) ===");
            Console.WriteLine($"=== NewUserRegistrationMasterPage: Calling MyDetailPage({id}) ===");
            
            MyDetailPage(id);
            
            System.Diagnostics.Debug.WriteLine("=== NewUserRegistrationMasterPage: Setting up SelectionChanged ===");
            Console.WriteLine("=== NewUserRegistrationMasterPage: Setting up SelectionChanged ===");
            
            flyoutPage.collectionViewFlyout!.SelectionChanged += OnSelectionChanged;
            
            System.Diagnostics.Debug.WriteLine("=== NewUserRegistrationMasterPage: Constructor completed successfully ===");
            Console.WriteLine("=== NewUserRegistrationMasterPage: Constructor completed successfully ===");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"=== NewUserRegistrationMasterPage CONSTRUCTOR ERROR ===");
            System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
            System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
            System.Diagnostics.Debug.WriteLine($"Inner Exception: {ex.InnerException?.Message}");
            
            Console.WriteLine($"=== NewUserRegistrationMasterPage CONSTRUCTOR ERROR ===");
            Console.WriteLine($"Error: {ex.Message}");
            Console.WriteLine($"Stack trace: {ex.StackTrace}");
            Console.WriteLine($"Inner Exception: {ex.InnerException?.Message}");
            
            throw; // Re-throw to see the error in output
        }
    }
    async void MyDetailPage(int i)
    {
        var service = new UserRegistrationApi();

        switch (i)
        {
            case 1:
                Detail = new NavigationPage(new PersonalDetailsPage());
                break;
            case 2:
                Detail = new NavigationPage(new ContactDetailsPage());
                break;
            case 3:
                Detail = new NavigationPage(new QualficationDetailsPage());
                break;
            case 4:
                Detail = new NavigationPage(new MiscellaneousDetailsPage());
                break;
            case 5:
                Detail = new NavigationPage(new EmploymentDetailsPage());
                break;
            case 6:
                Detail = new NavigationPage(new SubCategoryPage());
                break;
            case 7:
                Detail = new NavigationPage(new PhysicallyHandicappedPage());
                break;
            case 8:
                Detail = new NavigationPage(new ExServiceManPage());
                break;
            case 9:
                Detail = new NavigationPage(new NCOPage());
                break;
            case 10:

                bool m = await DisplayAlert(App.AppName, "Are you sure you want to final submit the registration details?" +
                    "\nOnce submitted no changes can be made.", "Yes", "No");
                if (m)
                {

                    RegNo = Preferences.Get("RegNo", "");

                    int response_finalsubmit = await service.FinalSubmit(RegNo);
                    if (response_finalsubmit == 200)
                    {                        
                        if (Application.Current?.Windows.FirstOrDefault() is Window mainWindow)
                        {
                            MainThread.BeginInvokeOnMainThread(() =>
                            {
                                mainWindow.Page=new NavigationPage (new PostLoginDashboardPage());

                            });
                        }

                    }
                }
                break;
        }
    }

    void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var item = e.CurrentSelection.FirstOrDefault() as FlyoutPageItem;
        if (item != null)
        {
            if (!((IFlyoutPageController)this).ShouldShowSplitMode)
                IsPresented = false;
            MyDetailPage(item.Id);
        }
    }
}
