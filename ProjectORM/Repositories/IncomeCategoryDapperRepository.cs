using Dapper;
using ProjectORM.Models;
using ProjectORM.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectORM.Repositories;

public class IncomeCategoryDapperRepository : IIncomeCategoryDapperRepository
{
    private const string connectionString = $"Server=localhost;Database=DemoDb;User Id=admin;Password=admin;";
    private readonly SqlConnection sqlConnection;

    public IncomeCategoryDapperRepository()
    {
        this.sqlConnection = new SqlConnection(connectionString);
        this.sqlConnection.Open();
    }

    public IEnumerable<IncomeCategory> GetAll()
    {
        return this.sqlConnection.Query<IncomeCategory>(sql: "select * from IncomeCategories");
    }
}
