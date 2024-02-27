using GigaFix.ViewModels.Dialogs;
using SukiUI.Controls;

namespace GigaFix.ViewModels.MainViews;

public class AddOrderViewModel : PageViewModel
{
    public override string Title => "Добавить заявку";
    public override string IconName => "mdi-plus";

    private readonly AttachExecutorViewModel _attachExecutorViewModel;
    public AddOrderViewModel(
        AttachExecutorViewModel _attachExecutorViewModel
        )
    {
        this._attachExecutorViewModel = _attachExecutorViewModel;
    }

    public void showExecutorDialog()
    {
        SukiHost.ShowDialog(_attachExecutorViewModel, allowBackgroundClose:true);
    }
}