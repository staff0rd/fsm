define(['plugins/http', 'durandal/app', 'knockout', 'canon'], function (http, app, ko, canon) {
    return {
        displayName: 'Contents',
        books: canon.books,
        activate: function () {
            console.log("length: " + canon.books().length);
        },
        select: function (item) {
            //the app model allows easy display of modal dialogs by passing a view model
            //views are usually located by convention, but you an specify it as well with viewUrl
            item.viewUrl = 'views/detail';
            app.showDialog(item);
        },
    };
});