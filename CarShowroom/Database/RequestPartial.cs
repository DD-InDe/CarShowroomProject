namespace CarShowroom.Database;

public partial class Request
{
    public string DateString => DateCreate.Value.ToString("d");
}