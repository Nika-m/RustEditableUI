using Oxide.Game.Rust.Cui;
using Oxide.Core.Libraries.Covalence;
using System;

namespace Oxide.Plugins
{
    [Info("RustEditableUI", "Nika", "0.1.1")]
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

        void OnPlayerConnected(Network.Message packet)
        {
            //welcome text form server in chat
            //put defaulr building kit in players inventory
        }
        #region MinecraftCommands
        //minecraft time set command
        [ChatCommand("time")]
        private void chatCommand_time(BasePlayer player, string command, string[] args)
        {
            if (args[0] == "set")
            {
                rust.RunServerCommand("env.time " + args[1]);
            }
        }

        [ChatCommand("weather")]
        private void chatCommand_weather(BasePlayer player, string command, string[] args)
        {
            switch (args[0])
            {
                case "clear":
                    rust.RunServerCommand("weather.load Clear");
                    break;
                case "rain":
                    rust.RunServerCommand("weather.load RainHeavy");
                    break;
                case "thunder":
                    rust.RunServerCommand("weather.load Storm");
                    break;
                case "fog":
                    rust.RunServerCommand("weather.load Fog");
                    break;
                case "cloudy":
                    rust.RunServerCommand("weather.load Overcast");
                    break;

            }
        }
        #endregion

        #region ChatCommands

        [ChatCommand("check")]
        private void chatCommand_check(BasePlayer player, string command, string[] args)
        {
            player.ChatMessage("ChatMessage: chat command check executed successfuly");
            SendReply(player, "<color=orange> Sendreply: chat command check executed successfuly </color>");
            ConsoleSystem.Run(ConsoleSystem.Option.Server.Quiet(), "say say_command_from_server");
            rust.RunServerCommand("say secondCommandExecutedFromServer");
            PrintToChat("printToChat command executed");
            PrintToChat(player, "printToChat command executed with player");

            //player.GiveItem(item);
            // Server.Command(string.Format("env.time {0}", 24);
        }

        [ChatCommand("menu")]
        private void chatCommand_menu(BasePlayer player, string command, string[] args)
        {
            CuiHelper.DestroyUi(player, "menu_panel");
            CuiElementContainer menu = generate_menu(player);
            CuiHelper.AddUi(player, menu);
        }
        #endregion

        #region ConsoleCommands

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

        [ConsoleCommand("menu_close")]
        private void cmd_menuClose(ConsoleSystem.Arg Args)
        {
            BasePlayer player = BasePlayer.FindByID(System.Convert.ToUInt64(Args.Args[0]));
            //var player = Args.Player();
            //var player = arg.Connection.player as BasePlayer;
            //var player = Args.Connection.player as BasePlayer;
            if (player == null)
            {
                PrintToChat("player is null");
                return;
            }
            CuiHelper.DestroyUi(player, "menu_panel");
        }
        [ConsoleCommand("show_grid")]
        private void cmd_showGrid(ConsoleSystem.Arg Args)
        {
            BasePlayer player = BasePlayer.FindByID(System.Convert.ToUInt64(Args.Args[0]));
            //var player = Args.Player();
            //var player = arg.Connection.player as BasePlayer;
            //var player = Args.Connection.player as BasePlayer;
            //if (player == null)
            //   return;
            //CuiHelper.DestroyUi(player, "menu_panel");
            CuiElementContainer menuWithGrid = generate_grid();
            PrintToChat("imin");
            CuiHelper.AddUi(player, menuWithGrid);

        }
        #endregion

        CuiElementContainer container = new CuiElementContainer();

