using CleanArch.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CleanArch.Domain.Entities;

public sealed class Genrer : Entity
{
    public string? Name { get; private set; }

    public Genrer(string name)
    {
        ValidateDomain(name);
    }
    public Genrer() { }

    [JsonConstructor]
    public Genrer(int id, string name)
    {
        DomainValidation.When(id < 0, "Invalid Id value.");
        Id = id;
        ValidateDomain(name);
    }

    public void Update(string name)
    {
        ValidateDomain(name);
    }

    private void ValidateDomain(string name)
    {
        DomainValidation.When(string.IsNullOrEmpty(name),
            "Invalid name. Name is required");

        DomainValidation.When(name.Length < 3,
            "Invalid name, too short, minimum 3 characters");

        DomainValidation.When(name?.Length > 250,
            "Invalid name, too long, maximum 50 characters");

        Name = name;

    }

}
