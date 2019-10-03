using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace SaitamaChallangeCounter
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    [Guid("D5DAB676-CEB9-4C65-8F3A-03C40929FFD0")]
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Set path of the savefile
            pathSavefile = GuidHelper.UserDataFolder + "\\SaitamaSave.xml";
            if (Debugger.IsAttached)
            {
                pathSavefile = pathSavefile.Remove(pathSavefile.Length - 4) + "-Debug.xml";
            }

            // Load the savefile
            try
            {
                LoadXML(pathSavefile);
            }
            catch (FileNotFoundException)
            {
                // File doesn't exist yet
                Save = new Save();
            }
            catch (InvalidOperationException)
            {
                // Error in XML file
                Save = new Save();
            }
            catch (UnauthorizedAccessException)
            {
                // Permission Denied
                Save = new Save();
            }

            // Set the datacontext for the gui
            DataContext = Save;

            // Set date in title to current
            Save.CurrentDate = DateTime.Now;

            // Center the window in the top right corner
            CornerWindow(CurrentScreen);
        }

        #region Variables

        private XmlSerializer XmlS = new XmlSerializer(typeof(Save));
        private string pathSavefile;

        #endregion Variables

        #region Attributes

        public Save Save { get; set; }

        public System.Windows.Forms.Screen CurrentScreen
        {
            get
            {
                if (System.Windows.Forms.Screen.AllScreens.Length > Save.ScreenNr)
                {
                    return System.Windows.Forms.Screen.AllScreens[Save.ScreenNr];
                }
                return System.Windows.Forms.Screen.AllScreens[0];
            }
        }

        public System.Windows.Forms.Screen NextScreen
        {
            get
            {
                Save.ScreenNr = (Save.ScreenNr + 1) % System.Windows.Forms.Screen.AllScreens.Length;
                return System.Windows.Forms.Screen.AllScreens[Save.ScreenNr];
            }
        }

        #endregion Attributes

        #region Methodes

        public void SaveXML(string path)
        {
            var writer = new StreamWriter(path);
            XmlS.Serialize(writer, Save);
            writer.Close();
        }

        public void LoadXML(string path)
        {
            var reader = new StreamReader(path);
            Save = (Save)XmlS.Deserialize(reader);
            reader.Close();
        }

        public void CornerWindow(System.Windows.Forms.Screen screen)
        {
            System.Drawing.Rectangle desktopArea = screen.WorkingArea;

            // Set Window to a fixed position on Desktop
            Left = desktopArea.Right - Width;
            Top = desktopArea.Top;

            // Set Window to TopMost
            // Topmost = true;
        }

        public void ChangeCounter(string tag, int amount)
        {
            // Detect if plus or minus button was pressed
            int sign = 1;
            if (tag.Contains("Minus"))
            {
                sign = -1;
            }

            // Detect which category was clicked
            if (tag.Contains("Push"))
            {
                Save.CountPushUps += amount * sign;
            }
            else if (tag.Contains("Squat"))
            {
                Save.CountSquats += amount * sign;
            }
            else if (tag.Contains("Sit"))
            {
                Save.CountSitUps += amount * sign;
            }
            else if (tag.Contains("Run"))
            {
                Save.CountRunning += amount * sign;
            }
            else
            {
                MessageBox.Show("Wrong Tag in Button!");
            }
        }

        #endregion Methodes

        #region Events

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }

        private void Btn_ChangeCounter(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            ChangeCounter(btn.Tag.ToString(), 1);

        }

        private void Btn_ChangeCounter10(object sender, MouseButtonEventArgs e)
        {
            Button btn = (Button)sender;
            ChangeCounter(btn.Tag.ToString(), 10);
        }

        private void Btn_ChangeScreen_Click(object sender, RoutedEventArgs e)
        {
            CornerWindow(NextScreen);
        }

        private void Btn_AlwaysOnTop_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            if (Topmost)
            {
                btn.Style = (Style)FindResource("btn-light");
            }
            else
            {
                btn.Style = (Style)FindResource("btn-dark");
            }

            // Change Topmost state
            Topmost = !Topmost;
        }

        private void Btn_Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            SaveXML(pathSavefile);
        }

        #endregion Events
    }
}
