(function() {
    $("body").hide();
    $('html').prepend($('<div id="loading-cover-image-content-hider" class="text-center"><img src="/Content/img/loading.gif" /></div>'));

    document.addEventListener("DOMContentLoaded", function () {
        $("#loading-cover-image-content-hider").remove();
        $("body").show();
    }, false)
}())