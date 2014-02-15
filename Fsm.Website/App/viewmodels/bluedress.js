define(['plugins/http', 'durandal/app', 'knockout'], function (http, app, ko) {
    var http = require('plugins/http'),
        ko = require('knockout');

    var url = 'http://api.flickr.com/services/feeds/photos_public.gne';

    var qs = {
        tags: 'blue dress',
        tagmode: 'any',
        format: 'json'
    };

    return {
        displayName: 'Blue Dress',
        viewUrl: 'views/flickr',
        images: ko.observableArray([]),
        activate: function () {
            var that = this;
            if (this.images().length > 0) {
                return;
            }

            return http.jsonp(url, qs, 'jsoncallback').then(function (response) {
                that.images(response.items);
            });
        },
        select: function (item) {
            item.viewUrl = 'views/detail';
            app.showDialog(item);
        },
    };
});