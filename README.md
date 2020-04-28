# Introduction 
Easy to use Meassures on C# code as it can be seen in the next sections:

- [Units](https://github.com/devoft/MeassureSystem#units)
  - [Length](https://github.com/devoft/MeassureSystem#length)
  - [Volume](https://github.com/devoft/MeassureSystem#volume)
  - [Weight](https://github.com/devoft/MeassureSystem#weight)
- [General features](https://github.com/devoft/MeassureSystem#general-features)
  - [ToString](https://github.com/devoft/MeassureSystem#ToString)
  - [Parsing](https://github.com/devoft/MeassureSystem#Parse)
  
# Units
## Length
```CSharp
Meter m = new Meter(6);                      // 6m
Meter m = 5.cm();                            // 5cm
Meter m = 2.cm() + 5.dm()                    // 0.52g
var m1 = -2.cm()                             // -2cm
var m = 2.km() + "20in"                      
```
## Volume
```Csharp
Metre3 lt = 5.cm() * 2.m() * 7.dm()           
```
## Weight
```CSharp
Gram g = 5.lb()                              
bool b = 5.lb() > 4.5.kg()                   
```
## Time
```CSharp
Seconds s = 2.h()                           // 2h
TimeSpan tm = 3.min()                       // 00:03:00
Seconds s = (Seconds) TimeSpan.FromHours(1) // 3600s
TimeSpan s = 2.h() + 20.min + 90.s()        // 02:21:30
```
# General features
The following applies to every meassure unit:
## ToString
```CSharp
string m = 500.cm();                         // "500cm"
string m = 500.dm();                         // "500dm"
string m = 500.cm() + 500.dm()               // "0.55m"
string m = 5.cm().ToString("N02")            // "5.00cm"
```
## Parse
```CSharp
Meter m = Meter.Parse("3cm")                 // 3cm
Meter m = (Meter) "20cm"                     // 20cm
var m = 2.km() + (Meter) "20m"               // 2020m
```

# Contribute
Help us create more meassure units and relationships among them by extensivly use of fancy C# language features.
