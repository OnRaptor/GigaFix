using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaFix.ViewModels
{
    public abstract class PageViewModel : ViewModelBase
    {
        public abstract string Title { get; }
        public virtual string IconName => "";
        public virtual Action? OnNavigate { get; }
    }
}
