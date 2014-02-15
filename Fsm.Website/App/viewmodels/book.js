define(['plugins/http', 'durandal/app', 'knockout', 'canon'], function (http, app, ko, canon) {
    var displayName = ko.observable();
    var chapters = ko.observableArray();
    return {
        displayName: displayName,
        chapters: chapters,
        activate: function (abbr) {
            console.log("Book count: " + canon.books().length);
            $.each(canon.books(), function (index, value) {
                if (value.abbreviation == abbr) {
                    displayName(value.name);
                    chapters(value.chapters);
                    return false;
                }
            });
        }
    };
});