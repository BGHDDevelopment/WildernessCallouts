using System;
using System.Dynamic;
using System.Threading.Tasks;
using CalloutAPI;
using CitizenFX.Core;
using CitizenFX.Core.Native;

namespace WildernessCallouts
{
    [CalloutProperties("Hiker Attacked", "BGHDDevelopment", "0.0.3", Probability.Medium)]
    public class PossibleAttack : Callout
    {
        private Ped vic, suspect;

        public PossibleAttack()
        {
            Random random = new Random();
            int x = random.Next(1, 100 + 1);
            if(x <= 40)
            { 
                InitBase(new Vector3(-589.331f, 5067.92f, 135.294f));
            }
            else if(x > 40 && x <= 65)
            {
                InitBase(new Vector3(-571.749f, 4920.47f, 169.622f));
            }
            else
            {
                InitBase(new Vector3(-1022.15f, 4715.98f, 240.977f)); //default
            }
            ShortName = "Hiker Attacked";
            CalloutDescription = "A hiker is being attacked by an unknown suspect.";
            ResponseCode = 3;
            StartDistance = 200f;
        }
        public async override void OnStart(Ped player)
        {
            base.OnStart(player);
            vic.AttachBlip();
            suspect.AttachBlip();
            dynamic data1 = await GetPedData(vic.NetworkId);
            string firstname = data1.Firstname;
            dynamic data2 = await GetPedData(suspect.NetworkId);
            string firstname2 = data2.Firstname;
            Random random = new Random();
            int x = random.Next(1, 100 + 1);
            if (x <= 40)
            {
                vic.Task.ReactAndFlee(suspect);
                DrawSubtitle("~r~[" + firstname + "] ~s~Please help me!", 5000);
                suspect.Task.FightAgainst(vic);
                suspect.Weapons.Give(WeaponHash.Nightstick, 1000, true, true);
                DrawSubtitle("~r~[" + firstname2 + "] ~s~Let me get your money!", 5000);
            }
            else if (x > 40 && x <= 65)
            {
                vic.Task.ReactAndFlee(suspect);
                DrawSubtitle("~r~[" + firstname + "] ~s~Leave me alone!", 5000);
                suspect.Weapons.Give(WeaponHash.Knife, 1000, true, true);
                suspect.Task.FightAgainst(player);
                DrawSubtitle("~r~[" + firstname2 + "] ~s~Die Pigs!", 5000);
            }
            else
            {
                vic.Task.ReactAndFlee(suspect);
                DrawSubtitle("~r~[" + firstname + "] ~s~Please don't kill me!", 5000);
                suspect.Weapons.Give(WeaponHash.Pistol, 1000, true, true);
                suspect.Task.FightAgainst(player);
                DrawSubtitle("~r~[" + firstname2 + "] ~s~Time to die!", 5000);
            }
        }
        public async override Task Init()
        {
            OnAccept();
            vic = await SpawnPed(GetRandomPed(), Location);
            suspect = await SpawnPed(GetRandomPed(), Location);
            dynamic data = new ExpandoObject();
            data.alcoholLevel = 0.02;
            SetPedData(vic.NetworkId,data);
            suspect.AlwaysKeepTask = true;
            suspect.BlockPermanentEvents = true;
            vic.AlwaysKeepTask = true;
            vic.BlockPermanentEvents = true;
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