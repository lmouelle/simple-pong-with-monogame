namespace project
{
    public interface IScoreNotificationSink
    {
        void IncrementScore(PlayerKind playerKind);
    }
}
