using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectORM.Models;

public class FinancialTransaction
{
    [Key]
    public int TransactionID { get; set; }
    public string? Name { get; set; }
    public bool IsIncome { get; set; }
    public string? Category { get; set; }
    public double Amount { get; set; }
    public string? Description { get; set; }
    public DateTime TransactionDate { get; set; }

}
