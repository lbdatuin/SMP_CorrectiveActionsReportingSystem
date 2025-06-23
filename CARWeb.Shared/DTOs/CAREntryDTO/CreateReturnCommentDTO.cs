﻿using CARWeb.Shared.Models.CAREntry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARWeb.Shared.DTOs.CAREntryDTO
{
    public class CreateReturnCommentDTO
    {
        public string From { get; set; } = string.Empty;
        public string Reason { get; set; } = string.Empty;
        public int CARHeaderId { get; set; }
    }
}
