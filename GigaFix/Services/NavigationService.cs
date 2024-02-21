using GigaFix.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaFix.Services
{
    public class NavigationService
    {
        public delegate void NavDelegate(PageViewModel vm);
        public NavDelegate Navigate;
    }
}
