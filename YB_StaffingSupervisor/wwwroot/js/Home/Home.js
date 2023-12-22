toastr.options = {
    "closeButton": true,
    "debug": false,
    "newestOnTop": false,
    "progressBar": true,
    "positionClass": "toast-top-right",
    "preventDuplicates": true,
    "onclick": null,
    "showDuration": "300",
    "hideDuration": "1000",
    "timeOut": "5000",
    "extendedTimeOut": "1000",
    "showEasing": "swing",
    "hideEasing": "linear",
    "showMethod": "fadeIn",
    "hideMethod": "fadeOut"
}

function ValidateLogin() {
    var UserName = $('#UserName').val();
    var Password = $('#Password').val();
    var msgerror = "";
    if (typeof UserName === "undefined" || UserName == "") {
        $('#UserName').css('border', '1px solid red');
        msgerror = msgerror + "* Please enter username. <br />";
    }
    else {
        $('#UserName').css('border', '0px solid grey');
    }
    if (typeof Password === "undefined" || Password == "") {
        $('#Password').css('border', '1px solid red');
        msgerror = msgerror + "* Please enter password. <br />";
    }
    else {
        $('#Password').css('border', '0px solid grey');
    }
    if (msgerror != "") {
        toastr.options.timeOut = 1500; // 1.5s
        toastr.error(msgerror, "WARNING!");
        return false;
    }
    else {
        return true;
    }
}

function Login() {
    if (ValidateLogin()) {
        var UserName = $('#UserName').val();
        var Password = $('#Password').val();
        $.ajax({
            url: "/Home/Index",
            type: "POST",
            data: { 'UserName': UserName, 'Password': Password },
            success: function (data) {
                if (data.tokenValue != null) {
                    localStorage.setItem('accessToken', data.tokenValue);
                    //window.location.href = data.returnURL;
                }
                else {
                    toastr.error(data.errorMessage, "Error!");
                }
            }
        });
    }
}