        CuiElementContainer generate_grid(double gridscale = 20, double linewidth = 3)
        {
            //generate panel for grid
            PrintToChat("generating grid");
            var gridPanel = container.Add(new CuiPanel
            {
                Image = {
                    Color = "0 0 0 0" //fully transparent
                },
                RectTransform = {
                    AnchorMin = "0 0",
                    AnchorMax = "1 1"
                },
            }, "menu_panel", "grid_panel");

            //generating vertical lines in grid_panel
            linewidth /= 1000;
            var xOffset = (1 / gridscale);
            for (int i = 1; i * xOffset < 1; i++)
            {


                container.Add(new CuiPanel
                {
                    Image = {
                    Color = "1 1 1 0.5",
                    FadeIn = Convert.ToSingle(i * xOffset * 2)
                },
                    RectTransform = {
                    AnchorMin = (Math.Round((i*xOffset-linewidth/2),3)+" 0"),
                    AnchorMax = (Math.Round((i*xOffset+linewidth/2),3)+" 1")
                    //AnchorMax = (AnchorMin.x+linewidth)+" 1"
                },
                }, "grid_panel", $"vertical_line_{i}");

            }
            //generating horizontal lines in grid_panel
            Puts("size before: " + container.Count);

            //horizontals grid
            linewidth = linewidth * 16 / 9;
            var yOffset = 1 / gridscale * 16 / 9;
            for (int i = 1; i * yOffset < 1; i++) // 16/9 considering aspect ratio 
            {
                PrintToConsole("horizontals");
                PrintToConsole(i.ToString());
                PrintToConsole((i * yOffset + ""));
                PrintToConsole("start: " + (1 - Math.Round((i * yOffset + linewidth / 2), 3)));
                PrintToConsole("end  : " + (1 - Math.Round((i * yOffset - linewidth / 2), 3)));

                // Puts(i.ToString());
                // Puts((i * yOffset + ""));
                // Puts(Math.Round((i * yOffset - linewidth / 2), 3) + " 0");
                // Puts(Math.Round((i * yOffset + linewidth / 2), 3) + " 0");

                container.Add(new CuiPanel
                {
                    Image = {
                    Color = "1 1 1 0.5",
                    FadeIn = Convert.ToSingle(i * yOffset * 16/9 * 3)
            },
                    RectTransform = {
                    AnchorMin = "0 "+(Math.Round(1-(i*yOffset+linewidth/2),3)),
                    AnchorMax = "1 "+(Math.Round(1-(i*yOffset-linewidth/2),3)) //not reversing Y,it wont make any sence for grid lines
                    //AnchorMax = (AnchorMin.y+linewidth)+" 1"
                },
                }, "grid_panel", $"horizontal_line_{i}");

            }
            Puts("size after: " + container.Count);


            return container; //returns grid_panel full of grid lines
        }


        //clickable argument in command
        CuiElementContainer generate_menu(BasePlayer player)
        {

            //var container = new CuiElementContainer();

            var menuPanel = container.Add(new CuiPanel
            {
                Image = {
                    Color = "0 0 0 0.5"
                },
                RectTransform = {
                    AnchorMin = "0.2 0.2",
                    AnchorMax = "0.8 0.8"
                },
                CursorEnabled = true


            }, "Hud", "menu_panel");

            var closeButton = container.Add(new CuiButton
            {
                Button = {
                    Command = "menu_close " + player.userID.ToString(),
                    //Command = string.Format("menu_close {0} {1}",arg1, arg2),
                    Color = "1 0 0 0.8"
                },
                RectTransform = {
                    AnchorMin = "0.9 0.95",
                    AnchorMax = "1 1"
                },
                Text = {
                    Text = "X",
                    FontSize = 10,
                    Align = UnityEngine.TextAnchor.MiddleCenter,
                }
            }, menuPanel, "close_button");


            var gridButton = container.Add(new CuiButton
            {
                Button = {
                    Command = "show_grid " + player.userID.ToString(),
                    //Command = string.Format("menu_close {0} {1}",arg1, arg2),
                    Color = "0 1 0 0.8"
                },
                RectTransform = {
                    AnchorMin = "0.9 0",
                    AnchorMax = "1 0.1"
                },
                Text = {
                    Text = "Grid",
                    FontSize = 20,
                    Align = UnityEngine.TextAnchor.MiddleCenter,
                }
            }, menuPanel, "grid_button");

            //var blurredElement = new CuiElement 
            //{ 
            //    Name = "blurred_element",
            //    Parent = menuPanel,
            //    Components ={
            //        new CuiRawImageComponent { Color = "0 0 0 0.5", Sprite = "assets/content/materials/highlight.png", Material = "assets/content/ui/uibackgroundblur-ingamemenu.mat" },
            //        new CuiRectTransformComponent{ AnchorMin = "0 0", AnchorMax = "0.9 0.9" }
            //    }
            //};
            //container.Add(blurredElement);
            return container;

        }


    }



}

