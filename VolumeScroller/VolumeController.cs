using NAudio.CoreAudioApi;

namespace VolumeScroller
{
    public static class VolumeController
    {

        private static MMDeviceEnumerator devEnum = new MMDeviceEnumerator();
        private static MMDevice defaultDevice = devEnum.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);

        public static void UpVolume()
        {
             defaultDevice.AudioEndpointVolume.VolumeStepUp();
        }

        public static void DownVolume()
        {
            defaultDevice.AudioEndpointVolume.VolumeStepDown();
        }

        public static float GetCurrentVolume()
        {
            var vol = defaultDevice.AudioEndpointVolume.MasterVolumeLevelScalar;
            return vol; 
        }

        public static bool IsMuted()
        {
            return defaultDevice.AudioEndpointVolume.Mute;
        }

        public static void ToggleMute()
        {
            defaultDevice.AudioEndpointVolume.Mute = !defaultDevice.AudioEndpointVolume.Mute; 
        }
    }
}
