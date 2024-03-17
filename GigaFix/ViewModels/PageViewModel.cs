using System;

namespace GigaFix.ViewModels;

public abstract class PageViewModel : ViewModelBase
{
    public abstract string Title { get; }
    public virtual string IconName => "";
    public virtual Action? OnNavigate { get; }
}