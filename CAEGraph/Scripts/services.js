caeApp.service('SaleService', function ($http) {
    this.getSales = function (period, start, end) {
        var query = '/api/SaleItems?';
        query += 'period=' + period;
        query += '&start=' + moment(start).format('YYYY-MM-DD');
        query += '&end=' + moment(end).format('YYYY-MM-DD');
        return $http.get(query).
          then(function (response) {
                return response.data;
            }, function (response) {
              alert("An error occured while retriving Sales data.");
          });
    };
});