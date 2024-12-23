namespace CarShowroom.Database;

public partial class User
{
    public string FullName => $"{LastName} {FirstName} {MiddleName}";
}