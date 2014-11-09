var pictureVideo = document.querySelector('#modalPictureWindowMain video');
var pictureCanvas = document.querySelector('#modalPictureWindowMain canvas');
var pictureCanvasCtx = pictureCanvas.getContext('2d');
var pictureLocalMediaStream = null;
var pictureFileBlob = null;

if (!navigator.getUserMedia)
    navigator.getUserMedia = navigator.webkitGetUserMedia || navigator.mozGetUserMedia;

function pictureSnapshot() {
    if (pictureLocalMediaStream) {
        pictureCanvasCtx.drawImage(pictureVideo, 0, 0, pictureVideo.width * 1, pictureVideo.height * 1);
        $(pictureCanvas).show();
        var base64String = pictureCanvas.toDataURL('image/webm');
        var blobBin = atob(base64String.split(',')[1]);
        var array = [];
        for (var i = 0; i < blobBin.length; i++) {
            array.push(blobBin.charCodeAt(i));
        }
        pictureFileBlob = new Blob([new Uint8Array(array)], { type: 'image/png' });

        var url = (window.URL || window.webkitURL).createObjectURL(pictureFileBlob);
        document.querySelector("#pictureRecordingHolder .save").href = url;
    }
}

function postPicture(blob) {
    var form = new FormData(document.querySelector("#soundRecordingHolder .hiddenForm"));
    form.append("imageFile", blob);
    form.append("wallItemId", window.wallItemHolderClickedId);
    var xhr = new XMLHttpRequest();
    xhr.open('POST', url);
    xhr.send(form);
    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4 && xhr.status == 200) {
            toastr.success("Wee");
        }
    }
}

$("#pictureRecordingHolder .sendButton").hide().click(PostBlobPicture);
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

function PostBlobPicture() {
    if (pictureFileBlob) {
        window.voiceWallAjax.comment.withPicture(pictureFileBlob, window.wallItemHolderClickedId);
        //var form = new FormData(document.querySelector("#commentContentModalWindowsHolder .hiddenForm"));
        //form.append("imageFile", pictureFileBlob);
        //var xhr = new XMLHttpRequest();
        //xhr.open('POST', 'UploadPicture');
        //xhr.send(form);
        
        //xhr.onreadystatechange = function () {
        //    if (xhr.readyState == 4 && xhr.status == 200) {
        //        toastr.success("Wee");
        //    }
        //}
    }
}

$("#modalPictureWindowMain").on('hidden.bs.modal', pictureResetStates);

function pictureResetStates() {
    pictuireVideoIsOn = false;
    if (pictureLocalMediaStream) {
        pictureLocalMediaStream.stop();
    }
    pictureLocalMediaStream = null;
    pictureFileBlob = null;
    $("#pictureRecordingHolder .sendButton").hide();
    $("#pictureRecordingHolder .saveButton").hide();
    $("#pictureRecordingHolder .cancelButton").hide();
    $("#pictureRecordingHolder .btn-record").show().html('<i class="fa fa-video-camera fa-2x"> Preview</i>');
    $("#pictureRecordingHolder video").show();
    $("#pictureRecordingHolder canvas").hide();
}
