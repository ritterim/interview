# Ayal Ciobotaru's Technical Interview

Some of the documentation I referenced in order to get the project done:

https://www.newtonsoft.com/json/help/html/Introduction.htm

https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/index.html

stackoverflow.com


## Requirements
1. Docker
1. Docker-compose 
1. .NET Core (Built with 3.1.402)

## How to run:

1. From a command line, navigate to interview\ReadingJson
1. Run the command `docker-compose up` (this could take a few minutes as Elasticsearch starts up)
1. In a new command prompt, navigate to interview/ReadingJson
1. Run command `dotnet restore`
1. Once the cluster health has changed to green, run the command `dotnet run` in the second command prompt