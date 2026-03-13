namespace LMS.Api.Shared;

public sealed class ValidationErrors
{
    private readonly List<Error> _errors = [];
    public IReadOnlyCollection<Error> Errors => _errors;

    private int RemoveIndex = 0;

    public void Add(Error error)
    {
        error.Type = ErrorType.Validation;
        _errors.Add(error);
    }

    public void Clear(Error error)
    {
        _errors.Clear();
    }

    public Error? Remove()
    {
        Error error = _errors[RemoveIndex];

        bool isRemoved = _errors.Remove(error);

        if (!isRemoved)
        {
            return null;
        }

        RemoveIndex++;
        int maxIndex = GetMaxIndex();

        if (RemoveIndex > maxIndex)
        {
            RemoveIndex = 0;
        }

        return error;
    }

    private int GetMaxIndex()
    {
        return _errors.Count - 1;
    }
}
