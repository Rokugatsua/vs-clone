using Godot;
using System;

public class Hitbox : Area2D
{
    [Export] private Shape2D _areaShape;
    private CollisionShape2D _collisionShape2D;
    private Timer _cooldownTimer;

    public override void _Ready()
    {
        _collisionShape2D = GetNode<CollisionShape2D>("CollisionShape2D");
        _collisionShape2D.Shape = _areaShape;
        _cooldownTimer = GetNode<Timer>("CooldownTimer");

        // change color of hurt box
        Modulate = new Color(0f, 0f, 1f, 1f);

        // Connections
        this.Connect("area_entered", this, nameof(OnAreaEntered));
    }


    private void OnAreaEntered(Hurtbox area)
    {
        area?.Damaged(1);
    }
}
