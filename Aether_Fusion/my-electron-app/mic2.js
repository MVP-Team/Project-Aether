const { remote } = require('electron')
const { dialog } = remote

const microphoneButton = document.getElementById("micC2");
recordButton.addEventListener("click", toggleRecording);
function toggleRecording(){
navigator.mediaDevices.getUserMedia({ audio: true })
  .then(stream => {
    // Create a new MediaRecorder object
    var mediaRecorder = new MediaRecorder(stream);

    // create a variable to hold the recorded audio data
    var recordedChunks = [];

    mediaRecorder.start();
    
    // set a variable to keep track of silence
    let silenceStart = null;
    let scriptProcessor = audioContext.createScriptProcessor(4096, 1, 1);
    scriptProcessor.connect(audioContext.destination);
    scriptProcessor.onaudioprocess = (event) => {
        let inputBuffer = event.inputBuffer;
        let inputData = inputBuffer.getChannelData(0);
        let silence = true;
        for (let i = 0; i < inputBuffer.length; i++) {
            if (Math.abs(inputData[i]) > 0.01) {
                silence = false;
                break;
            }
        }
        if (silence) {
            if (!silenceStart) {
                silenceStart = Date.now();
            } else if (Date.now() - silenceStart > 4000) {
                mediaRecorder.stop();
            }
        } else {
            silenceStart = null;
        }
    }

    mediaRecorder.ondataavailable = function(event) {
      recordedChunks.push(event.data);
    }

    mediaRecorder.onstop = function() {
        // Merge the recorded audio data
        var blob = new Blob(recordedChunks, { 'type' : 'audio/wav; codecs=1' });

        // Encode the blob to wav format
        var buffer = new ArrayBuffer(blob.size);
        var view = new Uint8Array(buffer);
        for (var i = 0; i < blob.size; i++) {
            view[i] = blob[i];
        }
        var data = encodeWAV(view);
        var audioBlob = new Blob([data.buffer], {type: 'audio/wav'});

        // Create an anchor element to trigger the download of the WAV file
        var url = URL.createObjectURL(audioBlob);
        var a = document.createElement('a');
        a.href = url;
        a.download = 'recording.wav';
        a.click();

        // Clean up
        microphone.disconnect();
        scriptProcessor.disconnect();
        audioContext.close();
    }
  }).catch(error => {
    console.error('There was an error accessing the microphone', error);
});
}
function encodeWAV(samples) {
    var buffer = new ArrayBuffer(44 + samples.length * 2);
    var view = new DataView(buffer);

     
    writeString(view, 0, 'RIFF');
    
    view.setUint32(4, 36 + samples.length * 2, true);
   
    writeString(view, 8, 'WAVE');
  
    writeString(view, 12, 'fmt ');
    
    view.setUint32(16, 16, true);
    
    view.setUint16(20, 1, true);
    
    view.setUint16(22, 1, true);
    
    view.setUint32(24, sampleRate, true);
    
    view.setUint32(28, sampleRate * 2, true);
    
    view.setUint16(32, 2, true);
   
    view.setUint16(34, 16, true);
    
    writeString(view, 36, 'data');
    
    view.setUint32(40, samples.length * 2, true);

    floatTo16BitPCM(view, 44, samples);

    return view;
}

function writeString(view, offset, string) {
    for (var i = 0; i < string.length; i++) {
        view.setUint8(offset + i, string.charCodeAt(i));
    }
}

function floatTo16BitPCM(output, offset, input) {
    for (var i = 0; i < input.length; i++, offset += 2) {
        var s = Math.max(-1, Math.min(1, input[i]));
        output.setInt16(offset, s < 0 ? s * 0x8000 : s * 0x7FFF, true);
    }
}