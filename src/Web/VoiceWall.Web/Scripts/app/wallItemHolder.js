$(function () {
    // start by hiding icons
    $(".wallItemMainHolder .commentInnerBtn").hide();
    $(".wallItemMainHolder .reactInnerBtn").hide();

    // handle picture opening
    $(".fancybox-image").fancybox({
        'titleShow': false,
        'transitionIn': 'elastic',
        'transitionOut': 'elastic',
        'easingIn': 'easeOutBack',
        'easingOut': 'easeInBack'
    });

    // handle media play global selector
    $('.wallItemMainHolder').on('click', '.multimedia-main-action:not(.fancybox-image)', playMedia);
    $('.wallItemMainHolder').on('click', '.small-multimedia-main-action:not(.fancybox-image)', playMedia);

    function playMedia(mediaHolderLink) {
        mediaHolderLink.preventDefault();
        var dataSet = $(mediaHolderLink.currentTarget.dataset)[0];
        if (!dataSet) {
            dataSet = $($(mediaHolderLink.currentTarget).parent()).dataset[0];
        }

        var dataSrc = dataSet.contentSrc;
        var dataType = dataSet.contentType;
        
        var audioPlayer = $('#modalContentPlayerHolder .audio-player');
        var videoPlayer = $('#modalContentPlayerHolder .video-player');

        if (dataType == 'Video') {
            audioPlayer.hide();
            videoPlayer.show().attr('src', dataSrc);

            $('#modalContentPlayerHolder').modal('show');
        }
        else if (dataType == 'Sound') {
            videoPlayer.hide();
            audioPlayer.show().attr('src', dataSrc);

            audioPlayer[0].pause();
            audioPlayer[0].load();//suspends and restores all audio element
            audioPlayer[0].play();

            $('#modalContentPlayerHolder').modal('show');
        }
    }

    // stop media on modal closing
    $("#modalContentPlayerHolder").on('hidden.bs.modal', function () {
        $('#modalContentPlayerHolder .audio-player')[0].pause();
        $('#modalContentPlayerHolder .video-player')[0].pause();
    });
    
    // animate buttons
    $('body').on('click', ".wallItemMainHolder .commentBtn", function (e) {
        var self = $(this);
        window.wallItemHolderClickedId = self.parent().parent().data("wall-item-id");

        var other = self.parent().parent().children().children('.reactInnerBtn');
        $(other[0]).transition({ x: 95 });
        $(other[1]).transition({ x: 190 });
        $(other[2]).transition({ x: 280 });
        other.hide();

        var own = self.siblings().show();
        $(own[0]).transition({ x: 15 });
        $(own[1]).transition({ x: 105 });
        $(own[2]).transition({ x: 200 });
    });

    $('body').on('click', ".wallItemMainHolder .reactBtn", function (e) {
        var self = $(this);
        window.wallItemHolderClickedId = self.parent().parent().data("wall-item-id");

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

    // attach events to buttons
    $('body').on('click', '.main-create-comment-holder .voiceBtn', function () {
        initAudio();
        $('#modalVoiceWindowMain').modal('show');
    });

    $('body').on('click', '.main-create-comment-holder .pictureBtn', function () {
        $('#modalPictureWindowMain').modal('show');
    });

    $('body').on('click', '.main-create-comment-holder .videoBtn', function () {
        $('#modalVideoWindowMain').modal('show');
    });

    $('body').on('click', '.main-create-comment-holder .likeBtn', function () {
        window.voiceWallAjax.react.like(window.wallItemHolderClickedId);
    });

    $('body').on('click', '.main-create-comment-holder .hateBtn', function () {
        window.voiceWallAjax.react.hate(window.wallItemHolderClickedId);
    });

    $('body').on('click', '.main-create-comment-holder .flagBtn', function () {
        window.voiceWallAjax.react.flag(window.wallItemHolderClickedId);
    });

    $('body').on('click', '.main-comments-holder .flagCommentBtn', function () {
        window.voiceWallAjax.react.flagComment($(this).parent().parent().data("wall-item-comment-id"));
    });
});