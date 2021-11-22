using ETModel;

namespace ETHotfix
{
    [MessageHandler]
    public class M2C_OwnPlantHandler:AMHandler<M2C_OwnPlant>
    
    {
        protected override async ETTask Run(ETModel.Session session, M2C_OwnPlant message)
        {
            Player player = PlayerComponent.Instance.MyPlayer;
            PlantConfig plantConfig=Game.Scene.GetComponent<ConfigComponent>().Get(typeof (PlantConfig), message.PlantId) as PlantConfig;
            if (!message.IsFinished)
            {
                
                PlantMarketComponent plantMarketComponent = 
                        Game.Scene.GetComponent<UIComponent>().Get(UIType.PlantMarket).GetComponent<PlantMarketComponent>();
                if (player.Id == message.OwnerId)
                {
                    
                    plantMarketComponent.warningText.text = "You have got Plant No." + plantConfig.Cost;
                    player.Money -= message.MaxPrice;
                    player.OwnPlants.Add((int)plantConfig.Id,plantConfig.Pic);
                }
            }
            else
            {
                if (player.Id == message.OwnerId)
                {
                    player.Money -= message.MaxPrice;
                    player.OwnPlants.Add((int)plantConfig.Id,plantConfig.Pic);
                    Game.Scene.GetComponent<UIComponent>().Get(UIType.Status).GetComponent<StatusComponent>().UpdateStatus();
                }
                
            }

            await ETTask.CompletedTask;
        }
    }
}