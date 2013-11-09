using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseStarterProject.Services
{
    public interface INavigationService
    {
        void Navigate(Views view);
        void Navigate(Views view, object parameter);
        void GoBack();
    }
}
