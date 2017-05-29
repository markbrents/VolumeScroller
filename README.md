# Volume Scroller
Volume Scroller is a program for controlling your computer’s audio volume via the mouse wheel. 
Just hover your pointer over the icon in the system tray and scroll your wheel to adjust the volume. 
You can also left-click on the icon to mute/unmute the volume.

## How it works 
Volume Scroller is a Window Forms program written in C#. The Windows Forms NotifyIcon component makes it easy to put an icon into the system tray. And while it provides events to check mouse clicks, it does not provide a way to tell when the mouse wheel scrolls.

However, in Windows, it is possible to install a mouse hook and be notified when events (including scrolling) happen.

The main problem is to figure out how to tell if the mouse is over our icon when the scroll event happens. Windows 7 introduced a function (Shell_NotifyIconGetRect) to get the location of an icon in the system tray. Unfortunately, using it from .NET involves a lot of Win32 app calls (p/invokes). Fortunately, someone calling themselves quppa figured it out and posted the code in a [blog post.](https://www.quppa.net/blog/2010/12/08/windows-7-style-notification-area-applications-in-wpf-part-2-notify-icon-position/) I wrapped this code into a class called NotifyIconInfo.

So, the idea is to install a mouse hook and have it call us when the wheel scrolls. Then we’ll get the location of the icon and the location of the mouse. If the mouse is over our icon, we know we should change the volume.

## Hooking
Hooking in Windows is a little complicated. Thankfully, there are .NET wrappers that do the hard work for us. The one I choose was [MouseKeyHook](https://github.com/gmamaladze/globalmousekeyhook). Once installed via nuget, it was simply a matter of declaring a variable of type IKeyboardMouseEvents and then setting up the hook and the event listeners.

## Volume
The next step was to control the volume. I was surprised how hard that turns out to be from .NET. Thankfully, I ran across the [NAudio](https://github.com/naudio/NAudio) library that gives makes it easy. I created a simple static class (VolumeController) to wrap the things we need (Volume Up, Volume Down, Mute, etc.)

## Icon(s)
As I was testing the program I decided that I wanted a visual indicator of the volume as it changed. I ended up using Gimp to create several 16x16 icons, each with a different level indicated. Then as the volume changes the icon gets updated to the appropriate one.

## Known Issues/Future Items
I need to create an installer that will put it in the user's startup. 

I've only tested it on Windows 10 64-bit.

## Alternatives
Here are some other programs that do similar things.

[Volumouse](http://www.nirsoft.net/utils/volumouse.html)

[Volume2](http://irzyxa.deviantart.com/art/Volume2-is-an-advanced-Windows-volume-control-270152280)

[Wheels Of Volume](http://drudger.deviantart.com/art/Wheels-of-Volume-2-0-277322003)
