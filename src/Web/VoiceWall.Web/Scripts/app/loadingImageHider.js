(function() {
    $("body").hide();
    $('html').prepend($('<img id="loading-cover-image-content-hider" src="/Content/img/loading.gif" />'));

    document.addEventListener("DOMContentLoaded", function () {
        $("#loading-cover-image-content-hider").remove();
        $("body").show();
    }, false)
}())