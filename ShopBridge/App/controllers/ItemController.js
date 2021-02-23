ShopBridgesApp.controller("ItemController", ['$scope', '$filter', '$q', '$http', '$window', '$location', '$route', '$routeParams', '$timeout', 'CommonServices', 'Upload', 'LocalServiceURL', function ($scope, $filter, $q, $http, $window, $location, $route, $routeParams, $timeout, CommonServices, Upload, LocalServiceURL) {
    $scope.PageTitle = "Item Master";
    if ($routeParams.ItemId != undefined) {
        $scope.ItemId = $routeParams.ItemId;
    };
    $scope.jcp = "";
    $scope.ItemMasterLists = [];
    $scope.ItemData = {};

    $scope.GetAllItems = function () {
        var postData = {
            title: "ItemMasters",
            fields: ["ItemId", "ItemName", "Description", "Price", "ImagePath", "IsActive", "Created", "CreatedBy"],
            filter: ["IsActive eq " + true],
            orderBy: "Created desc"
        };
        CommonServices.GetListItems(postData).then(function (response) {
            if (response && response.data.d.results.length > 0) {
                $scope.ItemMasterLists = response.data.d.results;
                $scope.InitialiseDatatable();
            } 
        });
    };

    $scope.GetItemById = function () {
        var postData = {
            title: "ItemMasters",
            fields: ["ItemId", "ItemName", "Description", "Price", "ImagePath", "IsActive", "Created", "CreatedBy"],
            filter: ["ItemId eq " + $scope.ItemId  + " and IsActive eq " + true],
            orderBy: "Created desc"
        };
        $scope.ItemMaster = [];
        CommonServices.GetListItems(postData).then(function (response) {
            if (response && response.data.d.results.length > 0) {
                $scope.ItemMaster = response.data.d.results[0];
                 $scope.InitialiseDatatable(); 
            }
        });
    };
    $scope.InitialiseDatatable = function () {
        $('#tblItems').DataTable({
            "aaData": $scope.ItemMasterLists,
            buttons: [
                {
                    extend: 'excelHtml5',
                    exportOptions: {
                        columns: [1, 2, 3]
                    },
                    title: 'Data export'
                }
            ],
            "aoColumnDefs": [
                { "className": "dt-left", "targets": "_all" },
            ],

            "bDestroy": true,
            "responsive": true,
            "rowReorder": {
                "selector": 'td:nth-child(2)'
            },
            "aoColumns": [
                {
                    "mDataProp": "ItemId", "mRender": function (data, type, row, meta) {
                        return meta.row + meta.settings._iDisplayStart + 1;
                    }
                },
                //{ "mDataProp": "ItemName" },
                {
                    "mDataProp": "ItemName", "mRender": function (data, type, row, meta) {
                        if (data != null) {
                            var itemRow = '<a title="View Image" href="#ViewItem/' + decodeURI(row.ItemId) + '">' + decodeURI(data)+'</a>';
                        }
                        return itemRow;
                    }
                },
                { "mDataProp": "Price" },
                //{ "mDataProp": "Description" },
                {
                    "mDataProp": "ImagePath", "mRender": function (data, type,row,  meta) {
                        if (data != null) {
                            var itemRow = '<a title="View Image" href="' + LocalServiceURL + decodeURI(data) + '" target="_blank"> <img src="' + LocalServiceURL + decodeURI(data) + '" class="img-thumbnail" height="50" width="50"></a>';
                            }
                            return itemRow;
                    }
                },
                {
                    "mDataProp": "ItemId", "mRender": function (data, type, row, meta) {
                        return '<button type="submit" id="btnDelete" class="btn btn-danger btn-sm" title="Delete"><span class="glyphicon glyphicon-trash"></span></button>';
                        
                    }
                }
            ]
        });
    };

    $('#tblItems tbody').on('click', '[id*=btnDelete]', function () {
        var table = $('#tblItems').DataTable();
        var data = table.row($(this).parents('tr')).data();
        $scope.DeleteItem(data.ItemId);

    });

    $scope.SaveItem = function (filePath) {
        try {
            var postData = {
                "ItemName": $scope.ItemData.ItemName,
                "Price": $scope.ItemData.Price.toString(),
                "Description": $scope.ItemData.Description,
                "ImagePath": filePath,
                "IsActive": true,
                "CreatedBy": "Nikhil Jojare", // Name will come from logged in user.
                "Created": new Date()
            };
            CommonServices.PostData("ItemMasters", postData).then(function (response) {
                //console.log(response);
                if (response.ItemId > 0) {
                    swal("Success", "Item Saved Successfully!", "success");
                        $location.path("/ViewItems");
                }
            }, function (data) {
                //console.log(data);
            });
        } catch (error) {
            console.log("Exception caught in the SaveItem function. Exception Logged as " + error.message);
        }
    };

    $scope.SaveItemData = function (isformValid) {
        try {
            var isValid = isformValid;
            $scope.submitted = !isValid;
            if (isValid) {
                var AttachedFile = document.getElementById('UploadedFile').files;
                if (AttachedFile && AttachedFile.length) {
                    var splitfile = AttachedFile[0].name;
                    var fileNameWithoutExt = splitfile.substring(0, splitfile.lastIndexOf("."));
                    var fileType = splitfile.substring(splitfile.lastIndexOf(".") + 1);
                    var date = $filter('date')(new Date(), "ddMMyyyyHHmmsss");
                    var filename = fileNameWithoutExt + "_"  + date + "." + fileType;
                    $scope.FileSaved = false;
                    Upload.upload({
                        url: LocalServiceURL + "/api/FileUpload/CropAndSaveImage?moduleName=Items&fileName=" + filename + "&x=" + $scope.jcp.active.pos.x + "&y=" + $scope.jcp.active.pos.y + "&w=" + $scope.jcp.active.pos.w + "&h=" + $scope.jcp.active.pos.h,
                        data: {
                            files: AttachedFile
                        }
                    }).then(function (response) {
                        $timeout(function () {
                            $scope.status = response.status;
                            if ($scope.status === 200) {
                                var filePath = "/Uploads/Items/crop_" + filename;
                                if ($scope.ItemId !== undefined && $scope.ItemId !== null && $scope.ItemId !== 0) {
                                    $scope.UpdateItem(filePath);
                                } else {
                                    $scope.SaveItem(filePath);
                                }
                            }
                        });
                    }, function (response) {
                        if (response.status > 0) {
                            var errorMsg = response.status + ': ' + response.data;
                            alert(errorMsg);
                        }
                    }, function (evt) {
                        var element = angular.element(document.querySelector('#dvProgress'));
                        $scope.Progress = Math.min(100, parseInt(100.0 * evt.loaded / evt.total));
                        element.html('<div style="width: ' + $scope.Progress + '%">' + $scope.Progress + '%</div>');
                    });
                } else {
                    if ($scope.ItemId !== undefined && $scope.ItemId !== null && $scope.ItemId !== 0) {
                        $scope.UpdateItem($scope.ItemData.ImagePath);
                    } else {
                        $scope.SaveItem(null);
                    }
                }
            }
        } catch (error) {
            console.log("Exception caught in the ItemController and in SaveItemData function. Exception Logged as " + error.message);
        }
    };

    

   
    $scope.DeleteItem = function (id) {
        try {
            if (id > 0) {

                swal({
                    title: "Are you sure?",
                    text: "Once deleted, you will not be able to recover this data!",
                    icon: "warning",
                    buttons: true,
                    dangerMode: true,
                })
                    .then((willDelete) => {
                        if (willDelete) {
                            var postData = {
                                "IsActive": false,
                                "ModifiedBy": "Nikhil Jojare", //LoggediN user
                                "Modified": new Date()
                            };
                            CommonServices.UpdateData("ItemMasters", postData, id).then(function (response) {
                                if (response !== undefined) {
                                    swal("Success", "Item Deleted Successfully!", "success");
                                    $route.reload();
                                }
                            }, function (data) {
                                //console.log(data);
                                });
                        } 
                    });

                
            }
        } catch (error) {
            console.log("Exception caught in the DeleteItem function. Exception Logged as " + error.message);
        }
    };


    $scope.init = function () {
        $scope.GetAllItems();
        if ($scope.ItemId > 0) {
            $scope.GetItemById();
        }
        Jcrop.load('widePhoto').then(img => {
            $scope.jcp = Jcrop.attach(img, {
                multi: false,
            });

            $scope.jcp.setOptions({
                shadeOpacity: 0.8,
            });

            const rect = Jcrop.Rect.sizeOf($scope.jcp.el);
            $scope.jcp.newWidget(rect.scale(.7, .5).center(rect.w, rect.h));
            $scope.jcp.focus();
        });
    };

    $scope.init();
}]);