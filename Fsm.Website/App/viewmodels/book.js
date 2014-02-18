define(['knockout', 'canon'], function (ko, canon) {
    var displayName = ko.observable();
    var chapters = ko.observableArray();
    return {
        displayName: displayName,
        chapters: chapters,
        update: function (abbr) {
            var that = this;
            var newBook = ko.utils.arrayFilter(canon.books(), function (item) {
                return item.abbreviation == abbr;
            })[0];
            if (newBook != null)
            {
                displayName(newBook.name)
                that.chapters(newBook.chapters);
            }
        },
        activate: function (abbr) {
            var that = this;
            this.update(abbr);
            canon.books.subscribe(function () {
                that.update(abbr);
            });
        }
    };
});