$(document).ready(function () {

    $("#Make_id").change(function () {
        var Mid = $(this).val(); alert(Mid);
        debugger
        $.ajax({
            type: "post",
            url: "/Try/GetModelList?Make_id=" + Mid,
            contentType: "html",
            success: function (response) {
                debugger
                $("#Model_id").empty();
                $("#Model_id").append(response);
            }
        })
    })
})



//open browse image popup on window
$('#u-img-1, #u-img-2, #u-img-3, #u-img-4, #u-img-5, #u-img-6').click(function () {

    var openn = $(this).attr('id');
    var fileOpen;

    // conditions for opening different files
    if (openn == 'u-img-1') {
        fileOpen = '#fileUpload1';

    } else if (openn == 'u-img-2') {
        fileOpen = '#fileUpload2';

    } else if (openn == 'u-img-3') {
        fileOpen = '#fileUpload3';

    } else if (openn == 'u-img-4') {
        fileOpen = '#fileUpload4';

    } else if (openn == 'u-img-5') {
        fileOpen = '#fileUpload5';

    } else if (openn == 'u-img-6') {
        fileOpen = '#fileUpload6';

    }

    //Triggering respective file tag
    $(fileOpen).trigger('click');

});




//select and upload
$('#fileUpload1, #fileUpload2, #fileUpload3, #fileUpload4, #fileUpload5, #fileUpload6').change(function () {

    name = '.u-img-1 img';

    var input = $(this).attr('id');
    //var input = $('#fileUpload2').data('clicked');


    if (input == 'fileUpload1') {
        name = '.u-img-1 img';

    } else if (input == 'fileUpload2') {
        name = '.u-img-2 img';

    } else if (input == 'fileUpload3') {
        name = '.u-img-3 img';

    } else if (input == 'fileUpload4') {
        name = '.u-img-4 img';

    } else if (input == 'fileUpload5') {
        name = '.u-img-5 img';

    } else if (input == 'fileUpload6') {
        name = '.u-img-6 img';

    }
    // Preview images
    var File = this.files;

    if (File && File[0]) {

        var fileReader = new FileReader();
        fileReader.readAsDataURL(File[0]);

        fileReader.onload = function (x) {

            var image = new Image();
            image.src = x.target.result;

            image.onload = function () {

                var width = this.width;
                var height = this.height;
                var type = File[0].type;
                // alert(width + " x " + height + " type: " + type);

                //if ((width == '1600' && height == '1200') && (type == 'image/png' || type == 'image/jpg' || type == 'image/jpeg')) {

                //    $('.upload-image-1 img').attr('src', x.target.result);
                //} else {
                //    alert("Size should be 1600 x 1200");
                //}


                $(name).attr('src', x.target.result);

            }




        }
    }

});

// remove picture
$('#remove1, #remove2, #remove3, #remove4, #remove5, #remove6').click(function () {

    var rem = $(this).attr('id');
    var img;

    if (rem == 'remove1') {
        img = '.u-img-1 img';
        $('#Image_Path_1').val('~/Content/images/download.png');

    } else if (rem == 'remove2') {
        img = '.u-img-2 img';
        $('#Image_Path_2').val('~/Content/images/download.png');


    } else if (rem == 'remove3') {
        img = '.u-img-3 img';
        $('#Image_Path_3').val('~/Content/images/download.png');


    } else if (rem == 'remove4') {
        img = '.u-img-4 img';
        $('#Image_Path_4').val('~/Content/images/download.png');


    } else if (rem == 'remove5') {
        img = '.u-img-5 img';
        $('#Image_Path_5').val('~/Content/images/download.png');


    } else if (rem == 'remove6') {
        img = '.u-img-6 img';
        $('#Image_Path_6').val('~/Content/images/download.png');


    }

    //change image
    $(img).attr('src', '/Content/images/download.png');



});