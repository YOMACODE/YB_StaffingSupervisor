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
function ExportLeaveRequestReport() {
    var SearchUserCode = $("#SearchUserCode").val();
    var SearchLeaveFrom = $("#SearchLeaveFrom").val();
    var SearchLeaveTo = $("#SearchLeaveTo").val();
    var SearchStatusType = $("#SearchStatusType").val();

    window.location = "/Supervisor/Leave/LeaveRequestReport?SearchUserCode=" + SearchUserCode + "&SearchLeaveFrom=" + SearchLeaveFrom + '&SearchLeaveTo=' + SearchLeaveTo + '&SearchStatusType=' + SearchStatusType;
}