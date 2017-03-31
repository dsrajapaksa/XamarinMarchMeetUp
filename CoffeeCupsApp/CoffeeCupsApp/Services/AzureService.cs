using CoffeeCupsApp.Model;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLitePCL;
using System.IO;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;

namespace CoffeeCupsApp.Services
{
    public class AzureService
    {
        public MobileServiceClient MobileService { get; set; } = null;
        IMobileServiceSyncTable<CupOfCoffee> coffeeTable;

        bool isInitialized;
        //To Initialize the Azure Mobile Service 
        public async Task Initialize()
        {
            //Create the Client 
            MobileService = new MobileServiceClient(@"http://easytable2.azurewebsites.net/");


            //InitialzeDatabase for path
            var path = "syncstore.db";
            path = Path.Combine(MobileServiceClient.DefaultDatabasePath, path);

            //setup our local sqlite store and intialize our table
            var store = new MobileServiceSQLiteStore(path);

            //Define table
            store.DefineTable<CupOfCoffee>();

            //Initialize SyncContext
            await MobileService.SyncContext.InitializeAsync(store);

            //Get our sync table that will call out to azure
            coffeeTable = MobileService.GetSyncTable<CupOfCoffee>();

            isInitialized = true;

        }

        //Sync Coffees with Online 
        public async Task SyncCoffee()
        {
                await coffeeTable.PullAsync("allCoffee", coffeeTable.CreateQuery());

                await MobileService.SyncContext.PushAsync();
        }

        //Get Coffees method 
        public async Task<IEnumerable<CupOfCoffee>> GetCoffees()
        {
            //Initialize & Sync
            await Initialize();
            await SyncCoffee();

            return await coffeeTable.OrderBy(c => c.DateUtc).ToEnumerableAsync(); ;
        }

        //Add a new Coffees 
        public async Task<CupOfCoffee> AddCoffee()
        {
            await Initialize();

            var coffee = new CupOfCoffee
            {
                DateUtc = DateTime.UtcNow,
                //MadeAtHome = atHome,
                //OS = Device.OS.ToString()
            };

            await coffeeTable.InsertAsync(coffee);

            await SyncCoffee();
            //return coffee
            return coffee;
        }

    }
}
