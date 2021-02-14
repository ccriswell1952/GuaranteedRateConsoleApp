using GuaranteedRateConsoleApp.Models;
using System.Collections.Generic;

namespace GuaranteedRateConsoleApp.DataLayer
{
    public interface IDataHandler
    {
        #region Public Methods

        bool AddCollectionItems(string jsonCollectionItems);
        bool DeleteCollectionItem(string searchForEmailAddress);
        List<CollectionItem> GetAllCollectionItems();
        List<CollectionItem> GetCollectionItems(string searchByItemName, string propertyName);
        List<CollectionItem> GetSortedList(int outputItem);
        #endregion Public Methods
    }
}