using System;

public interface ICutsceneElement
{
    /// <summary>
    /// Performs one animation step in a cutscene element.<br/>
    /// Another step occurring should not break the current step, but instead skip it.
    /// 
    /// <br/><br/>
    /// This method is also responsible for invoking the provided callback action
    /// when the cutscene should move on from this node.
    /// </summary>
    /// <param name="callback"></param>
    public void Step(Action callback);
}
