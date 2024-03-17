using GigaFix.ViewModels;

namespace GigaFix.Services;

public class NavigationService
{
    public delegate void SetTopPanelVisibilityDelegate(bool visible);

    public SetTopPanelVisibilityDelegate SetTopPanelVisibility;

    public delegate void NavDelegate(PageViewModel vm);

    public NavDelegate Navigate;
}