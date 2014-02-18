define(['plugins/http', 'durandal/app', 'knockout', 'models/book', 'models/chapter', 'models/verse'], function (http, app, ko, Book, Chapter, Verse) {
    var books = ko.observableArray();

    var getBookByAbbr = function (abbr) {
        var newBook = ko.utils.arrayFilter(books(), function (item) {
            return item.abbreviation == abbr;
        })[0];
        return newBook;
    }

    var getBookByNumber = function (number) {
        var newBook = ko.utils.arrayFilter(books(), function (item) {
            return item.number == number;
        })[0];
        return newBook;
    }

    var getBook = function (path)
    {
        var intRegex = /^\d+$/;
        if (intRegex.test(path))
            return getBookByNumber(number);
        else
            return getBookByAbbr(path);
    }

    $.getJSON('/api/canon/get', function (data) {
        var newBooks = [];
        $.each(data.Books, function (i, b) {
            var book = new Book(b.Number, b.Name, b.Abbreviation);
            $.each(b.Chapters, function (i, c) {
                var chapter = new Chapter(c.Number, c.Name);
                $.each(c.Verses, function (i, v) {
                    var verse = new Verse(v.Number, v.Text);
                    chapter.verses.push(verse);
                });
                book.chapters.push(chapter);
            });
            newBooks.push(book);
        });
        books(newBooks);
    });

    return {
        books: books,
        getBook: getBook,
        getBookByAbbr: getBookByAbbr,
        getBookByNumber: getBookByNumber
    };
});