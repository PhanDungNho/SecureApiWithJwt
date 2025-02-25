using Newtonsoft.Json;
using System.Collections.Generic;

namespace SecureApiWithJwt.DTOs.Responses
{

    public class ApiResponse<T>
    {
        public int Code { get; }
        public string Message { get; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public T? Data { get; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string[]>? Errors { get; }


        public ApiResponse(int code, string message, T? data, Dictionary<string, string[]>? errors)
        {
            Code = code;
            Message = message;
            Data = data;
            Errors = errors;
        }

        // Phan hoi thanh cong
        public static ApiResponse<T> Success(T data = default, string v = null)
        {
            return new ApiResponse<T>(0, "Success", data, null);
        }

        // Phan hoi that bai
        public static ApiResponse<T> Error(Dictionary<string, string[]> errors)
        {
            return new ApiResponse<T>(1, "Errors", default, errors);
        }

        // Phan hoi not found
        public static ApiResponse<T> NotFound(string message = "Not found")
        {
            return new ApiResponse<T>(1, message, default, default);
        }

        // Phan hoi bad request
        public static ApiResponse<T> BadRequest(string message = "Bad request")
        {
            return new ApiResponse<T>(1, message, default, default);
        }

        // Phan hoi conflict
        public static ApiResponse<T> Conflict(string message = "Conflict")
        {
            return new ApiResponse<T>(1, message, default, default);
        }
    }

}
