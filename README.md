![Loose Canon](/screenshot.PNG?raw=true)

This was intended to be a [durandal](http://durandaljs.com/)-based web-reader for anything that was broken up into books/chapters/verses with the following features;

* Book, chapter and verse lookup via url (ie; http://whatever/#book/abbr/chapter/verse) where;
 * *abbr* is an abbreviation of the book
 * *chapter* is a chapter number
 * *verse* is a verse number
* Verse ranges could be referenced from other locations, and then highlighted when the application was passed the ranges
* Filtering, where entire passages could be created based on references to the underlying data via the book/chapter/verse convention
* Searches that return book/chapter/verse references
* Lots more

However, then I ended up spending much more time writing (and then not finishing) the data scraper for the Church of the Flying Spaghetti Monster's [Loose Canon](http://www.loose-canon.info/) 
that I never ended up implementing all the sweet durandal features I was hoping for.

I can note that the web project using ASP.NET MVC couldn't be more overkill, and the DataScraper turned into something far less dynamic and structured than I had originally intended and is close to shameful.