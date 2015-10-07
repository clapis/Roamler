
(function (window, $) {
    'use strict';

    window.roamler = window.roamler || {
        proxies: {},
        viewmodels: {}
    };


    $(function() {

        var proxy = new roamler.proxies.LocationProxy();

        var map = new roamler.viewmodels.Map('map-canvas');
        var searchBox = new roamler.viewmodels.SearchBox('search-box');
        var resultsBox = new roamler.viewmodels.ResultsBox('results-box');

        function search(query) {

            // disable search button
            searchBox.disableSearch(true);
            resultsBox.clearResults();

            proxy.search(query)
                .done(function (data) {
                    map.setSearchResults(query, data.results);
                    resultsBox.setSearchResults(query, data.results);
                })
                .fail(function (data) {
                    console.log(data);
                    alert('Something went wrong :(');
                })
                .always(function () {
                    // re enable search button
                    searchBox.disableSearch(false);
                });
        }

        searchBox.onSearch(search);

    });


})(window, jQuery);
