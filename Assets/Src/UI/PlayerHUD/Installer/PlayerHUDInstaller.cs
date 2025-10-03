using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using VContainer;
using VContainer.Unity;

namespace Src.UI.PlayerHUD.Installer {
    public class PlayerHUDInstaller : SerializedMonoBehaviour, IInstaller {

        [OdinSerialize]
        private List<IInstaller> m_hudInstallers = new();

        public void Install(IContainerBuilder builder) {
            
            if (m_hudInstallers is null || m_hudInstallers.Count is 0) {
                return;
            }
            
            m_hudInstallers.ForEach(installer => installer.Install(builder));
        }
    }
}