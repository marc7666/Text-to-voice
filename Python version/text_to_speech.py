"""
__author__ = Marc Cervera Rosell
__date__ = 13/12/2022
"""
from gtts import gTTS

if __name__ == "__main__":
    text_to_reproduce = "Hi I'm Ari"
    language = "en"
    text_to_speech_converter = gTTS(text=text_to_reproduce, lang=language, slow=True)
    text_to_speech_converter.save("test.wav")
