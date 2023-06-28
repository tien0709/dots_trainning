using System.Collections.Generic;
using Unity.Entities;
using Unity.Rendering;
using UnityEngine;
using UnityEngine.Rendering;

public class MaterialChanger : IComponentData
{
    public Material material0;
    public Material material1;
    public uint frequency;
    public uint frame;
    public uint active;
}
