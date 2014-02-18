define(['knockout', 'canon', 'plugins/router'], function (ko, canon, router) {
    var abbr = ko.observable();
    var book = ko.observable();
    var next = ko.observable();
    var prev = ko.observable();

    function update() {
        var newBook = canon.getBookByAbbr(abbr());
        book(newBook);
        if (newBook) {
            prev(canon.getBookByNumber(newBook.number - 1));
            next(canon.getBookByNumber(newBook.number + 1));
        }
    }

    canon.books.subscribe(function () {
        update();
    });

    abbr.subscribe(function () {
        update();
    });

    return {
        abbr: abbr,
        next: next,
        prev: prev,
        book: book,
        activate: function (abbr) {
            this.abbr(abbr);
        }
    };
});