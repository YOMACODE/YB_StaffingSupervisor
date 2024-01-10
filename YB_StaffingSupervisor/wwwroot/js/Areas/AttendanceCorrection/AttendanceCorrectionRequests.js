function viewComment(id) {
    var viewbtnId = id;
    var ApproveRejectComment = $('[name = "attendanceCorrectionListing[' + viewbtnId + '].ApproveRejectComment"]').val();
    $("#hdnModelApproveRejectComment").val(ApproveRejectComment);
}
function viewRemark(id) {
    var viewbtnId = id;
    var Reamark = $('[name = "attendanceCorrectionListing[' + viewbtnId + '].Remark"]').val();
    $("#hdnModelApproveRejectComment").val(Reamark);
}
function viewVerifyAttendanceCorrection(id) {
    var viewbtnId = id;
    var AttendanceCorrectionId = $('[name = "attendanceCorrectionListing[' + viewbtnId + '].AttendanceCorrectionRequestId"]').val();
    $("#hdnModelAttendanceCorrectionId").val(AttendanceCorrectionId);
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

function ApproveRejectAttendanceCorrection() {
    var IsFormValid = true;
    if (ValidateApproveRejectStatus() == false) {
        IsFormValid = false;
    }
    if (ValidateApproveRejectComment() == false) {
        IsFormValid = false;
    }
    
    if (IsFormValid) {
        var AttendanceCorrectionId = $("#hdnModelAttendanceCorrectionId").val();
        var ApproveRejectStatus = $('#ApproveRejectStatus').val();
        var ApproveRejectComment = $('#ApproveRejectComment').val();
        var Token = $('#hdn_TokenValue').val();
        var DTO = {
            AttendanceCorrectionRequestId: AttendanceCorrectionId,
            ApproveRejectStatus: ApproveRejectStatus,
            ApproveRejectComment: ApproveRejectComment,
            Token: Token
        };
        $('#btnApproveRejectAttendanceCorrection').attr("disabled", "disabled");
        $.ajax({
            cache: false,
            type: "POST",
            url: "/Supervisor/AttendanceCorrection/ApproveRejectAttendanceCorrection",
            beforeSend: function (x) {
                $(".preloader").attr("style", "display:block;");
            },
            data: DTO,
            success: function (data) {
                $(".preloader").attr("style", "display:none;");
                $('#btnApproveRejectAttendanceCorrection').removeAttr("disabled", "disabled");
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
                $('#btnApproveRejectAttendanceCorrection').removeAttr("disabled", "disabled");
                if (xhr.status == 403) {
                    var url = $.parseJSON(xhr.responseText);
                    window.location.href = url;
                }
            }
        });
    }
}

function ExportAttendanceCorrectionReport() {
    var flag = 1;
    if ($('#SearchStatusType').val() == '') {
        toastr.error("* Please select Status.", "Error !");
        flag = 0;
    }

    if (flag == 1) {
        var YomaId = $("#SearchUserCode").val();
        var AttendenceFrom = $("#SearchAttendanceFrom").val();
        var AttendenceTo = $("#SearchAttendanceTo").val();
        var status = $("#SearchStatusType").val();

        window.location = "/Supervisor/AttendanceCorrection/AttendanceCorrectionReport?YomaId=" + YomaId + "&AttendenceFrom=" + AttendenceFrom + '&AttendenceTo=' + AttendenceTo + '&status=' + status;
    }
}