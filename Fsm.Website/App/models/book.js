define(function () {
    return function (number, name, abbreviation) {
        this.name = name;
        this.number = number;
        this.hash = '#book/' + abbreviation;
        this.abbreviation = abbreviation;
        this.chapters = [];
    };
});