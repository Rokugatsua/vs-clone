using Godot;
using System;

public class Hurtbox : Area2D
{

    [Signal] delegate void on_damaged(int damagePower);
    CollisionShape2D collisionShape2D;
    public override void _Ready()
    {
        //@onready
        collisionShape2D = GetNode<CollisionShape2D>("CollisionShape2D");

        // change color of hurt box
        this.Modulate = new Color(0, 0.9f, 0, 1); // yellow collor

        // Colision Mask
        this.SetCollisionLayerBit(0, false);
        this.SetCollisionMaskBit(0, false);

        this.SetCollisionLayerBit(5, true);
    }

    public void disableCollision(bool isDisabled=true)
    {
        
        collisionShape2D?.SetDeferred("disabled", true);
    }

    public void damage(int damagePower)
    {
        EmitSignal(nameof(on_damaged), damagePower);
    }
}