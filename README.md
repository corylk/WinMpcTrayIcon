# WinMpcTrayIcon

## Description
Control Music Player Daemon from the system tray!

WinMpcTrayIcon is a simple, lightweight Windows system tray icon wrapper for [mpc](https://musicpd.org/doc/mpc/html/). It does not independently implement the MPD protocol, but instead relays input and output to/from the mpc CLI.

### Features

* Playback controls
* Toggles for play modes (e.g. repeat / consume)
* Basic queue management (clear / add)
* Database update and search

### Usage

Middle-click for a status balloon, double-click to toggle play/pause, and right-click for a context menu of additional actions. 

mpc commands supported via the GUI in one way or another are:
`status`, `search`, `add`, `playlist`, `toggle`, `play`, `pause`, `stop`, `next`, `prev`, `clear`, `crop`, `update`, `repeat`, `random`, `single`, `consume`

### Screenshots
![Context menu](https://github.com/clkmsc/WinMpcTrayIcon/blob/master/images/2.png?raw=true)
![Status tooltip](https://github.com/clkmsc/WinMpcTrayIcon/blob/master/images/1.png?raw=true)
![Search](https://github.com/clkmsc/WinMpcTrayIcon/blob/master/images/3.png?raw=true)

## Setup

Run it like a console app with `dotnet` (from executable) or `dotnet run` (from source).

Publish an exe with `dotnet publish -c Release -r win10-x64 -p:PublishSingleFile=true`.

### Requirements

* [mpc](https://www.musicpd.org/download/mpc/0/)

### Configuration

The default configuration expects an mpd instance on localhost and mpc to be added to the system's PATH environment variable. Alternatively, build with your own configuration in `appsettings.json`.
