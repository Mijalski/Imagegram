namespace Mijalski.Imagegram.Domain.Accounts;

public class Account
{
    public Account(string name)
    {
        Name = name;
    }

    public string Name { get; }
}