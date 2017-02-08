angular
    .module('managerApp')
    .controller('customerController',
        function ($scope, $http, apiFactory) {
            $scope.loadAll = function() {
                $http.get('http://localhost:/api/get')
                    .then(function(response) {
                        $scope.customers = response.data;
                        $scope.loaded = true;
                    });
            }
            $scope.loadCustomer = function (id) {
                $scope.loaded = false;
                apiFactory.apiGet(
                    'http://localhost:52418/api/version',
                    function(response) {
                        $scope.customerDetails = { id: id, name: response.data };
                        $scope.loaded = true;
                    });
            };
        });