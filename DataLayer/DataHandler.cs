using ChoETL;
using GuaranteedRateConsoleApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace GuaranteedRateConsoleApp.DataLayer
{
    public class DataHandler : IDataHandler
    {
        #region Public Constructors

        /// <summary>
        /// Handles data get, post, delete
        /// </summary>
        public DataHandler()
        {
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        /// Adds Collection Items (post)
        /// </summary>
        /// <param name="jsonCollectionItems">A json collection of CollectionItems</param>
        /// <returns>bool</returns>
        public bool AddCollectionItems(string jsonCollectionItems)
        {
            bool itemsAdded = false;

            // get the existing file data
            List<CollectionItem> existingCollectionItems = GetCollectionItemsFromFile("All");

            // convert the jsonCollectionItems string to a List<CollectionItem>
            List<CollectionItem> deserializedData = JsonConvert.DeserializeObject<List<CollectionItem>>(jsonCollectionItems);

            // add the newly sent deserializedData to the existingCollectionItems
            existingCollectionItems.AddRange(deserializedData);

            // convert it all back to json data
            var serializedCollectionItems = JsonConvert.SerializeObject(existingCollectionItems);
            string filePath = GetDataFilePath("All");

            // convert the json data to a comma delimited value
            string csv = Json2CSV(serializedCollectionItems);

            // write it all back to file
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.Write(csv);
                    writer.Close();
                }
                itemsAdded = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("There was an error writing records to the file path " + filePath + ". Exception: " + ex.ToString());
            }
            return itemsAdded;
        }

        /// <summary>
        /// Deletes a Collection Item
        /// </summary>
        /// <param name="deleteByItemName">The email value of the CollectionItem to delete</param>
        /// <returns></returns>
        public bool DeleteCollectionItem(string searchForEmailAddress)
        {
            bool itemsDeleted = false;
            List<CollectionItem> existingCollectionItems = GetAllCollectionItems();
            List<CollectionItem> collectionItemsToRemove = existingCollectionItems
                        .Where(m => m.Email.ToLower() == searchForEmailAddress.ToLower())
                        .ToList();
            foreach (var item in collectionItemsToRemove)
            {
                existingCollectionItems.Remove(item);
            }

            return itemsDeleted;
        }

        /// <summary>
        /// Gets All Collection Items
        /// </summary>
        /// <returns>List<CollectionItem></returns>
        public List<CollectionItem> GetAllCollectionItems()
        {
            List<CollectionItem> collectionItems = new List<CollectionItem>();
            collectionItems.AddRange(GetCollectionItemsFromFile("All"));
            return collectionItems;
        }

        /// <summary>
        /// Gets Collection Items by search string
        /// </summary>
        /// <param name="searchFor">The value to search for</param>
        /// <param name="propertyName">The property's name in the CollectionItems class to search</param>
        /// <returns>List<CollectionItem></returns>
        public List<CollectionItem> GetCollectionItems(string searchFor, string propertyName)
        {
            List<CollectionItem> returnCollectionItems = new List<CollectionItem>();
            List<CollectionItem> collectionItems = GetAllCollectionItems();
            searchFor = searchFor.ToLower();
            switch (propertyName)
            {
                case "LastName":
                    returnCollectionItems = collectionItems
                        .Where(m => m.LastName.ToLower() == searchFor)
                        .ToList();
                    break;

                case "FirstName":
                    returnCollectionItems = collectionItems
                        .Where(m => m.FirstName.ToLower() == searchFor)
                        .ToList();
                    break;

                case "Email":
                    returnCollectionItems = collectionItems
                        .Where(m => m.Email.ToLower() == searchFor)
                        .ToList();
                    break;

                case "FavoriteColor":
                    returnCollectionItems = collectionItems
                        .Where(m => m.FavoriteColor.ToLower() == searchFor)
                        .ToList();
                    break;

                case "DateOfBirth":
                    DateTime dt;
                    bool isValidDate = DateTime.TryParse(searchFor, out dt);
                    if (isValidDate)
                    {
                        returnCollectionItems = collectionItems
             .Where(m => DateTime.Parse(m.DateOfBirth) == dt)
             .ToList();
                    }
                    break;

                default:
                    returnCollectionItems = new List<CollectionItem>();
                    break;
            }

            return returnCollectionItems;
        }

        /// <summary>
        /// Gets a list of CollectionItems From File
        /// </summary>
        /// <param name="delimiterType">The delimiter type of the file allowed values: Tab, Comma, Bar, or All</param>
        /// <returns>List of collection items</returns>
        public List<CollectionItem> GetCollectionItemsFromFile(string delimiterType)
        {
            CheckDelimiterType(delimiterType);

            Dictionary<string, string> dict = GetPropertyValue(delimiterType);
            string delimiter = dict.Values.ElementAt(0); // don't use this now that I use the AutoDetectDelimiter directive
            string fileType = dict.Keys.ElementAt(0);

            string filePath = GetDataFilePath(fileType);
            List<CollectionItem> collectionItems = new List<CollectionItem>();
            if (delimiterType.ToLower() == "tab")
            {
                collectionItems = this.GetCollectionListFromTabDelimitedFile();
            }
            else
            {
                var choCsvReader = new ChoCSVReader<CollectionItem>(filePath)
                    .WithFirstLineHeader()
                    .WithDelimiter(delimiter);
                int recordCounter = 1;
                foreach (var e in choCsvReader)
                {
                    // putting this into a try/catch so if invalid date is entered it won't stop the entire process.
                    try
                    {
                        collectionItems.Add(e);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("There was an error writing record #" + recordCounter.ToString() + " for the file path " + filePath + ". Exception: " + ex.ToString());
                    }
                }
            }
            return collectionItems;
        }

        /// <summary>
        /// Gets the Data File Path
        /// </summary>
        /// <param name="delimiterType">The delimiter type of the file allowed values: Tab, Comma, Bar, or All</param>
        /// <returns>The file path to the appropriate file</returns>
        public string GetDataFilePath(string delimiterType)
        {
            CheckDelimiterType(delimiterType);

            string appSetting = delimiterType + "_DelimitedFilePath";
            string relativePath = ConfigurationManager.AppSettings[appSetting];
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            var fullPath = Path.Combine(path, relativePath);
            return fullPath;
        }

        /// <summary>
        /// Gets a Sorted CollectionItem List
        /// </summary>
        /// <param name="outputItem">Either 1, 2 or 3</param>
        /// <returns></returns>
        public List<CollectionItem> GetSortedList(int outputItem)
        {
            if (outputItem > 3)
            {
                throw new ArgumentException("outputItem must be a number less than 3", nameof(outputItem));
            }
            List<CollectionItem> collectionItems = GetAllCollectionItems();

            switch (outputItem)
            {
                case 1:
                    return collectionItems
                        .OrderByDescending(o=>o.Email)
                        .ThenBy(t=>t.LastName)
                        .ToList();

                case 2:
                    return collectionItems
                        .OrderBy(o => DateTime.Parse(o.DateOfBirth))
                        .ToList();
                case 3:
                    return collectionItems
                        .OrderByDescending(o => o.LastName)
                        .ToList();
            }
            return collectionItems;
        }

        /// <summary>
        /// Syncs All Delimited Items Files into the CollectionItemsCommaDelimitedAll.csv file
        /// </summary>
        /// <returns>List<CollectionItem></returns>
        public List<CollectionItem> SyncAllItemsFile()
        {
            List<CollectionItem> existingCollectionItems = GetAllCollectionItems();
            List<CollectionItem> collectionItems = new List<CollectionItem>();
            collectionItems.AddRange(GetCollectionItemsFromFile("Comma"));
            collectionItems.AddRange(GetCollectionItemsFromFile("Bar"));
            collectionItems.AddRange(GetCollectionItemsFromFile("Tab"));
            List<string> existingEmails = existingCollectionItems
                        .Select(s => s.Email.ToLower())
                        .Distinct()
                        .ToList();
            foreach (var item in collectionItems)
            {
                if (!existingEmails.Contains(item.Email.ToLower()))
                {
                    existingCollectionItems.Add(item);
                }
            }
            // convert it all back to json data
            var serializedCollectionItems = JsonConvert.SerializeObject(existingCollectionItems);
            string filePath = GetDataFilePath("All");

            // convert the json data to a comma delimited value
            string csv = Json2CSV(serializedCollectionItems);

            // write it all back to file
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.Write(csv);
                    writer.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("There was an error writing records to the file path " + filePath + ". Exception: " + ex.ToString());
            }
            return collectionItems;
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Checks to make sure the user passed a usable Delimiter Type
        /// </summary>
        /// <param name="delimiterType">The delimiter type of the file allowed values: Tab, Comma, Bar, or All</param>
        private void CheckDelimiterType(string delimiterType)
        {
            string[] allowedDelimiterTypes = { "Comma", "Tab", "Bar", "All" };
            if (!allowedDelimiterTypes.Contains(delimiterType))
            {
                throw new ArgumentException("delimiterType must be either Tab, Comma, Bar or All", nameof(delimiterType));
            }
        }

        /// <summary>
        /// Gets a Collection List From a Tab Delimited File
        /// </summary>
        /// <returns>List<CollectionItem></returns>
        private List<CollectionItem> GetCollectionListFromTabDelimitedFile()
        {
            string filePath = GetDataFilePath("Tab");
            string fileContent = "";
            if (File.Exists(filePath))
            {
                using (FileStream stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        while (!reader.EndOfStream)
                        {
                            fileContent += reader.ReadToEnd();
                        }
                    }
                }
            }

            StringBuilder json = new StringBuilder();

            using (var r = ChoTSVReader.LoadText(fileContent)
                .WithFirstLineHeader()
                )
            {
                using (var w = new ChoJSONWriter(json))
                    w.Write(r);
            }
            return JsonConvert.DeserializeObject<List<CollectionItem>>(json.ToString());
        }

        /// <summary>
        /// Gets the delimited name and the delimiter for that name
        /// </summary>
        /// <param name="delimiterType">The delimiter type of the file allowed values: Tab, Comma, Bar, or All</param>
        /// <returns>Dictionary<string, string></returns>
        private Dictionary<string, string> GetPropertyValue(string delimiterType)
        {
            CheckDelimiterType(delimiterType);

            Dictionary<string, string> dic = new Dictionary<string, string>();
            switch (delimiterType)
            {
                case "Comma":
                    dic.Add("Comma", ",");
                    break;

                case "Bar":
                    dic.Add("Bar", "|");
                    break;

                case "All":
                    dic.Add("All", ",");
                    break;

                default:
                    dic.Add("Tab", "");
                    break;
            }
            return dic;
        }

        /// <summary>
        /// Converts Json to CSV
        /// </summary>
        /// <param name="json">The json data to turn into a comma delimited string</param>
        /// <returns>Comma Delimited string</returns>
        private string Json2CSV(string json)
        {
            StringBuilder csv = new StringBuilder();
            using (var r = ChoJSONReader.LoadText(json)
                )
            {
                using (var w = new ChoCSVWriter(csv)
                    .WithFirstLineHeader()
                    .NestedColumnSeparator('/')
                    )
                    w.Write(r);
            }

            return csv.ToString();
        }

        #endregion Private Methods
    }
}