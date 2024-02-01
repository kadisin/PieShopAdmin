<!DOCTYPE html>
<html>
<head>
</head>
<body style="margin: auto; text-align: center;">
    <h3>Admin Pie Shop</h3>
    
![image](https://github.com/kadisin/PieShopAdmin/assets/38622355/9f74fe95-f7e8-4639-b035-7333b3561f57)

<div class="container" style="text-align: center; margin: 30px">
<h4>Description (business perspective):</h4>

<p>Web application use to manage pies.</p>

<p>As a owner of pie shop I want to manage a pies (add, modify, delete, search etc).

<h4>Description (technical perspective)</h4>

<p>Web application use to manage pies</p>

<p>Project architecture:</p>

<p>Basicly is MVC architecture with extension using Repositories and ViewModels</p>

<p>Basic CRUD operation on pie's and categories (look interfaces <a href="https://github.com/kadisin/PieShopAdmin/blob/master/PieShopAdmin/Models/Repositories/IPieRepository.cs">IPieRepository</a> and <a href="https://github.com/kadisin/PieShopAdmin/blob/master/PieShopAdmin/Models/Repositories/ICategoryRepository.cs">ICategoryRepository</a>).</p>

<h4> Pie:</h4>
<li>Get all</li>
<li>Get by Id</li>
<li>Add pie</li>
<li>Update pie</li>
<li>Remove pie</li>
<li>Search pie (get pies with filter)</li>

<h4>Category:</h4>
<li>Get all</li>
<li>Get by Id</li>
<li>Add category</li>
<li>Update category</li>
<li>Delete category</li>
<p />
<p>Project to learn good practises using Entity framework - look notes (at the end)</p>
<p />
<p>Code first design</p>
<p>Dependency Injection</p>
<p>IQueryable vs IEnumerable</p>
<p>Track changes</p>
<p>Cache</p>
<p>Migrations</p>

Database structure:

![image](https://github.com/kadisin/PieShopAdmin/assets/38622355/73803411-973a-4359-8255-88682482a2fe) 
<h4>Intresting notes: </h4>
<p>Track changes: This is one of the main function on Entity framework.</p>
<p>EF track changes on rows (add, modify, delete) </p> 
<p>Changes are related to existing object DbContext</p>
<div class="border" style="border-style: solid;">
    <p>var entries = context.ChangeTracker.Entries();</p>
    <p>foreach (var entry in entries) { Console.WriteLine("Entity Name: {0}", entry.Entity.GetType().Name); Console.WriteLine("Status: {0}", entry.State); }</p>
    <p>Entity Name: Student Status: Unchanged</p>
    <p>Entity Name: Enrollment Status: Added</p>
</div>
<p>When we only load data to see on page (Categories to dropdown) then we could use AsNoTracking() method</p>
<h4>IEnumerable vs IQueryable</h4>
<div class="border" style="border-style: solid;">
    <h5>IEnumerable load all data from database to memory then filter it</h5>
    <p>IEnumerable employeeIEnumberable = db.Employee.Where(a =>; a.JobTitle.StartsWith("P")); employeeIEnumberable = employeeIEnumberable.Take(5)</p>
    <p>Looks in sql: select * from HumanResources.Employee where JobTitle like 'P%'</p>
    <h5>IQueryable filtering data on database then load to application</h5>
    <p>IQueryable employeeIQueryable = db.Employee.Where(a => a.JobTitle.StartsWith("P")); employeeIQueryable = employeeIQueryable.Take(5)</p>
    <p>Looks in sql: select top(5) * from HumanResources.Employee where JobTitle like 'P%'</p>
    <p>IQueryable is optymalization on database side</p>
</div>
<h4>In-memory Caching</h4>
<div class="border" style="border-style: solid;">
    <p>Using IMemoryCache - stores data in memory server</p>
    <p>Set and use cache basicly on get data (load data to memory)</p>
    <p>Remove cache on add, update, delete data</p>
</div>
</div>
</body>
</html>
