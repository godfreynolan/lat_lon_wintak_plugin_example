# Drone Latitude / Longitude WinTAK Plugin Example
This is an example plugin for WinTAK-CIV (Windows) which listens for MAVLink messages (e.g. broadcasted from a SITL simulator running on your computer), extracts the longitude and latitude of the drone from GPS_RAW_INT messages, and displays this information in a side panel.

## Requirements
- Visual Studio 2022
    - `.NET desktop development`
    - Ensure you have `.NET Framework 4.8.1 development tools` installed (bottom of installation details pane)
- WinTAK-CIV developer build (tested in V5.3)
    - On the [product page for WinTAK-CIV](tak.gov/products/wintak-civ), scroll down to the Downloadable Resources section, click on the Developer Resources tab, and download the SDK installer (e.g. `WinTAK-5.3.0.155-civ-sdk-installer-x64.exe`).
    - This project is based on the Visual Studio template provided in the same place: `WinTAK.Templates.vsix`.
- (Optional) Drone simulation software that can broadcast MAVLink messages in order to actually see longitude / latitude values change
    - Using WSL
        - Follow the steps on https://docs.px4.io/main/en/dev_setup/dev_env_windows_wsl.html to set up a PX4 SITL environment on Windows. 
            - Near the end, follow the steps for **QGroundControl running on Windows**, not Ubuntu.
        - In QGroundControl go to Application Settings, select the MAVLink tab, and enable MAVLink forwarding. If the port isn't 14445, change it accordingly in the code ([DockPane/DockPane.cs]()).
    - Using Docker
        - It should be possible to use this https://github.com/JonasVautherin/px4-gazebo-headless but it has not been tested

## Recreating this Plugin (starting from the WinTAK-CIV Plugin Template)
- Open Visual Studio 2022
- Click "Create a new project" and select "WinTAK Plugin (5.0)" on the left
- Click next and name your project "lat_lon_wintak_plugin_example"
    - If you want to name it something else, you'll have to modify some names in the provided [DockPane/DockPane.cs]() accordingly
- Once in your project, open the Project dropdown and select "lat_lon_wintak_plugin_example Properties"
    - In the Application tab, ensure "Target framework" is set to 4.8.1
    - In the Build tab, check "Allow unsafe code"
    - In the Debug tab:
        - Check "Start external program" and set it to WinTAK.exe
            - e.g. `C:\Program Files\WinTAK\WinTAK.exe`
        - Set "Working directory" to the directory containing WinTAK.exe 
            - e.g. `C:\Program Files\WinTAK`
- Open Views/View.xaml and insert the following code between the Grid tags
```xml
<StackPanel>
    <TextBlock>
        <Run Text="{Binding Lat}"/>
        <Run Text=","/>
        <Run Text="{Binding Lon}"/>
    </TextBlock>
</StackPanel>
```
- Replace your DockPane/DockPane.cs with the one in this repository
- Copy the MAVLink directory from this repository into your project. It should be on the same level as DockPanes, Views, etc.
- Build the app. When prompted, load the plugin.
    - If no prompt appears, go to the settings menu and see if you can load your plugin from the Plugin Manager
- Click into the plugins tab at the top, you'll see your plugin listed. Click its name to bring up a side panel
- You should see the coordinates 0,0. Start your simulator and if things are configured correctly the coordinates should automatically begin updating as the MAVLink client receives data.
    - If you don't see any changes, check the port in the code and make sure it is listening for MAVLink communication in the right place. Change it and rebuild the code if necessary.