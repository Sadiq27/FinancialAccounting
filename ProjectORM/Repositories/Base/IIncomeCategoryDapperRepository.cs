using ProjectORM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectORM.Repositories.Base;

public interface IIncomeCategoryDapperRepository
{
    IEnumerable<IncomeCategory> GetAll();
}
