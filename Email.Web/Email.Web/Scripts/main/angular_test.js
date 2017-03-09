var app = angular.module('myApp', []);
app.controller('chat', function ($scope, $parse, $interpolate) {
    $scope.clock = new Date();
    $scope.names = ['mm', 'ym', 'ty', 'lyf', 'lss'];
    $scope.users = [{ id: 1, name: "aliulimei", nick: "ymeimei" }, { id: 1, name: "wangnan", nick: "nanan" }, { id: 1, name: "wusongyun", nick: "ws" }]
    $scope.isA = function (user) {
        return user.name[0] == 'a';
    };
});
app.controller('parent', function ($scope) {
    $scope.person = { age: 20 };

});
app.controller('child', function ($scope) {
    $scope.sayhello = function () {
        $scope.person.name = "llm";
    }

});