using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradientPicker.ViewModels
{
    public class MainViewModel: ViewModelBase
    {

        public Page CurrentPage
        {
            get { return currentPage; }
            set
            {
                if (value != null && value != currentPage)
                {
                    currentPage = value;
                    RaisePropertyChanged(() => CurrentPage);
                }
            }
        }

        private Page currentPage;
    }

   
}
