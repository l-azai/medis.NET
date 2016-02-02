(function() {
    function videoSvc($http) {
        function getVideoCategories() {
            return $http.get('/api/videos/getVideoCategoryList');
        };

        function getVideosByCategory(category) {
            return $http.get('/api/videos/getVideosByCategory/' + category);
        };

        function getVideos() {
            return $http.get('/api/videos/getVideos');
        };

        function getVideosByName(name) {
            return $http.get('/api/videos/getVideosByName/' + name); //{ params: { name: name } });
        };

        function getVideoFileById(id) {
            return $http.get('/api/videos/getVideoFileById/' + id); //{ params: { id: id } });
        };

        function deleteVideo(id) {
            return $http.post('/api/videos/deleteVideo/' + id);
        };

        return {
            getVideoCategories: getVideoCategories,
            getVideosByCategory: getVideosByCategory,
            getVideosByName: getVideosByName,
            getVideos: getVideos,
            getVideoFileById: getVideoFileById,
            deleteVideo: deleteVideo
        };
    };

    angular.module("medisApp")
        .factory("VideoSvc", ["$http", videoSvc]);
}());
