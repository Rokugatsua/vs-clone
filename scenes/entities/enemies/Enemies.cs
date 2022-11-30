using Godot;
using System;

public class Enemies : KinematicBody2D
{
    Player player;


    private Vector2 velocity = new Vector2();
    [Export] float moveSpeed = 100f;
    public override void _Ready()
    {
        player = GetTree().GetNodesInGroup("player")[0] as Player;
        GD.Print(player);
        
    }


    public override void _PhysicsProcess(float delta)
    {
        Vector2 directionToPlayer = GlobalPosition.DirectionTo(player.GlobalPosition);
        velocity = MoveAndSlide(directionToPlayer * moveSpeed);

    }


//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
