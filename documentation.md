In this document I'm going to just write down my thinking as I make design decisions about this project. 

Immediately, I'm hit with apprehension about how I'm going to name whatever this is that I'm making. It seems like I'm going to be creating a little analytics dashboard for business users. So, maybe I'll call the domain project "CustomerAnalytics." I'm not a huge name of the names I picked but it is code so we can change it at any point. 

Architecture
===

My goal is to create a library that can be used by whatever front end and back end we want. The core logic piece will be independent of any dependencies. I have a fondness for the [Onion Architecture](http://jeffreypalermo.com/blog/the-onion-architecture-part-1/) because it allows you to delay implementation details and seamlessly switch out dependencies. 

After I've written that piece of it I will consume it with a little console app that will read from the json data, pass it to the logic, display it on the screen, and persist the information. If this was a larger app I would take a bit more time to make sure that those pieces were tested and had a good separation of concerns. I've tested console apps [before](https://github.com/Sobieck/SnippetSpeed) but that would be over engineering for this project. 

Implementation
===

The logic library is going to be a .netstardard library and I'm going to write it test first. I've been thinking about how I want users to consume this library and I honestly think it is a toss up between having all public methods for each question, or just producing one big results object. Producing one big result forces all consumers to get all the answers but it is easy to write and consume. A bunch of tiny methods would be nice so each consumer would have complete control (Does the information make sense alone without the other answers to give it context?) but it will be a little harder to write. I'm going to go down the path of writing a bunch of tiny methods because the questions are not very cohesive and I want to be able to pass in options and not have any values hard coded. 

### Question 1

The first question was really straight forward and I think I have it solved with 4 tests. 

### Question 2

This question is a bit more tricky. 
- What happens if there are no customers who meet those two requirements? 
    - I'm just going to return null in that case. I was thinking I could do something like F#'s Option type but why over complicate it. 

### Question 3

How am I going to present this information? I could create a new type that has a name and count and return a collection of those. Or, I could use a Dictionary<string, int> to represent the result. I'm going to go with the type because it will be a little easier to understand for the end user.  

I decided to start testing these functions with random test data produced from a little nuget package that my friend and I made. I find that writing tests by making a lot of example code to be tedious. 

### Question 4

This one should be relatively simple. 

### Question 5

The data needs to be in decimal format for this question, but the string in the json data has $ in it. This will lead to some issues when deserializing our json but I will handle that issue in that layer. This layer shouldn't be concerned about how the data is represented elsewhere. 

### Question 6

This should be easy. It is just going to be a FirstOrDefault on that id and then a concatenation of the came. 

### Console App

I need to have a DTO, because the $ sign in the balance field is going to not be parsable. So, I'm going to deserialize that as a string, and then do a little string manipulation and convert it to a decimal. I'm making it an extension method because it will just be easier to use. And, I'm not going to test it because I've spent too much time already on this (this makes me sad). 

After doing that I just called all the methods in an anonymous type, converted it to a pretty json string and printed it on the screen. I then saved it to a results.json as my "database."  

### End notes

I likely over did it a bit with this little application. But I had a lot of fun writing it. Thanks for giving me this opportunity! 

Libraries Used
---

- [Fluent Assertions](https://github.com/fluentassertions/fluentassertions)
- [Random Test Values](https://github.com/RasicN/random-test-values)
- [Newtonsoft.Json](http://www.newtonsoft.com/json)

