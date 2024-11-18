using INUPCO.Catalog.Domain.Common;
using INUPCO.Catalog.Domain.Entities.Customers;
using INUPCO.Catalog.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace INUPCO.Catalog.Domain.Entities.GenericItemPharmas;

public class GenericItemPharma : BaseEntity
{
    private GenericItemPharma() { }

    private GenericItemPharma(string code, string name, string? description, string? customerCode)
    {
        Code = code;
        Name = name;
        Description = description;
        CustomerCode = customerCode;
        IsActive = true;
        Status = ItemStatus.Draft;
        _comments = new List<Comment>();
    }

    public string Code { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public bool IsActive { get; private set; }
    public string? CustomerCode { get; private set; }
    public ItemStatus Status { get; private set; }
    private readonly List<Comment> _comments;
    public IReadOnlyCollection<Comment> Comments => _comments.AsReadOnly();
    private readonly List<CustomerGenericItemPharmaCodeMapping> _customerMappings = new();
    public IReadOnlyCollection<CustomerGenericItemPharmaCodeMapping> CustomerMappings => _customerMappings.AsReadOnly();

    public static GenericItemPharma Create(string code, string name, string? description, string? customerCode)
    {
        if (string.IsNullOrWhiteSpace(code))
            throw new ArgumentException("Code cannot be empty", nameof(code));
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty", nameof(name));

        return new GenericItemPharma(code, name, description, customerCode);
    }

    public void AddComment(string content, string userId, CommentType type)
    {
        _comments.Add(new Comment(content, userId, type));
    }

    public void Submit()
    {
        if (Status != ItemStatus.Draft)
            throw new InvalidOperationException("Only draft items can be submitted for review");

        Status = ItemStatus.UnderReview;
    }

    public void Approve(string approverId)
    {
        if (Status != ItemStatus.UnderReview)
            throw new InvalidOperationException("Only items under review can be approved");

        Status = ItemStatus.Approved;
    }

    public void Reject(string rejectorId, string reason)
    {
        if (Status != ItemStatus.UnderReview)
            throw new InvalidOperationException("Only items under review can be rejected");

        Status = ItemStatus.Rejected;
        AddComment(reason, rejectorId, CommentType.Rejection);
    }

    public void AddCustomerMapping(string customerCode, string customerSpecificCode)
    {
        if (_customerMappings.Any(x => x.CustomerCode == customerCode))
            throw new InvalidOperationException($"Mapping for customer {customerCode} already exists");

        var mapping = CustomerGenericItemPharmaCodeMapping.Create(customerCode, customerSpecificCode, this);
        _customerMappings.Add(mapping);
    }
} 