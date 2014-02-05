function ResetCtrl($scope, $http) {
    $scope.email = useremail;
    $scope.password = "";
    $scope.confirm = "";
    $scope.msg = "";
    $scope.sent = false;
    $scope.success = false;

    $scope.ResetPassword = function () {
        if ($scope.email != null && $scope.email != "") {
            var xsrf = $.param({ email: $scope.email, password: $scope.password });
            $http({
                method: 'POST',
                url: '/User/ResetPassword',
                data: xsrf,
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
            }).success(function (data, status, headers, config) {
                $scope.msg = data.msg;
                $scope.success = data.success;
                $scope.sent = true;
            });
        }
    }
}