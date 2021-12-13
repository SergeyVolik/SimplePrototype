using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.GraphToolsFoundation.Overdrive;

namespace SerV112.UtilityAIEditor
{
    public static class StateGroupNodeModelExtentions
    {
        public static void GenereteStateGroup(this StateGroupNodeModel model, string path)
        {
            List<string> @params = new List<string>();
            model.GetConnectedNodes(PortDirection.Input, PortType.Data).OfType<StateNodeModel>().ToList().ForEach(e => {
                @params.Add(e.Name);
            });

            var asset = model.GraphModel.AssetModel as AIGraphAssetModel;
            T4GenUtils.CreateEnum(path, model.Name, new CreateEnumSettings(model.Name, @params, asset.Namespace));

        }
    }
}
