using Godot;
using System;

public class Player : KinematicBody2D
{
    private const float FlipTolerance = 10.0f; // in pixel
    private Vector2 velocity = Vector2.Zero;

    [Export] float moveSpeed = 120.0f;


    private AnimatedSprite animatedSprite;

    private Area2D hurtbox;


    public override void _Ready()
    {
        // @onready
        animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
        hurtbox = GetNode<Area2D>("HurtBox") as Hurtbox;

        animatedSprite.Animation = "idle";
        animatedSprite.Playing = true;


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

        if (velocity != Vector2.Zero && animatedSprite.Animation != "run") 
        {
            animatedSprite.Animation = "run";
        }
        else if (velocity == Vector2.Zero && animatedSprite.Animation != "idle")
        {
            animatedSprite.Animation = "idle";
        }



        // flip sprite based of direction
        if (velocity.x < -FlipTolerance && !animatedSprite.FlipH)
        {
            animatedSprite.FlipH = true;
        }
        else if (velocity.x > FlipTolerance && animatedSprite.FlipH)
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
