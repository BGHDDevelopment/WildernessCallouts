using System;
using System.Threading.Tasks;
using CalloutAPI;
using CitizenFX.Core;
using CitizenFX.Core.Native;

namespace WildernessCallouts
{
    
    [CalloutProperties("Possible Dead Body", "BGHDDevelopment", "0.0.1", Probability.Medium)]
    public class PossibleDeadBody : Callout
    {
        Ped vic;
        
        public PossibleDeadBody()
        {
            Random random = new Random();
            int x = random.Next(1, 100 + 1);
            if(x <= 40)
            { 
                InitBase(new Vector3(449.098f, 5513.02f, 755.491f));
            }
            else if(x > 40 && x <= 65)
            {
                InitBase(new Vector3(1200.55f, 5782.78f, 519.177f));
            }
            else
            {
                InitBase(new Vector3(2042.01f, 5378.45f, 172.677f));
            }
            ShortName = "Possible Death Body";
            CalloutDescription = "Possible dead body found on trail.";
            ResponseCode = 2;
            StartDistance = 200f;
        }
        public async override Task Init()
        {
            OnAccept();
            vic = await SpawnPed(GetRandomPed(), Location);
            vic.Kill();
            vic.AlwaysKeepTask = true;
            vic.BlockPermanentEvents = true;
        }
        public async override void OnStart(Ped player)
        {
            vic.AttachBlip();
        }
        private void Notify(string message)
        {
            API.BeginTextCommandThefeedPost("STRING");
            API.AddTextComponentSubstringPlayerName(message);
            API.EndTextCommandThefeedPostTicker(false, true);
        }
        private void DrawSubtitle(string message, int duration)
        {
            API.BeginTextCommandPrint("STRING");
            API.AddTextComponentSubstringPlayerName(message);
            API.EndTextCommandPrint(duration, false);
        }
    }
}