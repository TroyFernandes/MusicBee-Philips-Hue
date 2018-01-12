﻿using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using MusicBeePlugin;
using SharpHue;
using MusicBeePlugin.SettingsComponent;
using System.Threading;
using System.Diagnostics;

namespace MusicBeePlugin
{
    public partial class Plugin
    {
        private MusicBeeApiInterface mbApiInterface;
        private PluginInfo about = new PluginInfo();
        SettingsSelector settingsSelector = new SettingsSelector();
        private Hue hue = new Hue();
        LightCollection lights;
        Random rnd = new Random();
        bool threadStop = true;
        bool stop = false;

        public PluginInfo Initialise(IntPtr apiInterfacePtr)
        {
            mbApiInterface = new MusicBeeApiInterface();
            mbApiInterface.Initialise(apiInterfacePtr);
            about.PluginInfoVersion = PluginInfoVersion;
            about.Name = "Philips Hue";
            about.Description = "Match your Philips Hue lights to the current song album art\n More Help: github.com/TroyFernandes/HueArtwork";
            about.Author = "Troy Fernandes (github.com/TroyFernandes)";
            about.TargetApplication = "";   // current only applies to artwork, lyrics or instant messenger name that appears in the provider drop down selector or target Instant Messenger
            about.Type = PluginType.General;
            about.VersionMajor = 1;  // your plugin version
            about.VersionMinor = 0;
            about.Revision = 0;
            about.MinInterfaceVersion = MinInterfaceVersion;
            about.MinApiRevision = MinApiRevision;
            about.ReceiveNotifications = (ReceiveNotificationFlags.PlayerEvents | ReceiveNotificationFlags.TagEvents);
            about.ConfigurationPanelHeight = 0;   // height in pixels that musicbee should reserve in a panel for config settings. When set, a handle to an empty panel will be passed to the Configure function
            ToolStripMenuItem mainMenuItem = (ToolStripMenuItem)mbApiInterface.MB_AddMenuItem("mnuTools/Hue Artwork", null, null);
            mainMenuItem.DropDown.Items.Add("Settings", null, OnOpen);
            mainMenuItem.DropDown.Items.Add("Stop", null, stopPlugin);
            mainMenuItem.DropDown.Items.Add("Resume", null, resumePlugin);
            Settings.Instance.Initialize();
            Settings.Instance.loadSettings(mbApiInterface.Setting_GetPersistentStoragePath());
            Configuration.Initialize(Settings.Instance.APIKey);
            settingsSelector.startup();
            lights = new LightCollection();
            foreach (string lightNames in Settings.Instance.HueLights)
            {
                new LightStateBuilder().For(lights[lightNames]).TurnOn().Apply();
            }
            return about;
        }

        public bool Configure(IntPtr panelHandle)
        {
            // save any persistent settings in a sub-folder of this path
            string dataPath = mbApiInterface.Setting_GetPersistentStoragePath();
            // panelHandle will only be set if you set about.ConfigurationPanelHeight to a non-zero value
            // keep in mind the panel width is scaled according to the font the user has selected
            // if about.ConfigurationPanelHeight is set to 0, you can display your own popup window
            if (panelHandle != IntPtr.Zero)
            {
                Panel configPanel = (Panel)Panel.FromHandle(panelHandle);
                Label prompt = new Label();
                prompt.AutoSize = true;
                prompt.Location = new Point(0, 0);
                prompt.Text = "Hue Settings:";
                configPanel.Controls.AddRange(new Control[] { prompt });
            }

            return false;
        }

        // called by MusicBee when the user clicks Apply or Save in the MusicBee Preferences screen.
        // its up to you to figure out whether anything has changed and needs updating
        public void SaveSettings()
        {
            // save any persistent settings in a sub-folder of this path
            string dataPath = mbApiInterface.Setting_GetPersistentStoragePath();

        }

        // MusicBee is closing the plugin (plugin is being disabled by user or MusicBee is shutting down)
        public void Close(PluginCloseReason reason)
        {

        }

        // uninstall this plugin - clean up any persisted files
        public void Uninstall()
        {
            //TODO: remove all the files

        }

