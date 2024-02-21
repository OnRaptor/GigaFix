using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GigaFix.Services;
using GigaFix.ViewModels.MainViews;

namespace GigaFix.ViewModels
{
    public class MainViewModel : PageViewModel
    {
        public override string Title => "Главный экран";

        public List<PageViewModel> Views { get; set; } = new ();

        public MainViewModel(
            NavigationService navigationService,
            OrdersListViewModel ordersListViewModel,
            AddOrderViewModel addOrderViewModel,
            SearchViewModel searchViewModel
            )
        {
            navigationService.SetTopPanelVisibility(false);
            Views.Add(ordersListViewModel);
            Views.Add(searchViewModel);
            Views.Add(addOrderViewModel);
        }
    }
}
