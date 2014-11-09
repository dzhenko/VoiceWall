/// <reference path="../../RecordRTC.js" />
var recordMergedVideo, recordMergedAudio;
var videoMergedPreview = document.querySelector('#videoAndAudioRecording .preview');
var videoMergedFile = !!navigator.mozGetUserMedia ? 'video.gif' : 'video.webm';
if (!navigator.getUserMedia)
    navigator.getUserMedia = navigator.webkitGetUserMedia || navigator.mozGetUserMedia;

var mergedStreamHolder = null;

$("#videoAndAudioRecording .sendButton").hide();
$("#videoAndAudioRecording .saveButton").hide();
$("#videoAndAudioRecording .cancelButton").hide();

function toggleMergedRecording(e) {
    if (e.classList.contains("recording")) {
        // stop recording
        e.classList.remove("recording");
        $("#videoAndAudioRecording .recordButton").hide();
        $(videoMergedPreview).hide();
        $("#videoAndAudioRecording .loading").show();
        recordMergedAudio.stopRecording(function () {
            recordMergedVideo.stopRecording(function () {
                convertMergedStreams(recordMergedVideo.getBlob(), recordMergedAudio.getBlob());
                mergedStreamHolder.stop();
                log('<a href="' + workerMergedPath + '" download="ffmpeg-asm.js">ffmpeg-asm.js</a> file download started. It is about 18MB in size; please be patient!');
            });
        });
    }
    else {
        e.classList.add("recording");
        navigator.getUserMedia({
            audio: true,
            video: true
        }, function (stream) {
            mergedStreamHolder = stream;
            videoMergedPreview.src = window.URL.createObjectURL(stream);
            videoMergedPreview.play();

            recordMergedAudio = RecordRTC(stream, {
                // bufferSize: 16384,
                onAudioProcessStarted: function () {
                    recordMergedVideo.startRecording();
                }
            });

            recordMergedVideo = RecordRTC(stream, {
                type: videoMergedFile.indexOf('gif') == -1 ? 'video' : 'gif'
            });

            recordMergedAudio.startRecording();
        }, function (error) { throw error; });
    }
}

var workerMergedPath = 'https://cdn.webrtc-experiment.com/ffmpeg_asm.js';
function processMergedInWebWorker() {
    var blob = URL.createObjectURL(new Blob(['importScripts("' + workerMergedPath + '");var now = Date.now;function print(text) {postMessage({"type" : "stdout","data" : text});};onmessage = function(event) {var message = event.data;if (message.type === "command") {var Module = {print: print,printErr: print,files: message.files || [],arguments: message.arguments || [],TOTAL_MEMORY: message.TOTAL_MEMORY || false};postMessage({"type" : "start","data" : Module.arguments.join(" ")});postMessage({"type" : "stdout","data" : "Received command: " +Module.arguments.join(" ") +((Module.TOTAL_MEMORY) ? ".  Processing with " + Module.TOTAL_MEMORY + " bits." : "")});var time = now();var result = ffmpeg_run(Module);var totalTime = now() - time;postMessage({"type" : "stdout","data" : "Finished processing (took " + totalTime + "ms)"});postMessage({"type" : "done","data" : result,"time" : totalTime});}};postMessage({"type" : "ready"});'], {
        type: 'application/javascript'
    }));

    var worker = new Worker(blob);
    URL.revokeObjectURL(blob);
    return worker;
}

var worker;

function convertMergedStreams(videoBlob, audioBlob) {
    var vab;
    var aab;
    var buffersReady;
    var workerReady;
    var posted = false;

    var fileReader1 = new FileReader();
    fileReader1.onload = function () {
        vab = this.result;

        if (aab) buffersReady = true;

        if (buffersReady && workerReady && !posted) postMessage();
    };
    var fileReader2 = new FileReader();
    fileReader2.onload = function () {
        aab = this.result;

        if (vab) buffersReady = true;

        if (buffersReady && workerReady && !posted) postMessage();
    };

    fileReader1.readAsArrayBuffer(videoBlob);
    fileReader2.readAsArrayBuffer(audioBlob);

    if (!worker) {
        worker = processMergedInWebWorker();
    }

    worker.onmessage = function (event) {
        var message = event.data;
        if (message.type == "ready") {
            log('<a href="' + workerMergedPath + '" download="ffmpeg-asm.js">ffmpeg-asm.js</a> file has been loaded.');
            workerReady = true;
            if (buffersReady)
                postMessage();
        } else if (message.type == "stdout") {
            log(message.data);
        } else if (message.type == "start") {
            log('<a href="' + workerMergedPath + '" download="ffmpeg-asm.js">ffmpeg-asm.js</a> file received ffmpeg command.');
        } else if (message.type == "done") {
            log(JSON.stringify(message));

            var result = message.data[0];
            log(JSON.stringify(result));

            var blob = new Blob([result.data], {
                type: 'video/mp4'
            });

            log(JSON.stringify(blob));

            $("#videoAndAudioRecording .loading").hide();
            $("#videoAndAudioRecording .play").show();
            $("#videoAndAudioRecording .play").attr("src", URL.createObjectURL(blob));

            $("#videoAndAudioRecording .sendButton").show().click(function () { window.voiceWallAjax.comment.withVideo(blob, window.wallItemHolderClickedId); });
            $("#videoAndAudioRecording .saveButton").show().attr("href", URL.createObjectURL(blob));
            $("#videoAndAudioRecording .cancelButton").show().click(videoMergedResetStates);
        }
    };
    var postMessage = function () {
        posted = true;

        worker.postMessage({
            type: 'command',
            arguments: [
                '-i', videoMergedFile,
                '-i', 'audio.wav',
                '-c:v', 'mpeg4',
                '-c:a', 'vorbis',
                '-b:v', '6400k',
                '-b:a', '4800k',
                '-strict', 'experimental', 'output.mp4'
            ],
            files: [
                {
                    data: new Uint8Array(vab),
                    name: videoMergedFile
                },
                {
                    data: new Uint8Array(aab),
                    name: "audio.wav"
                }
            ]
        });
    };
}

//function PostMergedBlob(blob) {
//    var form = new FormData(document.querySelector("#commentContentModalWindowsHolder .hiddenForm"));
//    form.append("videoAndAudioFile", blob);
//    form.append("wallItemId", window.wallItemHolderClickedId);
//    var xhr = new XMLHttpRequest();
//    xhr.open('POST', 'UploadVideoAndAudio');
//    xhr.send(form);
//    xhr.onreadystatechange = function () {
//        if (xhr.readyState == 4 && xhr.status == 200) {
//            toastr.success("Wee");
//        }
//    }
//}

function log(stuff) {
    //console.log(stuff);
}

$("#modalVideoWindowMain").on('hidden.bs.modal', videoMergedResetStates);

function videoMergedResetStates() {
    worker = null;

    if (mergedStreamHolder) {
        mergedStreamHolder.stop();
    }

    mergedStreamHolder = null;
    recordMergedVideo = null;
    recordMergedAudio = null;


    $("#videoAndAudioRecording .sendButton").hide();
    $("#videoAndAudioRecording .saveButton").hide();
    $("#videoAndAudioRecording .play").hide();
    $("#videoAndAudioRecording .preview").show();
    $("#videoAndAudioRecording .cancelButton").hide();
    $("#videoAndAudioRecording .recordButton").show().removeClass("recording");
}