
function InitializePage() {
    $("#SearchRequestModel_PageSize").change(function () {

        var url = '/Product/Index/?' + "PageSize=" + $("#SearchRequestModel_PageSize").val() + "&SortBy=" + $("#SortBy").val() + "&CategoryId=" + $("#CategoryId").val();
        $.ajax({
            url: url,
            type: 'GET',
            cache: false,
            success: function (result) {
                $('#gridContent').html(result);

            }
        });
        return false;
    });

    var specialKeys = new Array();
    specialKeys.push(8); //Backspace
    $(function () {
        $("#UserPageInput").bind("keypress", function (e) {
            //alert($("#UserPageInput").val());
            //$("#CurrentPageNumber").val($("#UserPageInput").val());
            var keyCode = e.which ? e.which : e.keyCode;
            // Checking value weather the key between the 0-9 or not! If not we are restricting 
            var ret = ((keyCode >= 48 && keyCode <= 57) || specialKeys.indexOf(keyCode) != -1);
           $(".error").css("display", ret ? "none" : "inline");
            return ret;
        });
        $("#UserPageInput").bind("paste", function (e) {
            return false;
        });
        $("#UserPageInput").bind("drop", function (e) {
            return false;
        });
    });
    $("#UserPageInput").change(function () {
       
        if ($("#UserPageInput").val() != "" && $("#UserPageInput").val() != 0 && ($("#UserPageInput").val() <= $("#PageCount").val())) {
            var url = '/Product/Index/?' + "PageSize=" + $("#PageSize").val() + "&SortBy=" + $("#SortBy").val() + "&CategoryId=" + $("#CategoryId").val() + "&PageNo=" + $("#UserPageInput").val();
            $.ajax({
                url: url,
                type: 'GET',
                cache: false,
                success: function (result) {
                    $('#gridContent').html(result);

                }
            });
         
           
        }
        $("#UserPageInput").val($("#CurrentPageNumber").val());
        return false;
    });

    $(".delete").click(function (e) {
        // e.preventDefault(); use this or return false
        var url = $(this).attr('href');
        $("#dialog-confirm").css('display', 'block');
        $('#yes').attr('data-delete-id', $(this).attr("data-delete-id"));
        //$("#dialog-confirm").dialog('open');
        return false;
    });
    $("#yes").click(function (e) {
        var url = '/Product/Delete/?' + "PageSize=" + $("#PageSize").val() + "&SortBy=" + $("#SortBy").val() + "&CategoryId=" + $("#CategoryId").val() + "&PageNo=" + $("#UserPageInput").val() + "&Id=" + $(this).attr("data-delete-id");
        $.ajax({
            url: url,
            type: 'GET',
            cache: false,
            success: function (result) {
                $('#gridContent').html(result);

            }
        });
        return false;
    });
    $("#no").click(function (e) {

        alert($(this).attr("data-delete-id"));

    });

}


$(function () {
    $("#reset").click(function () {
        $('#mySelect option:eq(0)').attr('selected', 'selected');
        $("#searchInput").val("");
        var url = '/Product/Index/';
        $.ajax({
            url: url,
            type: 'GET',
            cache: false,
            success: function (result) {
                $('#gridContent').html(result);

            }
        });
        return false;
    });


});
var spinnerVisibleCounter = 0;
function showProgress() {
    ++spinnerVisibleCounter;
    if (spinnerVisibleCounter > 0) {
        $("div#spinner").fadeIn("fast");
        //spinnerVisible = true;
    }
};
function hideProgress() {
    --spinnerVisibleCounter;
    if (spinnerVisibleCounter <= 0) {
        spinnerVisibleCounter = 0;
        var spinner = $("div#spinner");
        spinner.stop();
        spinner.fadeOut("fast");
        //spinnerVisible = false;
    }
};



