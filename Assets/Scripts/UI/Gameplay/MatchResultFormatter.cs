using HelicopterTag.Gameplay.Match;

namespace HelicopterTag.UI.Gameplay
{
    public class MatchResultFormatter
    {
        public string Format(PlayerResult result)
        {
            switch (result.Type)
            {
                case ResultType.TagScore:
                    return $"Score : {result.Value:0}";
                    break;

                case ResultType.SurvivalTime:
                    return $"Survival Time : {result.Value:0}s";
                    break;
                
                default:
                    return result.Value.ToString();
            }
        }
    }
}