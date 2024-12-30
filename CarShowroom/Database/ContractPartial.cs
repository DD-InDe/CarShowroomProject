namespace CarShowroom.Database;

public partial class Contract
{
    public string DateString => DateCreate.Value.ToString("d");
}