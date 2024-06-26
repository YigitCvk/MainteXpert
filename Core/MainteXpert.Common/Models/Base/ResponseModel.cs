namespace MainteXpert.Common.Models.Base
{
    public class ResponseModel<T>
    {
        public T Data { get; set; }
        public List<T> Datas { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public int HttpStatus { get; set; }
        public int TotalCount { get; set; }
        public List<string> Errors { get; set; }

        public ResponseModel(T Data)
        {
            this.Data = Data;
        }
        public ResponseModel(List<T> data)
        {
            this.Datas = data;
        }

        public ResponseModel(T data, string message = null)
        {
            Data = data;
            Message = message;
            TotalCount = data == null ? 0 : 1;
            Errors = new List<string> { message };

        }
        public static ResponseModel<T> Success(T data, string message = "")
        {
            ResponseModel<T> response = new ResponseModel<T>(data);
            response.IsSuccess = true;
            response.Message = $"İşlem Başarılı {message}";
            response.HttpStatus = (int)HttpStatusCode.OK;
            return response;

        }
        public static ResponseModel<T> Fail(T data, HttpStatusCode httpCode = HttpStatusCode.BadRequest, string message = "")
        {
            ResponseModel<T> response = new ResponseModel<T>(data);
            response.IsSuccess = false;
            response.Message = $"İşlem Başarısız {message}";
            response.HttpStatus = (int)httpCode;
            return response;
        }
    }

    public class ResponseModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public int HttpStatus { get; set; }

        public static ResponseModel Success(string message = "")
        {
            ResponseModel response = new ResponseModel();
            response.IsSuccess = true;
            response.Message = $"İşlem Başarılı {message}";
            response.HttpStatus = (int)HttpStatusCode.OK;
            return response;

        }
        public static ResponseModel Fail(string message = "", HttpStatusCode httpCode = HttpStatusCode.BadRequest)
        {
            ResponseModel response = new ResponseModel();
            response.IsSuccess = false;
            response.Message = $"İşlem Başarısız {message}";
            response.HttpStatus = (int)httpCode;
            return response;
        }



    }
    /*
    public static class ResponseModelExtension
    {

        public static ResponseModel Success(string message = "")
        {
            ResponseModel response = new ResponseModel();
            response.IsSuccess = true;
            response.Message = $"İşlem Başarılı {message}";
            response.HttpStatus = (int)HttpStatusCode.OK;
            return response;

        }
        public static ResponseModel Fail(string message = "", HttpStatusCode httpCode = HttpStatusCode.BadRequest)
        {
            ResponseModel response = new ResponseModel();
            response.IsSuccess = false;
            response.Message = $"İşlem Başarısız {message}";
            response.HttpStatus = (int)httpCode;
            return response;
        }
    }
    */
}
