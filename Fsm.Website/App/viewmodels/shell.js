define(['plugins/router', 'durandal/app'], function (router, app) {
    return {
        router: router,
        search: function() {
            //It's really easy to show a message box.
            //You can add custom options too. Also, it returns a promise for the user's response.
            app.showMessage('Search not yet implemented...');
        },
        activate: function () {
            router.map([
                { route: '', moduleId: 'viewmodels/contents' },
                //{ route: 'flickr', moduleId: 'viewmodels/flickr', nav: true },
                //{ route: 'bluedress', title: 'Blue Dress', moduleId: 'viewmodels/bluedress', nav: true },
                { route: 'contents', title: 'Contents', moduleId: 'viewmodels/contents', nav: true },
                { route: 'book/:abbr', moduleId: 'viewmodels/book' }

            ]).buildNavigationModel();
            
            return router.activate();
        }
    };
});