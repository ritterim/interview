# Introduction

This project contains a simple console app solution for the [RIMdev technical interview](https://github.com/ritterim/interview).

Simply follow the documentation below to run the .NET Core 3.1 LTS console app on your local machine or online using [Katacoda](https://www.katacoda.com/).

## Run the Console App

Run the following commands on your local machine in the terminal:
```bash
git clone https://github.com/portfolio-site-demo/interview.git
cd interview/PortfolioSiteExample/PortfolioSiteExample.ConsoleApp
dotnet run
```

**Optional:** Run the Console App using Katacoda.
1. Navigate to: https://www.katacoda.com/courses/container-runtimes/what-is-a-container
2. Select `START SCENARIO`.
3. Install .NET Core 3.1 by running the following commands in the terminal:
	```bash
	sudo apt-get install -y gpg
	wget -qO- https://packages.microsoft.com/keys/microsoft.asc | gpg --dearmor > microsoft.asc.gpg
	sudo mv microsoft.asc.gpg /etc/apt/trusted.gpg.d/
	wget -q https://packages.microsoft.com/config/ubuntu/19.04/prod.list
	sudo mv prod.list /etc/apt/sources.list.d/microsoft-prod.list
	sudo chown root:root /etc/apt/trusted.gpg.d/microsoft.asc.gpg
	sudo chown root:root /etc/apt/sources.list.d/microsoft-prod.list
	sudo apt-get install -y apt-transport-https
	sudo apt-get update
	sudo apt-get install -y dotnet-sdk-3.1
	# Original Source: https://dotnet.microsoft.com/download/linux-package-manager/ubuntu19-04/sdk-current
	```
4. Run the Console App:
	```bash
	git clone https://github.com/portfolio-site-demo/interview.git
	cd interview/PortfolioSiteExample/PortfolioSiteExample.ConsoleApp
	dotnet run
	```

## Example Output

```
What is the count of individuals over the age of 50?
55

Who is the last individual that registered who is still active?
Wolf, Ramsey

What are the counts of each favorite fruit?
strawberry: 31
apple: 38
banana: 31

What is the most common eye color?
blue

What is the total balance of all individuals combined?
$261,601.15

What is the full name of the individual with the id of 5aabbca3e58dc67745d720b1 in the format of lastname, firstname?
Saunders, Lourdes
```

## Technical Details

The following logic is implemented in [PortfolioSiteExample.ConsoleApp/Program.cs](https://github.com/portfolio-site-demo/interview/blob/master/PortfolioSiteExample/PortfolioSiteExample.ConsoleApp/Program.cs):
1. Initialize the app settings.
2. Load the records into memory and serialize the JSON into C# objects.
   - Note: The JSON was converted from to C# using https://json2csharp.com.
3. Use LINQ to create expressions to find the answer to each question.
4. Print out each answer to the console.

## Opportunities for Improvement

- Allow the user to enter query input values to make the responses more dynamic.  One option is to implement something like [GraphQL](https://graphql.org/) to provide access to specific data queries.
- Reduce code duplication by using lambda expressions that may be passed to a generic method that retrieves the answer.
- If processing millions of records, instead of loading all the data in memory, use a JSON import stream directed to a segmented bulk database insert process.  Then, query the database using LINQ for greater efficiency.

## Bonus

See a separate implementation using [Docker Compose](DockerComposeApp.md) to persist the result set to a database.