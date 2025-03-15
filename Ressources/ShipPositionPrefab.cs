using Godot;
using System;
[GlobalClass]
public partial class ShipPositionPrefab : Resource
{
    [Export] public Godot.Collections.Array<long[]> positions { get; set; }
}
