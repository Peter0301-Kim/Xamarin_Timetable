using RoomNaviMobile.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RoomNaviMobile.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SubjectView : ContentPage
    {
        public SubjectView(SubjectViewModel viewmodel)
        {
            InitializeComponent();

            viewmodel.Navigation = Navigation;
            BindingContext = viewmodel;
        }
    }
}