using System.ComponentModel.Composition;
using lat_lon_wintak_plugin_example.DockPanes;
using WinTak.Framework.Docking;
using WinTak.Framework.Tools;
using WinTak.Framework.Tools.Attributes;

namespace lat_lon_wintak_plugin_example.Buttons
{
    [Button("lat_lon_wintak_plugin_example_Buttons_lat_lon_wintak_plugin_exampleButton", "lat_lon_wintak_plugin_example Plugin",
        LargeImage = "pack://application:,,,/lat_lon_wintak_plugin_example;component/Assets/Settings_32x32.svg",
        SmallImage = "pack://application:,,,/lat_lon_wintak_plugin_example;component/Assets/Settings_16x16.png")]
    internal class lat_lon_wintak_plugin_exampleButton : Button
    {
        private IDockingManager _dockingManager;

        [ImportingConstructor]
        public lat_lon_wintak_plugin_exampleButton(IDockingManager dockingManager)
        {
            _dockingManager = dockingManager;
        }

        protected override void OnClick()
        {
            base.OnClick();

            var pane = _dockingManager.GetDockPane(lat_lon_wintak_plugin_exampleDockPane.Id);
            if (pane != null)
                pane.Activate();
        }
    }
}
