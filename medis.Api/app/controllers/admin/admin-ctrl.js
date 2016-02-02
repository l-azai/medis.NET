(function() {
    function adminCtrl($scope, $route, $modal, initVideoListModel, initCategories, AdminSvc, VideoSvc, MessageSvc) {
        // model
        $scope.videoCategories = initCategories;
        $scope.videoSearchResults = initVideoListModel.pagedResults;
        $scope.totalRecords = initVideoListModel.totalRecords;
        $scope.currentPage = 1;
        $scope.pageSize = 10;
        $scope.pageSizeList = [ 10, 20, 30, 40, 50 ];

        $scope.videosByName = function(name) {
            return VideoSvc.getVideosByName(name)
                .then(function(response) {
                    return response.data.map(function(item) {
                        return item.name;
                    });
                });
        };

        $scope.orderBy = function(prop) {
            $scope.sortDesc = $scope.sortName != prop ? true : !$scope.sortDesc;
            $scope.sortName = prop;
            $scope.loadGrid();
        };

        $scope.loadGrid = function() {
            var searchQuery = {
                skip: $scope.currentPage - 1,
                pageSize: $scope.pageSize,
                sortName: $scope.sortName,
                sortDescending: $scope.sortDesc,
                searchText: $scope.searchText,
                categoryFilter: $scope.categoryFilter
            };

            AdminSvc.getVideoSearchResults(searchQuery)
                .then(function(response) {
                    $scope.videoSearchResults = response.data.pagedResults;
                    $scope.totalRecords = response.data.totalRecords;
                });
        };

        $scope.confirmDelete = function(id, name) {
            var modalInstance = $modal.open({
                animation: false, // TODO: previously true, breaking change on ngAnimate that causes backdrop to not disappear
                templateUrl: 'partials/confirmation-modal.html',
                size: 'sm',
                controller: 'ModalCtrl',
                resolve: {
                    modalModel: function() {
                        var model = {
                            bodyText: 'Are you sure you want to delete video<br /><strong>' + name + '</strong>?',
                            value: id
                        };
                        return model;
                    }
                }
            });

            modalInstance.result.then(function(id) {
                VideoSvc.deleteVideo(id)
                    .then(function(response) {
                        $scope.loadGrid();
                        MessageSvc.setSuccessMsg('Video deleted');
                    });
            });
        };
    };

    angular.module("medisApp.ctrl")
        .controller("AdminCtrl", ["$scope", "$route", "$modal", "initVideoListModel", "initCategories",
            "AdminSvc", "VideoSvc", "MessageSvc",
        adminCtrl]);
}());
