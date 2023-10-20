$(document).ready(function () {
    $(".sample_structure").click(function () {
        $(".sample_structureshow").fadeIn();
        $(".sample_structureshow2").hide();
        $(".sample_structureshow3").hide();
        $(".sample_structureshow4").hide();
        $(".sample_structureshow5").hide();
        $(".sample_structureshow6").hide();
        $(".sample_structureshow7").hide();
        $(".sample_structureshow8").hide();
        $(".sample_structureshow9").hide();
    });

    $(".sample_structure2").click(function () {
        $(".sample_structureshow").hide();
        $(".sample_structureshow2").fadeIn();
        $(".sample_structureshow3").hide();
        $(".sample_structureshow4").hide();
        $(".sample_structureshow5").hide();
        $(".sample_structureshow6").hide();
        $(".sample_structureshow7").hide();
        $(".sample_structureshow8").hide();
        $(".sample_structureshow9").hide();
    });

    $(".sample_structure3").click(function () {
        $(".sample_structureshow").hide();
        $(".sample_structureshow2").hide();
        $(".sample_structureshow3").fadeIn();
        $(".sample_structureshow4").hide();
        $(".sample_structureshow5").hide();
        $(".sample_structureshow6").hide();
        $(".sample_structureshow7").hide();
        $(".sample_structureshow8").hide();
        $(".sample_structureshow9").hide();
    });
    $(".sample_structure4").click(function () {
        $(".sample_structureshow").hide();
        $(".sample_structureshow2").hide();
        $(".sample_structureshow3").hide();
        $(".sample_structureshow4").fadeIn();
        $(".sample_structureshow5").hide();
        $(".sample_structureshow6").hide();
        $(".sample_structureshow7").hide();
        $(".sample_structureshow8").hide();
        $(".sample_structureshow9").hide();
    });

    $(".sample_structure5").click(function () {
        $(".sample_structureshow").hide();
        $(".sample_structureshow2").hide();
        $(".sample_structureshow3").hide();
        $(".sample_structureshow4").hide();
        $(".sample_structureshow5").fadeIn();
        $(".sample_structureshow6").hide();
        $(".sample_structureshow7").hide();
        $(".sample_structureshow8").hide();
        $(".sample_structureshow9").hide();
    });
    $(".sample_structure6").click(function () {
        $(".sample_structureshow").hide();
        $(".sample_structureshow2").hide();
        $(".sample_structureshow3").hide();
        $(".sample_structureshow4").hide();
        $(".sample_structureshow5").hide();
        $(".sample_structureshow6").fadeIn();
        $(".sample_structureshow7").hide();
        $(".sample_structureshow8").hide();
        $(".sample_structureshow9").hide();
    });
    $(".sample_structure7").click(function () {
        $(".sample_structureshow").hide();
        $(".sample_structureshow2").hide();
        $(".sample_structureshow3").hide();
        $(".sample_structureshow4").hide();
        $(".sample_structureshow5").hide();
        $(".sample_structureshow6").hide();
        $(".sample_structureshow7").fadeIn();
        $(".sample_structureshow8").hide();
        $(".sample_structureshow9").hide();
    });
    $(".sample_structure8").click(function () {
        $(".sample_structureshow").hide();
        $(".sample_structureshow2").hide();
        $(".sample_structureshow3").hide();
        $(".sample_structureshow4").hide();
        $(".sample_structureshow5").hide();
        $(".sample_structureshow6").hide();
        $(".sample_structureshow7").hide();
        $(".sample_structureshow8").fadeIn();
        $(".sample_structureshow9").hide();
    });
    $(".sample_structure9").click(function () {
        $(".sample_structureshow").hide();
        $(".sample_structureshow2").hide();
        $(".sample_structureshow3").hide();
        $(".sample_structureshow4").hide();
        $(".sample_structureshow5").hide();
        $(".sample_structureshow6").hide();
        $(".sample_structureshow7").hide();
        $(".sample_structureshow8").hide();
        $(".sample_structureshow9").fadeIn();
    });

    $(".create-new-structure").click(function () {
        $(".create-new-structureshow").toggle();
    });

    $(".new-structure").click(function () {
        $(".add-new-structure").toggle();
        $(".new-structure").hide();

    });

    $(".show-edit").hover(function () {
        $('.editbtn').fadeIn();
    }, function () {
        $('.editbtn').fadeOut();
    });

    $(".clickedit").click(function () {
        $(".hideme").hide();
        $(".edit-date").fadeIn();
    });

    $(".edit-pfsetting").click(function () {
        $(".defaultoption").hide();
        $(".showeditoptions").fadeIn();
        $(".edit-pf").fadeIn();
    });

    $(".editpt").click(function () {
        $(".defaultoptionpt").hide();
        $(".showeditoptionspt").fadeIn();
        $(".choosestate").fadeIn();
        $(".ptsave").fadeIn();
        $("td.no-action").removeClass("no-action");
    });


    $(".edit-dw").click(function () {
        $(".ntn").hide();
        $(".showmeno").fadeIn();
        $(".chooseemp").fadeIn();
        $("div.no-action").removeClass("no-action");
    });


    $('#exampleCheckcustom').click(function () {
        if ($('#exampleCheckcustom').prop("checked") == true) {
            $(".mycitybox").prop('disabled', true);
        }
        else if ($('#exampleCheckcustom').prop("checked") == false) {
            $(".mycitybox").prop('disabled', false);
            $(".mycitybox").val('Select');
        }
    });

    $('#exampleCheck19').click(function () {
        if ($('#exampleCheck19').prop("checked") == true) {
            $(".mycitybox1").prop('disabled', true);
        }
        else if ($('#exampleCheck19').prop("checked") == false) {
            $(".mycitybox1").prop('disabled', false);
            $(".mycitybox1").val('Select');
        }
    });



    $(".format1").click(function () {
        $(".showf1").fadeIn();
        $(".showf2").hide();
        $(".showf3").hide();
        $(".showf4").hide();
        $(".showf5").hide();
        $(".showf6").hide();
        $(".showf7").hide();
        $(".format1").addClass("active_payslip");
        $(".format2").removeClass("active_payslip");
        $(".format3").removeClass("active_payslip");
        $(".format4").removeClass("active_payslip");
        $(".format5").removeClass("active_payslip");
        $(".format6").removeClass("active_payslip");
        $(".format7").removeClass("active_payslip");
    });

    $(".format2").click(function () {
        $(".showf2").fadeIn();
        $(".showf1").hide();
        $(".showf3").hide();
        $(".showf4").hide();
        $(".showf5").hide();
        $(".showf6").hide();
        $(".showf7").hide();
        $(".format2").addClass("active_payslip");
        $(".format1").removeClass("active_payslip");
        $(".format3").removeClass("active_payslip");
        $(".format4").removeClass("active_payslip");
        $(".format5").removeClass("active_payslip");
        $(".format6").removeClass("active_payslip");
        $(".format7").removeClass("active_payslip");
    });

    $(".format3").click(function () {
        $(".showf3").fadeIn();
        $(".showf2").hide();
        $(".showf1").hide();
        $(".showf4").hide();
        $(".showf5").hide();
        $(".showf6").hide();
        $(".showf7").hide();
        $(".format3").addClass("active_payslip");
        $(".format2").removeClass("active_payslip");
        $(".format1").removeClass("active_payslip");
        $(".format4").removeClass("active_payslip");
        $(".format5").removeClass("active_payslip");
        $(".format6").removeClass("active_payslip");
        $(".format7").removeClass("active_payslip");
    });

    $(".format4").click(function () {
        $(".showf4").fadeIn();
        $(".showf2").hide();
        $(".showf3").hide();
        $(".showf1").hide();
        $(".showf5").hide();
        $(".showf6").hide();
        $(".showf7").hide();
        $(".format4").addClass("active_payslip");
        $(".format2").removeClass("active_payslip");
        $(".format3").removeClass("active_payslip");
        $(".format1").removeClass("active_payslip");
        $(".format5").removeClass("active_payslip");
        $(".format6").removeClass("active_payslip");
        $(".format7").removeClass("active_payslip");
    });

    $(".format5").click(function () {
        $(".showf5").fadeIn();
        $(".showf2").hide();
        $(".showf3").hide();
        $(".showf4").hide();
        $(".showf1").hide();
        $(".showf6").hide();
        $(".showf7").hide();
        $(".format5").addClass("active_payslip");
        $(".format2").removeClass("active_payslip");
        $(".format3").removeClass("active_payslip");
        $(".format4").removeClass("active_payslip");
        $(".format1").removeClass("active_payslip");
        $(".format6").removeClass("active_payslip");
        $(".format7").removeClass("active_payslip");
    });

    $(".format6").click(function () {
        $(".showf6").fadeIn();
        $(".showf2").hide();
        $(".showf3").hide();
        $(".showf4").hide();
        $(".showf5").hide();
        $(".showf1").hide();
        $(".showf7").hide();
        $(".format6").addClass("active_payslip");
        $(".format2").removeClass("active_payslip");
        $(".format3").removeClass("active_payslip");
        $(".format4").removeClass("active_payslip");
        $(".format5").removeClass("active_payslip");
        $(".format1").removeClass("active_payslip");
        $(".format7").removeClass("active_payslip");
    });

    $(".format7").click(function () {
        $(".showf7").fadeIn();
        $(".showf2").hide();
        $(".showf3").hide();
        $(".showf4").hide();
        $(".showf5").hide();
        $(".showf6").hide();
        $(".showf1").hide();
        $(".format7").addClass("active_payslip");
        $(".format2").removeClass("active_payslip");
        $(".format3").removeClass("active_payslip");
        $(".format4").removeClass("active_payslip");
        $(".format5").removeClass("active_payslip");
        $(".format6").removeClass("active_payslip");
        $(".format1").removeClass("active_payslip");
    });


    $(".edu-info").click(function () {
        $(".edit-education").toggle();
    });

    $(".property-form").click(function () {
        $("#grad1").toggle();
    });

    $(".edit-personal").click(function () {
        $(".edit-data").fadeIn();
        $(".default-data").hide();
    });


    $(".edit-contact").click(function () {
        $(".edit-contactdata").fadeIn();
        $(".default-contactdata").hide();
    });


    $(".edit-address").click(function () {
        $(".edit-addressdata").fadeIn();
        $(".default-addressdata").hide();
    });


    $(".edit-bank").click(function () {
        $(".edit-bankdata").fadeIn();
        $(".default-bankdata").hide();
    });

    $(".edit-social").click(function () {
        $(".edit-socialdata").fadeIn();
        $(".default-socialdata").hide();
    });

    $(".edit-basic").click(function () {
        $(".edit-basicdata").fadeIn();
        $(".default-basicdata").hide();
    });

    $(".edit-work").click(function () {
        $(".edit-workdata").fadeIn();
        $(".default-workdata").hide();
    });

    $(".edit-regsignation").click(function () {
        $(".edit-resinationdata").fadeIn();
        $(".default-resignationdata").hide();
    });

    $(".edit-resume").click(function () {
        $(".edit-resumedata").fadeIn();
        $(".default-resumedata").hide();
    });

    $(".edit-profilevideo").click(function () {
        $(".edit-profilevideodata").fadeIn();
        $(".default-profilevideo").hide();
    });

    $(".edit-familydata").click(function () {
        $(".edit-familydatashow").fadeIn();
        $(".default-familydata").hide();
    });

    $(".edit-esicdis").click(function () {
        $(".edit-esicdisdata").fadeIn();
        $(".default-esicdisdata").hide();
    });

    $(".edit-offer").click(function () {
        $(".edit-offerdata").fadeIn();
        $(".default-offerdata").hide();
    });

    $(".edit-pr").click(function () {
        $(".edit-prdata").fadeIn();
        $(".default-prdata").hide();
    });

    $(".edit-pro").click(function () {
        $(".edit-prodata").fadeIn();
        $(".default-prodata").hide();
    });

    $(".edit-dec").click(function () {
        $(".edit-decdata").fadeIn();
        $(".default-decdata").hide();
    });

    $(".edit-bd").click(function () {
        $(".edit-bddata").fadeIn();
        $(".default-bddata").hide();
    });

    $(".edit-comp").click(function () {
        $(".edit-compdata").fadeIn();
        $(".default-compdata").hide();
    });

    $(".edit-rc").click(function () {
        $(".edit-rcdata").fadeIn();
        $(".default-rcdata").hide();
    });

    $(".edit-ot").click(function () {
        $(".edit-otdata").fadeIn();
        $(".default-otdata").hide();
    });

    $(".shoemoredetail").click(function () {
        $(".showmoredetaildata").fadeIn();
        $(".shoemoredetail").hide();
    });

    $(".hidemoredetail").click(function () {
        $(".showmoredetaildata").hide();
        $(".shoemoredetail").fadeIn();
    });


    $(".overview").click(function () {
        $(".overviewshow").fadeIn();
        $(".attendanceshow").hide();
        $(".variable").hide();
        $(".variableshow").hide();
        $(".soh").hide();
        $(".tax").hide();
        $(".tax-data").hide();
    });

    $(".al").click(function () {
        $(".overviewshow").hide();
        $(".attendanceshow").fadeIn();
        $(".variable").hide();
        $(".variableshow").hide();
        $(".salary_on_hold").hide();
        $(".tax").hide();
        $(".tax-data").hide();
    });

    $(".sr").click(function () {
        $(".overviewshow").hide();
        $(".attendanceshow").hide();
        $(".variable").fadeIn();
        $(".variableshow").hide();
        $(".salary_on_hold").hide();
        $(".tax").hide();
        $(".tax-data").hide();
    });

    $(".va").click(function () {
        $(".overviewshow").hide();
        $(".attendanceshow").hide();
        $(".variable").hide();
        $(".variableshow").fadeIn();
        $(".salary_on_hold").hide();
        $(".tax").hide();
        $(".tax-data").hide();
    });

    $(".soh").click(function () {
        $(".overviewshow").hide();
        $(".attendanceshow").hide();
        $(".variable").hide();
        $(".variableshow").hide();
        $(".salary_on_hold").fadeIn();
        $(".tax").hide();
        $(".tax-data").hide();
    });

    $(".to").click(function () {
        $(".overviewshow").hide();
        $(".attendanceshow").hide();
        $(".variable").hide();
        $(".variableshow").hide();
        $(".salary_on_hold").hide();
        $(".tax").fadeIn();
        $(".tax-data").hide();
    });


    $(".prp").click(function () {
        $(".overviewshow").hide();
        $(".attendanceshow").hide();
        $(".variable").hide();
        $(".variableshow").hide();
        $(".salary_on_hold").hide();
        $(".tax").hide();
        $(".tax-data").fadeIn();
    });

});

