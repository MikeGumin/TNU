using System.Diagnostics.Contracts;

namespace TNU;

public class OperationResult
{
    public string Value { get; set; } = string.Empty;
    
    public string ErrorMessage { get; set; } = string.Empty;
    
    public bool IsFailed { get; set; } = false;
    
    public bool IsSuccess { get; set; } = false;
    
    public static OperationResult Fail(string errorMessage)
    {
        return new OperationResult()
        { 
            ErrorMessage = errorMessage,
            IsFailed = true,
        };
    }
    
    public static OperationResult Ok()
    {
        return new OperationResult()
        { 
            IsSuccess = true,
        };
    }
}

public class OperationResult<T> : OperationResult
{
    public new T? Value { get; set; }
    
    public new static OperationResult<T> Fail(string errorMessage)
    {
        return new OperationResult<T>()
        { 
            ErrorMessage = errorMessage,
            IsFailed = true,
        };
    }
    
    public new static OperationResult<T> Ok()
    {
        return new OperationResult<T>()
        {
            IsSuccess = true,
        };
    }
    
    public static OperationResult<T> Ok(T value)
    {
        return new OperationResult<T>()
        {
            IsSuccess = true,
            Value = value,
        };
    }
}