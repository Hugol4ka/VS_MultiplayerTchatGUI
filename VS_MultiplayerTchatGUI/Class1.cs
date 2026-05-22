using Vintagestory.API.Common;
using Vintagestory.API.Server;
using Vintagestory.API.Datastructures;

namespace VS_MultiplayerTchatGUI
{
    public class VS_MultiplayerTchatGUIModSystem : ModSystem
    {
        public override void StartServerSide(ICoreServerAPI api)
        {
            base.StartServerSide(api);


            api.Event.PlayerChat += OnPlayerChat;
        }

        private void OnPlayerChat(IServerPlayer joueur, int canalId, ref string message, ref string displayName, BoolRef consume)
        {
            if (joueur == null) return;


            string codeClasse = "commoner";
            if (joueur.Entity?.WatchedAttributes != null)
            {
                codeClasse = joueur.Entity.WatchedAttributes.GetString("characterClass", "commoner");
            }

            string prefix = "";


            switch (codeClasse)
            {
                case "commoner":
                    prefix = "[#9b9ea1][Citoyen][#FFFFFF] ";
                    break;
                case "archivist":
                    prefix = "[#f1c40f][Archiviste][#FFFFFF] ";
                    break;
                case "blackguard":
                    prefix = "[#d9381e][Garde Noir][#FFFFFF] ";
                    break;
                case "brickmaker":
                    prefix = "[#d35400][Briquetier][#FFFFFF] ";
                    break;
                case "butcher":
                    prefix = "[#e74c3c][Boucher][#FFFFFF] ";
                    break;
                case "clockmaker":
                    prefix = "[#00cea6][Horloger][#FFFFFF] ";
                    break;
                case "farmhand":
                    prefix = "[#2ecc71][Fermier][#FFFFFF] ";
                    break;
                case "florist":
                    prefix = "[#ff69b4][Fleuriste][#FFFFFF] ";
                    break;
                case "forester":
                    prefix = "[#27ae60][Forestier][#FFFFFF] ";
                    break;
                case "hunter":
                    prefix = "[#e67e22][Chasseur][#FFFFFF] ";
                    break;
                case "malefactor":
                    prefix = "[#9b59b6][Malfaiteur][#FFFFFF] ";
                    break;
                case "messenger":
                    prefix = "[#3498db][Messager][#FFFFFF] ";
                    break;
                case "quarrier":
                    prefix = "[#7f8c8d][Carrier][#FFFFFF] ";
                    break;
                case "spelunker":
                    prefix = "[#a0522d][Spéléologue][#FFFFFF] ";
                    break;
                case "tailor":
                    prefix = "[#e84393][Tailleur][#FFFFFF] ";
                    break;
                case "vintner":
                    prefix = "[#800020][Vigneron][#FFFFFF] ";
                    break;
                default:
                    prefix = $"[#9b9ea1][{codeClasse}][#FFFFFF] ";
                    break;
            }

            displayName = prefix + displayName;
        }
    }
}