angular
    .module('managerApp')
    .component('customersList',
    {
        template:
            '<ul>' +
                '<li ng-repeat="cust in $ctrl.customers">' +
                '<a href="#!/customer/{{cust.id}}">{{cust.name}}</a>' +
                '</li>' +
            '</ul>' +
            '<button ng-click="$ctrl.test()">CustomersList counter={{$ctrl.counterCpy}}</button>',
        controller: function CustomerListController($scope) {
            this.$onInit = function() {
                this.counterCpy = $scope.$parent.counter;
            }
            this.customers = [
                { id: 1, name: "Customer 1" },
                { id: 2, name: "Customer 2" },
                { id: 3, name: "Customer 3" },
                { id: 4, name: "Customer 4" },
                { id: 5, name: "Customer 5" }
            ];
            this.test = function() {
                $scope.$parent.counter += 2;
                this.counterCpy = $scope.$parent.counter;
            }
        }
    });