﻿namespace Mijalski.Imagegram.Domain.Accounts;

public class Account
{
    public Account(string name)
    {
        Name = name;
    }

    public Account(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public Guid Id { get; }
    public string Name { get; }
}