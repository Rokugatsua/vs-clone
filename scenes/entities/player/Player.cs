using Godot;

public class Player : KinematicBody2D
{

    [Export] float MovementSpeed = 120.0f;
    [Export] public int HealthPoint = 100;


    private const float FlipTolerance = 10.0f; // in pixel
    private Vector2 _velocity = Vector2.Zero;
    private AnimatedSprite animatedSprite;
    private Area2D Hurtbox;


    public override void _Ready()
    {
        // @onready
        animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
        Hurtbox = GetNode<Area2D>("Hurtbox") as Hurtbox;

        animatedSprite.Animation = "idle";
        animatedSprite.Playing = true;


        // Connections
        Hurtbox.Connect("on_hurted", this, nameof(OnDamaged));
    }


    public override void _PhysicsProcess(float delta)
    {
        // move character of player
        Vector2 getDirection = GetInputDirection();
        _velocity = getDirection.Normalized() * MovementSpeed;
        _velocity = MoveAndSlide(_velocity);
    }


    public override void _Process(float delta)
    {

        if (_velocity != Vector2.Zero && animatedSprite.Animation != "run") 
        {
            animatedSprite.Animation = "run";
        }
        else if (_velocity == Vector2.Zero && animatedSprite.Animation != "idle")
        {
            animatedSprite.Animation = "idle";
        }



        // flip sprite based of direction
        if (_velocity.x < -FlipTolerance && !animatedSprite.FlipH)
        {
            animatedSprite.FlipH = true;
        }
        else if (_velocity.x > FlipTolerance && animatedSprite.FlipH)
        {
            animatedSprite.FlipH = false;
        }
    }


    public Vector2 GetInputDirection()
    {
        // input
        Vector2 direction = new Vector2();
        direction.x = Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left");
        direction.y = Input.GetActionStrength("move_down") - Input.GetActionStrength("move_up");
        return direction;
    }

    private void OnDamaged(int damagePower)
    {
        // implement damaged
        HealthPoint -= damagePower;
    }
}
