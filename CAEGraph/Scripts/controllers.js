caeApp.controller('ChartController', function ($scope, SaleService) {

    var chart;

    $scope.periods = [
        { name: 'Day', value: '0' },
        { name: 'Week', value: '1' },
        { name: 'Month', value: '2' },
        { name: 'Quarter', value: '3' },
        { name: 'Year', value: '4' }
    ];

    $scope.selectedPeriod = $scope.periods[2].value;

    $scope.startDate = moment().toDate();

    $scope.endDate = moment().add(6, 'M').toDate();

    $scope.showChart = function () {
        SaleService.getSales($scope.selectedPeriod, $scope.startDate, $scope.endDate).then(function (data) {
            chart = c3.generate({
                bindto: '#chart',
                data: {
                    json: data,
                    keys: {
                        x: 'Date',
                        value: ['TotalAmount', 'TotalSales']
                    },
                    names: {
                        TotalAmount: 'Total Sales',
                        TotalSales: 'Number of Sales'
                    },
                    types: {
                        TotalAmount: 'line',
                        TotalSales: 'bar'
                    },
                    axes: {
                        TotalAmount: 'y',
                        TotalSales: 'y2'
                    }
                },
                bar: {
                    width: {
                        ratio: 0.75
                    }
                },
                zoom: {
                    enabled: true
                },
                axis: {
                    x: {
                        type: 'timeseries',
                        tick: {
                            format: '%Y-%m-%d'
                        }
                    },
                    y2: {
                        show: true
                    }
                },
                grid: {
                    x: {
                        show: true
                    },
                    y: {
                        show: true
                    }
                }
            });
        });
    };

    $scope.resetZoom = function () {
        chart.unzoom();
    };
});