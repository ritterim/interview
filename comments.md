# Comments

## Instructions

### Prerequisites

* .NET core runtime (project built with version 3.1.401)

### Install and Execute

* To run: clone repo, restore packages, and build (or just run) and execute the following from a terminal/command prompt/Powershell:

```
dotnet restore

dotnet build

dotnet run
```

This can also all be done via Visual Studio using Solution Explorer/Package Manager Console and the Build/Debug options.

## Technical Choices

### Program.cs

* Main method of console application cannot be async since it cannot return to another caller. So we use a separate MainAsync method to allow asynchronous operations and wait on the Task returned from MainAsync() to complete

* Async operations are ideal here since we're dealing with I/O operations and do not want to block the thread from continuing other work. In a real world example this would be something like setting up UI components while waiting on data

* Uses Newtonsoft.Json to deserialize the string representation of the json in data.json into an object to easily access properties. The only non-framework assembly used in the project

### models/Data.cs

* Basic POCO class to represent the data contained in data.json

### models/Name.cs

* Another POCO class. This level of abstraction may be unnecessary as first and last name could just be separate properties on Data.cs, but this more closely follows the structure of the json in data.json

### services/DataService.cs

* Basic class with a single private member which holds a list of Data objects and is assigned via the public constructor

* The class constructor takes an IEnumerable as a parameter since we only really need read access to the collection and aren't doing any manipulation. Additionally if the data set were very large, IEnumerable saves memory by not initially loading the entire collection into memory and may defer execution until the enumerable is acted upon possibly optimizing along the way

* The only use of a List is when building a collection of key/value pairs for getting counts of favorite colors

* Holding the data inside a private property available to the class methods prevents the need from having to pass the necessary data into every method. This could get tricky when it comes to managing state in a larger application, but for our purposes here this is an acceptable choice

* Overall this class is very tightly coupled to a specific use-case. There's not much abstraction here 