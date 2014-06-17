
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
          if ( $("#UserPageInput").val() != "" && $("#UserPageInput").val() != 0 && ($("#UserPageInput").val() <= $("#PageCount").val())) {
            var url = '/Product/Index/?' + "PageSize=" + $("#PageSize").val() + "&SortBy=" + $("#SortBy").val() + "&CategoryId=" + $("#CategoryId").val() + "&PageNo=" + $("#UserPageInput").val();
            $.ajax({
                url: url,
                type: 'GET',
                cache: false,
                success: function(result) {
                    $('#gridContent').html(result);

                }
            });
        }
        return false;
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

