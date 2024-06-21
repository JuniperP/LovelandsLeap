using System;
using Godot;

public class WalkState : IMovementState
{
	public void HandleMovement(Player ctx, double delta)
	{
		Vector2 targetVelocity = ctx.Velocity;

		// Kill vertical velocity when hitting ceiling
		if (ctx.IsOnCeiling())
			targetVelocity.Y = 0;

		// Handle gravity
		float gravity = (float)ProjectSettings.GetSetting("physics/2d/default_gravity");
		if (!ctx.IsOnFloor())
			targetVelocity.Y += gravity * ctx.GravityMultiplier * (float)delta;

		// Handle jump
		else if (Input.IsActionJustPressed("move_up"))
			targetVelocity.Y = -ctx.JumpImpulse;

		// Smooth velocity towards horizontal direction
		float direction = Input.GetAxis("move_left", "move_right");
		targetVelocity.X = Mathf.MoveToward(
			ctx.Velocity.X,
			direction * ctx.Speed,
			ctx.Acceleration * (float)delta
		);

		// Cap vertical speed
		targetVelocity.Y = Mathf.Min(targetVelocity.Y, ctx.MaxFallSpeed);

		// Update velocity and move
		ctx.Velocity = targetVelocity;
		ctx.MoveAndSlide();
	}

	public void HandleAction(Player ctx)
	{
		if (Input.IsActionJustPressed("primary_click"))
		{
			if (ctx.TongueProjExists) // Skip if using tongue
			{
				ctx.TongueProj.RetractTongue(
					ctx.GlobalPosition - ctx.TongueProj.GlobalPosition
				);
				return;
			}

			Vector2 mousePos = ctx.GetViewport().GetMousePosition();
			Vector2 direction = (mousePos - ctx.Position).Normalized();

			// Skip if shooting too low
			float minSin = -Mathf.Sin(Mathf.DegToRad(ctx.TongueAngle));
			if (direction.Y > minSin)
				return;

			// Create tongue projectile
			ctx.TongueProj = ctx.TongueProjScene.Instantiate<TongueProjectile>();
			ctx.TongueProjExists = true;

			// Move projectile towards mouse position
			ctx.TongueProj.LinearVelocity = direction * ctx.TongueProjSpeed;

			// Setup projectile
			ctx.TongueProj.BodyEntered += ctx.EnableGrapple;
			ctx.AddChild(ctx.TongueProj);

			// Create and setup tongue line
			ctx.TongueLine = ctx.TongueLineScene.Instantiate<TongueLine>();
			ctx.TongueLine.Target = ctx.TongueProj;
			ctx.TongueProj.TongueLine = ctx.TongueLine;
			ctx.AddChild(ctx.TongueLine);
		}
	}

	public void EnableGrapple(Player ctx)
	{
		// Remove tongue projectile
		ctx.TongueProj.QueueFree();
		ctx.TongueProjExists = false;

		// Create spring and update line
		ctx.TongueSpring = ctx.TongueSpringScene.Instantiate<TongueSpring>();
		ctx.TongueSpring.GlobalPosition = ctx.TongueProj.GlobalPosition;
		ctx.TongueLine.Target = ctx.TongueSpring;

		// Create tongue weight
		ctx.TongueWeight = ctx.TongueWeightScene.Instantiate<RigidBody2D>();
		ctx.TongueSpring.Target = ctx.TongueWeight;
		ctx.TongueWeight.GlobalPosition = ctx.GlobalPosition;

		// Add spring and weight to scene
		ctx.CallDeferred(Node.MethodName.AddSibling, ctx.TongueWeight);
		ctx.CallDeferred(Node.MethodName.AddSibling, ctx.TongueSpring);

		// Change state to grapple
		ctx.MovementState = new GrappleState();
		ctx.StateEnum = Player.State.Grapple;
	}

	public void DisableGrapple(Player ctx) { }
}
