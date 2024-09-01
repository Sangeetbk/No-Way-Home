using UnityEngine;
using UnityEngine.Events;

public class Events
{
    [System.Serializable] public class EventFadeComplete : UnityEvent<bool> { }

    [System.Serializable] public class EventToPauseState : UnityEvent<bool> { }
    [System.Serializable] public class EventToMainMenu : UnityEvent<bool> { }
    [System.Serializable] public class EventToResumeState: UnityEvent<bool> { }
    [System.Serializable] public class EventUnloadScene: UnityEvent { }
}
