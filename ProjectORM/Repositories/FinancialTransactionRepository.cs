using ProjectORM.Context;
using ProjectORM.Models;
using ProjectORM.Repositories.Base;
using System.Collections.Generic;
using System.Linq;

namespace ProjectORM.Repositories;

public class FinancialTransactionRepository : IFinancialTransactionRepository
{
    private readonly AppDbContext dbContext;

    public FinancialTransactionRepository(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public FinancialTransaction GetTransactionById(int id)
    {
        return dbContext.FinancialTransactions.FirstOrDefault(t => t.TransactionID == id);
    }

    public IEnumerable<FinancialTransaction> GetAllTransactions()
    {
        return dbContext.FinancialTransactions.ToList();
    }


    public IEnumerable<FinancialTransaction> GetAllIncomeTransactions()
    {
        return dbContext.FinancialTransactions.Where(ft => ft.IsIncome).ToList();
    }
    public IEnumerable<FinancialTransaction> GetAllExpenseTransactions()
    {
        return dbContext.FinancialTransactions.Where(ft => !ft.IsIncome).ToList();
    }

    public void Add(FinancialTransaction transaction)
    {
        dbContext.FinancialTransactions.Add(transaction);
    }

    public void Update(FinancialTransaction transaction)
    {
        throw new System.NotImplementedException();
    }

    public void Delete(FinancialTransaction transaction)
    {
        throw new System.NotImplementedException();
    }

    public void SaveChanges()
    {
        dbContext.SaveChanges();
    }
}