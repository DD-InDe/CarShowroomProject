namespace CarShowroom.Database;

public partial class User
{
    public string Active => IsActive ? "✅" : "❎";
}