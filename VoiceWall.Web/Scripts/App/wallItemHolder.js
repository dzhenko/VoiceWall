$(function () {
    $('.wallItemMainHolder .multimedia-main-action').click(function (e) {
        var self = $(this);
        e.preventDefault();

    });

    // buttons
    var commentBtns = $(".wallItemMainHolder .commentBtn");
    var reactBtns = $(".wallItemMainHolder .reactBtn");

    $(".wallItemMainHolder .commentInnerBtn").hide();
    $(".wallItemMainHolder .reactInnerBtn").hide();

    commentBtns.click(function (e) {
        var self = $(this);
        window.wallItemHolderClickedId = self.parents('.wallItemMainHolder').first().data("wall-item-id");

        var other = self.parent().parent().children().children('.reactInnerBtn');
        $(other[0]).transition({ x: 95 });
        $(other[1]).transition({ x: 190 });
        $(other[2]).transition({ x: 280 });
        other.hide();

        var own = self.siblings().show();
        $(own[0]).transition({ x: 100 });
        $(own[1]).transition({ x: 195 });
        $(own[2]).transition({ x: 290 });
    });

    reactBtns.click(function (e) {
        var self = $(this);
        window.wallItemHolderClickedId = self.parents('.wallItemMainHolder').first().data("wall-item-id");

        var other = self.parent().parent().children().children('.commentInnerBtn');
        $(other[0]).transition({ x: -100 });
        $(other[1]).transition({ x: -195 });
        $(other[2]).transition({ x: -290 });
        other.hide();

        var own = self.siblings().show();
        $(own[0]).transition({ x: -95 });
        $(own[1]).transition({ x: -190 });
        $(own[2]).transition({ x: -280 });
    });
});