using ABManagerEditor.Controller.Creators;

namespace ABManagerEditor.Controller
{
    internal class ABCreators
    {
        internal ManagerSettingsCreator ManagerSettingsCreator { get; }
        internal HostSettingsCreator HostSettingsCreator { get; }
        internal ABCreators()
        {
            ManagerSettingsCreator = new ManagerSettingsCreator();
            HostSettingsCreator = new HostSettingsCreator();
        }
    }
}

