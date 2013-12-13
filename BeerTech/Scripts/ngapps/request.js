function RequestCtrl($scope, $http, $filter) {
    $scope.requests = [];
    $scope.loaded = false;
    $scope.noresults = true;
    $scope.rows = [];

    $scope.getAllRequests = function () {
        $http({
            method: 'GET',
            url: '/BeverageRequest/GetAllRequests'
        }).success(function (data, status, headers, config) {
            $scope.ready(data);
        });
    }

    $scope.getAllRequests();

    $scope.ready = function (data) {
        $scope.requests = data;
        if ($scope.requests != null) {
            $scope.noresults = ($scope.requests.length == 0);
            if (!$scope.noresults) {
                $scope.rows = chunk($scope.requests, 3);
            }
        }
        $scope.loaded = true;
    }

    $scope.date = function (dt) {
        var date = new Date(parseInt(dt.substr(6)));
        return date.getMonth() + "/" + date.getDate() + "/" + date.getFullYear();
    }

    $scope.panelType = function (status) {
        if(status == "Submitted") { return "panel-info"; } 
        else if(status == "Bought") { return "panel-success"; } 
        else if(status == "Rejected") { return "panel-danger"; }
        else if (status == "Not Found") { return "panel-warning"; }
        return "panel-default";
    }

    $scope.filterSearch = function () {
        $scope.rows = chunk($filter('filter')($scope.requests, $scope.search), 3);
    }
}

function chunk(a, s) {
    for (var x, i = 0, c = -1, l = a.length, n = []; i < l; i++)
        (x = i % s) ? n[c][x] = a[i] : n[++c] = [a[i]];
    return n;
}

