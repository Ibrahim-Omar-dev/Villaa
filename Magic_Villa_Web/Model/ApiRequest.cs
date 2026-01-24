using static Utility.SD;

namespace Magic_Villa_Web.Model
{
    public class ApiRequest
    {
        public ApiType ApiType { get; set; }=ApiType.GET;
        public string Url { get; set; }
        public object Data { get; set; }
    }
}