function openfileDialog3() {
    $("#fileLoader").click();
}

function openfileDialog1() {
    $("#fileLoader1").click();
}
function openfileDialog2() {
    $("#fileLoader2").click();
}



$(document).ready(function () {
    $('#fileLoader').change(function (e) {
        var fileName = e.target.files[0].name;
        // alert('The file "' + fileName +  '" has been selected.');
        $('#filenameshow').html(fileName);
        $('#filenameshow').show();
        $('#btnOpenFileDialog').hide();
    });

    $('#fileLoader1').change(function (e) {
        var fileNamegst = e.target.files[0].name;
        // alert('The file "' + fileName +  '" has been selected.');
        $('#filenameshow1').html(fileNamegst);
        $('#filenameshow1').show();
        $('#btnOpenFileDialog1').hide();
    });

    $('#fileLoader2').change(function (e) {
        var fileNamegst = e.target.files[0].name;
        // alert('The file "' + fileName +  '" has been selected.');
        $('#filenameshow2').html(fileNamegst);
        $('#filenameshow2').show();
        $('#btnOpenFileDialog2').hide();
    });

    $('.myselectbox').change(function () {
        var selectedCountry = $('.myselectbox').val();
        if (selectedCountry === 'custom') {
            $(".date-range").fadeIn();
        }
        else {
            $(".date-range").hide();
        }
    });


    //$('.mysel').change(function () {
    //    var selectedval = $('.mysel').val();
    //    if (selectedval === 'bank_passbook') {
    //        $(".bank_detail").fadeIn();
    //        $(".pan_detail").hide();
    //        $(".dl_detail").hide();
    //        $(".aadhar_detail").hide();
    //        $(".voter_detail").hide();
    //        $(".id-field").hide();
    //        $(".passport_detail").hide();
    //        $(".esic_detail").hide();
    //    }

    //    else if (selectedval === 'pan_card') {
    //        $(".pan_detail").fadeIn();
    //        $(".bank_detail").hide();
    //        $(".dl_detail").hide();
    //        $(".aadhar_detail").hide();
    //        $(".voter_detail").hide();
    //        $(".id-field").hide();
    //        $(".passport_detail").hide();
    //        $(".esic_detail").hide();
    //    }

    //    else if (selectedval === 'driving_licence') {
    //        $(".dl_detail").fadeIn();
    //        $(".bank_detail").hide();
    //        $(".pan_detail").hide();
    //        $(".aadhar_detail").hide();
    //        $(".voter_detail").hide();
    //        $(".id-field").hide();
    //        $(".passport_detail").hide();
    //        $(".esic_detail").hide();
    //    }

    //    else if (selectedval === 'aadhar_card') {
    //        $(".aadhar_detail").fadeIn();
    //        $(".bank_detail").hide();
    //        $(".pan_detail").hide();
    //        $(".dl_detail").hide();
    //        $(".voter_detail").hide();
    //        $(".id-field").hide();
    //        $(".passport_detail").hide();
    //        $(".esic_detail").hide();
    //    }

    //    else if (selectedval === 'voter_id') {
    //        $(".voter_detail").fadeIn();
    //        $(".bank_detail").hide();
    //        $(".pan_detail").hide();
    //        $(".dl_detail").hide();
    //        $(".aadhar_detail").hide();
    //        $(".id-field").hide();
    //        $(".passport_detail").hide();
    //        $(".esic_detail").hide();
    //    }

    //    else if (selectedval === 'passport') {
    //        $(".passport_detail").fadeIn();
    //        $(".voter_detail").hide();
    //        $(".bank_detail").hide();
    //        $(".pan_detail").hide();
    //        $(".dl_detail").hide();
    //        $(".aadhar_detail").hide();
    //        $(".id-field").hide();
    //        $(".esic_detail").hide();
    //    }

    //    else if (selectedval === 'esic_card') {
    //        $(".esic_detail").fadeIn();
    //        $(".passport_detail").hide();
    //        $(".voter_detail").hide();
    //        $(".bank_detail").hide();
    //        $(".pan_detail").hide();
    //        $(".dl_detail").hide();
    //        $(".aadhar_detail").hide();
    //        $(".id-field").hide();
    //    }

    //    else if (selectedval === 'choose_doc') {
    //        $(".esic_detail").hide();
    //        $(".passport_detail").hide();
    //        $(".voter_detail").hide();
    //        $(".bank_detail").hide();
    //        $(".pan_detail").hide();
    //        $(".dl_detail").hide();
    //        $(".aadhar_detail").hide();
    //        $(".id-field").hide();
    //    }


    //    else {
    //        $(".bank_detail").hide();
    //        $(".id-field").show();
    //    }
    //});


    $('.pfcontribution').change(function () {
        var myoption = $('.pfcontribution').val();
        $(".hidethis").hide();
        $("#showpf").html(myoption);
        if (myoption === 'Fixed Amount') {
            $(".showtxt").fadeIn();
        }
        else {
            $(".showtxt").hide();
        }
    });


});


