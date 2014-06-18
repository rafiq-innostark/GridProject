
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
        return false;
    });

    $(".delete").click(function (e) {
        // e.preventDefault(); use this or return false
        var url = $(this).attr('href');
          // alert($(this).attr("data-delete-id"));
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

//$(".delete").live("click", function (e) {
//    alert("test");
//    //        // e.preventDefault(); use this or return false
//    //        alert("tttt");
//    //        //url = $(this).attr('href');
//    $("#dialog-confirm").dialog('open');

//    return false;
//});
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



