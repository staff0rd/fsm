define(['knockout', 'canon', 'plugins/router', 'models/book'], function (ko, canon, router, Book) {
    var abbr = ko.observable();
    var book = ko.observable(new Book());
    var next = ko.observable();
    var prev = ko.observable();
    var chapter = ko.observable();

    var scrollToSelected = function (element, index, data)
    {
        if (chapter() && index.number == chapter())
        {
            setTimeout(function () {
                console.log($(element[1]).offset().top);
                console.log($("#4").offset().top);

                $('html, body').animate({
                    scrollTop: $(element[0]).next().offset().top - 100
                }, 2000);

            }, 200);
        }
    }

    function update() {
        var newBook = canon.getBookByAbbr(abbr());
        if (newBook) {
            book(newBook);
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
        scrollToSelected: scrollToSelected,
        chapter: chapter,
        activate: function (abbr, chapter) {
            this.abbr(abbr);
            this.chapter(chapter);
        }
    };
});