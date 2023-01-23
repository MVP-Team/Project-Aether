document.writeln("<script type='text/javascript' src='choices.js'></script>");
document.writeln("<script src='node_modules/electron-dl/index.js'></script>");
document.writeln(
  "<script src='node_modules/downloadjs/download.min.js'></script>"
);
var recorder, gumStream, audioCtx;
var recordButton = document.getElementById("micC2");
recordButton.addEventListener("click", toggleRecording);

function toggleRecording() {
  if (recorder && recorder.state == "recording") {
    recorder.stop();
    gumStream.getAudioTracks()[0].stop();
    clearInterval(silenceDetectionInterval);
  } else {
    navigator.mediaDevices
      .getUserMedia({
        audio: true,
      })
      .then(function (stream) {
        gumStream = stream;
        audioCtx = new (window.AudioContext || window.webkitAudioContext)();
        var source = audioCtx.createMediaStreamSource(stream);
        var analyser = audioCtx.createAnalyser();
        source.connect(analyser);
        analyser.fftSize = 2048;
        var bufferLength = analyser.frequencyBinCount;
        var dataArray = new Uint8Array(bufferLength);

        recorder = new MediaRecorder(stream);
        var audioBlob;
        recorder.ondataavailable = function (e) {
          audioBlob = new Blob([e.data], { type: "audio/wav; codecs=opus" });
        };
        recorder.onstop = function () {
          console.log("Recording stopped due to silence");
          download(audioBlob, {
            saveAs: false,
            directory: "Downloads",
            filename: "system_recorded_audio.wav"
          }).then((dl) =>
            console.log(`Downloaded to ${dl.getSavePath()}`).then(
              fetch(`http://localhost:5000/voice`, {
                mode: "cors",
              })
                .then((response) => response.text())
                .then((data) => {
                  output = choice(data);
                })
            )
          );
        };

        var silenceStart;
        var silenceThreshold = 3000; // threshold value in milliseconds
        var silenceDetectionInterval = setInterval(function () {
          analyser.getByteTimeDomainData(dataArray);
          var currentAmplitude = getCurrentAmplitude(dataArray);
          if (currentAmplitude < 128) {
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
