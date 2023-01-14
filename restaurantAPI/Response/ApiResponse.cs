using System.Net;

public class ApiResponse
{
    public bool Success { get; set; }
    public int StatusCode { get; set; }
    public object? Data { get; set; }
    public string? ErrorMessage { get; set; }

    public ApiResponse(int statusCode, object? data = null, string? errorMessage = null, bool success = true)
    {
        StatusCode = statusCode;
        Data = data;
        ErrorMessage = errorMessage;
        Success = success;
    }
}