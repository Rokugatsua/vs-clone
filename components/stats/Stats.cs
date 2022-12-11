using Godot;
using System;
public class Stats : Resource
{
    [Signal] delegate void ChangedHealth(int health);
    [Signal] delegate void KnockedOut();
    private int _health = 0;
    public int Health {get{return _health;} private set{}}
    [Export] public int HealthPoint{get; set;}
    [Export] public int Attack{get; set;}
    [Export] public int AttackSpeed{get; set;}
    [Export] public int Critical{get; set;}
    [Export] public int PickupRange{get; set;}
    [Export] public int MovementSpeed{get; set;}

    public void SetHealth(int health)
    {
        if (!(health >= 0 && health <= HealthPoint)) return;

        _health = health;
        EmitSignal(nameof(ChangedHealth), _health);
        if (_health == 0) {
            EmitSignal(nameof(KnockedOut));
        }
    }
}
