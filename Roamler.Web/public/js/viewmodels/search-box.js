(function (roamler, $) {
    'use strict';

    roamler.viewmodels.SearchBox = function (elementId) {

        var $form = $('#' + elementId + ' form');

        $form.submit(function (event) {
            event.preventDefault();
        });

        function getFormData() {
            return {
                latitude: +$('input[name=latitude]', $form).val(),
                longitude: +$('input[name=longitude]', $form).val(),
                distance: +$('input[name=distance]', $form).val(),
                top: +$('input[name=top]', $form).val()
            };
        }

        function disableSearch(disabled) {
            $(':input', $form).prop('disabled', disabled);
        }

        function subscribe(fn) {

            $form.submit(function() {
                var data = getFormData();
                fn(data);
            });

        }

        return {
            onSearch: subscribe,
            disableSearch: disableSearch
        };

    };

})(roamler, jQuery);