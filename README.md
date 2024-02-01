<div style="text-align: center">


![image](https://github.com/kadisin/PieShopAdmin/assets/38622355/73803411-973a-4359-8255-88682482a2fe)


Intresting notes:

Track changes:
This is one of the main function on Entity framework.
EF track changes on rows (add, modify, delete)
Changes are related to existing object DbContext

example:
var entries = context.ChangeTracker.Entries();

foreach (var entry in entries) {
	Console.WriteLine("Entity Name: {0}", entry.Entity.GetType().Name);
        Console.WriteLine("Status: {0}", entry.State);
}
-> 
Entity Name: Student
Status: Unchanged
Entity Name: Enrollment
Status: Added

Good practise -> When we only load data to see on page (Categories to dropdown)
then we could use AsNoTracking() method
Data on database are saved when we SaveChangesAsync()

IEnumerable vs IQueryable 
-> IEnumerable load all data from database to memory then filter it
example:
IEnumerable<Employee> employeeIEnumberable = db.Employee.Where(a =>; a.JobTitle.StartsWith("P"));
employeeIEnumberable = employeeIEnumberable.Take<Employee>(5);
looks in sql:
select * from HumanResources.Employee where JobTitle like 'P%';

-> IQueryable filtering data on database then load to application
example:
IQueryable<Employee> employeeIQueryable = db.Employee.Where(a => a.JobTitle.StartsWith("P"));
employeeIQueryable = employeeIQueryable.Take<Employee>(5);
looks in sql:
select top(5) * from HumanResources.Employee where JobTitle like 'P%';

IQueryable is optymalization on database side

In-memory Caching
-> using IMemoryCache - stores data in memory server
-> set and use cache basicly on get data (load data to memory)
-> remove cache on add, update, delete data

</div>
