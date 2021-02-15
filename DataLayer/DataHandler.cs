using ChoETL;
using GuaranteedRateConsoleApp.Models;
using GuaranteedRateTests.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuaranteedRateConsoleApp.DataLayer
{
    public partial class DataHandler : IDataHandler
    {
        #region Private Fields

        private Random random = new Random();

        private List<string> ColorList = new List<string>
        {
            "Red",
            "Blue",
            "Green",
            "White",
            "Yellow",
            "Purple",
            "Absolute Zero",
            "Acid green",
            "Aero",
            "Aero blue",
            "African violet",
            "Air superiority blue",
            "Alabaster",
            "Alice blue",
            "Alloy orange",
            "Almond",
            "Amaranth",
            "Amaranth (M&P)",
            "Amaranth pink",
            "Amaranth purple",
            "Amaranth red",
            "Amazon",
            "Amber",
            "Amber (SAE/ECE)",
            "Amethyst",
            "Android green",
            "Antique brass",
            "Antique bronze",
            "Antique fuchsia",
            "Antique ruby",
            "Antique white",
            "Ao (English)",
            "Apple green",
            "Apricot",
            "Aqua",
            "Aquamarine",
            "Arctic lime",
            "Army green",
            "Artichoke",
            "Arylide yellow",
            "Ash gray",
            "Asparagus",
            "Atomic tangerine",
            "Auburn",
            "Aureolin",
            "Avocado",
            "Azure",
            "Azure (X11/web color)",
            "B'dazzled blue",
            "Baby blue",
            "Baby blue eyes",
            "Baby pink",
            "Baby powder",
            "Baker-Miller pink",
            "Banana Mania",
            "Barbie Pink",
            "Barn red",
            "Battleship grey",
            "Beau blue",
            "Beaver",
            "Beige",
            "Big dip o’ruby",
            "Bisque",
            "Bistre",
            "Bistre brown",
            "Bitter lemon",
            "Bitter lime",
            "Bittersweet",
            "Bittersweet shimmer",
            "Black",
            "Black bean",
            "Black chocolate",
            "Black coffee",
            "Black coral",
            "Black olive",
            "Black Shadows",
            "Blanched almond",
            "Blast-off bronze",
            "Bleu de France",
            "Blizzard blue",
            "Blond",
            "Blood red",
            "Blue",
            "Blue (Crayola)",
            "Blue (Munsell)",
            "Blue (NCS)",
            "Blue (Pantone)",
            "Blue (pigment)",
            "Blue (RYB)",
            "Blue bell",
            "Blue jeans",
            "Blue sapphire",
            "Blue yonder",
            "Blue-gray",
            "Blue-green",
            "Blue-green (color wheel)",
            "Blue-violet",
            "Blue-violet (color wheel)",
            "Blue-violet (Crayola)",
            "Bluetiful",
            "Blush",
            "Bole",
            "Bone",
            "Bottle green",
            "Brandy",
            "Brick red",
            "Bright green",
            "Bright lilac",
            "Bright maroon",
            "Bright navy blue",
            "Bright yellow (Crayola)",
            "Brilliant rose",
            "Brink pink",
            "British racing green",
            "Bronze",
            "Brown",
            "Brown sugar",
            "Brunswick green",
            "Bud green",
            "Buff",
            "Burgundy",
            "Burlywood",
            "Burnished brown",
            "Burnt orange",
            "Burnt sienna",
            "Burnt umber",
            "Byzantine",
            "Byzantium",
            "Cadet",
            "Cadet blue",
            "Cadet blue (Crayola)",
            "Cadet grey",
            "Cadmium green",
            "Cadmium orange",
            "Cadmium red",
            "Cadmium yellow",
            "Café au lait",
            "Café noir",
            "Cambridge blue",
            "Camel",
            "Cameo pink",
            "Canary",
            "Canary yellow",
            "Candy apple red",
            "Candy pink",
            "Capri",
            "Caput mortuum",
            "Cardinal",
            "Caribbean green",
            "Carmine",
            "Carmine (M&P)",
            "Carnation pink",
            "Carnelian",
            "Carolina blue",
            "Carrot orange",
            "Castleton green",
            "Catawba",
            "Cedar Chest",
            "Celadon",
            "Celadon blue",
            "Celadon green",
            "Celeste",
            "Celtic blue",
            "Cerise",
            "Cerulean",
            "Cerulean (Crayola)",
            "Cerulean blue",
            "Cerulean frost",
            "CG blue",
            "CG red",
            "Champagne",
            "Champagne pink",
            "Charcoal",
            "Charleston green",
            "Charm pink",
            "Chartreuse (traditional)",
            "Chartreuse (web)",
            "Cherry blossom pink",
            "Chestnut",
            "Chili red",
            "China pink",
            "China rose",
            "Chinese red",
            "Chinese violet",
            "Chinese yellow",
            "Chocolate (traditional)",
            "Chocolate (web)",
            "Chocolate Cosmos",
            "Chrome yellow",
            "Cinereous",
            "Cinnabar",
            "Cinnamon Satin",
            "Citrine",
            "Citron",
            "Claret",
            "Cobalt blue",
            "Cocoa brown",
            "Coffee",
            "Columbia Blue",
            "Congo pink",
            "Cool grey",
            "Copper",
            "Copper (Crayola)",
            "Copper penny",
            "Copper red",
            "Copper rose",
            "Coquelicot",
            "Coral",
            "Coral pink",
            "Cordovan",
            "Corn",
            "Cornell red",
            "Cornflower blue",
            "Cornsilk",
            "Cosmic cobalt",
            "Cosmic latte",
            "Cotton candy",
            "Coyote brown",
            "Cream",
            "Crimson",
            "Crimson (UA)",
            "Crystal",
            "Cultured",
            "Cyan",
            "Cyan (process)",
            "Cyber grape",
            "Cyber yellow",
            "Cyclamen",
            "Dark blue-gray",
            "Dark brown",
            "Dark byzantium",
            "Dark cornflower blue",
            "Dark cyan",
            "Dark electric blue",
            "Dark goldenrod",
            "Dark green",
            "Dark green (X11)",
            "Dark jungle green",
            "Dark khaki",
            "Dark lava",
            "Dark liver",
            "Dark liver (horses)",
            "Dark magenta",
            "Dark moss green",
            "Dark olive green",
            "Dark orange",
            "Dark orchid",
            "Dark pastel green",
            "Dark purple",
            "Dark red",
            "Dark salmon",
            "Dark sea green",
            "Dark sienna",
            "Dark sky blue",
            "Dark slate blue",
            "Dark slate gray",
            "Dark spring green",
            "Dark turquoise",
            "Dark violet",
            "Dartmouth green",
            "Davy's grey",
            "Deep cerise",
            "Deep champagne",
            "Deep chestnut",
            "Deep jungle green",
            "Deep pink",
            "Deep saffron",
            "Deep sky blue",
            "Deep Space Sparkle",
            "Deep taupe",
            "Denim",
            "Denim blue",
            "Desert",
            "Desert sand",
            "Dim gray",
            "Dodger blue",
            "Dogwood rose",
            "Drab",
            "Duke blue",
            "Dutch white",
            "Earth yellow",
            "Ebony",
            "Ecru",
            "Eerie black",
            "Eggplant",
            "Eggshell",
            "Egyptian blue",
            "Eigengrau",
            "Electric blue",
            "Electric green",
            "Electric indigo",
            "Electric lime",
            "Electric purple",
            "Electric violet",
            "Emerald",
            "Eminence",
            "English green",
            "English lavender",
            "English red",
            "English vermillion",
            "English violet",
            "Erin",
            "Eton blue",
            "Fallow",
            "Falu red",
            "Fandango",
            "Fandango pink",
            "Fashion fuchsia",
            "Fawn",
            "Feldgrau",
            "Fern green",
            "Field drab",
            "Fiery rose",
            "Fire engine red",
            "Fire opal",
            "Firebrick",
            "Flame",
            "Flax",
            "Flirt",
            "Floral white",
            "Fluorescent blue",
            "Forest green (Crayola)",
            "Forest green (traditional)",
            "Forest green (web)",
            "French beige",
            "French bistre",
            "French blue",
            "French fuchsia",
            "French lilac",
            "French lime",
            "French mauve",
            "French pink",
            "French raspberry",
            "French rose",
            "French sky blue",
            "French violet",
            "Frostbite",
            "Fuchsia",
            "Fuchsia (Crayola)",
            "Fuchsia purple",
            "Fuchsia rose",
            "Fulvous",
            "Fuzzy Wuzzy"
        };

        private List<string> FirstNameList = new List<string>
        {
            "Sophia",
            "Olivia",
            "Riley",
            "Emma",
            "Ava",
            "Isabella",
            "Aria",
            "Aaliyah",
            "Amelia",
            "Mia",
            "Layla",
            "Zoe",
            "Camilla",
            "Charlotte",
            "Eliana",
            "Mila",
            "Everly",
            "Luna",
            "Avery",
            "Evelyn",
            "Harper",
            "Lily",
            "Ella",
            "Gianna",
            "Chloe",
            "Adalyn",
            "Charlie",
            "Isla",
            "Ellie",
            "Leah",
            "Nora",
            "Scarlett",
            "Maya",
            "Abigail",
            "Madison",
            "Aubrey",
            "Emily",
            "Kinsley",
            "Elena",
            "Paisley",
            "Madelyn",
            "Aurora",
            "Peyton",
            "Nova",
            "Emilia",
            "Hannah",
            "Sarah",
            "Ariana",
            "Penelope",
            "Lila",
            "Liam",
            "Noah",
            "Jackson",
            "Aiden",
            "Elijah",
            "Grayson",
            "Lucas",
            "Oliver",
            "Caden",
            "Mateo",
            "Muhammad",
            "Mason",
            "Carter",
            "Jayden",
            "Ethan",
            "Sebastian",
            "James",
            "Michael",
            "Benjamin",
            "Logan",
            "Leo",
            "Luca",
            "Alexander",
            "Levi",
            "Daniel",
            "Josiah",
            "Henry",
            "Jayce",
            "Julian",
            "Jack",
            "Ryan",
            "Jacob",
            "Asher",
            "Wyatt",
            "William",
            "Owen",
            "Gabriel",
            "Miles",
            "Lincoln",
            "Ezra",
            "Isaiah",
            "Luke",
            "Cameron",
            "Caleb",
            "Isaac",
            "Carson",
            "Samuel",
            "Colton",
            "Maverick",
            "Matthew"
        };

        private Random gen = new Random();

        private List<string> LastNameList = new List<string>
        {
            "Smith",
            "Johnson",
            "Williams",
            "Jones",
            "Brown",
            "Davis",
            "Miller",
            "Wilson",
            "Moore",
            "Taylor",
            "Anderson",
            "Thomas",
            "Jackson",
            "White",
            "Harris",
            "Martin",
            "Thompson",
            "Garcia",
            "Martinez",
            "Robinson",
            "Clark",
            "Rodriguez",
            "Lewis",
            "Lee",
            "Walker",
            "Hall",
            "Allen",
            "Young",
            "Hernandez",
            "King",
            "Wright",
            "Lopez",
            "Hill",
            "Scott",
            "Green",
            "Adams",
            "Baker",
            "Gonzalez",
            "Nelson",
            "Carter",
            "Mitchell",
            "Perez",
            "Roberts",
            "Turner",
            "Phillips",
            "Campbell",
            "Parker",
            "Evans",
            "Edwards",
            "Collins",
            "Stewart",
            "Sanchez",
            "Morris",
            "Rogers",
            "Reed",
            "Cook",
            "Morgan",
            "Bell",
            "Murphy",
            "Bailey",
            "Rivera",
            "Cooper",
            "Richardson",
            "Cox",
            "Howard",
            "Ward",
            "Torres",
            "Peterson",
            "Gray",
            "Ramirez",
            "James",
            "Watson",
            "Brooks",
            "Kelly",
            "Sanders",
            "Price",
            "Bennett",
            "Wood",
            "Barnes",
            "Ross",
            "Henderson",
            "Coleman",
            "Jenkins",
            "Perry",
            "Powell",
            "Long",
            "Patterson",
            "Hughes",
            "Flores",
            "Washington",
            "Butler",
            "Simmons",
            "Foster",
            "Gonzales",
            "Bryant ",
            "Alexander",
            "Russell",
            "Griffin ",
            "Diaz",
            "Hayes"
        };

        #endregion Private Fields

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
        public bool AddCollectionItems(string jsonCollectionItems, out Dictionary<int, int> recordCount)
        {
            bool itemsAdded = false;
            recordCount = new Dictionary<int, int>();
            // get the existing file data
            List<CollectionItem> existingCollectionItems = GetCollectionItemsFromFile("All");
            int beforeInsertion = existingCollectionItems.Count();
            // convert the jsonCollectionItems string to a List<CollectionItem>
            List<CollectionItem> deserializedData = JsonConvert.DeserializeObject<List<CollectionItem>>(jsonCollectionItems);

            // add the newly sent deserializedData to the existingCollectionItems
            existingCollectionItems.AddRange(deserializedData);
            int afterInsertion = existingCollectionItems.Count();
            recordCount.Add(beforeInsertion, afterInsertion);
            // convert it all back to json data
            var serializedCollectionItems = JsonConvert.SerializeObject(existingCollectionItems);
            string filePath = GetDataFilePath("All");

            // convert the json data to a comma delimited value
            string csv = JsonToCSV(serializedCollectionItems);

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

        public bool GenerateNewCollectionItems(out Dictionary<int, int> recordCountAfterAdd)
        {
            DataHandler dataHandler = new DataHandler();
            List<CollectionItem> list = new List<CollectionItem>();

            for (int i = 0; i < 6; i++)
            {
                var firstName = FirstNameList.RandomElement();
                var lastName = LastNameList.RandomElement();
                CollectionItem ci = new CollectionItem()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    FavoriteColor = ColorList.RandomElement(),
                    DateOfBirth = RandomDateGenerator(),
                    Email = firstName + lastName + "@" + RandomStringGenerator(2) + "Email.com"
                };
                list.Add(ci);
            }

            var content = JsonConvert.SerializeObject(list);
            recordCountAfterAdd = new Dictionary<int, int>();
            return dataHandler.AddCollectionItems(content, out recordCountAfterAdd);
        }

        public CollectionItem GetCollectionItemFromDelimitedString(string delimitedString)
        {
            string[] stringArray;
            if (delimitedString.Contains(","))
            {
                stringArray = delimitedString.Split(',');
            }
            else if (delimitedString.Contains("|"))
            {
                stringArray = delimitedString.Split('|');
            }
            else
            {
                stringArray = delimitedString.Split('\t');
            }
            CollectionItem item = new CollectionItem()
            {
                LastName = stringArray[0],
                FirstName = stringArray[1],
                Email = stringArray[2],
                FavoriteColor = stringArray[3],
                DateOfBirth = stringArray[4]
            };
            return item;
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
            string delimiter = dict.Values.ElementAt(0);
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
                        recordCounter ++;
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
        /// <param name="outputItem">Either 1, 2, 3, 4 or 5</param>
        /// <returns></returns>
        public List<CollectionItem> GetSortedList(int outputItem)
        {
            List<int> allowedInts = new List<int>() { 0, 1, 2, 3, 4, 5, 11 };
            if (!allowedInts.Contains(outputItem))
            {
                throw new ArgumentException("outputItem must be 0, 1, 2, 3, 4 or 5", nameof(outputItem));
            }
            List<CollectionItem> collectionItems = GetAllCollectionItems();

            switch (outputItem)
            {
                case 1:
                    return collectionItems
                        .OrderByDescending(o => o.Email)
                        .ThenBy(t => t.LastName)
                        .ToList();

                case 2:
                    return collectionItems
                        .OrderBy(o => DateTime.Parse(o.DateOfBirth))
                        .ToList();

                case 3:
                    return collectionItems
                        .OrderByDescending(o => o.LastName)
                        .ToList();

                case 4:
                    return collectionItems
                        .OrderBy(o => o.FavoriteColor)
                        .ToList();

                case 5:
                    return collectionItems
                        .OrderBy(o => o.FirstName)
                        .ToList();

                case 11:
                    return collectionItems
                        .OrderBy(o => o.Email)
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
            string csv = JsonToCSV(serializedCollectionItems);

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
        private string JsonToCSV(string json)
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

        private string RandomDateGenerator()
        {
            DateTime start = new DateTime(1995, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(gen.Next(range)).ToString("M/d/yyyy");
        }

        private string RandomStringGenerator(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz";
            var returnValue = new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
            return char.ToUpper(returnValue[0]) + returnValue.Substring(1);
        }

        #endregion Private Methods
    }
}