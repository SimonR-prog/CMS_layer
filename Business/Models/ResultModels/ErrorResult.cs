﻿namespace Business.Models.ResultModels;

public class ErrorResult : Result
{
    public ErrorResult(int statusCode, string errorMessage)
    {
        Success = false;
        StatusCode = statusCode;
        ErrorMessage = errorMessage;
    }
}
