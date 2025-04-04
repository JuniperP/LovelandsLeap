using Godot;

// Extensions allow additional methods for existing classes
public static class Extensions
{
	// Checks 3 conditions of a GodotObject ensuring validity
	public static bool IsValid<T>(this T node) where T : GodotObject
	{
		return node is not null
			&& GodotObject.IsInstanceValid(node)
			&& !node.IsQueuedForDeletion();
	}
}
