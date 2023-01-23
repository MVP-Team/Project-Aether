var recorder, gumStream, audioCtx;
var recordButton = document.getElementById("micC2");
recordButton.addEventListener("click", toggleRecording);

function toggleRecording() {
    if (recorder && recorder.state == "recording") {
        recorder.stop();
        gumStream.getAudioTracks()[0].stop();
        clearInterval(silenceDetectionInterval);
    } else {
        navigator.mediaDevices.getUserMedia({
            audio: true
        }).then(function(stream) {
            gumStream = stream;
            audioCtx = new (window.AudioContext || window.webkitAudioContext)();
            var source = audioCtx.createMediaStreamSource(stream);
            var analyser = audioCtx.createAnalyser();
            source.connect(analyser);
            analyser.fftSize = 2048;
            var bufferLength = analyser.frequencyBinCount;
            var dataArray = new Uint8Array(bufferLength);

            recorder = new MediaRecorder(stream);
            recorder.ondataavailable = function(e) {
                var url = URL.createObjectURL(e.data);
                download(url, "system_audio.wav")
            };
            recorder.onstop = function() {
                console.log("Recording stopped due to silence");
            };

            var silenceStart;
            var silenceThreshold = 3000; // threshold value in milliseconds
            var silenceDetectionInterval = setInterval(function() {
                analyser.getByteTimeDomainData(dataArray);
                var currentAmplitude = getCurrentAmplitude(dataArray);
                if (currentAmplitude < 128 ){
                    if (!silenceStart) {
                        silenceStart = Date.now();
                    } else if (Date.now() - silenceStart > silenceThreshold) {
                        recorder.stop();
                        gumStream.getAudioTracks()[0].stop();
                        clearInterval(silenceDetectionInterval);
                    }
                } else {
                    silenceStart = null;
                }
            }, 50);

            recorder.start();
        });
    }
}



function getCurrentAmplitude(dataArray) {
    var sum = 0;
    for (var i = 0; i < dataArray.length; i++) {
        sum += dataArray[i];
    }
    var average = sum / dataArray.length;
    return average;
}
function download(dataurl, filename) {
  const link = document.createElement("a");
  link.href = dataurl;
  link.download = filename;
  link.click();
}
