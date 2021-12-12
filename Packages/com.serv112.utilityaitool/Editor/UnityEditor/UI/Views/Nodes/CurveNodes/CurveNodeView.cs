using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEngine.UIElements;

namespace SerV112.UtilityAIEditor
{
    
    class CurveNodeView : CollapsibleInOutNode
    {
        public static readonly string paramContainerPartName = "parameter-container1";

      
        protected override void UpdateElementFromModel()
        {
            base.UpdateElementFromModel();

            bool collapsed = (NodeModel as ICollapsible)?.Collapsed ?? false;


            var paramsConteiner = this.SafeQ(paramContainerPartName);

            if (collapsed == false)
            {

                paramsConteiner.style.display = DisplayStyle.Flex;
            }
            else
            {

                paramsConteiner.style.display = DisplayStyle.None;
            }
        }

        protected override void PostBuildUI()
        {

            base.PostBuildUI();

        }


    }
}
