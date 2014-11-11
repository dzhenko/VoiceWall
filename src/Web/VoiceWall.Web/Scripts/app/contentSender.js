window.voiceWallAjax = (function () {
    function send(url, blob, id, cb, error) {
        var form = new FormData($("#hiddenAjaxPostForm"));
        
        form.append('__RequestVerificationToken', $('#hiddenAjaxPostForm input').val());
        
        if (blob) {
            form.append("File", blob);
        }

        if (id) {
            form.append("ContentId", id);
        }

        error = error || function (error) { toastr.error("Invalid data") };

        $.ajax({
            type: 'POST',
            url: url,
            data: form,
            processData: false,
            contentType: false
        }).done(cb).error(error);
    }

    function create(url, blob) {
        send(url, blob, null, function (response) {

            toastr.success("Successfully created a post!");
            $('body .new-content-placeholder').prepend(response);
        }, function (error) {
            toastr.error("Invalid data");
        });
    }

    function comment(url, blob, id) {
        var objToHide = $('*[data-wall-item-id="' + id + '"]').parent().siblings();
        var img = getLoadingImage(objToHide.width(), objToHide.height());

        objToHide.children().hide();
        objToHide.prepend(img);

        send(url, blob, id, function (response) {

            toastr.success("Successfully added a comment!");
            $(objToHide.children().first()).remove();
            $(objToHide.children().first()).after($(response));

            // handle picture opening
            $(".fancybox-image").fancybox({
                'titleShow': false,
                'transitionIn': 'elastic',
                'transitionOut': 'elastic',
                'easingIn': 'easeOutBack',
                'easingOut': 'easeInBack'
            });

            objToHide.children().show();
        }, function (error) {

            toastr.error("Invalid data");
            $(objToHide.children().first()).remove();
            objToHide.children().show();
        });
    }

    function react(url, id) {
        var objToHide = $('*[data-wall-item-id="' + id + '"]').siblings();
        var img = getLoadingImage(objToHide.width(), objToHide.height());

        objToHide.children().hide();
        objToHide.prepend(img);

        send(url, null, id, function (response) {

            toastr.success("Successfully reacted!");
            objToHide.parent().html(response);
        }, function (error) {

            toastr.error("Invalid data!");
            $(objToHide.children().first()).remove();
            objToHide.children().show();
        });
    }

    function flagComment(url, id) {
        var objToHide = $('*[data-wall-item-comment-id="' + id + '"]');
        var img = getLoadingImage(objToHide.width(), objToHide.height());

        objToHide.children().hide();
        objToHide.prepend(img);

        send(url, null, id, function (response) {

            toastr.success("Successfully flagged this comment");
            objToHide.replaceWith(response);
        }, function (error) {

            toastr.error("Invalid data");
            $(objToHide.children().first()).remove();
            objToHide.children().show();
        });
    }

    function getLoadingImage(width, height) {
        return $('<img src="/Content/img/loading.gif"  width="' + (width - 1) + '" height="' + height + '"/>');
    }

    return {
        create: {
            video: function (blob) {
                create("UploadVideo/Create", blob);
            },
            sound: function (blob) {
                create("UploadSound/Create", blob);
            },
            picture: function (blob) {
                create("UploadPicture/Create", blob);
            }
        },
        comment: {
            withVideo: function (blob, id) {
                comment("UploadVideo/Comment", blob, id);
            },
            withSound: function (blob, id) {
                comment("UploadSound/Comment", blob, id);
            },
            withPicture: function (blob, id) {
                comment("UploadPicture/Comment", blob, id);
            }
        },
        react: {
            like: function (id) {
                react("/ContentReactions/Like", id);
            },
            hate: function (id) {
                react("/ContentReactions/Hate", id);
            },
            flag: function (id) {
                react("/ContentReactions/Flag", id);
            },
            flagComment: function (id) {
                flagComment("/CommentReactions/Flag", id);
            }
        }
    }
}())