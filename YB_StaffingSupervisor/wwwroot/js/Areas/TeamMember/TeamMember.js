function ExportTeamMemberReport() {
	var flag = 1;
	//if (Validate() == false) {
	//	flag = 0;
	//}
	if (flag == 1) {
		var yomaid = $("#yomaid").val();
		var name = $("#name").val();
		var mobile = $("#mobile").val();
		var email = $("#email").val();
		var designation = $("#designation").val();
		var joining = $("#joining").val();


		window.location = "/Supervisor/MyTeam/ExportTeamMemberReport?yomaid=" + yomaid + '&name=' + name + '&mobile=' + mobile + '&email=' + email + '&designation=' + designation + '&joining=' + joining;
	}
}