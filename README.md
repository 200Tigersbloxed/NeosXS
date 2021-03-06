# NeosXS
Implement support for XSOverlay's Notifications in Neos

**NOTE:** in-game setup is **required** for this to work. Please see the [wiki](https://github.com/200Tigersbloxed/NeosXS/wiki) for more information.

## NeosXSPlugin

The Intro to working with the NeosXSPlugin.

**NOTE: The NeosXS Plugin for Neos is Experimental and is very unstable.**
*Use at your own risk; You have been Warned*

### Installing

Step 1) Download `NeosXSPlugin.dll` from the Releases Page

Step 2) Relocate the Plugin file to your Neos Install Location then into the `Libraries` Folder

Step 3) Add `-LoadAssembly "Libraries/NeosXSPlugin.dll"` to your Launch Options, or use the Neos Launcher

## NeosXS_Headless

Working with the Headless version of NeosXS
(this does not mean you can run this program without use of Neos at all)

Note: The Neos Headless App's StatusLabel is slow to update (10s) Spamming buttons will not make the process go faster.

### Installing

Step 1) Download `NeosXS_Headless.exe` from the Releases Page

Step 2) Move the exe file anywhere (A directory is not required, you can drop it on your desktop if you want to)

Step 3) Run NeosXS_Headless.exe

Note: If you get a smart screen filter, it only says this because the application is unsigned
(So no, it is not a virus)

Step 4) Configure settings to your liking
(only change them if you 1. need to 2. know what you're doing)

Step 5) Start the Websocket

## Wine

NeosXS_Headless is not known to be able to run on wine. Please feel free to submit an issue if you have confirmed Wine to work.
