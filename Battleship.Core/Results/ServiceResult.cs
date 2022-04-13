namespace Battleship.Core.Results;

public class ServiceResult
{
    protected ServiceResult(bool succeeded, IEnumerable<string> errors)
    {
        Succeeded = succeeded;
        Errors = errors.ToArray();
    }

    public bool Succeeded { get; }
    public string[] Errors { get; }

    public static ServiceResult Success() => new(true, Array.Empty<string>());

    public static ServiceResult Failure(IEnumerable<string> errors) => new(false, errors);

    public static ServiceResult Failure(string error) => new(false, new List<string> {error});
}

public class ServiceResult<T> : ServiceResult
{
    public T? Data { get; }

    private ServiceResult(T data, bool succeeded, IEnumerable<string> errors) : base(succeeded, errors)
    {
        Data = data;
    }

    private ServiceResult(bool succeeded, IEnumerable<string> errors) : base(succeeded, errors)
    {
        Data = default;
    }

    public static ServiceResult<T> Success(T data) => new(data, true, Array.Empty<string>());

    public new static ServiceResult<T> Failure(IEnumerable<string> errors) => new(false, errors);

    public new static ServiceResult<T> Failure(string error) => new(false, new List<string> {error});
}