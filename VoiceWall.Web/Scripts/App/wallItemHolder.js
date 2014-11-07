$(function () {
    $(".fancybox-image").fancybox({
        'titleShow': false,
        'transitionIn': 'elastic',
        'transitionOut': 'elastic',
        'easingIn': 'easeOutBack',
        'easingOut': 'easeInBack'
    });

    // media play
    $('.wallItemMainHolder .multimedia-main-action:not(.fancybox-image)').click(playMedia);
    $('.wallItemMainHolder .small-multimedia-main-action:not(.fancybox-image)').click(playMedia);

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
            audioPlayer.show().attr('src', 'http://www-mmsp.ece.mcgill.ca/documents/AudioFormats/WAVE/Samples/AFsp/M1F1-Alaw-AFsp.wav');

            audioPlayer[0].pause();
            audioPlayer[0].load();//suspends and restores all audio element
            audioPlayer[0].play();

            $('#modalContentPlayerHolder').modal('show');
        }
    }

    $("#modalContentPlayerHolder").on('hidden.bs.modal', function () {
        $('#modalContentPlayerHolder .audio-player')[0].pause();
        $('#modalContentPlayerHolder .video-player')[0].pause();
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