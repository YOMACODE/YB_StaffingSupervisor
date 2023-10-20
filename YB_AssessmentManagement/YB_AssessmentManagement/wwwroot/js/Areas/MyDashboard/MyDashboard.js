
$(document).ready(function () {
    if (sessionStorage.getItem("ClientListing") != null) {

        $('#hdnClientListing').val(sessionStorage.getItem("ClientListing"));
        $("#ClientCode").select2({
            multiple: true,
        });
        $('#ClientCode').val($('#hdnClientListing').val().split(",")).trigger('change');;
        $('#filter-box').addClass('show');
        sessionStorage.removeItem("ClientListing");
    }
    GetRegionWiseAssociateSplit();
    GetRegionWiseAssociateStatusSplit();
    GetDesignationSplitPerRegion();
    GetHiringDetails();
    GetWorkTenureAssociate();
    GetGrossSalaryPerRegion();
    
});
$('#ClientCode').on('change', function () {
    var getClientCodes = $('#ClientCode').val();
    $('#hdnClientListing').val('');
    if (getClientCodes != null && getClientCodes != '' && getClientCodes != undefined) {
        var getClient = getClientCodes.join(',')
        $('#hdnClientListing').val(getClient);
    }
});
$('#btnSearch').on('click', function () {
    if ($('#ClientCode').val() != "") {
        sessionStorage.setItem("ClientListing", $('#hdnClientListing').val());
    }
    location.reload(true);

});

function GetRegionWiseAssociateSplit(SearchClientId) {
    var SearchClientId = $('#hdnClientListing').val();
    $("#ViewRegionWiseAssociateSplit").empty();
    $.get("/Dashboard/MyDashBoard/GetRegionWiseAssociateSplit", { SearchClientId: SearchClientId }, function (data) {
        $("#ViewRegionWiseAssociateSplit").html(data);
        $("body").removeClass("loading");
    });
}
function GetRegionWiseAssociateStatusSplit(SearchClientId) {
    var SearchClientId = $('#hdnClientListing').val();
    $("#ViewRegionWiseAssociateStatusSplit").empty();
    $.get("/Dashboard/MyDashBoard/GetRegionWiseAssociateStatusSplit", { SearchClientId: SearchClientId }, function (data) {
        $("#ViewRegionWiseAssociateStatusSplit").html(data);
        $("body").removeClass("loading");
    });
}
function GetDesignationSplitPerRegion(SearchClientId) {
    var SearchClientId = $('#hdnClientListing').val();
    $("#ViewDesignationSplitPerRegion").empty();
    $.get("/Dashboard/MyDashBoard/GetDesignationSplitPerRegion", { SearchClientId: SearchClientId }, function (data) {
        $("#ViewDesignationSplitPerRegion").html(data);
        $("body").removeClass("loading");
    });
}
function GetHiringDetails(SearchClientId) {
    var SearchClientId = $('#hdnClientListing').val();
    $("#ViewHiringDetails").empty();
    $.get("/Dashboard/MyDashBoard/GetHiringDetails", { SearchClientId: SearchClientId }, function (data) {
        $("#ViewHiringDetails").html(data);
        $("body").removeClass("loading");
    });
}
function GetWorkTenureAssociate(SearchClientId, SortOrder, SortColumn, PageSize, Page) {
    var SearchClientId = $('#hdnClientListing').val();
    $("#ViewWorkTenureAssociate").empty();
    $.get("/Dashboard/MyDashBoard/GetWorkTenureAssociate", { SearchClientId: SearchClientId, sortOrder: SortOrder, sortColumn: SortColumn, pagesize: PageSize, page: Page }, function (data) {
        $("#ViewWorkTenureAssociate").html(data);
        $("body").removeClass("loading");
    });
}
function GetGrossSalaryPerRegion(SearchClientId, SortOrder, SortColumn, PageSize, Page) {
    var SearchClientId = $('#hdnClientListing').val();
    $("#ViewGrossSalaryPerRegion").empty();
    $.get("/Dashboard/MyDashBoard/GetGrossSalaryPerRegion", { SearchClientId: SearchClientId, sortOrder: SortOrder, sortColumn: SortColumn, pagesize: PageSize, page: Page }, function (data) {
        $("#ViewGrossSalaryPerRegion").html(data);
        $("body").removeClass("loading");
    });
}