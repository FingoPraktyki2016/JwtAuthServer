angular
    .module('managerApp')
    .factory('apiFactory',
        function($http) {
            function apiGet(apiUrl, callback) {
                $http.get(apiUrl)
                    .then(
                        callback,
                        function () {
                            $('#apiError').slideDown(500).delay(5000).slideUp(500);
                        });
            }

            var service = {
                service: 'API Factory',
                apiGet: apiGet
            }

            return service;
        });