using CommunityToolkit.Mvvm.ComponentModel;
using GigaFix.Services;
using System.Collections;

namespace GigaFix.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        [ObservableProperty]
        public PageViewModel currentViewModel;
        [ObservableProperty]
        public bool canNavigateBack;

        private Stack viewsStack = new Stack();
        public MainWindowViewModel(NavigationService ns, LoginViewModel loginVm)
        {
            CurrentViewModel = loginVm;
            viewsStack.Push(loginVm);
            ns.Navigate = (e) =>
            {
                CurrentViewModel = e;
                viewsStack.Push(e);
                CanNavigateBack = true;
            };
        }

        public void NavigateBack()
        {
            viewsStack.Pop();
            var vm = viewsStack.Peek() as PageViewModel;
            CurrentViewModel = vm;
            if (viewsStack.Count == 1 || viewsStack.Count == 0)
                CanNavigateBack = false;
        }
    }
}
