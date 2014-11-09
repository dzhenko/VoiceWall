/// <reference path="../../RecordRTC.js" />
var recordOnlyVideo;
var videoOnlyPreview = document.querySelector('#videoRecording .preview');
var videoOnlyFile = !!navigator.mozGetUserMedia ? 'video.gif' : 'video.webm';
var onlyVideoStreamHolder;

if (!navigator.getUserMedia)
    navigator.getUserMedia = navigator.webkitGetUserMedia || navigator.mozGetUserMedia;

$("#videoRecording .sendButton").hide();
$("#videoRecording .saveButton").hide();
$("#videoRecording .cancelButton").hide();

function toggleOnlyVideoRecording(e) {
    if (e.classList.contains("recording")) {
        // stop recording
        e.classList.remove("recording");
        $("#videoRecording .recordButton").hide();
        $(videoOnlyPreview).hide();
        $("#videoRecording .loading").show();
        recordOnlyVideo.stopRecording(function () {
            var blob = recordOnlyVideo.getBlob();

            $("#videoRecording .loading").hide();
            $("#videoRecording .play").show();
            $("#videoRecording .play").attr("src", URL.createObjectURL(blob));

            $("#videoRecording .sendButton").show().click(function () { window.voiceWallAjax.comment.withVideo(blob, window.wallItemHolderClickedId); });

            $("#videoRecording .saveButton").show().attr("href", URL.createObjectURL(blob));

            $("#videoRecording .cancelButton").show().click(videoOnlyResetStates);
        });
    }
    else {
        e.classList.add("recording");
        navigator.getUserMedia({
            video: true
        }, function (stream) {
            onlyVideoStreamHolder = stream;
            videoOnlyPreview.src = window.URL.createObjectURL(stream);
            videoOnlyPreview.play();

            recordOnlyVideo = RecordRTC(stream, {
                type: videoOnlyFile.indexOf('gif') == -1 ? 'video' : 'gif'
            });

            recordOnlyVideo.startRecording();
        }, function (error) { throw error; });
    }
}


//function PostOnlyVideoBlob(blob) {
//    var form = new FormData(document.querySelector("#commentContentModalWindowsHolder .hiddenForm"));
//    form.append("videoFile", blob);
//    form.append("wallItemId", window.wallItemHolderClickedId);
//    var xhr = new XMLHttpRequest();
//    xhr.open('POST', 'UploadVideo');
//    xhr.send(form);
//    xhr.onreadystatechange = function () {
//        if (xhr.readyState == 4 && xhr.status == 200) {
//            toastr.success("Wee");
//        }
//    }
//}

$("#modalVideoWindowMain").on('hidden.bs.modal', videoOnlyResetStates);

function videoOnlyResetStates() {
    if (onlyVideoStreamHolder) {
        onlyVideoStreamHolder.stop();
    }
    onlyVideoStreamHolder = null;
    recordOnlyVideo = null;

    $("#videoRecording .sendButton").hide();
    $("#videoRecording .saveButton").hide();
    $("#videoRecording .cancelButton").hide();
    $("#videoRecording .play").hide();
    $("#videoRecording .preview").show();
    $("#videoRecording .recordButton").show().removeClass("recording");
}
