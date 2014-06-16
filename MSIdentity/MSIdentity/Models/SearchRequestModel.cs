﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSIdentity.Models
{
    public class SearchRequestModel
    {
        //user select page size
        public int? PageSize { get; set; }
        public string SearchString { get; set; }
        //current page no.
        public int? PageNo { get; set; }
        public int? CategoryId { get; set; }
        //order by coulmn number
        public int SortBy { get; set; }
        //sort order
        public bool IsAsc { get; set; }
    }
}