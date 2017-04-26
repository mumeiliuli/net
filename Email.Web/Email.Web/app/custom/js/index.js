var app = angular.module("emailApp", ["ngRoute","ui.tree"]);
app.run(function ($rootScope, $location) {
    $rootScope.$on("$locationChangeStart",function(evt,next,current){
        console.log("路由变化前");
    });
    $rootScope.$on("$locationChangeSuccess", function (evt, current, previous) {
        console.log("路由加载后");
    });
    });

app.config(['$locationProvider', '$routeProvider', function ($locationProvider, $routeProvider) {
    $locationProvider.hashPrefix('');
    $routeProvider
    .when("/bill/:id", { templateUrl: "views/bill.html", controller: "BillController" })
    .when("/girl", { templateUrl: "views/girl.html" })
    .otherwise({ redirectTo: '/' });
}]);