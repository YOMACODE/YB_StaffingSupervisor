function viewMap(id) {
    var viewbtnId = id;
    var AttendanceDate = $('[name = "ClaimRequestListing[' + viewbtnId + '].ClaimInitiateDate"]').val();
    var hdnUserId = $('[name = "ClaimRequestListing[' + viewbtnId + '].UserId"]').val();
    var Token = $('#hdn_TokenValue').val();
    var DTO = {
        AttendanceDate: AttendanceDate,
        UserId: hdnUserId,
        Token: Token
    };

    $('#divViewMap').html("");
    $.ajax({
        cache: false,
        type: "POST",
        url: "/Supervisor/Attendance/GetAttendanceMeetingMapViewComponent",
        beforeSend: function (x) {
            $(".preloader").attr("style", "display:block;");
        },
        data: DTO,
        complete: function (response) {
            $(".preloader").attr("style", "display:none;");
            $('#divViewMap').html(response.responseText);
            $('.offcanvas').toggleClass('show');
        },
        error: function (xhr, ajaxOptions, thrownError) {
            $(".preloader").attr("style", "display:none;");
            toastr.error("Server error.", "Error !");
            if (xhr.status == 403) {
                var url = $.parseJSON(xhr.responseText);
                window.location.href = url;
            }
        }
    });
}
