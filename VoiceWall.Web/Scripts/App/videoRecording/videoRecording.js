/// <reference path="../../RecordRTC.js" />
var recordOnlyVideo;
var videoOnlyPreview = document.querySelector('#videoRecording .preview');
var videoOnlyFile = !!navigator.mozGetUserMedia ? 'video.gif' : 'video.webm';
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
            $("#videoRecording .play source").attr("src", URL.createObjectURL(blob));

            $("#videoRecording .sendButton").show().click(function () { PostOnlyVideoBlob(blob) });

            $("#videoRecording .saveButton").show().attr("href", URL.createObjectURL(blob));

            $("#videoRecording .cancelButton").show().click(function () {
                $("#videoRecording .sendButton").hide();
                $("#videoRecording .saveButton").hide();
                $("#videoRecording .cancelButton").hide();
                $("#videoRecording .play").hide();
                $(videoPreview).show();
                $("#videoRecording .recordButton").show();
            });


        });
    }
    else {
        e.classList.add("recording");
        navigator.getUserMedia({
            video: true
        }, function (stream) {
            videoOnlyPreview.src = window.URL.createObjectURL(stream);
            videoOnlyPreview.play();

            recordOnlyVideo = RecordRTC(stream, {
                type: videoOnlyFile.indexOf('gif') == -1 ? 'video' : 'gif'
            });

            recordOnlyVideo.startRecording();
        }, function (error) { throw error; });
    }
}


function PostOnlyVideoBlob(blob) {
    var form = new FormData(document.querySelector("#videoRecordingHolder .hiddenForm"));
    form.append("videoFile", blob);
    var xhr = new XMLHttpRequest();
    xhr.open('POST', 'UploadVideo');
    xhr.send(form);
    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4 && xhr.status == 200) {
            toastr.success("Wee");
        }
    }
}
