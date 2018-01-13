using System;
using System.Windows.Forms;
using SharpHue;

namespace MusicBeePlugin.SettingsComponent
{
    public partial class RegisterForm : Form
    {

        public RegisterForm()
        {
            InitializeComponent();

        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {

        }

        private void getKeyButton_Click(object sender, EventArgs e)
        {
            Configuration.AddUser();
            Settings.Instance.APIKey = Configuration.Username;
            apiKeyLabel.Text = Settings.Instance.APIKey;
        }
    }
}
