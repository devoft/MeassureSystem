# Introduction 
Easy to use Meassures on C# code as it can be seen in the next sections:

- [Units](https://github.com/devoft/MeassureSystem#units)
  - [Time](https://github.com/devoft/MeassureSystem#time)
  - [Pixel](https://github.com/devoft/MeassureSystem#pixel)
  - [Length](https://github.com/devoft/MeassureSystem#length)
  - [Area](https://github.com/devoft/MeassureSystem#area)
  - [Volume](https://github.com/devoft/MeassureSystem#volume)
  - [Weight](https://github.com/devoft/MeassureSystem#weight)
- [General features](https://github.com/devoft/MeassureSystem#general-features)
  - [ToString](https://github.com/devoft/MeassureSystem#ToString)
  - [Parsing](https://github.com/devoft/MeassureSystem#Parse)
  - [IComparable](https://github.com/devoft/MeassureSystem#IComparable)
  - [Linq query expressions](https://github.com/devoft/MeassureSystem#Linq-query-expressions) _**NEW!**_
  
If you want to know more about this and other projects visit us at [devoft](http://www.devoft.com)
  
## Units
### Time
```CSharp
Time s = 2.h();                           // 2h
TimeSpan tm = 3.min();                    // 00:03:00
Time s = (Time) TimeSpan.FromHours(1);    // 3600s
TimeSpan s = 2.h() + 20.min() + 90.s();   // 02:21:30
var (h, min, s, m) = 200.s();             // (0,3,20,0)
```
### Pixel
```CSharp
Pixel px = 3.px();                        // 3px
px += 2.inch();                           // 195px
button.Width = px;                        // 195
```
### Length
```CSharp
Length l = new Length(6);                 // 6m
Length l = 5.cm();                        // 5cm
Length l = 2.cm() + 5.dm()                // 0.52m
var l1 = -2.inch()                        // -2in
var l = 2.km() + (Length)"20in"           // 2000.508m                
```
### Area
```Csharp
Area surf = 5.m() * 2.m();                // 10m2       
```
### Volume
```Csharp
Volume lt1  = 5.cm() * 2.m() * 7.dm();    // 0.07m3       

Area   surf = 5.m() * 20.m();             // 100m2       
Volume lt2  = 5.m() * surf;               // 500m3       
```
### Weight
```CSharp
Weight g = 5.lb();                              
bool b = 5.lb() > 4.5m.kg();                  
```
## General features
The following applies to every meassure unit:
### ToString
```CSharp
string m = 500.cm();                      // "500cm"
string m = 500.dm();                      // "500dm"
string m = 500.cm() + 500.dm();           // "55m"
string m = 5.cm().ToString("N02");        // "5.00cm"
```
### Parse
```CSharp
Length l1 = Length.Parse("3cm");          // 3cm
Length l2 = (Length)"20cm";               // 20cm
var l3 = 2.km() + (Length) "20m";         // 2020m
```
### IComparable
If such a class `Job` is defines as follows:
```CSharp
class Job { public Time Duration { get; set; }}
```
and having a list of jobs:
```CSharp
var jobs = new [] { 
  new Job { Duration = 3.min() }, 
  new Job { Duration = 3.d() }, 
  new Job { Duration = 3.h() } 
}
```
Then `OrderBy` can be used with **`Time`** properties like `Duration`:
```CSharp
var sortedJobs = jobs.OrderBy(j => j.Duration);
```
> It is not available on EntityFramework(Core) or LinqToSql queries yet, but...

### Linq query expressions
Units expressions can be used as part of query expressions even with EntityFramework and LinkToSql as in the following example:
```CSharp
var job = from job in myContext.Jobs
          where job.Duration > 2.min() // Duration is of type TimeSpan
          select job;
```
#### Known issue
As long as EntityFramework(Core) and other ORMs do not support mappings to User Defined Types (UDT on Sql Servers), unit types cannot be used as type of properties in entity definitions. Despite that, it's still possible to use Units in queries meeting the following requirements:
- Units cannot be used as type of entity properties. 

  **Solution** We recomend to define property of entities of the following types:
  - `TimeSpan` where you would want `Time`.
  - `Decimal` where you would want `Length`, assuming that values are in meters.
  - `Decimal` where you would want `Area`, assuming that values are in squared meters.
  - `Decimal` where you would want `Volume`, assuming that values are in cubic meters.
  - `Decimal` where you would want `Weight`, assuming that values are in grams.
  - `Int32` where you would want `Pixel`.
- Clausured variables and unit types may not appear in the same expression. However, convertions to non-unit types can solve the issue in many cases:
  ##### Example 1 
  In this example `job` is the clausure and `.min()` is of a unit type:
  ```CSharp 
  myContext.Jobs.Where(job => job.Time.min() > 5.min()) // Time is int
  ``` 
  **Solution** Convert unit expressions to non-unit types (`.Minutes`):
  ```CSharp 
  myContext.Jobs.Where(job => job.Time > 5.min().Minutes) // Time is int
  ``` 
  ##### Example 2
  In this example the unit type appears in a Type Cast:
  ```CSharp
  myContext.Jobs.Where(job => (Time) job.Duration > 5.min()) // Duration is of pe TimeSpan
  ```
  **Solution 1** Cast to non-unit types instead:
  ```CSharp 
  myContext.Jobs.Where(job => job.Duration > (TimeSpan) 5.min())
  ``` 
  **Solution 2** Consider implicit conversions to non-unit types. In this case `Time` can be implicitly converted to `TimeSpan`, so the cast is not needed:
  ```CSharp 
  myContext.Jobs.Where(job => job.Duration > 5.min())
  ``` 

# Contributions
This project exists thanks to all the people who contribute:
[mariodvm](https://github.com/mariodvm), [johnnamdez](https://github.com/johnnamdez)

If you want to learn more about this and other projects visit us at [devoft](http://www.devoft.com)
