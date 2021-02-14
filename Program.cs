using GuaranteedRateConsoleApp.DataLayer;
using GuaranteedRateConsoleApp.Models;
using System;
using System.Collections.Generic;

namespace GuaranteedRateConsoleApp
{
    internal class Program
    {
        #region Private Methods

        private static void Main(string[] args)
        {
            // Test if input arguments were supplied.
            if (args.Length == 0)
            {
                throw new Exception("You did not enter a value between 1 and 3");
            }

            // Try to convert the input arguments to numbers. This will throw
            // an exception if the argument is not a number.
            // num = int.Parse(args[0]);
            List<int> allowedInts = new List<int>() { 1, 2, 3 };
            int num;
            bool test = int.TryParse(args[0], out num);
            if (!test && !allowedInts.Contains(num))
            {
                throw new Exception("You did not enter a value between 1 and 3");
            }
            DataHandler dh = new DataHandler();

            //Console.WriteLine("Hello World!");
            List<CollectionItem> list = dh.GetSortedList(num);
            string message = "";
            switch (num)
            {
                case 1:
                    message = "sorted by email (descending). Then by last name ascending";
                    break;

                case 2:
                    message = "sorted by birth date, ascending.";
                    break;

                case 3:
                    message = "sorted by last name, descending.";
                    break;
            }

            Console.WriteLine("\n******* " + message + " * ********\n");
            foreach (var item in list)
            {
                Console.WriteLine(item.FirstName + " " + item.LastName + " - Favorite Color: " + item.FavoriteColor + " - dob: " + item.DateOfBirth + " - email: " + item.Email);
            }
            Console.WriteLine("\n********************* Total Records: " + list.Count.ToString() + " ************************\n");
            Console.ReadLine();
        }

        #endregion Private Methods
    }
}