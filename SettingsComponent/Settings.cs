using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpHue;
using System.IO;
using System.Xml;
using System.Windows.Forms;

namespace MusicBeePlugin.SettingsComponent
{
    public class Settings
    {
        public string APIKey { get; set; }
        public List<string> HueLights;
        public bool AverageColor { get; set; }
        public bool ColorPalette { get; set; }
        public string QualitySetting { get; set; }
        public string Brightness { get; set; }
        public static Settings Instance = new Settings();
        public string storagePath { get; set; }

        public void Initialize()
        {
            HueLights = new List<string>();

        }
        private Settings()
        {

        }

        public void addLight(string name)
        {
            if (HueLights == null)
            {
                Initialize();
            }
            HueLights.Add(name);
        }

        public void loadSettings(string path)
        {
            if (HueLights == null)
            {
                Initialize();
            }
            if (File.Exists(@path + @"\HueArtwork_Settings.xml") && new FileInfo(@path + @"\HueArtwork_Settings.xml").Length == 0)
            {
                File.Delete(@path + @"\HueArtwork_Settings.xml");
            }
            if (!File.Exists(@path + @"\HueArtwork_Settings.xml"))
            {
                createConfig(@path);
                MessageBox.Show("You need to setup the plugin.");
                SettingsSelector form = new SettingsSelector();
                form.initialize();
                form.ShowDialog();

            }

            List<string> lightNames = new List<string>();

            XmlDocument doc = new XmlDocument();
            doc.Load(@path + @"HueArtwork_Settings.xml");
            APIKey = doc.DocumentElement.SelectSingleNode("/HueArtwork/appConfig/APIKey").InnerText;
            AverageColor = Boolean.Parse(doc.DocumentElement.SelectSingleNode("/HueArtwork/appConfig/AverageColor").InnerText);
            ColorPalette = Boolean.Parse(doc.DocumentElement.SelectSingleNode("/HueArtwork/appConfig/ColorPalette").InnerText);
            QualitySetting = doc.DocumentElement.SelectSingleNode("/HueArtwork/appConfig/QualitySettings").InnerText;
            Brightness = doc.DocumentElement.SelectSingleNode("/HueArtwork/appConfig/Brightness").InnerText;
            foreach (XmlNode node in doc.DocumentElement.SelectSingleNode("/HueArtwork/appConfig/HueLights"))
            {
                HueLights.Add(node.InnerText);
            }

            //MessageBox.Show("SettingsClass \nAPI KEY: "+APIKey+"\nAverage Color "+ AverageColor.ToString() + "\n Color Palette: " + Instance.ColorPalette.ToString() +"\n QualitySetting: "+ QualitySetting + "\n Brightness: " + Brightness);

        }

        public void saveSettings(string path)
        {

            File.WriteAllText(@path + @"\HueArtwork_Settings.xml", "");
            if (HueLights == null)
            {
                HueLights.Add("");
            }
            HueLights = HueLights.Distinct().ToList();
            XmlTextWriter xWriter = new XmlTextWriter(@path + @"\HueArtwork_Settings.xml", Encoding.UTF8);
            xWriter.Formatting = Formatting.Indented;
            xWriter.WriteStartElement("HueArtwork");
            xWriter.WriteStartElement("appConfig");
            xWriter.WriteElementString("APIKey", APIKey);
            xWriter.WriteStartElement("HueLights");
            foreach (string light in HueLights)
            {
                if (light != null)
                {
                    xWriter.WriteElementString("HueLights", light);
                }
            }
            xWriter.WriteEndElement();
            xWriter.WriteElementString("AverageColor", AverageColor.ToString());
            xWriter.WriteElementString("ColorPalette", ColorPalette.ToString());
            xWriter.WriteElementString("QualitySettings", QualitySetting);
            xWriter.WriteElementString("Brightness", Brightness);
            xWriter.WriteEndElement();
            xWriter.Close();
            MessageBox.Show("File saved @ " + @path + "HueArtwork_Settings.xml");

        }

        public void createConfig(string path)
        {
            XmlTextWriter xWriter = new XmlTextWriter(@path + @"\HueArtwork_Settings.xml", Encoding.UTF8);
            xWriter.Formatting = Formatting.Indented;
            xWriter.WriteStartElement("HueArtwork");
            xWriter.WriteStartElement("appConfig");
            xWriter.WriteElementString("APIKey", "");
            xWriter.WriteStartElement("HueLights");
            xWriter.WriteElementString("HueLights", "");
            xWriter.WriteEndElement();
            xWriter.WriteElementString("AverageColor", "True");
            xWriter.WriteElementString("ColorPalette", "False");
            xWriter.WriteElementString("QualitySettings", "100");
            xWriter.WriteElementString("Brightness", "10");
            xWriter.WriteEndElement();
            xWriter.Close();
        }

        public int qualityToInt(int quality)
        {
            if (quality == 100)
            {
                return 0;
            }
            else if (quality == 50)
            {
                return 1;
            }
            else if (quality == 25)
            {
                return 2;
            }
            return -1;
        }

        public void removeLight(string name)
        {
            HueLights.RemoveAt(HueLights.IndexOf(name));
        }

        public double qualityFactor(int quality)
        {
            if (quality == 100)
            {
                return 1;
            }
            else if (quality == 50)
            {
                return 0.5;
            }
            else if (quality == 25)
            {
                return 0.25;
            }
            return 1;
        }


    }
}
