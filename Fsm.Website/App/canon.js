define(['plugins/http', 'durandal/app', 'knockout', 'models/book', 'models/chapter', 'models/verse'], function (http, app, ko, Book, Chapter, Verse) {

    var books = ko.observableArray();

    $.getJSON('/api/canon/get', function (data) {
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
            books.push(book);
        });
    });
    return {
        books: books,
    };
});