(function(){
    var adminSvc = function($http) {
        var getVideoSearchResults = function(searchQuery) {
            return $http.get("/api/videos/search", { params: searchQuery });
        };

        return {
            getVideoSearchResults: getVideoSearchResults
        };
    };

    angular.module("medisApp")
        .factory("AdminSvc", ["$http", adminSvc]);
}());
