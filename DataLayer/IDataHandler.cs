using GuaranteedRateConsoleApp.Models;
using System.Collections.Generic;

namespace GuaranteedRateConsoleApp.DataLayer
{
    public partial interface IDataHandler
    {
        #region Public Methods

        bool AddCollectionItems(string jsonCollectionItems, out Dictionary<int, int> recordCount);
        bool DeleteCollectionItem(string searchForEmailAddress);
        bool GenerateNewCollectionItems(out Dictionary<int, int> recordCountAfterAdd);
        List<CollectionItem> GetAllCollectionItems();
        CollectionItem GetCollectionItemFromDelimitedString(string delimitedString);
        List<CollectionItem> GetCollectionItems(string searchByItemName, string propertyName);
        List<CollectionItem> GetSortedList(int outputItem);
        #endregion Public Methods
    }
}