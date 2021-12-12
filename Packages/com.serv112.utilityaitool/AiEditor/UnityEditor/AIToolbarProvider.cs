using UnityEditor.GraphToolsFoundation.Overdrive;

namespace SerV112.UtilityAIEditor
{

    public class AIToolbarProvider : IToolbarProvider
    {

        public bool ShowButton(string buttonName)
        {
            return buttonName != MainToolbar.EnableTracingButton;
        }
    }
}
