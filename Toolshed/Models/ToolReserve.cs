﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Toolshed.Models
{
    public class ToolReserve
    {
        [Key]
        public int ReserveId { get; set; }
        public string ReserveDate { get; set; }
        public string Who { get; set; }   
        public int ItemId { get; set; }
        public string ItemName { get; set; }
    }
}