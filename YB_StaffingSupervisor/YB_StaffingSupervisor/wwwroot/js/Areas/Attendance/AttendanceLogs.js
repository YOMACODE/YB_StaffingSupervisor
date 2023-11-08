/*Created By : Gaurav Sharma*/

/*Validation Form*/
function ValidateSearchYear() {
    var isValid = true;
    var SearchYear = $('#SearchYear').val();

    if (SearchYear == "") {
        $("#SearchYear-Error").text("*Please select Year.");
        $("#SearchYear-Error").attr("style", "display:Inline;");
        isValid = false;
    }
    else {
        $("#SearchYear-Error").text("");
        $("#SearchYear-Error").attr("style", "display:Inline;");
    }
    return isValid;
}
function ValidateSearchMonth() {
    var isValid = true;
    var SearchMonth = $('#SearchMonth').val();

    if (SearchMonth == "") {
        $("#SearchMonth-Error").text("*Please select Month.");
        $("#SearchMonth-Error").attr("style", "display:Inline;");
        isValid = false;
    }
    else {
        $("#SearchMonth-Error").text("");
        $("#SearchMonth-Error").attr("style", "display:Inline;");
    }
    return isValid;
}

function Validate() {
    var IsFormValid = true;
    var UserId = $("#hdnUserId").val();
    var Year = $("#SearchYear").val();
    var Month = $("#SearchMonth").val();

    if (ValidateSearchYear() == false) {
        IsFormValid = false;
    }
    if (ValidateSearchMonth() == false) {
        IsFormValid = false;
    }

    if (UserId.length == '' || Year.length == '' || Month.length == '') {
        IsFormValid = false;
    }
    return IsFormValid;
}
function checkall() {
    //all
    $('input:checkbox#all').each(function () {
        if (this.checked) {
            $('.trid').not("[disabled]").prop('checked', true);
        }
        else {
            $('.trid').not("[disabled]").prop('checked', false);
        }
    });
    var checkedValues = $('input:checkbox.trid:checked').map(function () {
        return this.value;
    }).get();
    $('#MultipleAttendanceRequestId').val(checkedValues);
}
function checkone() {
    var ischecked = true;
    $('input:checkbox.trid').each(function () {
        if (!this.checked) {
            ischecked = false;
        }
    });
    $('#all').prop('checked', ischecked);
    var checkedValues = $('input:checkbox.trid:checked').map(function () {
        return this.value;
    }).get();
    $('#MultipleAttendanceRequestId').val(checkedValues);
}

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
function BulkApproveRejectAttendance() {
    var flag = 1;

    if (!$('input:checkbox.trid').is(':checked')) {
        flag = 0;
        toastr.error("Please select atleast one row.", "Error !");
    }
    if (ValidateApproveRejectStatus() == false) {
        flag = 0;
    }
    if (ValidateApproveRejectComment() == false) {
        flag = 0;
    }
    if (flag == 1) {
        var hdnUserId = $('#hdnUserId').val();
        var attendanceRequestIds = $('input:checkbox.trid:checked').map(function () {
            return this.value;
        }).get().join(",");
        var token = $('#hdn_TokenValue').val();
        var DTO = {
            UserId: hdnUserId,
            AttendanceRequestIds: attendanceRequestIds,
            Token: token
        };
        $.ajax({
            cache: false,
            type: "POST",
            url: "/Supervisor/Attendance/BulkApproveRejectAttendance",
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