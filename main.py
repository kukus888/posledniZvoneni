import os
import vlc
import platform
import sys
import time
from PyQt5 import QtWidgets, QtGui, QtCore

filename = 'video.mp4'
class Player(QtWidgets.QMainWindow):
    def __init__(self, master=None):
            QtWidgets.QMainWindow.__init__(self, master)
            self.setWindowTitle("Media Player")
            self.mediaplayer = vlc.MediaPlayer(filename)
            #self.mediaplayer = self.instance.media_player_new()
            
            
            self.videoframe = QtWidgets.QFrame()
            #self.mediaplayer.set_media(filename)
            self.widget = QtWidgets.QWidget(self)
            self.setCentralWidget(self.widget)

            # In this widget, the video will be drawn
            if platform.system() == "Darwin": # for MacOS
                self.videoframe = QtWidgets.QMacCocoaViewContainer(0)
            else:
                self.videoframe = QtWidgets.QFrame()
            
            self.palette = self.videoframe.palette()
            self.palette.setColor(QtGui.QPalette.Window, QtGui.QColor(0, 0, 0))
            self.videoframe.setPalette(self.palette)
            self.videoframe.setAutoFillBackground(True)
            self.vboxlayout = QtWidgets.QVBoxLayout()
            self.vboxlayout.addWidget(self.videoframe)
            self.widget.setLayout(self.vboxlayout)
            if platform.system() == "Linux": # for Linux using the X Server
                 self.mediaplayer.set_xwindow(int(self.videoframe.winId()))
            elif platform.system() == "Windows": # for Windows
                 self.mediaplayer.set_hwnd(int(self.videoframe.winId()))
            elif platform.system() == "Darwin": # for MacOS
                 self.mediaplayer.set_nsobject(int(self.videoframe.winId()))

    def play(self):
        self.mediaplayer.play()
        time.sleep(2)
        
        print(exit)
    def video_end(self):
        pass

    def closeEvent(self,event):
        event.ignore()
    
def main():
    app = QtWidgets.QApplication(sys.argv)
    player = Player()
    player.showFullScreen()
    player.resize(640, 480)
    player.play()
    
    sys.exit(app.exec_())
    

if __name__ == "__main__":
    main()
    sys.exit(0)