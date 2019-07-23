using System.Threading.Tasks;

namespace RoomNaviMobile.Services.Dialog
{
    public interface IDialogService
    {
        Task ShowAlertAsync(string message, string title, string buttonLabel);
    }
}
