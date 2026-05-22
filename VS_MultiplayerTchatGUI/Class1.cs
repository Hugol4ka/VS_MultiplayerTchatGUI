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

            // 1. Récupération de la classe du joueur
            string codeClasse = "commoner";
            if (joueur.Entity?.WatchedAttributes != null)
            {
                codeClasse = joueur.Entity.WatchedAttributes.GetString("characterClass", "commoner");
            }

            // 2. Définition de la couleur et du nom traduit selon la classe
            string couleur = "#9b9ea1"; // Gris par défaut
            string nomClasse = "Citoyen";

            switch (codeClasse)
            {
                case "archivist": couleur = "#f1c40f"; nomClasse = "Archiviste"; break;
                case "blackguard": couleur = "#d9381e"; nomClasse = "Garde Noir"; break;
                case "brickmaker": couleur = "#d35400"; nomClasse = "Briquetier"; break;
                case "butcher": couleur = "#e74c3c"; nomClasse = "Boucher"; break;
                case "clockmaker": couleur = "#00cea6"; nomClasse = "Horloger"; break;
                case "farmhand": couleur = "#2ecc71"; nomClasse = "Fermier"; break;
                case "florist": couleur = "#ff69b4"; nomClasse = "Fleuriste"; break;
                case "forester": couleur = "#27ae60"; nomClasse = "Forestier"; break;
                case "hunter": couleur = "#e67e22"; nomClasse = "Chasseur"; break;
                case "malefactor": couleur = "#9b59b6"; nomClasse = "Malfaiteur"; break;
                case "messenger": couleur = "#3498db"; nomClasse = "Messager"; break;
                case "quarrier": couleur = "#7f8c8d"; nomClasse = "Carrier"; break;
                case "spelunker": couleur = "#a0522d"; nomClasse = "Spéléologue"; break;
                case "tailor": couleur = "#e84393"; nomClasse = "Tailleur"; break;
                case "vintner": couleur = "#800020"; nomClasse = "Vigneron"; break;
                case "commoner": 
                default: 
                    couleur = "#9b9ea1"; 
                    nomClasse = (codeClasse == "commoner") ? "Citoyen" : codeClasse; 
                    break;
            }

            // 3. Création du préfixe avec la syntaxe HTML de Vintage Story
            string prefix = $"<font color=\"{couleur}\">[{nomClasse}]</font> ";

            // 4. On bloque le message par défaut du jeu
            consume.value = true;

            // 5. On assemble le préfixe et le message (qui contient déjà le "Pseudo: texte")
            string messageFormate = prefix + message;

            // 6. On diffuse le message finalisé sur le canal
            apiServer.SendMessageToGroup(canalId, messageFormate, EnumChatType.OthersMessage);
        }
    }
}