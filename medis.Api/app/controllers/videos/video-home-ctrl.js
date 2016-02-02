(function(){
    var videoHomeCtrl = function($scope, categories){
        $scope.videoCategories = categories;

        $scope.formData = {};

        $scope.formSubmit = function (form) {
            debugger;
        }
    };

    angular.module("medisApp.ctrl")
        .controller("VideoHomeCtrl", ["$scope", "categories", videoHomeCtrl]);
}());
