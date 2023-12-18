$(document).ready(function () {

    $("#SalaryFrom").forceNumeric();
    $("#SalaryTo").forceNumeric();    

    //only numeric 
    jQuery('.numbersOnly').keyup(function (e) {
        this.value = this.value.replace(/[^0-9\.]/g, '');               
    });

    //only positive numeric without decimal
    jQuery('.positivenumberswithoutdecimalOnly').keyup(function (e) {
        this.value = this.value.replace(/[^0-9]/g, '');        
    });

    //only alphabet with space
    jQuery('.alphabetsOnly').keyup(function (e) {
        this.value = this.value.replace(/[^A-Za-z\s]+$/, '');        
    });

    $(function () {
        $(".spacenadspecial").keypress(function (e) {
            var keyCode = e.keyCode || e.which;

            $("#lblError").html("");

            //Regex for Valid Characters i.e. Alphabets and Numbers.
            var regex = /^[A-Za-z0-9]+$/;

            //Validate TextBox value against the Regex.
            var isValid = regex.test(String.fromCharCode(keyCode));
            if (!isValid) {
                $("#lblError").html("Only Alphabets and Numbers allowed.");
            }

            return isValid;
        });
    });
    $('.numberdecimal').keyup(function (event) {
        return isNumberDecimal(event, this)
    });

    $(".aaaa").keydown(function (e) {
        // Allow: backspace, delete, tab, escape, enter and .
        if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
            // Allow: Ctrl+A, Command+A
            (e.keyCode === 65 && (e.ctrlKey === true || e.metaKey === true)) ||
            // Allow: home, end, left, right, down, up
            (e.keyCode >= 35 && e.keyCode <= 40)) {
            // let it happen, don't do anything
            return;
        }
        // Ensure that it is a number and stop the keypress
        if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            e.preventDefault();
        }
    });

});
// forceNumeric() plug-in implementation
jQuery.fn.forceNumeric = function () {

    return this.each(function () {
        $(this).keydown(function (e) {
            var key = e.which || e.keyCode;

            if (!e.shiftKey && !e.altKey && !e.ctrlKey &&
                // numbers   
                key >= 48 && key <= 57 ||
                // Numeric keypad
                key >= 96 && key <= 105 ||
                // comma, period and minus, . on keypad
                key == 190 || key == 188 || key == 109 || key == 110 ||
                // Backspace and Tab and Enter
                key == 8 || key == 9 || key == 13 ||
                // Home and End
                key == 35 || key == 36 ||
                // left and right arrows
                key == 37 || key == 39 ||
                // Del and Ins
                key == 46 || key == 45)
                return true;

            return false;
        });
    });
}
//onkeypress--it will accept float and int
//example : onkeypress="javascript:return isNumber(event)"
function isNumber(evt) {
    var iKeyCode = (evt.which) ? evt.which : evt.keyCode
    if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
        return false;

    return true;
}


$(function () {
    $(".spacenadspecial").keypress(function (e) {
        var keyCode = e.keyCode || e.which;

        $("#lblError").html("");

        //Regex for Valid Characters i.e. Alphabets and Numbers.
        var regex = /^[A-Za-z0-9]+$/;

        //Validate TextBox value against the Regex.
        var isValid = regex.test(String.fromCharCode(keyCode));
        if (!isValid) {
            $("#lblError").html("Only Alphabets and Numbers allowed.");
        }

        return isValid;
    });
});

$(function () {
    $(".spacenadspecialVendor").keypress(function (e) {
        var keyCode = e.keyCode || e.which;

        $("#lblError1").html("");

        //Regex for Valid Characters i.e. Alphabets and Numbers.
        var regex = /^[A-Za-z0-9]+$/;

        //Validate TextBox value against the Regex.
        var isValid = regex.test(String.fromCharCode(keyCode));
        if (!isValid) {
            $("#lblError1").html("Only Alphabets and Numbers allowed.");
        }

        return isValid;
    });
});



//validate email--It will accept email address in upper case
function validateEmail(email) {
    var reg = /^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/
    if (reg.test(email)) {
        return true;
    }
    else {
        return false;
    }
}

//validate an email address with lower case chars.
function validateCaseSensitiveEmail(email) {
    var reg = /^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$/;
    if (reg.test(email)) {
        return true;
    }
    else {
        return false;
    }
}

//validate email address format
function ValidateEmail(email) {
    var expr = /^([\w-\.]+)@@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
    if (expr.test(email)) {
        return true;
    } else {
        return false;
    }
};

// Validate mobile number
function ValidateMobile(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    var actualkey = String.fromCharCode(charCode)
    if (actualkey == "-" || actualkey == "+") {
        return true;
    }
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
}

// Validate website format
function ValidateWebsite(website) {
    var expr = /^(http[s]?:\/\/){0,1}(www\.){0,1}[a-zA-Z0-9\.\-]+\.[a-zA-Z]{2,5}[\.]{0,1}$/;
    //var expr = /(ftp|http|https):\/\/(\w+:{0,1}\w*@)?(\S+)(:[0-9]+)?(\/|\/([\w#!:.?+=&%@!\-\/]))?/
    if (expr.test(website)) {
        return true;
    }
    else {
        return false;
    }
};

// forceNumeric() plug-in implementation
jQuery.fn.forceNumeric = function () {

    return this.each(function () {
        $(this).keydown(function (e) {
            var key = e.which || e.keyCode;

            if (!e.shiftKey && !e.altKey && !e.ctrlKey &&
                // numbers   
                key >= 48 && key <= 57 ||
                // Numeric keypad
                key >= 96 && key <= 105 ||
                // comma, period and minus, . on keypad
                key == 190 || key == 188 || key == 109 || key == 110 ||
                // Backspace and Tab and Enter
                key == 8 || key == 9 || key == 13 ||
                // Home and End
                key == 35 || key == 36 ||
                // left and right arrows
                key == 37 || key == 39 ||
                // Del and Ins
                key == 46 || key == 45)
                return true;

            return false;
        });
    });
}

function ValidateNumber(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    var actualkey = String.fromCharCode(charCode)
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {        
        return false;
    }
}

//only valid mobile number allow as per testing team 
//i)The first digit should contain number between 6 to 9.(ii)The rest 9 digit can contain any number between 0 to 9.
///(iii)Length must be 10 digit.It should not be less than or greater than 10 digit)
function validatemobilenumber(mobileno) {
    var regex = /^[6-9][0-9]{9}$/
    return regex.test(mobileno);
}

// Validate website format
function validateYoutubeUrl(website) {
    var expr = /^(https?\:\/\/)?(www\.)?(youtube\.com|youtu\.?be)\/.+$/;    
    if (expr.test(website)) {
        return true;
    }
    else {
        return false;
    }
};