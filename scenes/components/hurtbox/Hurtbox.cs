using Godot;
using System;

public class Hurtbox : Area2D
{
    [Signal] delegate void on_hurted(int damagePower);
    
    [Export] private Shape2D _areaShape;
    private CollisionShape2D _collisionShape2D;
    private Timer _cooldownTimer;

    public override void _Ready()
    {
        // @onready
        _collisionShape2D = GetNode<CollisionShape2D>("CollisionShape2D");
        _collisionShape2D.Shape = _areaShape;
        _cooldownTimer = GetNode<Timer>("CooldownTimer");

        // change color of hurt box
        Modulate = new Color(0, 0.9f, 0, 1); // yellow collor

        // Connections
        _cooldownTimer.Connect("timeout", this, nameof(OnCooldownTimerTimeout));
    }

    public void DisableCollision(bool isDisabled=true)
    {
        _collisionShape2D?.CallDeferred("set", "disabled", isDisabled);
    }

    public void Damaged(int damagePower)
    {
        DisableCollision(true);
        EmitSignal(nameof(on_hurted), damagePower);
        _cooldownTimer.Start();
    }

    private void OnCooldownTimerTimeout()
    {
        DisableCollision(false);
    }
}