using System.Collections.Generic;
using Unity.Entities;
using Unity.Rendering;
using UnityEngine;
using UnityEngine.Rendering;

namespace Components
{
    public partial struct MaterialChanger : IComponentData
    {

        public BatchMaterialID material0;
        public BatchMaterialID material1;
        public uint frequency;
        public uint frame;
        public uint active;
    }
}