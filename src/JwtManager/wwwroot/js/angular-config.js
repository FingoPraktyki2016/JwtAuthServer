angular
    .module('managerApp', ['ngRoute'])
    .config([
        '$routeProvider',
        function config($routeProvider) {
            $routeProvider
                .when('/',
                {
                    templateUrl: 'customer-list.html'
                })
                .when('/customer/:customerId',
                {
                    templateUrl: 'customer-details.html'
                })
                .otherwise(
                {
                    redirectTo: '/'
                });
        }
    ]);