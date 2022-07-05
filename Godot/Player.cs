using Godot;
using System;

public class Player : Area2D
{
    [Export]
    public int Speed = 400; // How fast the player will move (pixels/sec).

    public Vector2 ScreenSize; // Size of the game window.

    [Signal]
    public delegate void Hit();

    public override void _Ready()
    {
        ScreenSize = GetViewportRect().Size;

        // Hide player
        Hide();
    }

    public override void _Process(float delta)
    {
        var velocity = Vector2.Zero; // The player's movement vector.

        if (Input.IsActionPressed("move_right"))
        {
            velocity.x += 1;
        }

        if (Input.IsActionPressed("move_left"))
        {
            velocity.x -= 1;
        }

        if (Input.IsActionPressed("move_down"))
        {
            velocity.y += 1;
        }

        if (Input.IsActionPressed("move_up"))
        {
            velocity.y -= 1;
        }

        var animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");

        if (velocity.Length() > 0)
        {
            velocity = velocity.Normalized() * Speed;
            animatedSprite.Play();
        }
        else
        {
            animatedSprite.Stop();
        }

        // Change
        Position += velocity * delta;
        Position = new Vector2(
            x: Mathf.Clamp(Position.x, 0, ScreenSize.x),
            y: Mathf.Clamp(Position.y, 0, ScreenSize.y)
        );

        // Update animation
        if (velocity.x != 0)
        {
            animatedSprite.Animation = "walk";
        }
        else if (velocity.y != 0)
        {
            animatedSprite.Animation = "up";
        }

        if (velocity.x < 0)
        {
            animatedSprite.FlipH = true;
        }
        else
        {
            animatedSprite.FlipH = false;
        }

        if (velocity.y > 0)
        {
            animatedSprite.FlipV = true;
        }
        else
        {
            animatedSprite.FlipV = false;
        }
    }

    public void OnPlayerBodyEntered(PhysicsBody2D body)
    {
        Hide(); // Player disappears after being hit.
        EmitSignal(nameof(Hit));

        // Must be deferred as we can't change physics properties on a physics callback.
        GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred("disabled", true);
    }

    public void Start(Vector2 pos)
    {
        Position = pos;
        Show();
        GetNode<CollisionShape2D>("CollisionShape2D").Disabled = false;
    }
}
