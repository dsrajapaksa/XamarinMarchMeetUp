using CoffeeCupsApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoffeeCupsApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CoffeesPage : ContentPage
    {
        public CoffeesPage()
        {
            InitializeComponent();
            BindingContext = new CoffeesViewModel();
        }
    }
}
