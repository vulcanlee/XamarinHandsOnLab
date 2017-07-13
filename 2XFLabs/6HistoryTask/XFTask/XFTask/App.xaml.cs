using Prism.Unity;
using XFTask.Views;
using Xamarin.Forms;

namespace XFTask
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void OnInitialized()
        {
            InitializeComponent();

            NavigationService.NavigateAsync("SigninPage");
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<NavigationPage>();
            Container.RegisterTypeForNavigation<MainPage>();
            Container.RegisterTypeForNavigation<SigninPage>();
            Container.RegisterTypeForNavigation<NaviPage>();
            Container.RegisterTypeForNavigation<MDPage>();
            Container.RegisterTypeForNavigation<CodeScannerPage>();
            Container.RegisterTypeForNavigation<TaskEditPage>();
            Container.RegisterTypeForNavigation<TaskHistoryPage>();
            Container.RegisterTypeForNavigation<TaskHistoryDetailPage>();
        }
    }
}
