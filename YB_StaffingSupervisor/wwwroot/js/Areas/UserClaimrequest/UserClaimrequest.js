function ExportUserClaimrequestReports() {
	debugger;
	var flag = 1;
	if (Validate() == false) {
		flag = 0;
	}
	if (flag == 1) {
		var claimType = $("#claimType").val();
		var claimStatus = $("#claimStatus").val();
		var Year = $("#SearchYear").val();
		var Month = $("#SearchMonth").val();


		window.location = "/Supervisor/Claim/ExportUserClaimrequestReport?claimType=" + claimType + '&claimStatus=' + claimStatus + '&Year=' + Year + '&Month=' + Month;
	}
}

function ValidateSearchYear() {
    var isValid = true;
    var SearchYear = $('#SearchYear').val();

    if (SearchYear == "") {
        $("#SearchYear-Error").text("*Please select Year.");
        $("#SearchYear-Error").attr("style", "display:Inline;");
        isValid = false;
    }
    else {
        $("#SearchYear-Error").text("");
        $("#SearchYear-Error").attr("style", "display:Inline;");
    }
    return isValid;
}
function ValidateSearchMonth() {
    var isValid = true;
    var SearchMonth = $('#SearchMonth').val();

    if (SearchMonth == "") {
        $("#SearchMonth-Error").text("*Please select Month.");
        $("#SearchMonth-Error").attr("style", "display:Inline;");
        isValid = false;
    }
    else {
        $("#SearchMonth-Error").text("");
        $("#SearchMonth-Error").attr("style", "display:Inline;");
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
   

        if (ValidateSearchYear() == false) {
            IsFormValid = false;
        }
        if (ValidateSearchMonth() == false) {
            IsFormValid = false;
        }
    
    return IsFormValid;
}


