# Packer Library

# Notes

* Makefile only works for windows platform

To build 

```csharp
    dotnet build
```

Testing

```csharp
    dotnet test /p:CollectCoverage=true
```

# Usage

```csharp
    try
    {
        var results = Packer.Pack(@".\ExampleWrongFormatDataTest");
    }
    catch (APIException ex)
    { 
        //Exception Handling
    }
```

# Components

## Api
Packer is the entry point to the API. 
It contains single static method which orchestrates the flow.
Also responsible for initializing configuration object.

```csharp
    var parser = new Parser();
    return parser.Read(filePath)
                    .PackAll( (p) => 
                    {
                        //Can implement custom algorithm using IPackage object.
                        //It is not opened to external usage because of project requirements
                        var st = new Knapsack(p);
                        st.Calculate();
                    })
                    .PrintResults();    

```
***
## packer

Contains all implementations


### Util.Parser
Parser is responsible for parfing provided file.
Firts file is opened and all lines parsed one by one.

Exceptions are thrown if parsing is not finished correcty.
All exceptions are encapsulated under APIException.

```csharp
    public List<IPackage> Read(string filePath)
```
I didn't used file stream within using block because I wanted to catch and capsulate system exceptions. So file stream closed manually.

```csharp
   private static Package ParseLine(string line)
```
Used regular expressin to match items within line string and parsed them.
Whole parse operation of a line is within a single try/catch block.
I did not distinguish parsing errors. 

***
### Strategy.Knapsak

Kanpsak problem solved by dynamic programminc approach 
[Knapsak](https://en.wikipedia.org/wiki/Knapsack_problem#Dynamic_programming_in-advance_algorithm)

Knapsak object takes iPackage object to make calculations.
There are two main parts :

- Generating solution matrix
- Finding selected items from solution matrix.

__Since weight and costs values are not integers I assumed precision will be double digits (.00) 
and multiplied values by 100 before processing.__

__This increased used memory allocation because my matrix size on weight dimension 
increased to 10.000 (when weight is 100)__

***
### Model.Package

Package object holds items to be packed. 
There are two conditions when exception is throwed :

1- Constuctor : When package weight exceeds configuration value
```csharp
    public Package(double weight)
```
2- AddItem : When max items count limit reached
```csharp
    public void AddItem(IItem i)
```

***
### Model.Item

Items are added to packages. 
There are two conditions when exception is throwed :

- Constuctor : When package weight or cost exceeds configuration value

```csharp
    public Item(int id, double weight, double cost)
```

***
### Exceptions

All exceptions are encapsulated within APIException.
Also static error messages added to make it easier to track.


***
### Config.Cfg

Holds configuration values and implements singleton pattern.
It provides static Instance method to access config instance.

```csharp
    public static IConf Instance
    {
        get
        {
            if (config == null)
            {
                config = new Conf(maxItemCost: 100, maxItemWeight: 100, maxPackageWeight: 100, maxItemCount: 15);
            }
            return config;
        }
    }
```
    It also provides Init method to set desired values for configuration.

***
### ListExtensions

Extension methods are added List<IPackage> type to help more fluent approach.

```csharp
     public static List<IPackage> PackAll(this List<IPackage> packages)
     public static List<IPackage> PackAll(this List<IPackage> packages, Action<IPackage> f)
     public static string PrintResults(this List<IPackage> packages)
``` 

***
## test

Test project tests two component

1- Api tests
Test done by creating file from hard-coded sample data. 
I didn't prefer using real files since they are neede to be delivered among sourcode.

2- Model tests
Model tests focus on Package and Item objects. 
Objects data consistency and business behavours are tested.

***
## Class Diagram

![Packer Class Diagram!](
http://www.plantuml.com/plantuml/png/jLZDRXit4BxxAOYSP4W6qWAzB1mrRc8NGYDOI0puKFH0BqSI4Iufa9Gb8_MxboEEY1rfjRPRrAVT-VdupS_CqleOelHntPYpsiWGn1H2a0i8ul8oQQAknsFnuqoalw7uaINFOYgtamZDFUZ5CefAXEYrNOWFuXs9t5n8AtIo4R2j4oIgZMvq3EzEW7roWKEXuDy0SP3aRLqKKYdHE0yYBfEaaLlTh1iXZN4RK3ly8A9BMX0hMNzF6UY2dsKL1XcNx9pVt5PiSkmr7Va0hHFff5FKlsWBC-ankclldMVOKsdcpZSonWIWP9HYe-DIMp7NvfZcJHBUhi0UusJ9PfaCHFHFA8rEU92Aa9wf1wvca-kKx5NKpkwBpuI39dT9bS5_7GbjBVYYhyhJUPTtlt7_KUU9PL6AoxylBfWvHV15XtYzZM13alMsCfRqofW1LJrKw7eveKgFprC6kUID02CnhwvgfFOcq1Hh_0fXRMBeeKR5BOL35VZZ8Elv8XbFFZaxxw8_mFBFP5nIaZkGfqw1kOCGmnwz9Hhi2xUROmzoLpX4UNHQjNJVm40TGNNfxfRURK8fv6IULydl20V-BWtHJ-_VlyeH5qU_6wNmo9FDzoHSFRX7U8Fxr7c8UE8lo2jSAGNgl_YcUeNertLq_kSJE3ixzFYiGoqzlXGe5isXF_Q2Njm-WlTQmRtptqCnEUWdLdQIyRfVRsMpCZ25nlcdpp9AL1SuRdJldLtSxFRPAKlSXE0fHRYqxfTWSQzP5s_SsgfZfXWEKRmG3VkYABT-CC1LkRSwb2LdtNh2NLlKQHkpeZLecDKoQwJ5nq6BKvL1HumCan8Bhc37L-dmyyDPdjZqP6lOdvyMtmBYU7nif-F_GkXWoKmxwi0viPj4dn4Zq0l6wbCCdiye8_Z1s55CMl1pMOEOj9hdHSezPnHrVpmlH_FS9eTmHEuqgudgu389j6zH-U2SOTgPTtDz0gwFViOi4yZ9Kc345wa7x8cl8DMjDK_v0uRYCSDyHNNJeaE_WQvs_cnZO34oeqIbJL8hR4jcU-U-xRviWuwFN2lL5q8hDkARperGTwoO5KyiIGClHV6tyuEXGH94V2GsVE3mE7hP3t__HFIDNby33BixQlX2JmqxkcDNhZnt-4IfBetIcrvD3TJgczE98yW1ozbvdtAK3wXQy6JVssx3ZeORlhxP2vdUqgKtHn_dyRYhQyciizCnxNH8pZozj4Vr3ojN8OdtkqMQUcraXEDlAxV43YVegWmyAMgPL3e-bSJq5r7pHSfQ8MJPdt-r9krGjl9GQxpXKwj-4EcoHZgFb-VYCSekmnBBW6AeA8BIACTv8C0RRSnKHg-tnnghzbE1uR6BLCbpBiJ-ZIzD7ygj7RkdwpxEx_wbDintIA_bNlPAC7w9oI3u7-d0Q0ijDtB0gEXSQYvyFli8Lk7l2Fy0)
