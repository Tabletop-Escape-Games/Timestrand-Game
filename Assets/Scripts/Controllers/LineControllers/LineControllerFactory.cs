using Interfaces;

namespace Controllers.LineControllers
{
    public static class LineControllerFactory
    {
        public static ILineController CreateLineController(string lineControllerType)
        {
            return lineControllerType switch
            {
                "arrowKeys" => new ArrowKeyLineController(),
                "QE" => new QELineController(),
                "ZC" => new ZCLineController(),
                "IP" => new IPLineController(),
                "BM" => new BMLineController(),
                "BlueButton" => new ButtonLineController("Blue"),
                "RedButton" => new ButtonLineController("Red"),
                "GreenButton" => new ButtonLineController("Green"),
                "YellowButton" => new ButtonLineController("Yellow"),
                _ => null
            };
        }
    }
}