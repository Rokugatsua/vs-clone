using Godot;
using System;

public class Enemies : KinematicBody2D
{
    private const float FlipTolerance = 10.0f; // in pixel

    Player player;

    private Vector2 velocity = new Vector2();
    [Export] float moveSpeed = 100f;

    AnimatedSprite animatedSprite2D;
    Area2D hurtbox;
    public override void _Ready()
    {
        // @onready
        animatedSprite2D = GetNode<AnimatedSprite>("AnimatedSprite");
        hurtbox = GetNode<Area2D>("Hurtbox") as Hurtbox;

        // animated sprite configuration
        animatedSprite2D.Animation = "idle";
        animatedSprite2D.Playing = true;


        player = GetTree().GetNodesInGroup("player")[0] as Player;
        GD.Print(player);
        

        // connections
        hurtbox.Connect("on_damaged", this, nameof(_onDamaged));

    }


    public override void _PhysicsProcess(float delta)
    {
        Vector2 directionToPlayer = GlobalPosition.DirectionTo(player.GlobalPosition);
        velocity = MoveAndSlide(directionToPlayer * moveSpeed);

    }


    public override void _Process(float delta)
    {
        if (velocity != Vector2.Zero && animatedSprite2D.Animation != "run")
        {
            animatedSprite2D.Animation = "run";
        }
        else if (velocity == Vector2.Zero && animatedSprite2D.Animation != "idle")
        {
            animatedSprite2D.Animation = "idle";
        }

        // flip prite base direction move
        if (velocity.x < -FlipTolerance && !animatedSprite2D.FlipH)
        {
            animatedSprite2D.FlipH = true;
        }
        else if (velocity.x > -FlipTolerance && animatedSprite2D.FlipH)
        {
            animatedSprite2D.FlipH = false;
        }
        
    }

    private void _onDamaged(int damagePower)
    {
        // implement damaged to entities
    }
}
