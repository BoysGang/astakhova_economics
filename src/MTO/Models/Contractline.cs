﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MTO.Models
{
    public partial class MTOContext : DbContext
    {
        public DbSet<ContractLine> ContractLines { get; set; }

    }

    [Table("contractline")]
    public class ContractLine
    {
        [Key]
        public int PK_ContractLine { get; set; }

        public int PK_Resource { get; set; }

        public float Amount { get; set; }

        public Decimal UnitPrice { get; set; }

        [Column(TypeName = "Date")]
        public DateTime DeliveryDate { get; set; }

        public int PK_Contract { get; set; }
        public Contract Contract
        { 
            get
            {
                return Program.db.Contracts.Find(PK_Contract);
            }
        }
    }
}