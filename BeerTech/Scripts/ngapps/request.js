function RequestCtrl($scope, $http, $filter) {
    $scope.requests = [];
    $scope.fridges = [];
    $scope.loaded = false;
    $scope.noresults = true;
    $scope.rows = [];

    $scope.getAllRequests = function () {
        $http({
            method: 'GET',
            url: '/BeverageRequest/GetAllRequests'
        }).success(function (data, status, headers, config) {
            $scope.requestsReady(data);
        });
    }

    $scope.getFridges = function () {
        $http({
            method: 'GET',
            url: '/Fridge/GetAllFridges'
        }).success(function (data, status, headers, config) {
            $scope.fridges = data;
        });
    }

    $scope.getAllRequests();
    $scope.getFridges();

    $scope.requestsReady = function (data) {
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

    $scope.classType = function (status, classType) {
        if(status == "Submitted") { return classType + "-info"; } 
        else if (status == "Bought") { return classType + "-success"; }
        else if (status == "Rejected") { return classType + "-danger"; }
        else if (status == "Not Found") { return classType + "-warning"; }
        return classType + "-default";
    }

    $scope.filterSearch = function () {
        $scope.rows = chunk($filter('filter')($scope.requests, $scope.search), 3);
    }

    $scope.filterFridge = function () {
        if ($scope.fridge == null) {
            $scope.rows = chunk($scope.requests, 3);
        }
        else {
            $scope.rows = chunk($filter('filter')($scope.requests, $scope.fridge.ID), 3);
        }
    }

    $scope.updateStatus = function (row, col, id, status) {
        var xsrf = $.param({ id: id, status: status });
        $http({
            method: 'POST',
            url: '/BeverageRequest/UpdateStatus',
            data: xsrf,
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
        }).success(function (data, reqstatus, headers, config) {
            $scope.rows[row][col].Status = status;
        });
    }

    $scope.deleteRequest = function (row, col, id) {
        var xsrf = $.param({ id: id });
        $http({
            method: 'POST',
            url: '/BeverageRequest/DeleteRequest',
            data: xsrf,
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
        }).success(function (data, status, headers, config) {
            $scope.getAllRequests();
        });
    }
}

function chunk(a, s) {
    for (var x, i = 0, c = -1, l = a.length, n = []; i < l; i++)
        (x = i % s) ? n[c][x] = a[i] : n[++c] = [a[i]];
    return n;
}

