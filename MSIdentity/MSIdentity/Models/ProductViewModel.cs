﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace MSIdentity.Models
{
    public class ProductViewModel
    {
        public IPagedList<Product> ProductList { get; set; }
        public SelectList Categories { get; set; }
        public int? TotalPrice { get; set; }
        public int? TotalNoOfRec{ get; set; }
    }
}