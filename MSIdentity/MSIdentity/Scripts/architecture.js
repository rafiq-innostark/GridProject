$(function () {
    $('#myPager').on('click', 'a', function () {
        $.ajax({
            url: this.href,
            type: 'GET',
            cache: false,
            success: function (result) {
                $('#gridContent').html(result);
            }
        });
        return false;
    });
});
$(function () {
    $("#NumSelected").change(function () {
        var url = '/Product/Index/?' + "pageSize=" + $("#NumSelected").val();
        $.ajax({
            url: url,
            type: 'GET',
            cache: false,
            success: function (result) {
                $('#gridContent').html(result);
               


            }
        });

    });
});