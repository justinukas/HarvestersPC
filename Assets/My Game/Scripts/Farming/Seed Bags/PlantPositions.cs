using System.Collections.Generic;
using UnityEngine;

namespace Main.Farming.SeedBags
{
    public static class PlantPositions
    {
        static PlantPositions()
        {
            ListWheatPositions();
            ListCarrotPositions();
        }

        public static List<Vector3> wheatPositionsList = new List<Vector3>();
        public static List<Vector3> carrotPositionsList = new List<Vector3>();

        public static void ListWheatPositions()
        {
            float initialWheatOffsetX = 0.4f;
            float initialWheatOffsetZ = 0.4f;
            float offset = 0.2f;
            float currentWheatOffsetX = initialWheatOffsetX;
            float currentWheatOffsetZ = initialWheatOffsetZ;

            for (int rows = 1; rows <= 5; rows++)
            {
                for (int columns = 1; columns <= 5; columns++)
                {
                    wheatPositionsList.Add(new Vector3(currentWheatOffsetX, 0.30f, currentWheatOffsetZ));
                    currentWheatOffsetZ -= offset;
                }
                currentWheatOffsetX -= offset;
                currentWheatOffsetZ = initialWheatOffsetZ;
            }
        }

        public static void ListCarrotPositions()
        {
            Vector3[] carrotOffsets = {
                    new Vector3(-0.4f, 0.2f, -0.382f), //bottom right
                    new Vector3(0f, 0.2f, -0.382f), //middle right
                    new Vector3(0f, 0.2f, 0f), //middle
                    new Vector3(0f, 0.2f, 0.3935f), //middle left
                    new Vector3(0.4f, 0.2f, -0.382f), //top right 
                    new Vector3(-0.4f, 0.2f, 0.3935f), //bottom left 
                    new Vector3(0.4f, 0.2f, 0.3935f), //top right
                    new Vector3(0.4f, 0.2f, 0f), //middle top
                    new Vector3(-0.4f, 0.2f, 0f) //middle bottom
                };

            carrotPositionsList.AddRange(carrotOffsets);
        }
    }
}
