window.addEventListener("load", function () {
    // Closes the sidebar menu
    $("#menu-close").click(function (e) {
        e.preventDefault();
        $("#sidebar-wrapper").toggleClass("active");
    });

    // Opens the sidebar menu
    $("#menu-toggle").click(function (e) {
        e.preventDefault();
        $("#sidebar-wrapper").toggleClass("active");
    });

    $("#back-to-the-top-btn").click(function () {
        $('html,body').animate({
            scrollTop: 0
        }, 1000);
        return false;
    });

    $("#search-form-holder").mouseover(function () {
        $("#search-form-text").show();
    }).mouseout(function () {
        $("#search-form-text").hide();
    });



    var windowIsScrooledTop = true;
    $(window).scroll(function () {
        if ($(this).scrollTop() == 0) {
            // top reached
            windowIsScrooledTop = true;

            $("#tiny-navigation-container").hide();
            $("#top-navigation-container").show();
            $("#back-to-the-top-btn").hide();

        }
        else if (windowIsScrooledTop) {
            // we move down from the top
            windowIsScrooledTop = false;

            // animation magic
            $("#tiny-navigation-container").show();
            $("#top-navigation-container").hide();
            $("#back-to-the-top-btn").show();
        }
    });
});