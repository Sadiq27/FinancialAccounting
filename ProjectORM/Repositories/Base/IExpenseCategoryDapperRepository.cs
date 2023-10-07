using ProjectORM.Models;
using System.Collections.Generic;

namespace ProjectORM.Repositories.Base;

public interface IExpenseCategoryDapperRepository
{
    IEnumerable<ExpenseCategory> GetAll();
}
