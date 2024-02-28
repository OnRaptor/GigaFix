using GigaFix.ViewModels.Dialogs;
using SukiUI.Controls;

namespace GigaFix.ViewModels.MainViews;

public class OrdersListViewModel : PageViewModel
{
    public override string Title => "Список заявок";
    public override string IconName => "mdi-list-box";
    private readonly EditOrderViewModel _editOrderViewModel;

    public OrdersListViewModel(
        EditOrderViewModel editOrderViewModel
        )
    {
        _editOrderViewModel = editOrderViewModel;
    }
    
    public void ShowEditDialog()
    {
        SukiHost.ShowDialog(_editOrderViewModel, allowBackgroundClose:true);
    }
}