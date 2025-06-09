using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Application.Common.Response;

public class PagedResponse<T>
{
    public int ActualPage { get; }
    public int PageSize { get; }
    public int TotalPages { get; }
    public int TotalItems { get; }
    public List<T> Items { get; }

    public PagedResponse(int actualPage, int pageSize, int totalPages, int totalItems, List<T> items)
    {
        ActualPage = actualPage;
        PageSize = pageSize;
        TotalPages = (int)Math.Ceiling((double)totalItems/pageSize);
        TotalItems = totalItems;
        Items = items;
    }
}
