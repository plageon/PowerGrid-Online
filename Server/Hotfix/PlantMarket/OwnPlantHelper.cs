using ETModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETHotfix
{
    public static class OwnPlantHelper
    {
        public static void SendM2C_OwnPlant(Player player,int plantId,bool isFinished)
        {
            M2C_OwnPlant m2C_OwnPlant = new M2C_OwnPlant();
            m2C_OwnPlant.PlantId = plantId;
            m2C_OwnPlant.OwnerId = (int)Game.Scene.GetComponent<PlayerComponent>().MaxPriceId;
            m2C_OwnPlant.IsFinished = isFinished;
            m2C_OwnPlant.MaxPrice = Game.Scene.GetComponent<PlayerComponent>().MaxPrice;
            player.UniqueSession.Send(m2C_OwnPlant);
        }
    }
}
