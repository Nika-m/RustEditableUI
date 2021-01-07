using Oxide.Game.Rust.Cui;
//using Oxide.Core.Libraries.Covalence;

namespace Oxide.Plugins
{
    [Info("RustEditableUI", "Nika", "0.0.1")]
    public class RustEditableUI : RustPlugin
    {
        #region CuiTEMPLATE
        private static string TEMPLATE = @"[
          {
            ""name"": ""56ab-8ea8-2dd4"",
            ""parent"": ""Hud"",
            ""components"": [
              {
                ""type"": ""UnityEngine.UI.Button"",
                ""command"": ""env.time 20"",
                ""close"": """",
                ""color"": ""0.8 0.24 0.16 1"",
                ""text"": ""{buttontext}""
              },
              {
                ""type"": ""RectTransform"",
                ""anchormin"": ""0 0.9"",
                ""anchormax"": ""0.2 1""
              },
                {
                    ""type"": ""NeedsControl""
                }
            ]
          },
          {
            ""name"": ""5cba-57fa-fa04"",
            ""parent"": ""56ab-8ea8-2dd4"",
            ""components"": [
              {
                ""type"": ""UnityEngine.UI.Text"",
                ""color"": ""1 1 1 1"",
                ""fontSize"": 20,
                ""align"": ""UpperLeft"",
                ""text"":""{labeltext}""
              },
              {
                ""type"": ""RectTransform"",
                ""anchormin"": ""0 0"",
                ""anchormax"": ""1 1""
              }
            ]
          },
          {
            ""name"": ""029e-ac9c-45ab"",
            ""parent"": ""Hud"",
            ""components"": [
              {
                ""type"": ""UnityEngine.UI.Text"",
                ""color"": ""1 0.1 0.1 1"",
                ""fontSize"": 20,
                ""align"": ""MiddleCenter""
              },
              {
                ""type"": ""RectTransform"",
                ""anchormin"": ""0.3 0.904"",
                ""anchormax"": ""0.513 0.95""
              }
            ]
          },
          {
            ""name"": ""1c4f-d6f7-313e"",
            ""parent"": ""Hud"",
            ""components"": [
              {
                ""type"": ""UnityEngine.UI.Image"",
                ""color"": ""0.1 0.61 0.1 1""
              },
              {
                ""type"": ""RectTransform"",
                ""anchormin"": ""0.065 0.632"",
                ""anchormax"": ""0.365 0.832""
              }
            ]
          },
          {
            ""name"": ""5208-9e52-ef29"",
            ""parent"": ""1c4f-d6f7-313e"",
            ""components"": [
              {
                ""type"": ""UnityEngine.UI.Text"",
                ""color"": ""1 1 1 1"",
                ""fontSize"": 20,
                ""align"": ""MiddleCenter""
              },
              {
                ""type"": ""RectTransform"",
                ""anchormin"": ""0 0.77"",
                ""anchormax"": ""0.127 1""
              }
            ]
          }
        ]
        ";
        #endregion
        void Init()
        {
            Puts("here we go");
        }
        [Command("menu")]
        private void consoleCommand_menu(BasePlayer player, string command, string[] args)
        {
            //var bPlayer = (BasePlayer)player.Object;
            var labelText = "Hello World!";
            var buttonText = "Close";
            var filledTemplate = TEMPLATE.Replace("{labeltext}", labelText).Replace("{buttontext}", buttonText);

            CuiHelper.DestroyUi(player, "RustEditableUI");
            CuiHelper.AddUi(player, filledTemplate);
        }

        [ChatCommand("menu")]
        private void chatCommand_menu(BasePlayer player, string command, string[] args)
        {
            player.ChatMessage("chat command menu executed successfuly");
        }
        void OnPlayerConnected(Network.Message packet)
        {

            //welcome text form server in chat
            //put defaulr building kit in players inventory
        }
    }
}
