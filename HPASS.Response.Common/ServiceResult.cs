using System.Text.Json.Serialization;

namespace HPASS.Response.Common
{
    public class ServiceResult<T>
    {

        [JsonConstructor]
        public ServiceResult(T result, string successReponseCode)
        {
            this.Result = result;
            this.ResponseCode = successReponseCode;
            this.HasError = false;
        }

        public ServiceResult(string errorResponseCode)
        {
            this.ResponseCode = errorResponseCode;
            this.HasError = true;
        }

        public string ResponseCode { get; private set; }

        public T Result { get; private set; }

        public bool HasError { get; private set; }



    }

}