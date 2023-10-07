using ProjectORM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectORM.Repositories.Base;

public interface IFinancialTransactionRepository
{
    FinancialTransaction GetTransactionById(int id);
    IEnumerable<FinancialTransaction> GetAllTransactions();
    IEnumerable<FinancialTransaction> GetAllIncomeTransactions();
    IEnumerable<FinancialTransaction> GetAllExpenseTransactions();
    void Add(FinancialTransaction transaction);
    void Update(FinancialTransaction transaction);
    void Delete(FinancialTransaction transaction);
    void SaveChanges();
}
