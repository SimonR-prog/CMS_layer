namespace Business.Models.ResultModels;

public class SuccessResult : Result
{
    public SuccessResult(int statusCode)
    {
        Success = true;
        StatusCode = statusCode;
    }
}
