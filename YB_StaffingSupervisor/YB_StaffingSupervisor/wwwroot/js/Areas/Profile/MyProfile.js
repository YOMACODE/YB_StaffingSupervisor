function ResetPassword() {
    debugger;
    var newPassword = $('#newpassword').val();
    var confirmPassword = $('#confirmpassword').val();
    var username = $('#username').val();
    if (newPassword == confirmPassword) {
        $.ajax({
            cache: false,
            type: "POST",
            data: { 'username': username, 'newPassword': newPassword },
            url: "/Supervisor/Profile/ResetPassword",
            beforeSend: function (x) {
                $(".preloader").attr("style", "display:block;");
            },
            success: function (data) {
                $(".preloader").attr("style", "display:none;");
                $('#btnApproveRejectAttendance').removeAttr("disabled", "disabled");
                if (data.msg == "Password Changed Successfully.") {
                    toastr.success(data.msg, "Success !");
                    location.reload();
                }
                else if (data.msg == "Something went wrong,Please try again.") {
                    toastr.error(data.msg, "Error !");
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
    else {
        toastr.error('New Password And Confirm Password Does Not Match!!!', "Error !");
    }
}