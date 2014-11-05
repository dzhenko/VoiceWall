/// <reference path="../../RecordRTC.js" />
var recordVideo, recordAudio;
var videoPreview = document.querySelector('#videoRecordingHolder .preview');
var videoFile = !!navigator.mozGetUserMedia ? 'video.gif' : 'video.webm';
if (!navigator.getUserMedia)
    navigator.getUserMedia = navigator.webkitGetUserMedia || navigator.mozGetUserMedia;

$("#videoRecordingHolder .sendButton").hide();
$("#videoRecordingHolder .saveButton").hide();
$("#videoRecordingHolder .cancelButton").hide();

function toggleRecording(e) {
    if (e.classList.contains("recording")) {
        // stop recording
        e.classList.remove("recording");
        $("#videoRecordingHolder .recordButton").hide();
        $(videoPreview).hide();
        $("#videoRecordingHolder .loading").show();
        recordAudio.stopRecording(function () {
            recordVideo.stopRecording(function () {
                convertStreams(recordVideo.getBlob(), recordAudio.getBlob());

                log('<a href="' + workerPath + '" download="ffmpeg-asm.js">ffmpeg-asm.js</a> file download started. It is about 18MB in size; please be patient!');
            });
        });
    }
    else {
        e.classList.add("recording");
        navigator.getUserMedia({
            audio: true,
            video: true
        }, function (stream) {
            videoPreview.src = window.URL.createObjectURL(stream);
            videoPreview.play();

            recordAudio = RecordRTC(stream, {
                // bufferSize: 16384,
                onAudioProcessStarted: function () {
                    recordVideo.startRecording();
                }
            });

            recordVideo = RecordRTC(stream, {
                type: videoFile.indexOf('gif') == -1 ? 'video' : 'gif'
            });

            recordAudio.startRecording();
        }, function (error) { throw error; });
    }
}

var workerPath = 'https://cdn.webrtc-experiment.com/ffmpeg_asm.js';
function processInWebWorker() {
    var blob = URL.createObjectURL(new Blob(['importScripts("' + workerPath + '");var now = Date.now;function print(text) {postMessage({"type" : "stdout","data" : text});};onmessage = function(event) {var message = event.data;if (message.type === "command") {var Module = {print: print,printErr: print,files: message.files || [],arguments: message.arguments || [],TOTAL_MEMORY: message.TOTAL_MEMORY || false};postMessage({"type" : "start","data" : Module.arguments.join(" ")});postMessage({"type" : "stdout","data" : "Received command: " +Module.arguments.join(" ") +((Module.TOTAL_MEMORY) ? ".  Processing with " + Module.TOTAL_MEMORY + " bits." : "")});var time = now();var result = ffmpeg_run(Module);var totalTime = now() - time;postMessage({"type" : "stdout","data" : "Finished processing (took " + totalTime + "ms)"});postMessage({"type" : "done","data" : result,"time" : totalTime});}};postMessage({"type" : "ready"});'], {
        type: 'application/javascript'
    }));

    var worker = new Worker(blob);
    URL.revokeObjectURL(blob);
    return worker;
}

var worker;

function convertStreams(videoBlob, audioBlob) {
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
        worker = processInWebWorker();
    }

    worker.onmessage = function (event) {
        var message = event.data;
        if (message.type == "ready") {
            log('<a href="' + workerPath + '" download="ffmpeg-asm.js">ffmpeg-asm.js</a> file has been loaded.');
            workerReady = true;
            if (buffersReady)
                postMessage();
        } else if (message.type == "stdout") {
            log(message.data);
        } else if (message.type == "start") {
            log('<a href="' + workerPath + '" download="ffmpeg-asm.js">ffmpeg-asm.js</a> file received ffmpeg command.');
        } else if (message.type == "done") {
            log(JSON.stringify(message));

            var result = message.data[0];
            log(JSON.stringify(result));

            var blob = new Blob([result.data], {
                type: 'video/mp4'
            });

            log(JSON.stringify(blob));

            $("#videoRecordingHolder .loading").hide();
            $("#videoRecordingHolder .play").show();
            $("#videoRecordingHolder .play source").attr("src", URL.createObjectURL(blob));

            $("#videoRecordingHolder .sendButton").show().click(function () { PostBlob(blob) });
            $("#videoRecordingHolder .saveButton").show().attr("href", URL.createObjectURL(blob));
            $("#videoRecordingHolder .cancelButton").show().click(function () {
                $("#videoRecordingHolder .sendButton").hide();
                $("#videoRecordingHolder .saveButton").hide();
                $("#videoRecordingHolder .cancelButton").hide();
                $("#videoRecordingHolder .play").hide();
                $("#videoRecordingHolder .recordButton").show();
            });
        }
    };
    var postMessage = function () {
        posted = true;

        worker.postMessage({
            type: 'command',
            arguments: [
                '-i', videoFile,
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
                    name: videoFile
                },
                {
                    data: new Uint8Array(aab),
                    name: "audio.wav"
                }
            ]
        });
    };
}

function PostBlob(blob) {
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

function log(stuff) {
    // console.log(stuff);
}