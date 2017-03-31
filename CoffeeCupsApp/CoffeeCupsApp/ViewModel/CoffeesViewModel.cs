using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmHelpers;
using System.Windows.Input;
using Xamarin.Forms;
using CoffeeCupsApp.Model;
using CoffeeCupsApp.Services;
using System.Diagnostics;

namespace CoffeeCupsApp.ViewModel
{
    public class CoffeesViewModel : BaseViewModel
    {
        AzureService azureService;
        public CoffeesViewModel()
        {
            azureService = new AzureService();
        }

       public ObservableRangeCollection<CupOfCoffee> Coffees{get;} = new ObservableRangeCollection<CupOfCoffee>();


    //At Home Property 
    bool atHome;
        public bool AtHome
        {
            get
            {
                return atHome;
            }
            set
            {
                SetProperty(ref atHome, value);
            }
        }

        //=========Command to Load all the Coffees Data========= 
        ICommand loadCoffeesCommand;
        public ICommand LoadCoffeesCommand => 
            loadCoffeesCommand?? (loadCoffeesCommand= new Command(async () => await ExecuteLoadCoffeesCommandAsync()));

        async Task ExecuteLoadCoffeesCommandAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                //Get all here !

                var coffees = await azureService.GetCoffees();
                Coffees.ReplaceRange(coffees);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
               
            }
            finally
            {
                IsBusy = false;
            }
        }

        //=============Command to add a Coffees==============
        ICommand addCoffeeCommand;
        public ICommand AddCoffeeCommand =>
            addCoffeeCommand ?? (addCoffeeCommand = new Command(async () => await ExecuteAddCoffeesCommandAsync()));

        async Task ExecuteAddCoffeesCommandAsync()
        {

            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                //Add a Coffee

                var coffee = await azureService.AddCoffee();
                Coffees.Add(coffee);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
