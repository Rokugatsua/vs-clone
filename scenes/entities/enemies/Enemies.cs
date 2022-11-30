using Godot;
using System;

public class Enemies : KinematicBody2D
{
    Player player;


    private Vector2 velocity = new Vector2();
    [Export] float moveSpeed = 100f;

    AnimatedSprite animatedSprite2D;
    Area2D hurtbox;
    public override void _Ready()
    {
        // @onready
        animatedSprite2D = GetNode<AnimatedSprite>("AnimatedSprite");
        hurtbox = GetNode<Area2D>("HurtBox") as Hurtbox;

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
        if (velocity != Vector2.Zero)
        {
            animatedSprite2D.Play("run");
        }
        if (velocity.x < 0.0f)
        {
            animatedSprite2D.FlipH = true;
        }
        else if (velocity.x > 0.0f)
        {
            animatedSprite2D.FlipH = false;
        }
        
    }

    private void _onDamaged(int damagePower)
    {
        // implement damaged to entities
    }
}
