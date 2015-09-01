caeApp.controller('ChartController', function ($scope, ChartService) {

    var chart;
    var currentTime = moment().utc();

    $scope.periods = [
        { name: 'Day', value: '0' },
        { name: 'Week', value: '1' },
        { name: 'Month', value: '2' },
        { name: 'Quarter', value: '3' },
        { name: 'Year', value: '4' }
    ];

    $scope.selectedPeriod = $scope.periods[2].value;

    $scope.startDate = currentTime.toDate();

    $scope.endDate = moment(currentTime).add(6, 'M').toDate();

    $scope.showChart = function () {
        var datesValid = ChartService
            .checkDates($scope.selectedPeriod,
                $scope.startDate,
                $scope.endDate);
        if (datesValid.valid) {
            ChartService.getSales($scope.selectedPeriod,
                $scope.startDate,
                $scope.endDate)
            .then(function (response) {
                if (response.status === 'success') {
                    chart = ChartService.getChart(response.result);
                } else {
                    alert(response.message);
                    chart = ChartService.getChart([]);
                }
            });
        } else {
            alert(datesValid.message);
            chart = ChartService.getChart([]);
        }
    };

    $scope.resetZoom = function () {
        chart.unzoom();
    };

    $scope.open = function ($event) {
        $scope.status.opened = true;
    };

    $scope.dateOptions = {
        formatYear: 'yy',
        startingDay: 1
    };

    $scope.status = {
        opened: false
    };

});