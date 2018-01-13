# Philips Hue Plugin for MusicBee

This is a plugin for [MusicBee](https://getmusicbee.com/) that communicates with the Philips Hue system. The plugin works by getting the album colors from the album art of the currently playing song and sends the color to the Philips bulbs. There is currently options to display a solid color, or cycle through a color palette.


<video src="https://github.com/TroyFernandes/MusicBee-Philips-Hue/blob/setup-images/Setup%20Images/gif%20(1).mp4" width="320" height="200" controls preload></video>




There is no music syncing as of right now, but I do want to add this in the future.

If you want to make changes to my code, and rehost it, feel free to do so.

NOTE: I just wanted to mention that I've only tried this on one color bulb. I can't afford to shell out $60 CAD for every bulb. However I dont think adding more will be an issue. 

# Program Specifics

Here are just some things to note about the plugin. Nothing of great importance. If you don't care, you can skip to Installation.

By default, the plugin will look for the embedded artwork in the file, if its not found, it will look for a file called cover.jpg. If the cover file is not found, the color white will be displayed on the bulb. 

I can't exactly control what colors the library choses from the image. I would write my own, however image processing is another beast I just dont have time to dedicate to. So if you see your bulb turn blue when theres one small blue item in the image, just be aware.

The program will jump a little in memory usage for a short time when the track is changed, however, it will close all resources once the processing is finished. 

The Color Palette mode will have a little bit of CPU usage increase because we need to send the next color to the bulb every 4.5s while waiting for an exit flag to stop the running thread and start a new one (if the track is changed).

If you want to have little to no increase in resource usage, choose the Color Average mode. You just wont get any cool color changing effects.

# Requirements
- Newtonsoft.Json

     You dont actually have to download anything extra. The releases will contain all the files needed. 



# Libraries Used
Special thanks to the people who created these libraries.

- [SharpHue](https://github.com/qJake/SharpHue)
- [Color Thief](https://github.com/lokesh/color-thief)
- [TagLib Sharp](https://github.com/mono/taglib-sharp)

# MusicBee API

- [MusicBee API](https://getmusicbee.com/forum/index.php?topic=1972.0)

# Installation
Please read the installation instructions carefully. Not following the instructions in order is a sure fire way to not make the plugin work. 
1) Download the Zip file from [releases](https://github.com/TroyFernandes/MusicBee-Philips-Hue/releases) and copy them into your MusicBee/Plugins folder. (Most likely C:\Program Files (x86)\MusicBee\Plugins)

![](https://github.com/TroyFernandes/MusicBee-Philips-Hue/blob/b184e55c9d2175e1fbb2d48f03de7ffa684b287c/Setup%20Images/copy%20over%20files.JPG)

2) A prompt will pop up the first time you launch MusicBee with the plugin. Click ok and a new window should come up. Click the "Register Plugin" button

     Read the instructions of the new window carefuly before you attempt anything. I will give a quick rundown of what those steps tell you to do.

     Press the button on your Hue bridge. You have 30s from the moment you press it to click the "Get Key" button. If all works out, and you see your key, then the plugin is registered.

![](https://github.com/TroyFernandes/MusicBee-Philips-Hue/blob/b184e55c9d2175e1fbb2d48f03de7ffa684b287c/Setup%20Images/register%20plugin.JPG)

3) Close the register plugin window and click the save button 

![](https://github.com/TroyFernandes/MusicBee-Philips-Hue/blob/b184e55c9d2175e1fbb2d48f03de7ffa684b287c/Setup%20Images/save%20settings.JPG)


     Just to be safe, close the settings window as well as MusicBee, and then launch MusicBee again.


4) In MusicBee, go to Tools > Hue Artwork > Settings.

     Click the "List Lights" button. You should see all your bulbs connected to your hub. One at a time, click your light name in the left box and click the "Add" button. Once you have all the bulbs you want to control added, click the "Save" button once again. 

![](https://github.com/TroyFernandes/MusicBee-Philips-Hue/blob/b184e55c9d2175e1fbb2d48f03de7ffa684b287c/Setup%20Images/list%20lights%20then%20add%20then%20save.JPG)

     Highlight the lights in the right side box and click delete if you want to remove them from the group. Then click save to save changes.
     
If anything goes wrong during the installation process, you can delete all the files and start over. You need to delete the HueArtwork_Settings.xml file found in C:\Users\Username\AppData\Roaming\MusicBee as well as all the plugin files. (Do this while MusicBee is closed)
 
# Plugin Options

For any of these options, before you change anything, it would be best if you restart MusicBee, choose the settings you want, then save the settings. e.g Dont change settings while the plugin/song is playing. This will ensure that there are no crashes or anything unecessary

1) Average Color

    Gets the average color of all pixels in the album art and sends it as one solid color to the bulb(s)
  
2) Color Palette

    Gets the 8 most dominant colors and cycles through them. You might ocassionally see some colors stay longer than others, this is because of how the colors are chosen
    
3) Brightness Slider

    Left-most side of slider is 0% and right most side is 100%. 
    
4) Quality Settings

    Choose if you want the album art to be resized before it is processed. ex (1200x1200) album art gets processed as (300x300) if 25% quality is selected. 
    
    To be honest you can probably just leave this at 100% (aka dont resize anything). Be aware however, if you choose a lower quality, the colors from the album art will be less accurate
    
5) Pause Plugin

    Go to Tools > Hue Artwork > Stop to stop the plugin. This option will reset if you restart MusicBee. Go to Edit > Preferences > Plugins and disable the plugin if you no longer want to use it
    
6) Resume Plugin

    Go to Tools > Hue Artwork > Stop to stop the plugin. This option will reset if you restart MusicBee. Go to Edit > Preferences > Plugins and disable the plugin if you no longer want to use it
    
# Side Note

This is the very first piece of software that I'm making available to people other than myself. I'm using this as a learning process so if things aren't as smooth, please dont get too mad :). If theres a feature you would like or you want to suggest improvements, open up an issue and I'll take a look. I can't guaruntee updates will be frequent, or if there'll be any for that matter as I'll be having school but I'll try my best. This of course means that if you want to make changes to my code, please feel free to do so.
