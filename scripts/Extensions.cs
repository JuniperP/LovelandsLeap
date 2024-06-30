using Godot;

public static class Extensions
{
	public static bool IsValid<T>(this T node) where T : GodotObject
	{
		return node is not null
			&& GodotObject.IsInstanceValid(node)
			&& !node.IsQueuedForDeletion();
	}
}
