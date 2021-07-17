using Newtonsoft.Json;

namespace ServerApp.Models
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public override string  ToString() //toString metodunu ezerek aşşağıdaki işlemi yapmasını sağladık
        {
            return JsonConvert.SerializeObject(this);//serialize etmiş olduk
        }
    }
}