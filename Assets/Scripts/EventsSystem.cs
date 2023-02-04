public class EventsSystem
{
    public delegate void EventHandler();

    public static event EventHandler OnLaterEffectScreenFaded;
    public static void CallOnLaterEffectScreenFaded() { if (OnLaterEffectScreenFaded != null) OnLaterEffectScreenFaded(); }

}