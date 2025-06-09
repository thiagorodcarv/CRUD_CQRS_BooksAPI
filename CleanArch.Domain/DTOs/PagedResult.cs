using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Domain.DTOs;

public class PagedResult<T>
{
    public IReadOnlyList<T> Items { get; set; }
    public int TotalItems { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }

    public PagedResult(IReadOnlyList<T> items, int totalItems, int page, int pageSize)
    {
        Items = items;
        TotalItems = totalItems;
        Page = page;
        PageSize = pageSize;
    }
}