using CortexDeveloper.ECSMessages.Components;
using Unity.Entities;

namespace UI_canvas
{
    public struct EndGameCommand : IComponentData, IMessageComponent { 
        public float Point;
        public float Level;
    }
}