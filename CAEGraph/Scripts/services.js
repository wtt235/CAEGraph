caeApp.service('ChartService', function ($http) {
    this.getSales = function (period, start, end) {
        var query = '/api/SaleItems?';
        query += 'period=' + period;
        query += '&start=' + moment(start).utc().format('YYYY-MM-DD');
        query += '&end=' + moment(end).utc().format('YYYY-MM-DD');
        return $http.get(query).
            then(function(response) {
                return {
                    status: "success",
                    result: response.data
                }
            }, function(response) {
                return {
                    status: "error",
                    message: "An error occured while retriving Sales data."
                }
            });
    };
    this.getChart = function(data) {
        return c3.generate({
            bindto: '#chart',
            data: {
                json: data,
                keys: {
                    x: 'Date',
                    value: ['TotalAmount', 'TotalSales']
                },
                names: {
                    TotalAmount: 'Sum $',
                    TotalSales: 'Sales'
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
                    label: { text: 'Time', position: 'outer-center' },
                    tick: {
                        format: '%Y-%m-%d'
                    }
                },
                y: {
                    label: { text: 'Sum', position: 'outer-middle' },
                    position: 'outer-middle'
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
    }
    this.checkDates = function (period, start, end) {
        if (typeof start === 'undefined' || typeof end === 'undefined') {
            return {
                valid: false,
                message: "One or both of the specified dates is not a defined date or is invalid."
            }
        }
        var startDate = moment(start).utc();
        var endDate = moment(end).utc();
        if (!startDate.isBefore(endDate)) {
            return {
                valid: false,
                message: "The start date must come before the end date."
            }
        }
        if (period === '0' && endDate.diff(startDate, 'd') > 365) {
            return {
                valid: false,
                message: "Only up to a year can be displayed in Day view."
            }
        }
        return {valid: true};
    }
});