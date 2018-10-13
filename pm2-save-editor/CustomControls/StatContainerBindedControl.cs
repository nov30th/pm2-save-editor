using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pm2_save_editor.CustomControls
{
    public interface IStatContainerBindedControl
    {
        /// <summary>
        /// Bind the control to a container
        /// </summary>
        /// <param name="bindTarget">The container to which the control will be bound</param>
        void Bind(IStatContainer bindTarget);
        /// <summary>
        /// Initalize the control (generate label if necessary, load inital contents, etc) - must be called after bind
        /// </summary>
        void Initalize();
        /// <summary>
        /// Get the Stat to which the control expects to be bound
        /// </summary>
        Stat GetBindTarget();
    }
}
