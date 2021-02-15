using GuaranteedRateConsoleApp.DataLayer;
using GuaranteedRateConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using static GuaranteedRateConsoleApp.GuarenteedRatesApi;

namespace GuaranteedRateConsoleApp
{
    internal class Program
    {
        #region Public Methods

        public static void HandleUserInputAsync()
        {
            List<int> allowedInts = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
            int num;
            string enteredValue = Console.ReadLine();
            bool test = int.TryParse(enteredValue, out num);
            if (!test || !allowedInts.Contains(num))
            {
                Console.WriteLine("You did not enter a value between 0 and 8. Please enter numbers only.");
                HandleUserInputAsync();
            }
            else
            {
                DataHandler dh = new DataHandler();

                if (num == 0)
                {
                    Dictionary<int, int> returnCounts = new Dictionary<int, int>();
                    if (dh.GenerateNewCollectionItems(out returnCounts))
                    {
                        int recordsBefore = returnCounts.Keys.ElementAt(0);
                        int recordsAfter = returnCounts.Values.ElementAt(0);

                        Console.WriteLine("You successfully added 6 more records to the collection. There were initially " + recordsBefore.ToString() + " records, but there are " + recordsAfter.ToString() + " after the adition was made to the collection.");
                        HandleUserInputAsync();
                    }
                }
                else if (num == 6)
                {
                    PostDelimitedStringToApi("Comma");
                }
                else if (num == 7)
                {
                    PostDelimitedStringToApi("Bar");
                }
                else if (num == 8)
                {
                    PostDelimitedStringToApi("Tab");
                }
                else
                {
                    List<CollectionItem> list = dh.GetSortedList(num);
                    string message = "";
                    switch (num)
                    {
                        case 1:
                            message = "1: sorted by email, descending. Then by last name, ascending.";
                            break;

                        case 2:
                            message = "2: sorted by birth date, ascending.";
                            break;

                        case 3:
                            message = "3: sorted by last name, descending.";
                            break;

                        case 4:
                            message = "4: sorted by Favorite Color, ascending.";
                            break;

                        case 5:
                            message = "5: sorted by first name, ascending.";
                            break;
                    }

                    Console.WriteLine("\n******* " + message + " * ********\n");
                    foreach (var item in list)
                    {
                        Console.WriteLine(item.FirstName + " " + item.LastName + " - Favorite Color: " + item.FavoriteColor + " - dob: " + item.DateOfBirth + " - email: " + item.Email);
                    }
                    Console.WriteLine("\n********************* Total Records: " + list.Count.ToString() + " ************************\n");
                    Console.WriteLine("******* End of " + message + " * ********\n");
                    GetMessage();
                    HandleUserInputAsync();
                }
            }
        }

        #endregion Public Methods

        #region Private Methods

        private static void GetMessage()
        {
            Console.WriteLine(@"Enter a value between 0 and 8. Enter numbers only.
0: adds 6 more records to the existing collection.
1: shows all records sorted by email, descending. Then by last name, ascending.
2: shows all records sorted by birth date, ascending.
3: shows all records sorted by last name, descending.
4: shows all records sorted by Favorite Color, ascending.
5: shows all records sorted by first name, ascending.
6: posts a comma (,) delimited string to the web service and see the results.
7: posts a bar (|) delimited string to the web service and see the results.
8: posts a tab delimited string to the web service and see the results.
                ");
        }

        private static void Main(string[] args)
        {
            GuarenteedRatesApi.StartService();
            GetMessage();
            HandleUserInputAsync();
        }

        private static void PostDelimitedStringToApi(string delimiterType)
        {
            ChannelFactory<IGuranteedRatesNormalService> factory = new ChannelFactory<IGuranteedRatesNormalService>(new BasicHttpBinding(), new EndpointAddress(GRNormalServiceBaseAddress));
            IGuranteedRatesNormalService proxy = factory.CreateChannel();

            string delimitedString = "";
            switch (delimiterType)
            {
                case "Comma":
                    delimitedString = "Watson,Chili,ChiliWatson@Mail.com,Alabaster,10/21/1987";
                    break;

                case "Bar":
                    delimitedString = "Castro|Billy|BillyCastro@Mail.com|Indigo|11/2/1961";
                    break;

                case "Tab":
                    delimitedString = "Chandelton	Sally	SallyCaldelton@Mail.com	White	7/14/1982";
                    break;
            }
            Console.WriteLine("\nPosting this " + delimiterType.ToLower() + " delimited string to '" + GuarenteedRatesApi.GRNormalServiceBaseAddress + "/records': " + delimitedString);
            Console.WriteLine("\nReturning this Json data:");
            var proxyCall = proxy.CallAcceptPost(delimitedString);
            Console.WriteLine(proxyCall);
            HandleUserInputAsync();
        }

        #endregion Private Methods
    }
}