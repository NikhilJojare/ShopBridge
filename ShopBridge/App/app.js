//Define an angular module for our app 
var ShopBridgesApp = angular.module("ShopBridgesApp", ["ngRoute", "ngFileUpload"]);


ShopBridgesApp.constant("LocalServiceURL", "http://localhost:64258");

//Define Routing for app
ShopBridgesApp.config(['$routeProvider', '$locationProvider',
    function ($routeProvider, $locationProvider) {
        $locationProvider.hashPrefix('');
        $locationProvider.html5Mode({
            enabled: false,
            requireBase: true
        });
        $routeProvider.when("/", {
            templateUrl: '/template/ItemDashboard.html',
            controller: 'ItemController'
        }).when("/items", {
            templateUrl: '/template/ItemDashboard.html',
            controller: 'ItemController'
        }).when("/AddItems", {
            templateUrl: '/template/AddItems.html',
            controller: 'ItemController'
        }).when("/ViewItem/:ItemId", {
            templateUrl: '/template/ViewItem.html',
            controller: 'ItemController'
        }).otherwise({
            redirectTo: "/"
        });
    }]);

ShopBridgesApp.directive('validFile', function () {
    return {
        require: 'ngModel',
        link: function (scope, el, attrs, ngModel) {
            //change event is fired when file is selected
            el.bind('change', function () {
                scope.$apply(function () {
                    ngModel.$setViewValue(el.val());
                    ngModel.$render();
                });
            });
        }
    }
});

ShopBridgesApp.directive('myUpload', function () {
    return {
        restrict: 'A',
        link: function (scope, elem, attrs) {
            var reader = new FileReader();
            reader.onload = function (e) {
                scope.image = e.target.result;
                scope.$apply();
            }
            elem.on('change', function () {
                reader.readAsDataURL(elem[0].files[0]);
            });
        }
    };
});

