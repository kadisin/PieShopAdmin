<div style="text-align: center;">


![image](https://github.com/kadisin/PieShopAdmin/assets/38622355/9f74fe95-f7e8-4639-b035-7333b3561f57)

<p style="text-align: center"> Description (business perspective): </p>
<p> >Web application use to manage pies. </p>
<p> As a owner of pie shop I want to manage a pies (add, modify, delete, search etc).</p>

<p> Description (technical perspective) </p>
<p> Web application use to manage pies</p>
<p> Project architecture: </p>
<p> Basicly is MVC architecture with extension using Repositories classes to connect to database, ViewModels </p>
<p> Basic CRUD operation on pie's and categories (look interfaces IPieRepository and ICategoryRepository).</p>
<p> Pie:</p>
<p> -Get all</p>
<p> -Get by Id</p>
<p> -Add pie</p>
<p> -Update pie</p>
<p>-Remove pie</p>
<p> -Search pie (get pies with filter)</p>

<p>Category: </p>
<p>-Get all</p>
<p>-Get by Id</p>
<p>-Add category</p>
<p>-Update category</p>
<p>-Delete category</p>

<p> Project to learn good practises using Entity framework - look notes (at the end)</p>
<p>-Dependency Injection</p>
<p>-IQueryable vs IEnumerable</p>
<p>-Track changes</p>
<p>-Cache</p>
<p>-Migrations</p>

<p>Database structure:</p>
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
