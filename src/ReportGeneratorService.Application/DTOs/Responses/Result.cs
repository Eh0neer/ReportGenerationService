namespace ReportGeneratorService.Application.DTOs.Responses;

public class Result<T>
{
    public bool IsSuccess { get; }
    public T? Value { get; }
    public string Error { get; }
    public bool IsFailure => !IsSuccess;

    protected Result(bool isSuccess, T? value, string error)
    {
        if(isSuccess && value == null)
            throw new ArgumentNullException(nameof(value));
        
        if(!isSuccess && string.IsNullOrWhiteSpace(error))
            throw new ArgumentNullException("Error message is required for failure", nameof(error));
        
        IsSuccess = isSuccess;
        Value = value;
        Error = error;
    }
    
    public static Result<T> Success(T value) => new Result<T>(true, value, string.Empty);
    public static Result<T> Failure(string error) => new Result<T>(false, default, error);
}

public class Result
{
    public bool IsSuccess { get; }
    public string Erorr { get; }
    public bool IsFailure => !IsSuccess;

    protected Result(bool isSuccess, string error)
    {
        if(!isSuccess && string.IsNullOrWhiteSpace(error))
            throw new ArgumentNullException("Error message is required for failure", nameof(error));
        
        IsSuccess = isSuccess;
        Erorr = error;
    }
    
    public static Result Success() => new Result(true, string.Empty);
    public static Result Failure(string error) => new Result(false, error);
}