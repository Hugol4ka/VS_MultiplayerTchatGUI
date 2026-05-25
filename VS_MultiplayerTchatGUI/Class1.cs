using Vintagestory.API.Common;
using Vintagestory.API.Server;
using Vintagestory.API.Datastructures;

namespace VS_MultiplayerTchatGUI
{
    public class VS_MultiplayerTchatGUIModSystem : ModSystem
    {
        private ICoreServerAPI? apiServer;

        public override void StartServerSide(ICoreServerAPI api)
        {
            base.StartServerSide(api);
            apiServer = api;

            api.Event.PlayerChat += OnPlayerChat;
        }

        private void OnPlayerChat(IServerPlayer joueur, int canalId, ref string message, ref string displayName, BoolRef consume)
        {
            if (joueur == null || apiServer == null) return;

            string couleur = "#9b9ea1";
            string nomAffichage = "Citoyen";
            bool estStaff = false;

            string role = joueur.Role?.Code?.ToLower() ?? "player";

            if (role == "admin")
            {
                couleur = "#f21b03";
                nomAffichage = "Admin";
                estStaff = true;
            }
            else if (role == "helper" || role == "helpeur" || role == "moderator")
            {
                couleur = "#efc002";
                nomAffichage = "Helpeur";
                estStaff = true;
            }

            if (!estStaff)
            {
                string codeClasse = "commoner";
                if (joueur.Entity?.WatchedAttributes != null)
                {
                    codeClasse = joueur.Entity.WatchedAttributes.GetString("characterClass", "commoner");
                }

                switch (codeClasse)
                {
                    case "archivist": couleur = "#f1c40f"; nomAffichage = "Archiviste"; break;
                    case "blackguard": couleur = "#d9381e"; nomAffichage = "Garde Noir"; break;
                    case "brickmaker": couleur = "#d35400"; nomAffichage = "Briquetier"; break;
                    case "butcher": couleur = "#e74c3c"; nomAffichage = "Boucher"; break;
                    case "clockmaker": couleur = "#00cea6"; nomAffichage = "Horloger"; break;
                    case "farmhand": couleur = "#2ecc71"; nomAffichage = "Fermier"; break;
                    case "florist": couleur = "#ff69b4"; nomAffichage = "Fleuriste"; break;
                    case "forester": couleur = "#27ae60"; nomAffichage = "Forestier"; break;
                    case "hunter": couleur = "#e67e22"; nomAffichage = "Chasseur"; break;
                    case "malefactor": couleur = "#9b59b6"; nomAffichage = "Malfaiteur"; break;
                    case "messenger": couleur = "#3498db"; nomAffichage = "Messager"; break;
                    case "quarrier": couleur = "#7f8c8d"; nomAffichage = "Carrier"; break;
                    case "spelunker": couleur = "#a0522d"; nomAffichage = "Spéléologue"; break;
                    case "tailor": couleur = "#e84393"; nomAffichage = "Tailleur"; break;
                    case "vintner": couleur = "#800020"; nomAffichage = "Vigneron"; break;
                    default: couleur = "#9b9ea1"; nomAffichage = "Citoyen"; break;
                }
            }

            string texteBrut = message;
            string ciblePseudo = joueur.PlayerName + ": ";
            
            if (texteBrut.StartsWith(ciblePseudo))
            {
                texteBrut = texteBrut.Substring(ciblePseudo.Length);
            }

            consume.value = true;

            string messageFormate = $"<font color=\"{couleur}\">[{nomAffichage}] {joueur.PlayerName}</font>: {texteBrut}";

            apiServer.SendMessageToGroup(canalId, messageFormate, EnumChatType.OthersMessage);
        }
    }
}