        // receive event notifications from MusicBee
        // you need to set about.ReceiveNotificationFlags = PlayerEvents to receive all notifications, and not just the startup event
        public void ReceiveNotification(string sourceFileUrl, NotificationType type)
        {
            //query the duration of the track when the plugin receives a track change event and using a timer query the play position via the api every second for example
            // perform some action depending on the notification type
            switch (type)
            {
                case NotificationType.PluginStartup:
                    // perform startup initialisation


                    switch (mbApiInterface.Player_GetPlayState())
                    {
                        case PlayState.Playing:
                        case PlayState.Paused:
                            // ...case NotificationType.TrackChanged:

                            break;
                    }
                    break;
                case NotificationType.TrackChanged:

                    using (var albumArtBMP = new Bitmap(hue.getAlbumArt(@mbApiInterface.NowPlaying_GetFileUrl())))
                    {

                        if (Settings.Instance.AverageColor && !stop)
                        {
                            var rgbList = hue.getRGBAverage(albumArtBMP, int.Parse(Settings.Instance.QualitySetting));
                            var xy = hue.ToXYZ(rgbList[0], rgbList[1], rgbList[2]);
                            foreach (string lightName in Settings.Instance.HueLights)
                            {
                                new LightStateBuilder().For(lights[lightName]).XYCoordinates(xy[0], xy[1]).Apply();

                            }
                        }
                        if (Settings.Instance.ColorPalette)
                        {
                            List<Tuple<double, double>> colors = new List<Tuple<double, double>>();
                            Thread thread;

                            var palette = hue.getColorPalette(albumArtBMP, 8, int.Parse(Settings.Instance.QualitySetting));
                            foreach (string str in palette)
                            {
                                int red = hue.getRed(str);
                                int green = hue.getGreen(str);
                                int blue = hue.getBlue(str);
                                var xy = hue.ToXYZ(red, green, blue);
                                colors.Add(Tuple.Create((double)xy[0], (double)xy[1]));
                            }
                            if (palette.Count == 0)
                            {
                                var xyWhite = hue.ToXYZ(255, 255, 255);
                                colors.Add(Tuple.Create((double)xyWhite[0], (double)xyWhite[1]));
                            }

                            threadStop = !threadStop;
                            System.Threading.Thread.Sleep(500);
                            thread = new Thread(() => sendColors(colors, colors.Count));
                            thread.IsBackground = true;
                            thread.Start();
                        }

                    }

                    break;
            }
        }

        // return an array of lyric or artwork provider names this plugin supports
        // the providers will be iterated through one by one and passed to the RetrieveLyrics/ RetrieveArtwork function in order set by the user in the MusicBee Tags(2) preferences screen until a match is found
        public string[] GetProviders()
        {
            return null;
        }

        // return lyrics for the requested artist/title from the requested provider
        // only required if PluginType = LyricsRetrieval
        // return null if no lyrics are found
        public string RetrieveLyrics(string sourceFileUrl, string artist, string trackTitle, string album, bool synchronisedPreferred, string provider)
        {
            return null;
        }

        // return Base64 string representation of the artwork binary data from the requested provider
        // only required if PluginType = ArtworkRetrieval
        // return null if no artwork is found
        public string RetrieveArtwork(string sourceFileUrl, string albumArtist, string album, string provider)
        {
            //Return Convert.ToBase64String(artworkBinaryData)
            return null;
        }
        public void donothing(object sender, EventArgs args)
        {
            Console.WriteLine(mbApiInterface.MusicBeeVersion.ToString());
        }

        private void OnOpen(object sender, EventArgs args)
        {
            settingsSelector = new SettingsSelector();
            settingsSelector.startup();
            settingsSelector.Show();
        }

        private void stopPlugin(object sender, EventArgs args)
        {
            stop = true;
            if (Settings.Instance.ColorPalette)
            {
                threadStop = true;
            }

        }
        private void resumePlugin(object sender, EventArgs args)
        {
            stop = false;
            threadStop = !threadStop;
        }


        public void sendColors(List<Tuple<double, double>> colors, int max)
        {
            var stopwatch = new Stopwatch();
            int index;
            stopwatch.Start();
            while (!threadStop)
            {
                index = rnd.Next(0, max);
                foreach (string lightName in Settings.Instance.HueLights)
                {
                    if (stopwatch.ElapsedMilliseconds > 4500)
                    {
                        new LightStateBuilder().For(lights[lightName]).TransitionTime(30).XYCoordinates(colors[index].Item1, colors[index].Item2).Apply();
                        stopwatch.Restart();
                    }
                }
            }
            threadStop = !threadStop;
            return;
        }
    }
}