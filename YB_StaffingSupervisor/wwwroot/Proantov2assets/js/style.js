$(document).ready(function () {
    $("#one").click(function () {
        $(".btn-floating, .sidebar-nav ul .sidebar-item.selected > .sidebar-link, #sumit, .happen, .blue, .between,.price, .helli, .next, .next1, .hellobutton, #abc").css("background", "rgb(245, 151, 81)");
        $(".customizer .service-panel-toggle").css("background", "#e84c1d");
    });
    $("#two").click(function () {
        $(".btn-floating, .sidebar-nav ul .sidebar-item.selected > .sidebar-link, #sumit, .happen, .blue, .between,.price, .helli, .next, .next1, .hellobutton, #abc").css("background", "#fe5419");
        $(".customizer .service-panel-toggle").css("background", "red");
    });
    $("#three").click(function () {
        $(".btn-floating, .sidebar-nav ul .sidebar-item.selected > .sidebar-link, #sumit, .happen, .blue, .between,.price, .helli, .next, .next1, .hellobutton, #abc").css("background", "#00b0ff");
        $(".customizer .service-panel-toggle").css("background", "rgb(15, 140, 196) none repeat scroll 0% 0%");
    });
    $("#four").click(function () {
        $(".btn-floating, .sidebar-nav ul .sidebar-item.selected > .sidebar-link, #sumit, .happen, .blue, .between,.price, .helli, .next, .next1, .hellobutton, #abc").css("background", "#6659f7");
        $(".customizer .service-panel-toggle").css("background", "rgb(50, 33, 236) none repeat scroll 0% 0%");
    });
    $("#five").click(function () {
        $(".btn-floating, .sidebar-nav ul .sidebar-item.selected > .sidebar-link, #sumit,.happen, .blue, .between,.price, .helli, .next, .next1, .hellobutton, #abc").css("background", "#1d2e42");
        $(".customizer .service-panel-toggle").css("background", "rgb(0, 0, 0) none repeat scroll 0% 0%");
    });
});


$(document).ready(function () {

    $('#linkButton').click(function () {
        toastr.success('CITY ADDED');
    });
    $('#linkButton2').click(function () {
        toastr.success('STATE ADDED');
    });
});
function hello() {
    toastr.warning('Deleted Successful')
}
function hello2() {
    toastr.warning('Deleted Successful')
}


$(document).ready(function () {
    $('#mdate, #mdate4, #mdate3, #edate, #edate5, #edate6, #mdate6, #edate7').bootstrapMaterialDatePicker({
        weekStart: 0, time: false
    }
    );
    $('#timepicker').bootstrapMaterialDatePicker({
        format: 'HH:mm', time: true, date: false
    }
    );
    $('#date-format').bootstrapMaterialDatePicker({
        format: 'dddd DD MMMM YYYY - HH:mm'
    }
    );
    $('#min-date').bootstrapMaterialDatePicker({
        format: 'DD/MM/YYYY HH:mm', minDate: new Date()
    }
    );
    $('#date-fr').bootstrapMaterialDatePicker({
        format: 'DD/MM/YYYY HH:mm', lang: 'fr', weekStart: 1, cancelText: 'ANNULER'
    }
    );
    $('#date-end').bootstrapMaterialDatePicker({
        weekStart: 0
    }
    );
    $('#date-start').bootstrapMaterialDatePicker({
        weekStart: 0
    }
    ).on('change', function (e, date) {
        $('#date-end').bootstrapMaterialDatePicker('setMinDate', date);
    }
    );
});


