namespace Assets.Script.interfaces
{
    public interface GameState
    {
        void stateUpdate();
        void getData();
        void action(string action);
        string getStateName();
    }
}