using ChlorophyGUITests.Contract;

namespace ChlorophyGUITests.Components
{
    class Watering : IWatering
    {
        public string WateringMethod(Models.PlantDetails plant)
        {
            IWateringMessageFacade _wateringMessage = new WateringMessageFacade();
            return _wateringMessage.WateringReminder(plant);
        }
    }
}
