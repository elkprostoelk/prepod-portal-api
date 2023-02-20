namespace PrepodPortal.Common.DTO;

public class ServiceResult
{
    public bool IsSuccessful { get; set; } = true;

    public IList<string> Errors { get; set; } = new List<string>();
}

public class ServiceResult<T> : ServiceResult
{
    public T? Container { get; set; }
}