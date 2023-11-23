function viewComment(id) {
    var viewbtnId = id;
    var ApproveRejectComment = $('[name = "attendanceListing[' + viewbtnId + '].ApproveRejectComment"]').val();
    $("#hdnModelApproveRejectComment").val(ApproveRejectComment);
}
function viewVerifyAttendance(id) {
    var viewbtnId = id;
    var AttendanceId = $('[name = "attendanceListing[' + viewbtnId + '].DailyAttendanceId"]').val();
    $("#hdnModelAttendanceId").val(AttendanceId);
    $('#ApproveRejectStatus').val("");
    $('#ApproveRejectStatus-Error').text("");
    $('#ApproveRejectComment').val("");
    $('#ApproveRejectComment-Error').text("");
}
function ValidateApproveRejectStatus() {
    var isValid = true;
    var ApproveRejectStatus = $('#ApproveRejectStatus').val();

    if (ApproveRejectStatus == "") {
        $("#ApproveRejectStatus-Error").text("*Please select Approve/Reject Status.");
        $("#ApproveRejectStatus-Error").attr("style", "display:Inline;");
        isValid = false;
    }
    else {
        $("#ApproveRejectStatus-Error").text("");
        $("#ApproveRejectStatus-Error").attr("style", "display:Inline;");
    }
    return isValid;
}
function ValidateApproveRejectComment() {
    var isValid = true;
    var ApproveRejectComment = $('#ApproveRejectComment').val();
    if (ApproveRejectComment == "") {
        $("#ApproveRejectComment-Error").text("*Please enter comment.");
        $("#ApproveRejectComment-Error").attr("style", "display:Inline;");
        isValid = false;
    }
    else {
        var regX = /^[\W]+$/;
        if ($.isNumeric(ApproveRejectComment) || regX.test(ApproveRejectComment)) {
            $("#ApproveRejectComment-Error").text("*Please enter valid comment.");
            $("#ApproveRejectComment-Error").attr("style", "display:Inline;");
            isValid = false;
        }
        else {
            $("#ApproveRejectComment-Error").text("");
            $("#ApproveRejectComment-Error").attr("style", "display:Inline;");
        }
    }
    return isValid;
}

function ApproveRejectAttendance() {
    var IsFormValid = true;
    if (ValidateApproveRejectStatus() == false) {
        IsFormValid = false;
    }
    if (ValidateApproveRejectComment() == false) {
        IsFormValid = false;
    }
    
    if (IsFormValid) {
        var AttendanceId = $("#hdnModelAttendanceId").val();
        var ApproveRejectStatus = $('#ApproveRejectStatus').val();
        var ApproveRejectComment = $('#ApproveRejectComment').val();
        var Token = $('#hdn_TokenValue').val();
        var DTO = {
            attendanceId: AttendanceId,
            ApproveRejectStatus: ApproveRejectStatus,
            ApproveRejectComment: ApproveRejectComment,
            Token: Token
        };
        $('#btnApproveRejectAttendance').attr("disabled", "disabled");
        $.ajax({
            cache: false,
            type: "POST",
            url: "/Supervisor/Attendance/ApproveRejectAttendance",
            beforeSend: function (x) {
                $(".preloader").attr("style", "display:block;");
            },
            data: DTO,
            success: function (data) {
                $(".preloader").attr("style", "display:none;");
                $('#btnApproveRejectAttendance').removeAttr("disabled", "disabled");
                if (data.msg == "Approved successfully." || data.msg == "Rejected successfully.") {
                    toastr.success(data.msg, "Success !");
                    location.reload();
                }
                else {
                    toastr.error(data.msg, "Error !");
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                $(".preloader").attr("style", "display:none;");
                toastr.error("Server error.", "Error !");
                $('#btnApproveRejectAttendance').removeAttr("disabled", "disabled");
                if (xhr.status == 403) {
                    var url = $.parseJSON(xhr.responseText);
                    window.location.href = url;
                }
            }
        });
    }
}
function viewMap(id) {
    var viewbtnId = id;
    var AttendanceDate = $('[name = "attendanceListing[' + viewbtnId + '].AttendanceDate"]').val();
    var hdnUserId = $('#hdnUserId').val();
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