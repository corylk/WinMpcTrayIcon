# WinMpcTrayIcon

## Description
Control Music Player Daemon from the system tray!

WinMpcTrayIcon is a simple Windows system tray icon wrapper for mpc. It does not independently implement the MPD protocol, but instead relays input and output to/from the mpc CLI.

### Usage

Middle-click for a status balloon, double-click to toggle play/pause, and right-click for a context menu of additional actions. 

Supported mpc commands are:
* `mpc status`
* `mpc toggle`
* `mpc play`
* `mpc pause`
* `mpc stop`
* `mpc next`
* `mpc prev`
* `mpc clear`
* `mpc update`
* `mpc repeat`
* `mpc random`
* `mpc single`
* `mpc consume`

### Screenshots
![Context menu](https://github.com/clkmsc/WinMpcTrayIcon/blob/master/images/2.png?raw=true)
![Status tooltip](https://github.com/clkmsc/WinMpcTrayIcon/blob/master/images/1.png?raw=true)

## Setup

Run it like a console app with `dotnet` (from executable) or `dotnet run` (from source).

### Requirements

* [mpc](https://www.musicpd.org/download/mpc/0/)

### Configuration

The default configuration expects an mpd instance on localhost and mpc to be added to the system's PATH environment variable. Alternatively, build with your own configuration in `appsettings.json`.
