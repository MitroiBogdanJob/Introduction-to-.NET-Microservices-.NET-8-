namespace Mango.Web.Models
{
    public class ResponseDto
        //this you will get when you call an Api
    {
        public object Result { get; set; }
        public bool IsSUcess { get; set; } = true;
        public string Message { get; set; } = "";
    }
}
