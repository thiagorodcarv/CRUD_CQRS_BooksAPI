using CleanArch.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CleanArch.Domain.Entities;

public sealed class Author : Entity
{
    public string? Name { get; private set; }

    public Author(string name)
    {
        ValidateDomain(name);
    }
    public Author() { }

    [JsonConstructor]
    public Author(int id, string name)
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
            "Invalid name, too long, maximum 250 characters");

        Name = name;

    }

}
