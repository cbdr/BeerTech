function UserCtrl($scope, $http) {
    $scope.signedin = false;
    $scope.username = "";

    $scope.CheckLogin = function () {
        $http({
            method: 'GET',
            url: '/User/CheckLogin'
        }).success(function (data, status, headers, config) {
            $scope.signedin = data.signedin;
            $scope.username = data.username;
        });
    }

    $scope.CheckLogin();

    $scope.SignIn = function () {
        if ($scope.email != null && $scope.email != "" && $scope.password != null && $scope.password != "") {
            var xsrf = $.param({ email: $scope.email, password: $scope.password });
            $http({
                method: 'POST',
                url: '/User/Login',
                data: xsrf,
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
            }).success(function (data, status, headers, config) {
                $scope.signedin = data.signedin;
                if ($scope.signedin) {
                    $scope.username = $scope.email;
                }
            });
        }
    }

    $scope.SignUp = function () {
        if ($scope.email != null && $scope.email != "" && $scope.password != null && $scope.password != "") {
            var xsrf = $.param({ email: $scope.email, password: $scope.password });
            $http({
                method: 'POST',
                url: '/User/Create',
                data: xsrf,
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
            }).success(function (data, status, headers, config) {
                $scope.signedin = data.signedin;
                if ($scope.signedin) {
                    $scope.username = $scope.email;
                    $scope.email = "";
                    $scope.password = "";
                }
                else {

                }
            });
        }
    }

    $scope.SignOut = function () {
        $http({
            method: 'GET',
            url: '/User/Logout'
        }).success(function (data, status, headers, config) {
            $scope.signedin = data.signedin;
        });
    }
}