//multistep form

$(document).ready(function () {

    var current_fs, next_fs, previous_fs; //fieldsets
    var opacity;

    $(".next").click(function () {

        current_fs = $(this).parent();
        next_fs = $(this).parent().next();

        //Add Class Active
        $("#progressbar li").eq($("fieldset").index(next_fs)).addClass("active");

        //show the next fieldset
        next_fs.show();
        //hide the current fieldset with style
        current_fs.animate({ opacity: 0 }, {
            step: function (now) {
                // for making fielset appear animation
                opacity = 1 - now;

                current_fs.css({
                    'display': 'none',
                    'position': 'relative'
                });
                next_fs.css({ 'opacity': opacity });
            },
            duration: 600
        });
    });

    $(".previous").click(function () {

        current_fs = $(this).parent();
        previous_fs = $(this).parent().prev();

        //Remove class active
        $("#progressbar li").eq($("fieldset").index(current_fs)).removeClass("active");

        //show the previous fieldset
        previous_fs.show();

        //hide the current fieldset with style
        current_fs.animate({ opacity: 0 }, {
            step: function (now) {
                // for making fielset appear animation
                opacity = 1 - now;

                current_fs.css({
                    'display': 'none',
                    'position': 'relative'
                });
                previous_fs.css({ 'opacity': opacity });
            },
            duration: 600
        });
    });

    $('.radio-group .radio').click(function () {
        $(this).parent().find('.radio').removeClass('selected');
        $(this).addClass('selected');
    });

    $(".submit").click(function () {
        return false;
    })

});


