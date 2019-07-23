using RoomNaviMobile.Services.Dialog;
using RoomNaviMobile.Services.Settings;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace RoomNaviMobile.ViewModel.Base
{
    public abstract class ViewModelBase : ExtendedBindableObject
    {

        protected readonly IDialogService DialogService;
        public INavigation Navigation { get; set; }


        private bool _isBusy;

        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }

            set
            {
                _isBusy = value;
                RaisePropertyChanged(() => IsBusy);
            }
        }

        public ViewModelBase()
        {
            var settingsService = DependencyInjector.Resolve<ISettingsService>();
            DialogService = DependencyInjector.Resolve<IDialogService>();

        }

        public virtual Task InitializeAsync(object navigationData)
        {
            return Task.FromResult(false);
        }
    }
}