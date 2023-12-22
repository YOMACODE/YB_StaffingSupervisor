//it will read on edit time when countryId>0 and stateId>0
$(document).ready(function () {
    var countryId = $('.CommanCountry').val();
    var stateId = $('.hdnCommanState').val();
    var cityId = $('.hdnCommanCity').val();
    var branchId = $('.hdnCommanBranch').val();
    if (countryId != '' && countryId != 'Select Country') {
        $.ajax({
            type: "GET",
            url: "/Common/DropDownListofState",
            data: { CountryId: countryId, Token: $('#hdn_TokenValue').val() },
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (stateresponse) {
                if (stateresponse != null) {
                    $('.CommanState').html('');
                    var options = '';
                    options += '<option value="">Select State</option>';
                    for (var i = 0; i < stateresponse.length; i++) {
                        if (stateresponse[i].stateId == stateId) {
                            options += '<option value="' + stateresponse[i].stateId + '" selected="true">' + stateresponse[i].stateName + '</option>';
                        } else {
                            options += '<option value="' + stateresponse[i].stateId + '">' + stateresponse[i].stateName + '</option>';
                        }
                    }
                    $('.CommanState').append(options);
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                $("body").removeClass("loading");
                if (xhr.status == 403) {
                    var url = $.parseJSON(xhr.responseText);
                    window.location.href = url;
                }
            }  
        });
    }
    if (countryId != '' && stateId != '' && countryId != 'Select Country' && stateId != 'undefined') {
        $.ajax({
            type: "GET",
            url: "/Common/DropDownListofCity",
            data: { CountryId: countryId, StateId: stateId, Token: $('#hdn_TokenValue').val() },
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (cityresponse) {
                if (cityresponse != null) {
                    $('.CommanCity').html('');
                    var options = '';
                    options += '<option value="">Select City</option>';
                    for (var i = 0; i < cityresponse.length; i++) {
                        if (cityresponse[i].cityId == cityId) {
                            options += '<option value="' + cityresponse[i].cityId + '" selected="true">' + cityresponse[i].cityName + '</option>';
                        } else {
                            options += '<option value="' + cityresponse[i].cityId + '">' + cityresponse[i].cityName + '</option>';
                        }
                    }
                    $('.CommanCity').append(options);
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                $("body").removeClass("loading");
                if (xhr.status == 403) {
                    var url = $.parseJSON(xhr.responseText);
                    window.location.href = url;
                }
            }  
        });
    }
});

function StateBasedonCountryID() {
    var countryId = $(".CommanCountry").val() == "Select Country" ? 0 : $(".CommanCountry").val();
    $.ajax({
        type: "GET",
        url: "/Common/DropDownListofState",
        data: { CountryId: countryId, Token: $('#hdn_TokenValue').val() },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (stateresponse) {
            if (stateresponse != null && stateresponse.length > 0) {
                $('.CommanState').html('');
                var options = '';
                options += '<option value="">Select State</option>';
                for (var i = 0; i < stateresponse.length; i++) {
                    options += '<option value="' + stateresponse[i].stateId + '">' + stateresponse[i].stateName + '</option>';
                }
                $('.CommanState').append(options);
            }
            else {
                $('.CommanState').html('');

                var options = '';
                options += '<option value="">Select State</option>';
                $('.CommanState').append(options);

                $('.CommanCity').html('');
                var optionscity = '';
                optionscity += '<option value="">Select City</option>';
                $('.CommanCity').append(optionscity);
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            $("body").removeClass("loading");
            if (xhr.status == 403) {
                var url = $.parseJSON(xhr.responseText);
                window.location.href = url;
            }
        }  
    });
}
function CityBasedonCountryStateID() {
    var countryId = $('.CommanCountry').val();
    var stateId = $('.CommanState').val();
    $.ajax({
        type: "GET",
        url: "/Common/DropDownListofCity",
        data: { CountryId: countryId, StateId: stateId, Token: $('#hdn_TokenValue').val() },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (cityresponse) {
            if (cityresponse != null) {
                $('.CommanCity').html('');
                var options = '';
                options += '<option value="">Select City</option>';
                for (var i = 0; i < cityresponse.length; i++) {
                    options += '<option value="' + cityresponse[i].cityId + '">' + cityresponse[i].cityName + '</option>';
                }
                $('.CommanCity').append(options);
            }
            //Changes
            else {
                $('.CommanCity').html('');
                options = '<option value="">Select City</option>';
                $('.CommanCity').append(options);
            }
            $('.CommanCity').trigger("liszt:updated");
        },
        error: function (xhr, ajaxOptions, thrownError) {
            $("body").removeClass("loading");
            if (xhr.status == 403) {
                var url = $.parseJSON(xhr.responseText);
                window.location.href = url;
            }
        }  
    });
}

function WorkLocationBasedOnCSCId() {
    var countryId = $('.CommanCountry').val();
    var stateId = $('.CommanState').val();
    var cityId = $('.CommanCity').val();

    $.ajax({
        type: "GET",
        url: "/Common/GetWorkLocationList",
        data: { CountryId: countryId, StateId: stateId, CityId: cityId, Token: $('#hdn_TokenValue').val() },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (worklocationresponse) {
            if (worklocationresponse != null) {
                $('.CommanWorkLocation').html('');
                var options = '';
                options += '<option value="">Select Work Location</option>';
                for (var i = 0; i < worklocationresponse.length; i++) {
                    options += '<option value="' + worklocationresponse[i].workLocationId + '">' + worklocationresponse[i].workLocationName + '</option>';
                }
                $('.CommanWorkLocation').append(options);
            }
            //Changes
            else {
                $('.CommanWorkLocation').html('');
                options = '<option value="">Select Work Location</option>';
                $('.CommanWorkLocation').append(options);
            }
            $('.CommanWorkLocation').trigger("liszt:updated");
        },
        error: function (xhr, ajaxOptions, thrownError) {
            $("body").removeClass("loading");
            if (xhr.status == 403) {
                var url = $.parseJSON(xhr.responseText);
                window.location.href = url;
            }
        }  
    });
}

function BranchBasedonStateID() {
    var countryId = $(".CommanCountry").val() == "Select Country" ? 0 : $(".CommanCountry").val();
    var stateId = $(".CommanState").val() == "Select Branch" ? 0 : $(".CommanState").val();
    $.ajax({
        type: "GET",
        url: "/Master/Holiday/GetBranchList",
        data: { CountryId: countryId, StateId: stateId, Token: $('#hdn_TokenValue').val() },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response != null) {
                $('.CommanBranch').html('');
                var options = '';
                options += '<option value="">Select Branch</option>';
                for (var i = 0; i < response.length; i++) {
                    options += '<option value="' + response[i].branchId + '">' + response[i].branchName + '</option>';
                }
                $('.CommanBranch').append(options);
            }
            else {
                $('.CommanBranch').html('');

                var options = '';
                options += '<option value="">Select Branch</option>';
                $('.CommanBranch').append(options);
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            $("body").removeClass("loading");
            if (xhr.status == 403) {
                var url = $.parseJSON(xhr.responseText);
                window.location.href = url;
            }
        }  
    });
}

function ValidateNumber(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    var actualkey = String.fromCharCode(charCode)
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        //alert("Enter numbers only!");
        return false;
    }
}