using Vintagestory.API.Common;
using Vintagestory.API.Server;
using Vintagestory.API.Datastructures;

namespace VS_MultiplayerTchatGUI
{
    public class VS_MultiplayerTchatGUIModSystem : ModSystem
    {

        private ICoreServerAPI apiServer = null!; 

        public override void StartServerSide(ICoreServerAPI api)
        {
            base.StartServerSide(api);
            apiServer = api;
            api.Event.PlayerChat += OnPlayerChat;
        }

        private void OnPlayerChat(IServerPlayer joueur, int canalId, ref string message, ref string displayName, BoolRef consume)
        {
            if (joueur == null) return;

            consume.value = true;

            string texteBrut = message;
            string ciblePseudo = joueur.PlayerName + ": ";
            
            if (texteBrut.StartsWith(ciblePseudo))
            {
                texteBrut = texteBrut.Substring(ciblePseudo.Length);
            }

            string codeClasse = "commoner";
            if (joueur.Entity?.WatchedAttributes != null)
            {
                codeClasse = joueur.Entity.WatchedAttributes.GetString("characterClass", "commoner");
            }

            string couleurClasse = "#9b9ea1";
            string nomClasse = "Citoyen";

            switch (codeClasse)
            {
                case "archivist": couleurClasse = "#f1c40f"; nomClasse = "Archiviste"; break;
                case "blackguard": couleurClasse = "#d9381e"; nomClasse = "Garde Noir"; break;
                case "brickmaker": couleurClasse = "#d35400"; nomClasse = "Briquetier"; break;
                case "butcher": couleurClasse = "#e74c3c"; nomClasse = "Boucher"; break;
                case "clockmaker": couleurClasse = "#00cea6"; nomClasse = "Horloger"; break;
                case "farmhand": couleurClasse = "#2ecc71"; nomClasse = "Fermier"; break;
                case "florist": couleurClasse = "#ff69b4"; nomClasse = "Fleuriste"; break;
                case "forester": couleurClasse = "#27ae60"; nomClasse = "Forestier"; break;
                case "hunter": couleurClasse = "#e67e22"; nomClasse = "Chasseur"; break;
                case "malefactor": couleurClasse = "#9b59b6"; nomClasse = "Malfaiteur"; break;
                case "messenger": couleurClasse = "#3498db"; nomClasse = "Messager"; break;
                case "quarrier": couleurClasse = "#7f8c8d"; nomClasse = "Carrier"; break;
                case "spelunker": couleurClasse = "#a0522d"; nomClasse = "Spéléologue"; break;
                case "tailor": couleurClasse = "#e84393"; nomClasse = "Tailleur"; break;
                case "vintner": couleurClasse = "#800020"; nomClasse = "Vigneron"; break;
                default: couleurClasse = "#9b9ea1"; nomClasse = "Citoyen"; break;
            }

            string role = joueur.Role?.Code?.ToLower() ?? "player";
            string couleurStaff = "";
            string nomStaff = "";
            bool estStaff = false;

            if (role == "admin")
            {
                couleurStaff = "#e74c3c";
                nomStaff = "Admin";
                estStaff = true;
            }
            else if (role == "moderator")
            {
                couleurStaff = "#3498db";
                nomStaff = "Modo";
                estStaff = true;
            }
            else if (role == "helper" || role == "helpeur")
            {
                couleurStaff = "#f1c40f";
                nomStaff = "Helper";
                estStaff = true;
            }

            string messageFormate = "";

            if (estStaff)
            {
                messageFormate = $"<font color=\"{couleurStaff}\">[{nomStaff}]</font> <font color=\"{couleurClasse}\">[{nomClasse}]</font> <strong>{joueur.PlayerName}</strong>: {texteBrut}";
            }
            else
            {
                messageFormate = $"<font color=\"{couleurClasse}\">[{nomClasse}]</font> <strong>{joueur.PlayerName}</strong>: {texteBrut}";
            }

            apiServer.SendMessageToGroup(canalId, messageFormate, EnumChatType.Notification);
        }
    }
}