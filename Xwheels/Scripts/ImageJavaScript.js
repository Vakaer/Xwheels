//open browse image popup on window
$('#UserImage1').click(function () {

    $('#fileUpload1').trigger('click');
    $('#fileUpload1').change(function () {

        if (this.files && this.files[0]) {
            var fileReader = new FileReader();
            fileReader.readAsDataURL(this.files[0]);

            fileReader.onload = function (img) {
                $('#UserImage1').attr('src', img.target.result);
            }
        }
    });
});
$('#UserImage2').click(function () {

    $('#fileUpload2').trigger('click');
    $('#fileUpload2').change(function () {

        if (this.files && this.files[0]) {
            var fileReader = new FileReader();
            fileReader.readAsDataURL(this.files[0]);

            fileReader.onload = function (img) {
                $('#UserImage2').attr('src', img.target.result);
            }
        }
    });

});
$('#UserImage3').click(function () {

    $('#fileUpload3').trigger('click');

    $('#fileUpload3').change(function () {

        if (this.files && this.files[0]) {
            var fileReader = new FileReader();
            fileReader.readAsDataURL(this.files[0]);

            fileReader.onload = function (img) {
                $('#UserImage3').attr('src', img.target.result);
            }
        }
    });

});
$('#UserImage4').click(function () {

    $('#fileUpload4').trigger('click');
    $('#fileUpload4').change(function () {

        if (this.files && this.files[0]) {
            var fileReader = new FileReader();
            fileReader.readAsDataURL(this.files[0]);

            fileReader.onload = function (img) {
                $('#UserImage4').attr('src', img.target.result);
            }
        }
    });

});
$('#UserImage5').click(function () {

    $('#fileUpload5').trigger('click');
    $('#fileUpload5').change(function () {

        if (this.files && this.files[0]) {
            var fileReader = new FileReader();
            fileReader.readAsDataURL(this.files[0]);

            fileReader.onload = function (img) {
                $('#UserImage5').attr('src', img.target.result);
            }
        }
    });

});
$('#UserImage6').click(function () {

    $('#fileUpload6').trigger('click');
    $('#fileUpload6').change(function () {

        if (this.files && this.files[0]) {
            var fileReader = new FileReader();
            fileReader.readAsDataURL(this.files[0]);

            fileReader.onload = function (img) {
                $('#UserImage6').attr('src', img.target.result);
            }
        }
    });
});


//select and upload image






$('.remove-photo1').click(function () {
    $('#UserImage1').attr('src', "/Content/images/download.png");

});

$('.remove-photo2').click(function () {
    $('#UserImage2').attr('src', "/Content/images/download.png");

});

$('.remove-photo3').click(function () {
    $('#UserImage3').attr('src', "/Content/images/download.png");

});

$('.remove-photo4').click(function () {
    $('#UserImage4').attr('src', "/Content/images/download.png");

});

$('.remove-photo5').click(function () {
    $('#UserImage5').attr('src', "/Content/images/download.png");

});

$('.remove-photo6').click(function () {
    $('#UserImage6').attr('src', "/Content/images/download.png");

});


//    //open browse image popup on window
//$('.UserImage1').click(function () {

//        $('#fileUpload1').trigger('click');
//    });
//$('.UserImage2').click(function () {

//    $('#fileUpload2').trigger('click');
//});
//$('.UserImage3').click(function () {

//    $('#fileUpload3').trigger('click');
//});
//$('.UserImage4').click(function () {

//    $('#fileUpload4').trigger('click');
//});
//$('.UserImage5').click(function () {

//    $('#fileUpload5').trigger('click');
//});
//$('.UserImage6').click(function () {

//    $('#fileUpload6').trigger('click');
//});

////select and upload image
//function myPreview(name) {
        

//    var File = this.files;
//    alert(File);
//    if (File && File[0]) {
            
//        var fileReader = new FileReader();
//        fileReader.readAsDataURL(File[0]);

//        fileReader.onload = function (x) {

//            var image = new Image();
//            image.src = x.target.result;

//            image.onload = function () {

//                var width = this.width;
//                var height = this.height;
//                var type = File[0].type;
//                alert(width + " x " + height + " type: " + type);

//                //if ((width == '1600' && height == '1200') && (type == 'image/png' || type == 'image/jpg' || type == 'image/jpeg')) {

//                //    $('.upload-image-1 img').attr('src', x.target.result);
//                //} else {
//                //    alert("Size should be 1600 x 1200");
//                //}
//                alert(name);

//                $(name).attr('src', x.target.result);

//            }




//        }
//    }

//}
         
//// remove picture
//$('.remove-photo').click(function () {
//    $(this+'img').attr('src', "~/Content/images/default.png");



//});

//        <script>
//            function myPop(UploadId, FileId) {

//                $(UploadId).click(function () {

//                    $(FileId).trigger('click');
//                });
//            }


////diplay uploaded picture

//function changeImage() {


//}

//$('#file-upload-1').change(function () {

//    var File = this.files;

//    if (File && File[0]) {

//        var fileReader = new FileReader();
//        fileReader.readAsDataURL(File[0]);

//        fileReader.onload = function (x) {

//            var image = new Image();
//            image.src = x.target.result;

//            image.onload = function () {

//                var width = this.width;
//                var height = this.height;
//                var type = File[0].type;
//                alert(width + " x " + height + " type: " + type);

//                //if ((width == '1600' && height == '1200') && (type == 'image/png' || type == 'image/jpg' || type == 'image/jpeg')) {

//                //    $('.upload-image-1 img').attr('src', x.target.result);
//                //} else {
//                //    alert("Size should be 1600 x 1200");
//                //}

//                $('.upload-image-1 img').attr('src', x.target.result);

//            }




//        }
//    }

//});




//        $(".upload-image-1").click(function () {



//            $("#file-upload").trigger('click');
//        });

////diplay uploaded picture

//$('#file-upload').change(function () {

//    var File = this.files;

//    if (File && File[0]) {

//        var fileReader = new FileReader();
//        fileReader.readAsDataURL(File[0]);

//        fileReader.onload = function (x) {

//            var image = new Image();
//            image.src = x.target.result;

//            image.onload = function () {

//                var width = this.width;
//                var height = this.height;
//                var type = File[0].type;
//                alert(width + " x " + height + " type: " + type);

//                //if ((width == '1600' && height == '1200') && (type == 'image/png' || type == 'image/jpg' || type == 'image/jpeg')) {

//                //    $('.upload-image-1 img').attr('src', x.target.result);
//                //} else {
//                //    alert("Size should be 1600 x 1200");
//                //}

//                $('.upload-image-1 img').attr('src', x.target.result);

//            }




//        }
//    }

//});


////removing picture

//$('.remove-icon').click(function () {

//    $('#img').attr('src', '/Content/images/download.png');
//    $('#Image').val("~/Content/images/download.png");

//});
