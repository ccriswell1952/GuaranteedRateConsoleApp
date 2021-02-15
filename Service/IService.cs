using GuaranteedRateConsoleApp.Models;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Threading.Tasks;

namespace GuaranteedRateConsoleApp.Service
{
    [ServiceContract]
    public interface IService
    {
        //#region Public Methods

        //[OperationContract]
        //[WebInvoke(Method = "GET",
        //     ResponseFormat = WebMessageFormat.Json,
        //     BodyStyle = WebMessageBodyStyle.Wrapped,
        //     UriTemplate = "records/{sortByValue}")]
        //[return: MessageParameter(Name = "Data")]
        //List<CollectionItem> SortBy(string sortByValue);

        //[OperationContract]
        //[WebInvoke(Method = "Post",
        //     ResponseFormat = WebMessageFormat.Json,
        //     BodyStyle = WebMessageBodyStyle.Wrapped,
        //     UriTemplate = "records")]
        //[return: MessageParameter(Name = "Data")]
        //Task<CollectionItem> AcceptPost(string delimitedString);

        //#endregion Public Methods
    }
}