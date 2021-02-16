using GuaranteedRateConsoleApp.DataLayer;
using GuaranteedRateConsoleApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Web;

namespace GuaranteedRateConsoleApp
{
    public class GuarenteedRatesApi
    {
        #region Public Fields

        public static readonly string GRNormalServiceBaseAddress = "http://localhost:8000";
        public static readonly string GRRestServiceBaseAddress = "http://localhost:8008";

        #endregion Public Fields

        #region Public Interfaces

        /// <summary>
        /// Interface for the normal service
        /// </summary>
        [ServiceContract]
        public interface IGuranteedRatesNormalService
        {
            #region Public Methods

            [OperationContract]
            [WebInvoke(Method = "Post",
                 ResponseFormat = WebMessageFormat.Json,
                 BodyStyle = WebMessageBodyStyle.Wrapped,
                 UriTemplate = "records")]
            //[return: MessageParameter(Name = "Data")]
            string CallAcceptPost(string delimitedString);

            [OperationContract]
            [WebInvoke(Method = "GET",
     ResponseFormat = WebMessageFormat.Json,
     BodyStyle = WebMessageBodyStyle.Wrapped,
     UriTemplate = "records/{sortByValue}")]
            [return: MessageParameter(Name = "Data")]
            List<CollectionItem> CallSortBy(string sortByValue);

            #endregion Public Methods
        }

        /// <summary>
        /// Interface for the restful service
        /// </summary>
        [ServiceContract]
        public interface IGuranteedRatesRestService
        {
            #region Public Methods

            [OperationContract]
            [WebInvoke(Method = "Post",
                 ResponseFormat = WebMessageFormat.Json,
                 BodyStyle = WebMessageBodyStyle.Wrapped,
                 UriTemplate = "records")]
            [return: MessageParameter(Name = "Data")]
            string AcceptPost(string delimitedString);

            [OperationContract]
            [WebInvoke(Method = "GET",
     ResponseFormat = WebMessageFormat.Json,
     BodyStyle = WebMessageBodyStyle.Wrapped,
     UriTemplate = "records/{sortByValue}")]
            [return: MessageParameter(Name = "Data")]
            List<CollectionItem> SortBy(string sortByValue);

            #endregion Public Methods
        }

        #endregion Public Interfaces

        #region Public Methods

        public static void StartService()
        {
            ServiceHost restServiceHost = new ServiceHost(typeof(GranteedRatesRestService), new Uri(GRRestServiceBaseAddress));
            restServiceHost.AddServiceEndpoint(typeof(IGuranteedRatesRestService), new WebHttpBinding(), "").Behaviors.Add(new WebHttpBehavior());
            restServiceHost.Open();

            ServiceHost normalServiceHost = new ServiceHost(typeof(GuranteedRatesNormalService), new Uri(GRNormalServiceBaseAddress));
            normalServiceHost.AddServiceEndpoint(typeof(IGuranteedRatesNormalService), new BasicHttpBinding(), "");
            normalServiceHost.Open();

            Console.WriteLine("The REST API started and you can access it from your browser using the URL '" + GRRestServiceBaseAddress + "/'.");
            Console.WriteLine("Use '" + GRRestServiceBaseAddress + "/records/email' to return records sorted by email.");
            Console.WriteLine("Use '" + GRRestServiceBaseAddress + "/records/birthdate' to return records sorted by birthdate.");
            Console.WriteLine("Use '" + GRRestServiceBaseAddress + "/records/name' to return records sorted by first name.");
        }

        #endregion Public Methods

        #region Public Classes

        public class GranteedRatesRestService : IGuranteedRatesRestService
        {
            #region Public Methods

            public string AcceptPost(string delimitedString)
            {
                DataHandler dl = new DataHandler();
                CollectionItem item = dl.GetCollectionItemFromDelimitedString(delimitedString);
                return JsonConvert.SerializeObject(item);
            }

            [return: MessageParameter(Name = "Data")]
            public List<CollectionItem> SortBy(string sortByValue)
            {
                DataHandler dl = new DataHandler();
                List<CollectionItem> list = new List<CollectionItem>();
                switch (sortByValue)
                {
                    case "email":
                        list = dl.GetSortedList(11);
                        break;

                    case "birthdate":
                        list = dl.GetSortedList(2);
                        break;

                    case "name":
                        list = dl.GetSortedList(5);
                        break;
                }
                return list;
            }

            #endregion Public Methods
        }

        public class GuranteedRatesNormalService : IGuranteedRatesNormalService
        {
            #region Private Fields

            private static GuranteedRatesRestClient client = new GuranteedRatesRestClient(GRRestServiceBaseAddress);

            #endregion Private Fields

            #region Public Methods

            [return: MessageParameter(Name = "Data")]
            public string CallAcceptPost(string delimitedString)
            {
                DataHandler dl = new DataHandler();
                CollectionItem item = dl.GetCollectionItemFromDelimitedString(delimitedString);
                return JsonConvert.SerializeObject(item);
            }

            [return: MessageParameter(Name = "Data")]
            public List<CollectionItem> CallSortBy(string sortByValue)
            {
                DataHandler dl = new DataHandler();
                List<CollectionItem> list = new List<CollectionItem>();
                switch (sortByValue)
                {
                    case "email":
                        list = dl.GetSortedList(11);
                        break;

                    case "birthdate":
                        list = dl.GetSortedList(2);
                        break;

                    case "name":
                        list = dl.GetSortedList(5);
                        break;
                }
                return list;
            }

            #endregion Public Methods
        }

        public class GuranteedRatesRestClient : ClientBase<IGuranteedRatesRestService>, IGuranteedRatesRestService
        {
            #region Public Constructors

            public GuranteedRatesRestClient(string address)
                : base(new WebHttpBinding(), new EndpointAddress(address))
            {
                this.Endpoint.Behaviors.Add(new WebHttpBehavior());
            }

            #endregion Public Constructors

            #region Public Methods

            public string AcceptPost(string delimitedString)
            {
                using (new OperationContextScope(this.InnerChannel))
                {
                    return base.Channel.AcceptPost(delimitedString);
                }
            }

            public List<CollectionItem> SortBy(string sortByValue)
            {
                using (new OperationContextScope(this.InnerChannel))
                {
                    return base.Channel.SortBy(sortByValue);
                }
            }

            #endregion Public Methods
        }

        #endregion Public Classes
    }
}