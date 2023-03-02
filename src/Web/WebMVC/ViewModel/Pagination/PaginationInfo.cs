﻿namespace WebMVC.ViewModel.Pagination;

public class PaginationInfo
{
    public int TotalItems { get; set; }
    public int ItemsOnPage { get; set; }
    public int ActualPage { get; set; }
    public int TotalPage { get; set; }
    public string Next { get; set; }
    public string Previous { get; set; }
}
