using Dapper;
using ProjectORM.Models;
using ProjectORM.Repositories.Base;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ProjectORM.Repositories;

public class ExpenseCategoryDapperRepository : IExpenseCategoryDapperRepository
{
    private const string connectionString = $"Server=localhost;Database=DemoDb;User Id=admin;Password=admin;";
    private readonly SqlConnection sqlConnection;

    public ExpenseCategoryDapperRepository()
    {
        this.sqlConnection = new SqlConnection(connectionString);
        this.sqlConnection.Open();
    }

    public IEnumerable<ExpenseCategory> GetAll()
    {
        return this.sqlConnection.Query<ExpenseCategory>(sql: "select * from ExpenseCategories");
    }
}
