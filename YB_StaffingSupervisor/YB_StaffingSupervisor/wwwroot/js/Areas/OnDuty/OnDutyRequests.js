function viewComment(id) {
    var viewbtnId = id;
    var ApproveRejectComment = $('[name = "onDutyListing[' + viewbtnId + '].ApproveRejectComment"]').val();
    $("#hdnModelApproveRejectComment").val(ApproveRejectComment);
}
function viewRemark(id) {
    var viewbtnId = id;
    var Reamark = $('[name = "onDutyListing[' + viewbtnId + '].Remark"]').val();
    $("#hdnModelApproveRejectComment").val(Reamark);
}
function viewVerifyOnDuty(id) {
    var viewbtnId = id;
    var OnDutyId = $('[name = "onDutyListing[' + viewbtnId + '].OnDutyRequestId"]').val();
    $("#hdnModelOnDutyId").val(OnDutyId);
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

function ApproveRejectOnDuty() {
    var IsFormValid = true;
    if (ValidateApproveRejectStatus() == false) {
        IsFormValid = false;
    }
    if (ValidateApproveRejectComment() == false) {
        IsFormValid = false;
    }
    
    if (IsFormValid) {
        var OnDutyId = $("#hdnModelOnDutyId").val();
        var ApproveRejectStatus = $('#ApproveRejectStatus').val();
        var ApproveRejectComment = $('#ApproveRejectComment').val();
        var Token = $('#hdn_TokenValue').val();
        var DTO = {
            onDutyRequestId: OnDutyId,
            ApproveRejectStatus: ApproveRejectStatus,
            ApproveRejectComment: ApproveRejectComment,
            Token: Token
        };
        $('#btnApproveRejectOnDuty').attr("disabled", "disabled");
        $.ajax({
            cache: false,
            type: "POST",
            url: "/Supervisor/OnDuty/ApproveRejectOnDuty",
            beforeSend: function (x) {
                $(".preloader").attr("style", "display:block;");
            },
            data: DTO,
            success: function (data) {
                $(".preloader").attr("style", "display:none;");
                $('#btnApproveRejectOnDuty').removeAttr("disabled", "disabled");
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
                $('#btnApproveRejectOnDuty').removeAttr("disabled", "disabled");
                if (xhr.status == 403) {
                    var url = $.parseJSON(xhr.responseText);
                    window.location.href = url;
                }
            }
        });
    }
}