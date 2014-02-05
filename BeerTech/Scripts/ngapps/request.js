function RequestCtrl($scope, $http, $filter) {
    $scope.requests = [];
    $scope.fridges = [];
    $scope.results = [];
    $scope.range = [];
    $scope.details = [];
    $scope.pages = null;
    $scope.pgNum = 1;
    $scope.prows = null;
    $scope.limit = 9;
    $scope.loaded = false;
    $scope.noresults = true;
    $scope.rows = [];
    $scope.guide = false;
    $scope.newrequest = false;
    $scope.beerid = "";

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
        $scope.details = [];
        $scope.requests = data;
        if ($scope.requests != null) {
            $scope.noresults = ($scope.requests.length == 0);
            if (!$scope.noresults) {
                $.each($scope.requests, function (key, value) {
                    if (value.BeerAPIID != null && value.BeerAPIID != "") {
                        $scope.getDetails(value.BeerAPIID);
                    }
                });
                $scope.rows = chunk($scope.requests, 3);
            }
        }
        $scope.loaded = true;
    }

    $scope.getDetails = function (apiId) {
        $http({
            method: 'GET',
            url: '/Beer/GetBeer',
            params: {
                id: apiId
            }
        }).success(function (data, status, headers, config) {
            $scope.details.push(data.data);
        });
    }

    $scope.date = function (dt) {
        var date = new Date(parseInt(dt.substr(6)));
        return (date.getMonth()+1) + "/" + date.getDate() + "/" + date.getFullYear();
    }

    $scope.user = function (email) {
        return email.replace('@careerbuilder.com', '');
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

    $scope.preparePage = function (num) {
        $scope.pgNum = num;
        $scope.prows = chunk($scope.pages[$scope.pgNum-1], 3);
    }

    $scope.searchBreweryAPI = function (brew) {
        if (brew != null && brew != "") {
            $http({
                method: 'GET',
                url: '/Search/SearchBeers/',
                params: {
                    keyword: brew,
                    page: 1
                }
            }).success(function (data, status, headers, config) {
                $scope.results = data.data;
                $scope.totalResults = data.totalResults;
                $scope.pages = chunk($scope.results, $scope.limit);
                for (var i = 1; i <= $scope.pages.length; i++) {
                    $scope.range.push(i);
                }
                $scope.preparePage($scope.pgNum);
            });
        }
    }
    $scope.selectBeer = function(id, name) {
        $scope.beerid = id;
        $scope.newBeverage = name;
    }

    $scope.beerClass = function (id) {
        if (id == $scope.beerid) {
            return "panel-success";
        }
        return "panel-default";
    }

    $scope.newRequest = function () {
        if ($scope.newFridge != null && $scope.newFridge != "" && $scope.newBeverage != null && $scope.newBeverage != "") {
            var xsrf = $.param({ FridgeID: $scope.newFridge.ID, BeverageTitle: $scope.newBeverage, BeerAPIID: $scope.beerid });
            $http({
                method: 'POST',
                url: '/BeverageRequest/CreateRequest',
                data: xsrf,
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
            }).success(function (data, reqstatus, headers, config) {
                $scope.getAllRequests();
                $scope.newrequest = false;
            });
        }
    }
}

function chunk(a, s) {
    for (var x, i = 0, c = -1, l = a.length, n = []; i < l; i++)
        (x = i % s) ? n[c][x] = a[i] : n[++c] = [a[i]];
    return n;
}

