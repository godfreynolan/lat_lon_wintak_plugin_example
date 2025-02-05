using System.ComponentModel.Composition;
using Prism.Mef.Modularity;
using Prism.Modularity;
using WinTak.Common.Messaging;
using WinTak.Framework;
using WinTak.Framework.Messaging;

namespace lat_lon_wintak_plugin_example
{
    [ModuleExport(typeof(lat_lon_wintak_plugin_exampleModule), InitializationMode = InitializationMode.WhenAvailable)]
    internal class lat_lon_wintak_plugin_exampleModule : IModule, ITakModule
    {
        private readonly IMessageHub _messageHub;

        [ImportingConstructor]
        public lat_lon_wintak_plugin_exampleModule(IMessageHub messageHub)
        {
            _messageHub = messageHub;
        }

        // Modules will be initialized during startup. Any work that needs to be done at startup can
        // be initiated from here.
        public void Initialize()
        {
            _messageHub.Subscribe<ClearContentMessage>(OnClearContent);
        }

        // This method will be called when the WinTAK splash screen has closed and 'startup' is finished
        public void Startup()
        {
        }

        // This method is called when WinTAK is shutting down. Any cleanup operations can be run from here.
        public void Terminate()
        {
        }

        // Thes message is invoked if the user has initiated clear content. Delete any user data
        private void OnClearContent(ClearContentMessage args)
        {
        }
    }
}
