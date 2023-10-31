
function viewApproveRejectAttendance(id, btnText) {
    var viewbtnId = id;
    var AttendanceId = $('[name = "auditInvoiceListing[' + viewbtnId + '].DailyAttendanceId"]').val();
    var comment = $('[name = "userAttendanceListing[' + viewbtnId + '].ApproveRejectedComment"]').val();
    $("#hdnModelDailyAttendanceId").val(AttendanceId);
    $("#approveRejectReason").val(comment);

    var isAttendanceApproved = $('[name = "userAttendanceListing[' + viewbtnId + '].IsInvoiceApprovedByAudit"]').val();
    if (isAttendanceApproved == 'Approved' || btnText == 'Rejected View' || btnText == 'Approved View') {
        $("#approve").hide();
    }
    else {
        $("#approve").show();
    }

    if (btnText == 'Reject') {
        $("#approve").val("Reject");
    }
    else {
        $("#approve").val("Approve");
    }
}

function funComment() {
    var isValid = true;
    var comment = $('#approveRejectReason').val();
    var statusVal = $('#approve').val();
    if (comment == "" && statusVal == "Reject") {
        $('#errorComment').text('*Please enter comment.');
        isValid = false;
    }
    else {
        $('#errorComment').text('');
    }
    return isValid;
}

function ApproveRejectAttendance() {
    if (funComment() == false) {
        return false;
    }
    else {
        var chkButton = $("#approve").val();
        var Status;
        if (chkButton == "Approve") {
            Status = 1;
        }
        else {
            Status = 0;
        }
        var AttendanceId = $("#hdnModelAttendanceId").val();
        var Reason = $('#approveRejectReason').val();
        var Token = $('#hdn_TokenValue').val();
        $("body").addClass("loading");
        $.ajax({
            cache: false,
            type: "POST",
            url: "/Supervisor/Attendance/ApproveRejectAttendance",
            data: { "AttendanceId": AttendanceId, "Status": Status, "Reason": Reason, "Token": Token },
            success: function (data) {
                if (data.msg == "Attendance approved successfully." || data.msg == "Attendance rejected successfully.") {
                    toastr.success(data.msg, "Success !");
                    location.reload();
                }
                else {
                    toastr.error(data.msg, "Error !");
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                $("body").removeClass("loading");
            }
        });
    }
}