$(document).ready(function () {

    $('#customSwitch1').click(function () {
        if ($('#customSwitch1').prop("checked") == true) {
            $("#mySelect").prop('disabled', false);

        }
        else if ($('#customSwitch1').prop("checked") == false) {
            $("#mySelect").prop('disabled', true);
            $("#mySelect").val('Select');
        }
    });



    $('#customSwitch2').click(function () {
        if ($('#customSwitch2').prop("checked") == true) {
            $("#mySelect1").prop('disabled', false);
        }
        else if ($('#customSwitch2').prop("checked") == false) {
            $("#mySelect1").prop('disabled', true);
            $("#mySelect1").val('Select');
        }
    });



    $('#customSwitch3').click(function () {
        if ($('#customSwitch3').prop("checked") == true) {
            $("#mySelect2").prop('disabled', false);
        }
        else if ($('#customSwitch3').prop("checked") == false) {
            $("#mySelect2").prop('disabled', true);
            $("#mySelect2").val('Select');
        }
    });



    $('#customSwitch4').click(function () {
        if ($('#customSwitch4').prop("checked") == true) {
            $("#mySelect3").prop('disabled', false);
        }
        else if ($('#customSwitch4').prop("checked") == false) {
            $("#mySelect3").prop('disabled', true);
            $("#mySelect3").val('Select');
        }
    });



    $('#customSwitch5').click(function () {
        if ($('#customSwitch5').prop("checked") == true) {
            $("#mySelect4").prop('disabled', false);
        }
        else if ($('#customSwitch5').prop("checked") == false) {
            $("#mySelect4").prop('disabled', true);
            $("#mySelect4").val('Select');
        }
    });


    $('#customSwitch6').click(function () {
        if ($('#customSwitch6').prop("checked") == true) {
            $("#mySelect5").prop('disabled', false);
        }
        else if ($('#customSwitch6').prop("checked") == false) {
            $("#mySelect5").prop('disabled', true);
            $("#mySelect5").val('Select');
        }
    });



    $('#customSwitch7').click(function () {
        if ($('#customSwitch7').prop("checked") == true) {
            $("#mySelect6").prop('disabled', false);
        }
        else if ($('#customSwitch6').prop("checked") == false) {
            $("#mySelect6").prop('disabled', true);
            $("#mySelect6").val('Select');
        }
    });



    $('#customSwitch8').click(function () {
        if ($('#customSwitch8').prop("checked") == true) {
            $("#mySelect7").prop('disabled', false);
        }
        else if ($('#customSwitch8').prop("checked") == false) {
            $("#mySelect7").prop('disabled', true);
            $("#mySelect7").val('Select');
        }
    });



    $('#customSwitch9').click(function () {
        if ($('#customSwitch9').prop("checked") == true) {
            $("#mySelect8").prop('disabled', false);
        }
        else if ($('#customSwitch9').prop("checked") == false) {
            $("#mySelect8").prop('disabled', true);
            $("#mySelect8").val('Select');
        }
    });


    $('#customSwitch10').click(function () {
        if ($('#customSwitch10').prop("checked") == true) {
            $("#mySelect9").prop('disabled', false);
        }
        else if ($('#customSwitch10').prop("checked") == false) {
            $("#mySelect9").prop('disabled', true);
            $("#mySelect9").val('Select');
        }
    });


    $('#customSwitch11').click(function () {
        if ($('#customSwitch11').prop("checked") == true) {
            $("#mySelect10").prop('disabled', false);
        }
        else if ($('#customSwitch11').prop("checked") == false) {
            $("#mySelect10").prop('disabled', true);
            $("#mySelect10").val('Select');
        }
    });


    $('#customSwitch11').click(function () {
        if ($('#customSwitch11').prop("checked") == true) {
            $("#mySelect10").prop('disabled', false);
        }
        else if ($('#customSwitch11').prop("checked") == false) {
            $("#mySelect10").prop('disabled', true);
            $("#mySelect10").val('Select');
        }
    });


    $('#customSwitch12').click(function () {
        if ($('#customSwitch12').prop("checked") == true) {
            $("#mySelect11").prop('disabled', false);
        }
        else if ($('#customSwitch12').prop("checked") == false) {
            $("#mySelect11").prop('disabled', true);
            $("#mySelect11").val('Select');
        }
    });

    $('#customSwitch13').click(function () {
        if ($('#customSwitch13').prop("checked") == true) {
            $("#mySelect12").prop('disabled', false);
        }
        else if ($('#customSwitch13').prop("checked") == false) {
            $("#mySelect12").prop('disabled', true);
            $("#mySelect12").val('Select');
        }
    });


    $('#customSwitch14').click(function () {
        if ($('#customSwitch14').prop("checked") == true) {
            $("#mySelect13").prop('disabled', false);
        }
        else if ($('#customSwitch14').prop("checked") == false) {
            $("#mySelect13").prop('disabled', true);
            $("#mySelect13").val('Select');
        }
    });


    $('#customSwitch15').click(function () {
        if ($('#customSwitch15').prop("checked") == true) {
            $("#mySelect14").prop('disabled', false);
        }
        else if ($('#customSwitch15').prop("checked") == false) {
            $("#mySelect14").prop('disabled', true);
            $("#mySelect14").val('Select');
        }
    });


    $('#customSwitch16').click(function () {
        if ($('#customSwitch16').prop("checked") == true) {
            $("#mySelect15").prop('disabled', false);
        }
        else if ($('#customSwitch16').prop("checked") == false) {
            $("#mySelect15").prop('disabled', true);
            $("#mySelect15").val('Select');
        }
    });

});




function checkFilled() {
    document.getElementById('heading').style.top = '-25px';
    document.getElementById('heading').style.color = '#f29757';
}
function checkFilled1() {
    document.getElementById('heading2').style.top = '-25px';
    document.getElementById('heading2').style.color = '#f29757';
}
function checkFilled2() {
    document.getElementById('heading3').style.top = '-25px';
    document.getElementById('heading3').style.color = '#f29757';
}
function checkFilled3() {
    document.getElementById('heading3').style.top = '-25px';
    document.getElementById('heading3').style.color = '#f29757';
}
function checkFilled4() {
    document.getElementById('heading4').style.top = '-25px';
    document.getElementById('heading4').style.color = '#f29757';
}
function checkFilled5() {
    document.getElementById('heading5').style.top = '-25px';
    document.getElementById('heading5').style.color = '#f29757';
}
function checkFilled6() {
    document.getElementById('heading6').style.top = '-25px';
    document.getElementById('heading6').style.color = '#f29757';
}
function checkFilled7() {
    document.getElementById('heading7').style.top = '-25px';
    document.getElementById('heading7').style.color = '#f29757';
}
function checkFilled8() {
    document.getElementById('heading8').style.top = '-25px';
    document.getElementById('heading8').style.color = '#f29757';
}


//document.addEventListener('DOMContentLoaded', function () {
//    var elems = document.querySelectorAll('.fixed-action-btn');
//    var instances = M.FloatingActionButton.init(elems, {
//        direction: 'bottom',
//        hoverEnabled: false
//    });
//});

