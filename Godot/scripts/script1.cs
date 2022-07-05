using Godot;
using System;

public class script1 : Node
{
    // Declare member variables here. Examples:
    private int a = 2;
    public string b = "text";
    public static string Value = "StaticValue!";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GD.Print("Executed C# code !");
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
