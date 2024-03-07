using ChlorophyGUITests.Contract;

namespace ChlorophyGUITests.Components
{
    internal class WateringMessageFacade : IWateringMessageFacade
    {
        public string WateringReminder(Models.PlantDetails plant)
        {
            string wateringReminder = "";

            if (plant.WateringFrequency != null)
            {
                if (plant.WateringDate != null)
                {
                    if (plant.WateringDate != null)
                    {
                        TimeSpan difference = MainPage.Today.Subtract((DateTime)plant.WateringDate);
                        int dateDifference = difference.Days;

                        if (dateDifference != plant.WateringFrequency || dateDifference > 0)
                        {
                            if (plant.WateringFrequency - dateDifference == 1)
                            {
                                wateringReminder = (plant.WateringFrequency - dateDifference).ToString() + " day before watering";
                            }
                            else if (plant.WateringFrequency - dateDifference > 1)
                            {
                                wateringReminder = (plant.WateringFrequency - dateDifference).ToString() + " days before watering";
                            }
                            else if (dateDifference >= plant.WateringFrequency || dateDifference <= 0)
                            {
                                wateringReminder = "I need water!";
                            }
                        }
                    }
                }
            }
            else
            {
                wateringReminder = "No watering information";
            }

            return wateringReminder;
        }
    }
}
