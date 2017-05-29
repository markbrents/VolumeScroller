using Gma.System.MouseKeyHook;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace VolumeScroller
{
    public partial class MainForm : Form
    {

        private string m_AppName = "Volume Scroller";

        IKeyboardMouseEvents m_Global_Hook;

        public MainForm()
        {
            InitializeComponent();
            UpdateIcon();
            SetHook();
            VolumeController.VolumeChanged += VolumeController_VolumeChanged;
            this.Text = m_AppName;
            lblProgramTitle.Text = m_AppName;
        }

        private void VolumeController_VolumeChanged(object sender, EventArgs e)
        {
            UpdateIcon(); 
        }

        private void SetHook()
        {
            m_Global_Hook = Hook.GlobalEvents();
            m_Global_Hook.MouseWheel += Global_Hook_MouseWheel; ;
        }

        private void UnsetHook()
        {
            m_Global_Hook.MouseWheel -= Global_Hook_MouseWheel;
        }

        private void Global_Hook_MouseWheel(object sender, MouseEventArgs e)
        {
            var iconLocation = NotifyIconInfo.GetNotifyIconLocation(volumeNotifyIcon);

            if (iconLocation.left <= Cursor.Position.X && 
                iconLocation.right >= Cursor.Position.Y && 
                iconLocation.top <= Cursor.Position.Y && 
                iconLocation.bottom >= Cursor.Position.Y)
            {
                if (e.Delta > 0)
                    VolumeController.UpVolume();
                else
                    VolumeController.DownVolume();

                UpdateIcon(); 
            }
        }

        private void UpdateIcon()
        {
                volumeNotifyIcon.Icon = GetVolumeIcon();
        }

        private Icon GetVolumeIcon()
        {
            if (VolumeController.IsMuted())
                return Properties.Resources.MuteIcon; 

            var vol = VolumeController.GetCurrentVolume();

            if (vol >= .90)
                return Properties.Resources.Level10Icon;

            if (vol >= .80)
                return Properties.Resources.Level9Icon;

            if (vol >= .70)
                return Properties.Resources.Level8Icon;

            if (vol >= .60)
                return Properties.Resources.Level7Icon;

            if (vol >= .50)
                return Properties.Resources.Level6Icon;

            if (vol >= .40)
                return Properties.Resources.Level5Icon;

            if (vol >= .30)
                return Properties.Resources.Level4Icon;

            if (vol >= .20)
                return Properties.Resources.Level3Icon;

            if (vol >= .10)
                return Properties.Resources.Level2Icon;

            return Properties.Resources.Level1Icon;

        }

        private void volumeNotifyIcon_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                VolumeController.ToggleMute();
                UpdateIcon();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            this.Hide();
        }


    }
}
