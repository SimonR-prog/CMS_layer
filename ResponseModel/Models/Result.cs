using ResponseModel.Interfaces;
namespace ResponseModel.Models;


public abstract class Result : IResult
{
    public bool Success { get; protected set; }
    public int StatusCode { get; protected set; }
    public string? ErrorMessage { get; protected set; }

    public static Result Ok()
    {
        return new SuccessResult(200);
    }
    public static Result BadRequest(string message)
    {
        return new ErrorResult(400, message);
    }
    public static Result NotFound(string message)
    {
        return new ErrorResult(404, message);
    }
    public static Result AlreadyExists(string message)
    {
        return new ErrorResult(409, message);
    }
    public static Result Error(string message)
    {
        return new ErrorResult(500, message);
    }

}
public class Result<T> : Result
{
    public T? Content { get; private set; }
    public static Result<T> Ok(T? content)
    {
        return new Result<T>
        {
            Success = true,
            StatusCode = 200,
            Content = content
        };
    }


    //Chatgpt recommended to add this to fix some of the issues that I was having with the return in customer service.
    //This allows me to still return an IEnum of a specific type, which we made into an empty list so that theres fewer issues with the service.
    public static Result<T> Error(T? content, string message)
    {
        return new Result<T>
        {
            Success = false,
            StatusCode = 500,
            Content = content,
            ErrorMessage = message
        };
    }
}
