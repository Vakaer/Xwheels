
    //$(document).ready(function () {

    //    $("#tick").click(function () {

    //        $("#Password").attr('type', 'text');
    //    });


    //});
    $(document).ready(function () {

        $("#tick").click(function () {


            if ($("#Password").attr('type')== 'password') {
                $("#Password").attr('type', 'text');
            } else {
                $("#Password").attr('type', 'password');
            }

        })

    });



