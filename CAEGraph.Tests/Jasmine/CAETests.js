describe('ChartService', function () {
    var ChartService;
    beforeEach(function () {
        module("caeApp");
        inject(function ($injector) {
            ChartService = $injector.get('ChartService');
        });
    });

    describe("gets a list of sales items", function() {
        var $httpBackend;

        beforeEach(function() {
            inject(function($injector) {
                $httpBackend = $injector.get('$httpBackend');
            });
        });

        it("should return data", function() {
            var start = moment('2015-08-01');
            var end = moment('2015-09-01');
            var query = '/api/SaleItems?';
            query += 'period=0';
            query += '&start=' + start.format('YYYY-MM-DD');
            query += '&end=' + end.format('YYYY-MM-DD');
            var data = [
                {
                    "Date": "2014-01-01",
                    "TotalAmount": 1,
                    "TotalSales": 1
                },
                {
                    "Date": "2014-01-03",
                    "TotalAmount": 1,
                    "TotalSales": 2
                },
                {
                    "Date": "2014-02-01",
                    "TotalAmount": 1,
                    "TotalSales": 1
                },
                {
                    "Date": "2015-01-01",
                    "TotalAmount": 1,
                    "TotalSales": 2
                }
            ];
            $httpBackend.expectGET(query);
            $httpBackend.whenGET(query).respond(data);
            ChartService.getSales('0', start.toDate(), end.toDate()).then(function(data) {
                expect(data.result.length).toEqual(4);
            });
            $httpBackend.flush();
        });
    });


    describe("checks dates for validity", function() {
        var start = moment('2015-08-01').toDate();
        var end = moment('2015-09-01').toDate();

        it("should return date as valid", function () {
            var result = ChartService.checkDates('0', start, end);
            expect(result.valid).toEqual(true);
        });

        it("should return date as invalid since start > end", function() {
            var result = ChartService.checkDates('0', end, start);
            expect(result.valid).toEqual(false);
        });

        it("should return date as invalid since end-start days is > 365", function () {
            var bigEnd = moment('2018-09-01').toDate();
            var result = ChartService.checkDates('0', start, bigEnd);
            expect(result.valid).toEqual(false);
        });

    });
});