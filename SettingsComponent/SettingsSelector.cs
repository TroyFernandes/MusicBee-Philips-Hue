using System;
using System.Windows.Forms;
using SharpHue;

namespace MusicBeePlugin.SettingsComponent
{
    //get the settings cs file and just do the get set stuff
    public partial class SettingsSelector : Form
    {


        //Settings settings = new Settings();
        public SettingsSelector()
        {

            InitializeComponent();
            // var one = Settings.Instance.QualitySetting;

        }

        public void initialize()
        {
            qualitySettingsCombobox.Items.Add("Full Quality");
            qualitySettingsCombobox.Items.Add("50% Quality");
            qualitySettingsCombobox.Items.Add("25% Quality");
            qualitySettingsCombobox.SelectedIndex = 0;
            averageColorRadio.Checked = true;
            colorPaletteRadio.Checked = false;
            brightnessTrackbar.Value = 10;
            listLightsButton.Enabled = false;
        }

        public void startup()
        {
            //Configuration.Initialize("ZiZmjXmSs-pKxRStcrZdC7NCBjRANgY7NYRASMC8");
            qualitySettingsCombobox.Items.Add("Full Quality");
            qualitySettingsCombobox.Items.Add("50% Quality");
            qualitySettingsCombobox.Items.Add("25% Quality");

            qualitySettingsCombobox.SelectedIndex = Settings.Instance.qualityToInt(int.Parse(Settings.Instance.QualitySetting));
            averageColorRadio.Checked = Settings.Instance.AverageColor;
            colorPaletteRadio.Checked = Settings.Instance.ColorPalette;
            brightnessTrackbar.Value = int.Parse(Settings.Instance.Brightness);

            foreach (string lightNames in Settings.Instance.HueLights)
            {
                addListBox.Items.Add(lightNames);
            }

            if (Settings.Instance.APIKey != null)
            {
                listLightsButton.Enabled = true;
            }
            //MessageBox.Show("SettingsSelector \nAPI KEY: " + Settings.Instance.APIKey + "\nAverage Color " + Settings.Instance.AverageColor.ToString() + "\n Color Palette: " + Settings.Instance.ColorPalette.ToString() + "\n QualitySetting: " + Settings.Instance.QualitySetting);

        }

        private void listLightsButton_Click(object sender, EventArgs e)
        {
            hueLightsListBox.Items.Clear();
            LightCollection lights = new LightCollection();
            foreach (Light light in lights)
            {
                hueLightsListBox.Items.Add(light.Name);
            }

        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (hueLightsListBox.SelectedItem != null && !addListBox.Items.Contains(hueLightsListBox.SelectedItem))
            {
                addListBox.Items.Add(hueLightsListBox.SelectedItem);
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (addListBox.SelectedItem != null)
            {

                try
                {
                    //Settings.Instance.removeLight(addListBox.GetItemText(addListBox.SelectedItem));
                    Settings.Instance.HueLights.RemoveAt(Settings.Instance.HueLights.IndexOf(addListBox.GetItemText(addListBox.SelectedItem)));
                    addListBox.Items.Remove(addListBox.SelectedItem);
                    outputLabel.Text = "Removed " + addListBox.GetItemText(addListBox.SelectedItem);
                }
                catch (IndexOutOfRangeException exception)
                {
                    MessageBox.Show("Could not find item. Please press the refresh button.");
                }

            }
            else
            {
                outputLabel.Text = "Please select an item to remove";
                outputLabel.ForeColor = System.Drawing.Color.DarkRed;
            }
        }

        private void saveSettingsButton_Click(object sender, EventArgs e)
        {

            foreach (string s in addListBox.Items)
            {
                Settings.Instance.addLight(s);
            }
            if (qualitySettingsCombobox.SelectedIndex == 0)
            {
                Settings.Instance.QualitySetting = "100";
            }
            else if (qualitySettingsCombobox.SelectedIndex == 1)
            {
                Settings.Instance.QualitySetting = "50";

            }
            else if (qualitySettingsCombobox.SelectedIndex == 2)
            {
                Settings.Instance.QualitySetting = "25";

            }

            if (averageColorRadio.Checked)
            {
                Settings.Instance.AverageColor = true;
                Settings.Instance.ColorPalette = false;
                colorPaletteRadio.Checked = false;

            }
            if (colorPaletteRadio.Checked)
            {
                Settings.Instance.AverageColor = false;
                Settings.Instance.ColorPalette = true;
                averageColorRadio.Checked = false;

            }

            Settings.Instance.Brightness = (brightnessTrackbar.Value).ToString();
            try
            {
                LightCollection lights = new LightCollection();
                foreach (string lightNames in Settings.Instance.HueLights)
                {
                    var brightness = Math.Round((int.Parse(Settings.Instance.Brightness)) * 25.5);
                    new LightStateBuilder().For(lights[lightNames]).Brightness(Convert.ToByte(brightness)).Apply();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("An error occured. Do you have an api key registered?");
                Console.WriteLine(exception);
            }


            Settings.Instance.saveSettings(@"C:\Users\Troy F\AppData\Roaming\MusicBee");
            outputLabel.Text = "Settings Saved";
            if (!string.IsNullOrEmpty(Settings.Instance.APIKey))
            {
                listLightsButton.Enabled = true;
            }
            outputLabel.ForeColor = System.Drawing.Color.DarkGreen;

        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            RegisterForm registerForm = new RegisterForm();
            registerForm.ShowDialog();


        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            outputLabel.Text = "";
            foreach (string str in Settings.Instance.HueLights)
            {
                if (!addListBox.Items.Contains(str))
                {
                    addListBox.Items.Add(str);
                }
            }
            addListBox.Update();

        }

        private void SettingsSelector_Load(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void brightnessTrackbar_Scroll(object sender, EventArgs e)
        {
            brightnessLabel.Text = "Brightness " + (brightnessTrackbar.Value * 10).ToString();
        }
    }

}
