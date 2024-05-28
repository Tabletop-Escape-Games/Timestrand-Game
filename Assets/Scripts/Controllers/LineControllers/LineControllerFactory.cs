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
                _ => null
            };
        }
    }
}