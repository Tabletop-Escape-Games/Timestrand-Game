using Interfaces;
using System.Collections.Generic;

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

        public static ILineController CreateButtonLineController(string colorTag, List<ButtonController> buttons)
        {
            List<ButtonController> controllers = new List<ButtonController>();
            foreach (ButtonController buttonController in buttons)
            {
                if (buttonController.HasColorTag(colorTag))
                {
                    controllers.Add(buttonController);
                }
            }
            return new ButtonLineController(controllers);
        }
    }
}