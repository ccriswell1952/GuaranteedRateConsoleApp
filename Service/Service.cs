using GuaranteedRateConsoleApp.DataLayer;
using GuaranteedRateConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace GuaranteedRateConsoleApp.Service
{
    public class Service : IService
    {
        #region Public Methods

        //[return: MessageParameter(Name = "Data")]
        //public Task<CollectionItem> AcceptPost(string delimitedString)
        //{
        //    DataHandler dl = new DataHandler();
        //    CollectionItem item = dl.GetCollectionItemFromDelimitedString(delimitedString);
        //    return Task.FromResult(item);
        //}

        //[return: MessageParameter(Name = "Data")]
        //List<CollectionItem> IService.SortBy(string sortByValue)
        //{
        //    DataHandler dl = new DataHandler();
        //    List<CollectionItem> list = new List<CollectionItem>();
        //    switch (sortByValue)
        //    {
        //        case "email":
        //            list = dl.GetSortedList(11);
        //            break;
        //        case "birthdate":
        //            list = dl.GetSortedList(2);
        //            break;
        //        case "name":
        //            list = dl.GetSortedList(5);
        //            break;
        //    }
        //    return list;
        //}

        #endregion Public Methods
    }
}