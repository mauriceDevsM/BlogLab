using System;
using System.Collections.Generic;
using System.Text;

namespace BlogLab.Models.Blog
{
    public class PagedResult<T>
    {
        public IEnumerable<T> Items { get; set; }

        public int TotalCount { get; set; }
    }
}
