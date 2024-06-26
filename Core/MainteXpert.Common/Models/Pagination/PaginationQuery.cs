﻿namespace MainteXpert.Common.Models.Pagination
{
    public class PaginationQuery
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public PaginationQuery()
        {
            this.PageNumber = 1;
            this.PageSize = int.MaxValue;
        }
        public PaginationQuery(int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
            this.PageSize = pageSize < 10 ? int.MaxValue : pageSize;
        }
    }
}
