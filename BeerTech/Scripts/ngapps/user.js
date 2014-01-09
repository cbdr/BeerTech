function UserCtrl($scope, $http) {
    $scope.signedin = false;
    $scope.error = false;
    $scope.username = "";
    $scope.msg = "";

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
                if (data.signedin == null) {
                    $scope.signedin = true;
                }
                else {
                    $scope.error = true;
                    $scope.msg = data.msg;
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
                if (data.signedin == null) {
                    $scope.signedin = true;
                    $scope.username = $scope.email;
                    $scope.email = "";
                    $scope.password = "";
                }
                else {
                    $scope.error = true;
                    $scope.msg = data.msg;
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

function getCookie(c_name) {
    var c_value = document.cookie;
    var c_start = c_value.indexOf(" " + c_name + "=");
    if (c_start == -1) {
        c_start = c_value.indexOf(c_name + "=");
    }
    if (c_start == -1) {
        c_value = null;
    }
    else {
        c_start = c_value.indexOf("=", c_start) + 1;
        var c_end = c_value.indexOf(";", c_start);
        if (c_end == -1) {
            c_end = c_value.length;
        }
        c_value = unescape(c_value.substring(c_start, c_end));
    }
    return c_value;
}
