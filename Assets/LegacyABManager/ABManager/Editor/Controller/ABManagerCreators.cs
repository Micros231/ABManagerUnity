using ABManagerEditor.Controller.Creators;

namespace ABManagerEditor.Controller
{
    public class ABManagerCreators
    {
        internal ABMSettingsCreator SettingsCreator { get; }
        internal ABMGroupCreator GroupCreator { get; }
        internal ABMAssetCreator AssetCreator { get; }

        public ABManagerCreators()
        {
            SettingsCreator = new ABMSettingsCreator();
            GroupCreator = new ABMGroupCreator();
            AssetCreator = new ABMAssetCreator();
        }

    }
}

