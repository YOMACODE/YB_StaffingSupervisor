function ExportLeaveApprovalRejectReport() {
	debugger;
	var flag = 1;
	//if (Validate() == false) {
	//	flag = 0;
	//}
	if (flag == 1) {
		var yomaid = $("#yomaid").val();
		var name = $("#name").val();



		window.location = "/MyTeam/MyTeam/ExportLeaveReport?SearchUserCode=" + yomaid + '&SearchAssociateName=' + name;
	}
}

function ClearButton() {
	$('#yomaid').val('');
	$('#name').val('');
	$('#yomaid').val('');
}