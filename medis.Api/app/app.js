(function(){
    var mod = angular.module("medisApp",
        ["medisApp.ctrl", "medisApp.svc", "medisApp.filters", "medisApp.directives",
        "ngRoute", "ngFileUpload", "ui.bootstrap", "ngSanitize", "ngAnimate", "mgo-angular-wizard"]);

    mod.config(function($routeProvider, $locationProvider){
        $routeProvider
            .when("/videos", {
                templateUrl: "/app/views/partials/videos-index.html",
                controller: "VideoHomeCtrl",
                resolve: {
                    categories: function(VideoSvc) {
                        return VideoSvc.getVideoCategories()
                            .then(function(response) {
                                return response.data;
                            }, function(response){
                                console.log('Error: getVideoCategories. ' + response);
                            });
                    }
                }
            })
            .when("/videos/:category", {
                templateUrl: "app/views/partials/category-videos.html",
                controller: "VideoFilesCtrl",
                resolve: {
                    videos: function(VideoSvc, $route) {
                        return VideoSvc.getVideosByCategory($route.current.params.category)
                            .then(function(response) {
                                return response.data;
                            }, function(response){
                                console.log('Error: videos by category. ' + response);
                            });
                    }
                }
            })
            .when("/admin/videos", {
                templateUrl: "app/views/partials/admin/admin-videos.html",
                controller: "AdminCtrl",
                resolve: {
                    initVideoListModel: function(AdminSvc) {
                        return AdminSvc.getVideoSearchResults()
                            .then(function(response) {
                                return response.data;
                            }, function(response) {
                                console.log('Error: admin videos. ' + response); 
                            });
                    },
                    initCategories: function (VideoSvc) {
                        return VideoSvc.getVideoCategories()
                            .then(function (response) {
                                return response.data;
                            }, function (response) {
                                console.log('Error: admin videos. ' + response)
                            });
                    }
                }
            })
            .when("/admin/video/add", {
                templateUrl: "app/views/partials/admin/admin-video-add.html",
                controller: "AdminVideoAddCtrl",
                resolve: {
                    model: function(VideoSvc) {
                        return VideoSvc.getVideoCategories()
                            .then(function(response){
                                return response.data;
                            }, function(response) {
                                console.log('Error: AdminVideoAdd. ' + response);
                            });
                    }
                }
            })
            .when("/admin/video/:id/edit", {
                templateUrl: "app/views/partials/admin/admin-video-edit.html",
                controller: "AdminVideoEditCtrl",
                resolve: {
                    model: function(VideoSvc, $q, $route) {
                        var promiseCat = VideoSvc.getVideoCategories();
                        var promiseVideoFile = VideoSvc.getVideoFileById($route.current.params.id);

                        return $q.all([promiseCat, promiseVideoFile]).then(function(response){
                                    var videoEditModel = {};
                                    videoEditModel.videoCategories = response[0].data;

                                    videoEditModel.id = response[1].data._id;
                                    videoEditModel.categoryId = response[1].data.catId;
                                    videoEditModel.videoName = response[1].data.name;
                                    videoEditModel.yearReleased = response[1].data.yearReleased;
                                    videoEditModel.hasImage = response[1].data.imageGfsFilename ? true : false;
                                    videoEditModel.hasVideo = response[1].data.videoGfsFilename ? true : false;

                                    return videoEditModel;
                                });
                    }
                }
            })
            .when("/admin/test-media", {
                templateUrl: "app/views/partials/test-media.html",
                controller: "TestMediaCtrl"
            })
            .otherwise({
                redirectTo: "/videos"
            });

        $locationProvider.html5Mode(true);
    });
}());
