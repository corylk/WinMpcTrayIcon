# WinMpcTrayIcon

## Description
Control Music Player Daemon from the system tray!

WinMpcTrayIcon is a simple, lightweight Windows system tray icon wrapper for [mpc](https://musicpd.org/doc/mpc/html/). It does not independently implement the MPD protocol, but instead relays input and output to/from the mpc CLI.

### Features

* Playback controls
* Toggles for play modes (e.g. repeat / consume)
* Basic queue management
* Database update and search

mpc commands supported via the GUI in one way or another are: `status`, `search`, `add`, `playlist`, `toggle`, `play`, `pause`, `stop`, `next`, `prev`, `clear`, `crop`, `update`, `repeat`, `random`, `single`, `consume`

### Usage

* Middle-click for a status notification
* Double-click to toggle play/pause
* Right-click for a context menu of additional actions
* Refer to the mpc manual for search syntax

### Screenshots

#### Status notification
![Status tooltip](https://github.com/clkmsc/WinMpcTrayIcon/blob/master/images/1.png?raw=true)

#### Context menu
![Context menu](https://github.com/clkmsc/WinMpcTrayIcon/blob/master/images/2.png?raw=true)

#### Search window
![Search](https://github.com/clkmsc/WinMpcTrayIcon/blob/master/images/3.png?raw=true)

## Setup

Publish an exe with `dotnet publish -c Release -r win10-x64 -p:PublishSingleFile=true`. Or else run it like a console app with `dotnet` (from executable) or `dotnet run` (from source).

### Requirements

* [mpc](https://www.musicpd.org/download/mpc/0/) - the application will run but not function without it.

### Configuration

The default configuration expects an mpd instance on localhost and mpc to be added to the system's PATH environment variable. Alternatively, build with your own configuration in `appsettings.json`.
