var app = angular.module("emailApp", ["ngRoute"]);
app.run(function ($rootScope, $location) {
    $rootScope.$on("$locationChangeStart",function(evt,next,current){
        console.log("路由变化前");
    });
    $rootScope.$on("$locationChangeSuccess", function (evt, current, previous) {
        console.log("路由加载后");
    });
    });
app.controller("BillController", function ($scope, $routeParams) {
    $scope.id = $routeParams.id
});
app.config(['$locationProvider', '$routeProvider', function ($locationProvider, $routeProvider) {
    $locationProvider.hashPrefix('');
    $routeProvider
    .when("/bill/:id", { templateUrl: "views/bill.html", controller: "BillController" })
    .when("/girl", { templateUrl: "views/girl.html" })
    .otherwise({ redirectTo: '/' });
}]);