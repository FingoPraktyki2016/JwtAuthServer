angular
    .module('managerApp')
    .component('customerDetails',
    {
        template: '<h3>Details for: {{$ctrl.customer.name}} //{{$ctrl.type}}</h3>',
        controller: function CustomerDetailsController($routeParams) {
            this.$onInit = function () {
                var id = $routeParams.customerId;
                this.customer = { id: id, name: "Customer #" + id };
                console.log($routeParams);
            };
        },
        bindings: {
            type: '@'
        }
    });