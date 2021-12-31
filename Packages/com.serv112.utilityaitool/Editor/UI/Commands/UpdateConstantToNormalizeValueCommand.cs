using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEngine.GraphToolsFoundation.CommandStateObserver;

namespace SerV112.UtilityAIEditor
{
    public enum UpdateToNormailzeField
    {
        Value, Max, Min
    }
    public class UpdateConstantToNormalizeValueCommand : UndoableCommand
    {

        /// <summary>
        /// The constant to update.
        /// </summary>
        public NormalizedFloatConstant Constant;
        /// <summary>
        /// The new value.
        /// </summary>
        public float Value;

        public UpdateToNormailzeField FiledToUpdate;
        /// <summary>
        /// The node model that owns the constant, if any.
        /// </summary>
        public IGraphElementModel OwnerModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateConstantToNormalizeValueCommand"/> class.
        /// </summary>
        public UpdateConstantToNormalizeValueCommand()
        {
            UndoString = "Update Constant Value";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateConstantToNormalizeValueCommand"/> class.
        /// </summary>
        /// <param name="constant">The constant to update.</param>
        /// <param name="value">The new value.</param>
        /// <param name="owner">The node model that owns the constant, if any.</param>
        public UpdateConstantToNormalizeValueCommand(NormalizedFloatConstant constant, float value, UpdateToNormailzeField filedToUpdate, IGraphElementModel ownerModel = null) : this()
        {
            FiledToUpdate = filedToUpdate;
            Constant = constant;
            Value = value;
            OwnerModel = ownerModel;
        }

        /// <summary>
        /// Default command handler.
        /// </summary>
        /// <param name="graphToolState">The state.</param>
        /// <param name="command">The command.</param>
        public static void DefaultHandler(GraphToolState graphToolState, UpdateConstantToNormalizeValueCommand command)
        {
            graphToolState.PushUndo(command);

            using (var graphUpdater = graphToolState.GraphViewState.UpdateScope)
            {
                var data = command.Constant.Value;
                switch (command.FiledToUpdate)
                {
                    case UpdateToNormailzeField.Value:


                        if (data.Max > command.Value)
                        {
                            data.Value = command.Value;
                        }
                        else data.Value = data.Max;

                        if (data.Min < command.Value)
                        {
                            data.Value = command.Value;
                        }
                        else data.Value = data.Min;
                        break;
                    case UpdateToNormailzeField.Max:
                        if (data.Min < command.Value)
                        {
                            data.Max = command.Value;
                        }

                        if (data.Max < data.Value)
                        {
                            data.Value = data.Max;
                        }
                        break;
                    case UpdateToNormailzeField.Min:
                        if (data.Max > command.Value)
                        {
                            data.Min = command.Value;
                        }

                       
                        break;
                    default:
                        break;
                }
                command.Constant.Value = data;

                if (command.OwnerModel != null)
                {
                    graphUpdater.MarkChanged(command.OwnerModel);
                }
            }
        }
    }
}
