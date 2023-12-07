namespace BioImager
{
    public class DSystemConfiguration                   // 52 lines
    {
        // Properties
        public string Title { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        // Static Properties
        public static bool FullScreen { get; private set; }
        public static bool VerticalSyncEnabled { get; private set; }
        public static float ScreenDepth { get; private set; }
        public static float ScreenNear { get; private set; }
        public static FormBorderStyle BorderStyle { get; set; }
        public static string ShaderFilePath { get; private set; }

        // Constructors
        public DSystemConfiguration(bool fullScreen, bool vSync) : this("Bio", fullScreen, vSync) { }
        public DSystemConfiguration(string title, bool fullScreen, bool vSync) : this(title, 800, 600, fullScreen, vSync) { }
        public DSystemConfiguration(string title, int width, int height, bool fullScreen, bool vSync)
        {
            FullScreen = fullScreen;
            Title = title;
            VerticalSyncEnabled = vSync;

            if (!FullScreen)
            {
                Width = width;
                Height = height;
            }
            else
            {
                Width = Screen.PrimaryScreen.Bounds.Width;
                Height = Screen.PrimaryScreen.Bounds.Height;
            }
        }


        // Static Constructor
        static DSystemConfiguration()
        {
            VerticalSyncEnabled = true;
            ScreenDepth = 1000.0f;
            ScreenNear = -0.1f;
            BorderStyle = FormBorderStyle.None;
            ShaderFilePath = Application.StartupPath + @"\Graphics\Shaders\";
        }
    }
}