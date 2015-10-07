(function(roamler, $) {
    'use strict';

    roamler.viewmodels.ResultsBox = function(elementId) {

        
        var $box = $('#' + elementId);
        var $info = $('.info', $box);
        var $table = $('table', $box);
        var $tbody = $('tbody', $table);
        

        (function() {
            clearResults();
        })();

        function addResult(result) {

            var $row = $('<tr>');

            $row.append($('<td>').text(result.distance.toFixed(2) + ' m'));
            $row.append($('<td>').text(result.location.address));

            $tbody.append($row);
        }

        function clearResults() {
            $box.hide();
            $tbody.empty();
            $info.empty();
        }

        function setSearchResults(query, results) {

            $info.text(results.length + " results found");

            for (var i = 0; i < results.length; i++) {
                addResult(results[i]);
            }

            $table.toggle(results.length !== 0);

            $box.fadeIn();
        }

        return {
            clearResults: clearResults,
            setSearchResults: setSearchResults
        };
    }

})(roamler, jQuery);