using GuaranteedRateConsoleApp.DataLayer;
using GuaranteedRateConsoleApp.Models;
using System;
using System.Collections.Generic;

namespace GuaranteedRateConsoleApp
{
    internal class Program
    {
        #region Private Methods

        private static void HandleUserInput()
        {
            List<int> allowedInts = new List<int>() { 1, 2, 3, 4, 5 };
            int num;
            string enteredValue = Console.ReadLine();
            bool test = int.TryParse(enteredValue, out num);
            if (!test || !allowedInts.Contains(num))
            {
                Console.WriteLine("You did not enter a value between 1 and 5. Please enter numbers only.");
                HandleUserInput();
            }
            else
            {
                DataHandler dh = new DataHandler();

                //Console.WriteLine("Hello World!");
                List<CollectionItem> list = dh.GetSortedList(num);
                string message = "";
                switch (num)
                {
                    case 1:
                        message = "1 = sorted by email, descending. Then by last name, ascending.";
                        break;

                    case 2:
                        message = "2 = sorted by birth date, ascending.";
                        break;

                    case 3:
                        message = "3 = sorted by last name, descending.";
                        break;

                    case 4:
                        message = "4 = sorted by Favorite Color, ascending.";
                        break;

                    case 5:
                        message = "5 = sorted by first name, ascending.";
                        break;
                }

                Console.WriteLine("\n******* " + message + " * ********\n");
                foreach (var item in list)
                {
                    Console.WriteLine(item.FirstName + " " + item.LastName + " - Favorite Color: " + item.FavoriteColor + " - dob: " + item.DateOfBirth + " - email: " + item.Email);
                }
                Console.WriteLine("\n********************* Total Records: " + list.Count.ToString() + " ************************\n");
                Console.WriteLine("******* End of " + message + " * ********\n");
                Console.WriteLine("Please enter another value between 1 and 5.\n");
                HandleUserInput();
            }
        }

        private static void Main(string[] args)
        {
            Console.WriteLine(@"Please enter a value between 1 and 5. Please enter numbers only.
                    1 = sorted by email, descending. Then by last name, ascending.
                    2 = sorted by birth date, ascending.
                    3 = sorted by last name, descending.
                    4 = sorted by Favorite Color, ascending.
                    5 = sorted by first name, ascending.
                ");
            HandleUserInput();
        }

        #endregion Private Methods
    }
}