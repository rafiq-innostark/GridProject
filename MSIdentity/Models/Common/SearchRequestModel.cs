using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MSIdentity.Models
{
    public class SearchRequestModel
    {
        //user select page size or number of records to be displayed
        public int? PageSize { get; set; }
        public string SearchString { get; set; }
        //current page no.
        public int? PageNo { get; set; }
        public int? CategoryId { get; set; }
        //sort order
        public bool IsAsc { get; set; }
        // delete item id
        public int Id { get; set; }

        /// <summary>
        /// Order By Name
        /// </summary>

        public short SortBy { get; set; }

        /// <summary>
        /// Product Order By
        /// </summary>
        public OrderProductByColumn ProductOrderBy
        {
            get
            {
                return (OrderProductByColumn)SortBy;
            }
            set
            {
                SortBy = (short)value;
            }
        }



    }
}