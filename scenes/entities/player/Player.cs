using Godot;
using System;

public class Player : KinematicBody2D
{
    private Vector2 velocity = Vector2.Zero;

    [Export] float moveSpeed = 120.0f;


    private AnimatedSprite animatedSprite;

    private Area2D hurtbox;


    public override void _Ready()
    {
        // @onready
        animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
        hurtbox = GetNode<Area2D>("HurtBox") as Hurtbox;

        // Connections
        hurtbox.Connect("on_damaged", this, nameof(_onDamaged));
    }


    public override void _PhysicsProcess(float delta)
    {
        // move character of player
        Vector2 getDirection = getInputDirection();
        velocity = getDirection.Normalized() * moveSpeed;
        velocity = MoveAndSlide(velocity);
    }


    public override void _Process(float delta)
    {
        base._Process(delta);


        // flip sprite based of direction
        if (velocity.x < 0)
        {
            animatedSprite.FlipH = true;
        }
        else if (velocity.x > 0)
        {
            animatedSprite.FlipH = false;
        }
    }


    public Vector2 getInputDirection()
    {
        // input
        Vector2 direction = new Vector2();
        direction.x = Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left");
        direction.y = Input.GetActionStrength("move_down") - Input.GetActionStrength("move_up");
        return direction;
    }

    private void _onDamaged(int damagePower)
    {
        // implement damaged
    }
}
