using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectORM.Models;

public class IncomeCategory
{
    [Key]
    public int CategoryID { get; set; }
    public string? Name { get; set; }

    public override string ToString() => Name ?? "Unknown";
}
