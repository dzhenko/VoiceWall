var pictureVideo = document.querySelector('video');
var pictureCanvas = document.querySelector('canvas');
var pictureCanvasCtx = pictureCanvas.getContext('2d');
var pictureLocalMediaStream = null;

if (!navigator.getUserMedia)
    navigator.getUserMedia = navigator.webkitGetUserMedia || navigator.mozGetUserMedia;

function pictureSnapshot() {
    if (pictureLocalMediaStream) {
        pictureCanvasCtx.drawImage(pictureVideo, 0, 0, pictureVideo.width * 1, pictureVideo.height * 1);
        $(pictureCanvas).show();
        // document.querySelector('img').src = canvas.toDataURL('image/webm');
    }
}
$("#pictureRecordingHolder .sendButton").hide();
$("#pictureRecordingHolder .saveButton").hide();
$("#pictureRecordingHolder .cancelButton").hide().click(function () {
    $("#pictureRecordingHolder video").show();
    $("#pictureRecordingHolder canvas").hide();
    $("#pictureRecordingHolder .btn-record").show().addClass('recording');
    $("#pictureRecordingHolder .sendButton").hide();
    $("#pictureRecordingHolder .saveButton").hide();
    $("#pictureRecordingHolder .cancelButton").hide();
});

function pictureToggleCapture(e) {
    if (e.classList.contains("recording")) {
        // stop recording
        e.classList.remove("recording");
        $("#pictureRecordingHolder .btn-record").hide();
        pictureSnapshot();
        $("#pictureRecordingHolder video").hide();
        $("#pictureRecordingHolder canvas").show();

        $("#pictureRecordingHolder .sendButton").show();
        $("#pictureRecordingHolder .saveButton").show();
        $("#pictureRecordingHolder .cancelButton").show();
    }
    else {
        e.classList.add("recording");
        $(pictureCanvas).hide();
        $("#pictureRecordingHolder .btn-record").show();
        $("#pictureRecordingHolder video").show();
        $("#pictureRecordingHolder canvas").hide();

        $("#pictureRecordingHolder .sendButton").hide();
        $("#pictureRecordingHolder .saveButton").hide();
        $("#pictureRecordingHolder .cancelButton").hide();

        if (!pictuireVideoIsOn) {
            pictuireVideoIsOn = true;
            navigator.getUserMedia({
                video: true
            }, function (stream) {
                pictureVideo.src = window.URL.createObjectURL(stream);
                pictureLocalMediaStream = stream;
                $("#pictureRecordingHolder .btn-record").html('<i class="fa fa-eye fa-2x"> Capture</i>');
            }, function (err) { console.log(err) });
        }
    }
}

var pictuireVideoIsOn = false;

pictureVideo.addEventListener('click', pictureSnapshot, false); // change

