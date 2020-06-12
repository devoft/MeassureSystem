# Introduction 
Easy to use Meassures on C# code as it can be seen in the next sections:

- [Units](https://github.com/devoft/MeassureSystem#units)
  - [Length](https://github.com/devoft/MeassureSystem#length)
  - [Volume](https://github.com/devoft/MeassureSystem#volume)
  - [Weight](https://github.com/devoft/MeassureSystem#weight)
  - [Time](https://github.com/devoft/MeassureSystem#time)
- [General features](https://github.com/devoft/MeassureSystem#general-features)
  - [ToString](https://github.com/devoft/MeassureSystem#ToString)
  - [Parsing](https://github.com/devoft/MeassureSystem#Parse)
  
## Units
### Length
```CSharp
Length l = new Length(6);                      // 6m
Length l = 5.cm();                             // 5cm
Length l = 2.cm() + 5.dm()                     // 0.52m
var l1 = -2.cm()                               // -2cm
var l = 2.km() + (Length)"20in"                //2000.508m                
```

### Volume
```Csharp
Volume lt = 5.cm() * 2.m() * 7.dm();          //0.07m3       
```
### Weight
```CSharp
Weight g = 5.lb();                              
bool b = 5.lb() > 4.5m.kg();                  
```
### Time
```CSharp
Time s = 2.h();                                  // 2h
TimeSpan tm = 3.min();                                   // 00:03:00
Time s = (Time) TimeSpan.FromHours(1);   // 3600s
TimeSpan s = 2.h() + 20.min() + 90.s();                  // 02:21:30
var (h, min, s, m) = 200.s();                            // (0,3,20,0)
```
## General features
The following applies to every meassure unit:
### ToString
```CSharp
string m = 500.cm();                         // "500cm"
string m = 500.dm();                         // "500dm"
string m = 500.cm() + 500.dm();              // "55m"
string m = 5.cm().ToString("N02");           // "5.00cm"
```
### Parse
```CSharp
Length l1 = Length.Parse("3cm");                // 3cm
Length l2 = (Length)"20cm";                     // 20cm
var l3 = 2.km() + (Length) "20m";               // 2020m
```

# Contributions
This project exists thanks to all the people who contribute:
[mariodvm](https://github.com/mariodvm), [johnnamdez](https://github.com/johnnamdez)

If you want to learn more about this and other projects visit us at [devoft](http://www.devoft.com)
