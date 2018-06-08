In this document I'm going to just write down my thinking as I make design decisions about this project. 

Immediately, I'm hit with apprehension about how I'm going to name whatever this is that I'm making. It seems like I'm going to be creating a little analytics dashboard for business users. So, maybe I'll call the domain project "CustomerAnalytics."

Architecture
===

My goal is to create a library that can be used by whatever front end and back end we want. The core logic piece will be independent of any dependencies. I have a fondness for the [Onion Architecture](http://jeffreypalermo.com/blog/the-onion-architecture-part-1/) because it allows you to delay implementation details and seamlessly switch out dependencies. 

After I've written that piece of it I will consume it with a little console app that will read from the json data, pass it to the logic, display it on the screen, and persist the information. If this was a larger app I would take a bit more time to make sure that those pieces were tested and had a good separation of concerns. I've tested console apps [before](https://github.com/Sobieck/SnippetSpeed) but that would be over engineering for this project. 

Implementation
===

The logic library is going to be a .netstardard library and I'm going to write it test first. I've been thinking about how I want users to consume this library and I honestly think it is a toss up between having all public methods for each question, or just producing one big results object. Producing one big result forces all consumers to get all the answers but it is easy to write and consume. A bunch of tiny methods would be nice so each consumer would have complete control but it will be a little harder to write. If we want to change the API in the future, it would just mean changing interface to make my private methods public. 



Libraries Used
---

- [Fluent Assertions](https://github.com/fluentassertions/fluentassertions)
