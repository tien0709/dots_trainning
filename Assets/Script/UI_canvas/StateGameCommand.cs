using CortexDeveloper.ECSMessages.Components;
using Unity.Entities;

namespace UI_canvas
{
    public struct StateGameCommand : IComponentData, IMessageComponent {
        public float currentState;
     }
}