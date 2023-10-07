drop table FinancialTransactions
drop table IncomeCategories
drop table ExpenseCategories
drop table Users

insert into IncomeCategories(Name)
values('Salary'), ('Business'), ('Others')

insert into ExpenseCategories(Name)
values('Food'), ('Shopping'), ('Medical'), ('Household'), ('Travel'), ('Vehicle'), ('Others')


select *
from FinancialTransactions

delete 
from FinancialTransactions
where TransactionID >= 0

