(function(roamler, google) {
    'use strict';

    roamler.viewmodels.Map = function(elementId) {

        var map;
        var infoWindow;
        var searchMarker;
        var resultMarkers = [];
        

        (function () {
            initMap(elementId);
        })();

        function initMap(elementId) {
            
            infoWindow = new google.maps.InfoWindow();
            
            map = new google.maps.Map(document.getElementById(elementId), {
                center: { lat: 0, lng: 0 },
                zoom: 2,
                disableDefaultUI: true
            });

            searchMarker = new google.maps.Marker({
                 icon: 'http://maps.google.com/mapfiles/ms/icons/blue-dot.png'
            });
        }

        function addResult(result) {

            var location = result.location;
            var description = result.distance.toFixed(2) + 'm - ' + location.address;

            var marker = new google.maps.Marker({
                position: { lat: location.coordinates.latitude, lng: location.coordinates.longitude },
                map: map,
                title: description
            });

            marker.addListener('click', function () {
                infoWindow.setContent(marker.title);
                infoWindow.open(map, marker);
            });

            resultMarkers.push(marker);

            return marker;
        }

        
        function clearLocations() {
            for (var i = 0; i < resultMarkers.length; i++) {
                resultMarkers[i].setMap(null);
                google.maps.event.clearInstanceListeners(resultMarkers[i]);
            }

            searchMarker.setMap(null);

            resultMarkers = [];
        }

        function setSearchResults(query, results) {

            clearLocations();

            var bounds = new google.maps.LatLngBounds();

            for (var i = 0; i < results.length; i++) {
                var marker = addResult(results[i]);
                bounds.extend(marker.position);
            }

            var center = { lat: query.latitude, lng: query.longitude };

            searchMarker.setPosition(center);
            searchMarker.setMap(map);
            bounds.extend(searchMarker.position);

            map.fitBounds(bounds);
        }

        return {
            setSearchResults: setSearchResults
        };

    }

})(roamler, google);