using Godot;
using System;

public class player : KinematicBody2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.

    private Vector2 velocity = Vector2.Zero;

    [Export] float move_speed = 200.0f;


    public override void _Ready()
    {
        

    }


    public override void _PhysicsProcess(float delta)
    {
        Vector2 getDirection = getInputDirection();
        velocity = getDirection.Normalized() * move_speed;
        velocity = MoveAndSlide(velocity);
    }




    public Vector2 getInputDirection()
    {
        Vector2 direction = new Vector2();
        direction.x = Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left");
        direction.y = Input.GetActionStrength("move_down") - Input.GetActionStrength("move_up");
        return direction;
    }

}
