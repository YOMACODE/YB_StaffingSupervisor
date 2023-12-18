$(".uploadProfileImageFile").on('change', function (event) {
    var profileImageFile = $('.uploadProfileImageFile');
    var allowedExtensions = /(\.jpg|\.JPG|\.jpeg|\.JPEG|\.png|\.PNG|\.bmp|\.BMP)$/;
    if (profileImageFile.val() == null || profileImageFile.val() == '' || profileImageFile.val() == undefined) {
        toastr.error("* Please upload again a valid file.", "Error !");
    }
    else if (profileImageFile.val() != "" && !allowedExtensions.exec(profileImageFile.val())) {
        toastr.error("* Please upload again a valid file.", "Error !");
    }
    else if (profileImageFile.val() != "" && profileImageFile.length > 0) {
        const fsize = profileImageFile[0].files[0].size
        const file = Math.round(fsize / 1024);
        if (file <= 0 || file > 5120) {
            toastr.error("* Please upload document between 1KB to 5 MB.", "Error !");
        }
        else {
            var fileName, fileExtension;
            fileName = event.target.value;
            fileExtension = fileName.replace(/^.*\./, '');
            if (fileExtension == 'png' || fileExtension == 'PNG' || fileExtension == 'jpeg' || fileExtension == 'JPG' ||
                fileExtension == 'JPEG' || fileExtension == 'jpg' || fileExtension == 'bmp' || fileExtension == 'BMP') {

                var imgfile = event.target.files[0];
                var reader = new FileReader();

                reader.onload = function (e) {
                    $('#profileImage').attr('src', e.target.result);
                    $('#profileImage').css({ 'width': '100%', "height": '100%' });

                    // Call the function to upload the image
                    uploadProfileImageFile(imgfile);
                };

                reader.readAsDataURL(imgfile);

            }
            else {
                toastr.error("* Please upload again a valid file.", "Error !");
                $('#profileImage').attr('src', '/Proantov2assets/images/profile_noimage.png');
                $('#profileImage').css({ 'width': '100%', "height": '100%' });
            }
        }
    }
    else {
        toastr.error("Something went wrong. please try again!", "Error !");
    }
});
function uploadProfileImageFile(file) {
    var formData = new FormData();
    formData.append('profileImageFile', file);
    formData.append('Token', $('#hdn_TokenValue').val());


    // AJAX request to upload the image file
    $.ajax({
        url: '/Supervisor/Profile/UploadProfileImage',
        type: 'POST',
        data: formData,
        processData: false,
        contentType: false,
        success: function (data) {
            if (data.msg == "Profile image change successfully.") {
                toastr.success(data.msg, "Success !");
                //location.reload();
            }
            else {
                toastr.error(data.msg, "Error !");
            }
        },
        error: function (xhr, status, error) {
            toastr.error(error, "Error !");
            if (xhr.status == 403) {
                var url = $.parseJSON(xhr.responseText);
                window.location.href = url;
            }
        }
    });
}
function ChangeProfileImage(imgSrcPath) {
    if (imgSrcPath === null || imgSrcPath === undefined || imgSrcPath === "") {
        toastr.error("Image path not available", "Error !");
    }
    else {
        var Token = $('#hdn_TokenValue').val();
        var DTO = {
            imgSrcPath: imgSrcPath,
            Token: Token
        };
        $.ajax({
            cache: false,
            type: "POST",
            url: "/Supervisor/Profile/ChangeProfileImage",
            beforeSend: function (x) {
                //$(".preloader").attr("style", "display:block;");
                //$(".avatar-pic").off('click');
                $('.avatar-pic').prop('onclick', null).off('click');
            },
            data: DTO,
            success: function (data) {
                //$(".preloader").attr("style", "display:none;");
                $('.avatar-pic').attr('onclick', 'ChangeProfileImage($(this).attr("imgpath"))');
                if (data.msg == "Profile image change successfully.") {
                    toastr.success(data.msg, "Success !");
                    $('#profileImage').attr('src', '/' + imgSrcPath);
                    //location.reload();
                }
                else {
                    toastr.error(data.msg, "Error !");
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                //$(".preloader").attr("style", "display:none;");
                $('.avatar-pic').attr('onclick', 'ChangeProfileImage($(this).attr("imgpath"))');
                toastr.error("Server error.", "Error !");
                if (xhr.status == 403) {
                    var url = $.parseJSON(xhr.responseText);
                    window.location.href = url;
                }
            }
        });
    }
}
function togglePassword(inputId) {
    var input = $('#' + inputId);
    if (input.attr('type') === "password") {
        input.attr('type', 'text');
    } else {
        input.attr('type', 'password');
    }
}
function ValidateNewPassword() {
    var isValid = true;
    var newPassword = $('#newPassword').val();
    if (newPassword == "") {
        $("#newPassword-Error").text("*Please enter password.");
        $("#newPassword-Error").attr("style", "display:Inline;");
        isValid = false;
    }
    else {
        var regX = /^[\W]+$/;
        if ($.isNumeric(newPassword) || regX.test(newPassword)) {
            $("#newPassword-Error").text("*Please enter valid password.");
            $("#newPassword-Error").attr("style", "display:Inline;");
            isValid = false;
        }
        else {
            $("#newPassword-Error").text("");
            $("#newPassword-Error").attr("style", "display:Inline;");
        }
    }
    return isValid;
}
function ValidateConfirmPassword() {
    var isValid = true;
    var confirmPassword = $('#confirmPassword').val();
    if (confirmPassword == "") {
        $("#confirmPassword-Error").text("*Please enter confirm password.");
        $("#confirmPassword-Error").attr("style", "display:Inline;");
        isValid = false;
    }
    else {
        var regX = /^[\W]+$/;
        if ($.isNumeric(confirmPassword) || regX.test(confirmPassword)) {
            $("#confirmPassword-Error").text("*Please enter valid confirm password.");
            $("#confirmPassword-Error").attr("style", "display:Inline;");
            isValid = false;
        }
        else {
            $("#confirmPassword-Error").text("");
            $("#confirmPassword-Error").attr("style", "display:Inline;");
        }
    }
    return isValid;
}
function ValidateComparePassword() {
    var isValid = true;
    var newPassword = $("#newPassword").val();
    var confirmPassword = $('#confirmPassword').val();

    if (newPassword !== confirmPassword) {
        isValid = false;
    }

    return isValid;
}
function ChangePassword() {
    var IsFormValid = true;
    if (ValidateNewPassword() == false) {
        IsFormValid = false;
    }
    if (ValidateConfirmPassword() == false) {
        IsFormValid = false;
    }
    if (ValidateComparePassword() == false) {
        toastr.error("New and Confirm Password not matched", "Error !");
        IsFormValid = false;
    }

    if (IsFormValid) {
        var newPassword = $("#newPassword").val();
        //var confirmPassword = $('#confirmPassword').val();
        var Token = $('#hdn_TokenValue').val();
        var DTO = {
            newPassword: newPassword,
            //confirmPassword: confirmPassword,
            Token: Token
        };
        $('#btnChangePassword').attr("disabled", "disabled");
        $.ajax({
            cache: false,
            type: "POST",
            url: "/Supervisor/Profile/ChangePassword",
            beforeSend: function (x) {
                $(".preloader").attr("style", "display:block;");
            },
            data: DTO,
            success: function (data) {
                $(".preloader").attr("style", "display:none;");
                $('#btnChangePassword').removeAttr("disabled", "disabled");
                if (data.msg == "Password change successfully.") {
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
                $('#btnChangePassword').removeAttr("disabled", "disabled");
                if (xhr.status == 403) {
                    var url = $.parseJSON(xhr.responseText);
                    window.location.href = url;
                }
            }
        });
    }
}