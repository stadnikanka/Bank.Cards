namespace Bank.Cards.Application.Infrastructure;

public class OperationResult<T>
{
    public T? Response { get; init; }

    public bool IsSuccess { get; init; }

    public int? ErrorCode { get; init; }

    public string? ErrorMessage { get; init; }

    public static OperationResult<T> Success(T response) => new() { Response = response, IsSuccess = true };

    public static OperationResult<T> Failure(int errorCode, string errorMessage) => new() { IsSuccess = false, ErrorCode = errorCode, ErrorMessage = errorMessage };

    public static OperationResult<T> BadRequest(string errorMessage) => Failure(400, errorMessage);

    public static OperationResult<T> NotFound(string errorMessage) => Failure(404, errorMessage);
}

