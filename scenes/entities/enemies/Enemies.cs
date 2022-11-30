using Godot;
using System;

public class Enemies : KinematicBody2D
{
    private const float FlipTolerance = 10.0f; // in pixel

    private Vector2 _velocity = new Vector2();
    [Export] float moveSpeed = 100f;
    [Export] public int HealthPoint = 100;

    private Player _player;
    private AnimatedSprite _animatedSprite2D;
    private Area2D _hurtbox;

    public override void _Ready()
    {
        // @onready
        _animatedSprite2D = GetNode<AnimatedSprite>("AnimatedSprite");
        _hurtbox = GetNode<Area2D>("Hurtbox") as Hurtbox;

        // animated sprite configuration
        _animatedSprite2D.Animation = "idle";
        _animatedSprite2D.Playing = true;

        _player = GetTree().GetNodesInGroup("player")[0] as Player;
        
        // connections
        _hurtbox.Connect("on_hurted", this, nameof(OnDamaged));
    }

    public override void _PhysicsProcess(float delta)
    {
        // Movement
        Vector2 directionToPlayer = GlobalPosition.DirectionTo(_player.GlobalPosition);
        _velocity = MoveAndSlide(directionToPlayer * moveSpeed);
    }

    public override void _Process(float delta)
    {
        if (_velocity != Vector2.Zero && _animatedSprite2D.Animation != "run")
        {
            _animatedSprite2D.Animation = "run";
        }
        else if (_velocity == Vector2.Zero && _animatedSprite2D.Animation != "idle")
        {
            _animatedSprite2D.Animation = "idle";
        }

        // flip prite base direction move
        if (_velocity.x < -FlipTolerance && !_animatedSprite2D.FlipH)
        {
            _animatedSprite2D.FlipH = true;
        }
        else if (_velocity.x > FlipTolerance && _animatedSprite2D.FlipH)
        {
            _animatedSprite2D.FlipH = false;
        }   
    }

    private void Die()
    {
        QueueFree();
    }

    private void OnDamaged(int damagePower)
    {
        // implement damaged to entities
        HealthPoint -= damagePower;
    }
}
