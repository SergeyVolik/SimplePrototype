using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.GraphToolsFoundation.Overdrive;

namespace SerV112.UtilityAIEditor
{
    public interface IExandableInputPortNode
    {
        void RemovePort(PortOrientation orientation, PortDirection direction);
        void AddPort(PortOrientation orientation, PortDirection direction);
    }

}
