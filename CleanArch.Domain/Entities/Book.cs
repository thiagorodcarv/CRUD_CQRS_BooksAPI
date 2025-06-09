using CleanArch.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CleanArch.Domain.Entities;

public sealed class Book : Entity 
{
    public string? Title { get; private set; }
    public int? AuthorId { get; private set; }
    public int? GenrerId { get; private set; }


    public Book(string title, int? authorID, int? genrerID)
    {
        ValidateDomain(title, authorID, genrerID);
    }
    public Book() { }

    [JsonConstructor]
    public Book(int id, string title, int? authorID, int? genrerID)
    {
        DomainValidation.When(id < 0, "Invalid Id value.");
        Id = id;
        ValidateDomain(title, authorID, genrerID);
    }

    public void Update(string title, int? authorID, int? genrerID)
    {
        ValidateDomain(title, authorID, genrerID);
    }

    private void ValidateDomain(string title, int? authorID, int? genrerID)
    {
        DomainValidation.When(string.IsNullOrEmpty(title),
            "Invalid title. Title is required");

        DomainValidation.When(title.Length < 3,
            "Invalid title, too short, minimum 3 characters");

        DomainValidation.When(title?.Length > 250,
            "Invalid title, too long, maximum 250 characters");
        
        DomainValidation.When(authorID < 0,
           "Invalid authorID value");

        DomainValidation.When(genrerID < 0,
           "Invalid genrerID value");

        Title = title;
        AuthorId = authorID;
        GenrerId = genrerID;
    }
}
