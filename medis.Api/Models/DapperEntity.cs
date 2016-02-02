using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace medis.Api.Models
{
    public class DapperEntity
    {
        public int Id { get; set; }

        public DateTime DateCreated { get; set; }
        public int CreatedByUserId { get; set; }
        
        public DateTime? DateUpdated { get; set; }
        public int? UpdatedByUserId { get; set; }
        
        public DateTime? DateDeleted { get; set; }
        public int? DeletedByUserId { get; set; }
    }
}