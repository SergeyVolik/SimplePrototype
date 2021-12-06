using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEditor.GraphToolsFoundation.Overdrive.BasicModel;
using UnityEngine;
using UnityEngine.UIElements;

namespace SerV112.UtilityAIEditor
{
    public class AIOnboardingProvider : OnboardingProvider
    {
        public override VisualElement CreateOnboardingElements(CommandDispatcher store)
        {
            var template = new GraphTemplate<AIStencil>(AIStencil.graphName);
            return AddNewGraphButton<AIGraphAssetModel>(template);
        }
    }
}
