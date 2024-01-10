function viewComment(id) {
    var viewbtnId = id;
    var ApproveRejectComment = $('[name = "leaveListing[' + viewbtnId + '].ApproveRejectComment"]').val();
    $("#hdnModelApproveRejectComment").val(ApproveRejectComment);
}
function viewLeaveReason(id) {
    var viewbtnId = id;
    var LeaveReason = $('[name = "leaveListing[' + viewbtnId + '].LeaveReason"]').val();
    $("#hdnModelApproveRejectComment").val(LeaveReason);
}
function viewVerifyLeave(id) {
    var viewbtnId = id;
    var LeaveRequestId = $('[name = "leaveListing[' + viewbtnId + '].LeaveRequestId"]').val();
    $("#hdnModelLeaveRequestId").val(LeaveRequestId);
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

function ApproveRejectLeave() {
    var IsFormValid = true;
    if (ValidateApproveRejectStatus() == false) {
        IsFormValid = false;
    }
    if (ValidateApproveRejectComment() == false) {
        IsFormValid = false;
    }

    if (IsFormValid) {
        var LeaveRequestId = $("#hdnModelLeaveRequestId").val();
        var ApproveRejectStatus = $('#ApproveRejectStatus').val();
        var ApproveRejectComment = $('#ApproveRejectComment').val();
        var Token = $('#hdn_TokenValue').val();
        var DTO = {
            LeaveRequestId: LeaveRequestId,
            ApproveRejectStatus: ApproveRejectStatus,
            ApproveRejectComment: ApproveRejectComment,
            Token: Token
        };
        $('#btnApproveRejectLeave').attr("disabled", "disabled");
        $.ajax({
            cache: false,
            type: "POST",
            url: "/Supervisor/Leave/ApproveRejectLeave",
            beforeSend: function (x) {
                $(".preloader").attr("style", "display:block;");
            },
            data: DTO,
            success: function (data) {
                $(".preloader").attr("style", "display:none;");
                $('#btnApproveRejectLeave').removeAttr("disabled", "disabled");
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
                $('#btnApproveRejectLeave').removeAttr("disabled", "disabled");
                if (xhr.status == 403) {
                    var url = $.parseJSON(xhr.responseText);
                    window.location.href = url;
                }
            }
        });
    }
}


function ExportLeaveReports() {
    debugger;
    var flag = 1;
    if (Validate() == false) {
        flag = 0;
    }
    if (flag == 1) {
        var SearchStatusType = $("#ApprovalStatus").val();
        


        window.location = "/Supervisor/Leave/ExportLeaveRequestReport?SearchStatusType=" + SearchStatusType ;
    }
}

function ValidateApprovalStatus() {
    var isValid = true;
    var ApprovalStatus = $('#ApprovalStatus').val();

    if (ApprovalStatus == "") {
        $("#ApprovalStatus-Error").text("*Please select Approval Status.");
        $("#ApprovalStatus-Error").attr("style", "display:Inline;");
        isValid = false;
    }
    else {
        $("#ApprovalStatus-Error").text("");
        $("#ApprovalStatus-Error").attr("style", "display:Inline;");
    }
    return isValid;
}


function Validate() {
    var IsFormValid = true;

    //if (ValidateClient() == false) {
    //    IsFormValid = false;
    //}
    //if (ValidateBusieness() == false) {
    //    IsFormValid = false;
    //}

    //if (ValidateSearchAttendanceType() == false) {
    //    IsFormValid = false;
    //}


    if (ValidateApprovalStatus() == false) {
        IsFormValid = false;
    }


    return IsFormValid;
}