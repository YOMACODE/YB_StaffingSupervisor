function viewCommentQ(id) {
    var viewbtnId = id;
    var ApproveRejectComment = $('[name = "quickAttendanceListing[' + viewbtnId + '].ApproveRejectComment"]').val();
    $("#hdnModelApproveRejectComment").val(ApproveRejectComment);
}
function viewCommentS(id) {
    var viewbtnId = id;
    var ApproveRejectComment = $('[name = "selfieBasedAttendanceListing[' + viewbtnId + '].ApproveRejectComment"]').val();
    $("#hdnModelApproveRejectComment").val(ApproveRejectComment);
}