function edit() {
    document.getElementById("tomcat").style.display = "none";
    document.getElementById("tomcat2").style.display = "block";
}
function back() {
    document.getElementById("tomcat").style.display = "block";
    document.getElementById("tomcat2").style.display = "none";
}
function save() {
    document.getElementById("tomcat").style.display = "block";
    document.getElementById("tomcat2").style.display = "none";
}
function editr() {
    document.getElementById("two").style.display = "none";
    document.getElementById("twotwo").style.display = "block";
}
function back2() {
    document.getElementById("two").style.display = "block";
    document.getElementById("twotwo").style.display = "none";
}
function save2() {
    document.getElementById("two").style.display = "block";
    document.getElementById("twotwo").style.display = "none";
}
function edit3() {
    document.getElementById("three").style.display = "none";
    document.getElementById("threethree").style.display = "block";
}
function back3() {
    document.getElementById("three").style.display = "block";
    document.getElementById("threethree").style.display = "none";
}
function save3() {
    document.getElementById("three").style.display = "block";
    document.getElementById("threethree").style.display = "none";
}
function edit4() {
    document.getElementById("four").style.display = "none";
    document.getElementById("fourfour").style.display = "block";
}
function back4() {
    document.getElementById("four").style.display = "block";
    document.getElementById("fourfour").style.display = "none";
}
function save4() {
    document.getElementById("four").style.display = "block";
    document.getElementById("fourfour").style.display = "none";
}
function edit6() {
    document.getElementById("six").style.display = "none";
    document.getElementById("sixsix").style.display = "block";
}
function back6() {
    document.getElementById("six").style.display = "block";
    document.getElementById("sixsix").style.display = "none";
}
function save6() {
    document.getElementById("six").style.display = "block";
    document.getElementById("sixsix").style.display = "none";
}
function edit7() {
    document.getElementById("seven").style.display = "none";
    document.getElementById("sevenseven").style.display = "block";
}
function back7() {
    document.getElementById("seven").style.display = "block";
    document.getElementById("sevenseven").style.display = "none";
}
function save7() {
    document.getElementById("seven").style.display = "block";
    document.getElementById("sevenseven").style.display = "none";
}
function edit5() {
    document.getElementById("five").style.display = "none";
    document.getElementById("fivefive").style.display = "block";
}
function back5() {
    document.getElementById("five").style.display = "block";
    document.getElementById("fivefive").style.display = "none";
}
function save5() {
    document.getElementById("five").style.display = "block";
    document.getElementById("fivefive").style.display = "none";
}
function edit8() {
    document.getElementById("eight").style.display = "none";
    document.getElementById("eighteight").style.display = "block";
}
function back8() {
    document.getElementById("eight").style.display = "block";
    document.getElementById("eighteight").style.display = "none";
}
function save8() {
    document.getElementById("eight").style.display = "block";
    document.getElementById("eighteight").style.display = "none";
}
function edit9() {
    document.getElementById("nine").style.display = "none";
    document.getElementById("ninenine").style.display = "block";
}
function back9() {
    document.getElementById("nine").style.display = "block";
    document.getElementById("ninenine").style.display = "none";
}
function save9() {
    document.getElementById("nine").style.display = "block";
    document.getElementById("ninenine").style.display = "none";
}
function edit10() {
    document.getElementById("ten").style.display = "none";
    document.getElementById("tenten").style.display = "block";
}
function back10() {
    document.getElementById("ten").style.display = "block";
    document.getElementById("tenten").style.display = "none";
}
function save10() {
    document.getElementById("ten").style.display = "block";
    document.getElementById("tenten").style.display = "none";
}


