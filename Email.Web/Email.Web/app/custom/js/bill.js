app.controller("BillController", function ($scope,$http, $routeParams) {
    $scope.id = $routeParams.id;
    $scope.list = [];
    $scope.toggle = function (scope) {
        scope.toggle();
    };
    $scope.nodeClick = function (scope) {
        console.log("a");
    };
    $http({
        method: "get",
        url: "/Angular/TreeData",
    }).then(
    function successCallback(response) {
        $scope.list = response.data;
      
    }, function errorCallback(response) {
        console.log(response);
    })
});