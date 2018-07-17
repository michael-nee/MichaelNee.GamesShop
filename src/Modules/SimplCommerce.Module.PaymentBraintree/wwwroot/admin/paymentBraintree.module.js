/*global angular*/
(function () {
    'use strict';

    angular
        .module('simplAdmin.paymentBraintree', [])
        .config(['$stateProvider',
            function ($stateProvider) {
                $stateProvider
                    .state('payments-braintree-config', {
                        url: '/payments/braintree/config',
                        templateUrl: 'modules/paymentBraintree/admin/braintree/braintree-config-form.html',
                        controller: 'BraintreeConfigFormCtrl as vm'
                    })
                ;
            }
        ]);
})();