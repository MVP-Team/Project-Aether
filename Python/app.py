import whisper

# We can pick which model to load.
# Models can be listed with `whisper.available_models()`.
model = whisper.load_model("base")

# We can pass in a filename or a tensor (PyTorch or numpy).
result = model.transcribe("system_recorded_audio.wav", task="translate")

# Print the transcript.
my_file = open("audio_text.txt","w+")
my_file.write(result["text"])
