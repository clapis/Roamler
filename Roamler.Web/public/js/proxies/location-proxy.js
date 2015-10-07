(function (roamler, $) {
    'user strict';

    roamler.proxies.LocationProxy = function () {

        function search(query) {

            var data = {
                coordinate: { latitude: query.latitude, longitude: query.longitude },
                maxDistance: query.distance,
                maxResults: query.top
            };

            return $.ajax({
                method: 'POST',
                url: '/api/location/search',
                data: JSON.stringify(data),
                contentType: 'application/json'
            });
        }

        return {
            search: search
        };
    }


})(roamler, jQuery);