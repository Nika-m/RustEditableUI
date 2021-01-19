using Oxide.Game.Rust.Cui;
using Oxide.Core.Libraries.Covalence;
using System;
using System.Collections.Generic;

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
                switch (args[1])
                {
                    case "day":
                        rust.RunServerCommand("env.time 12");
                        break;
                    case "night":
                        rust.RunServerCommand("env.time 24");
                        break;
                    case "evening":
                        rust.RunServerCommand("env.time 17");
                        break;
                    case "noon":
                        rust.RunServerCommand("env.time 14");
                        break;
                    default:
                        rust.RunServerCommand("env.time " + args[1]);
                        break;
                }
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
        CuiElementContainer container = new CuiElementContainer(); //global

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

        private Dictionary<string, string> ColorLib = new Dictionary<string, string>
        {
            {"transparent", "0 0 0 0" },
            {"black", "0 0 0 1" },
            {"white", "1 1 1 1" },
            {"red",   "1 0 0 1" },
            {"green", "0 1 0 1" },
            {"blue",  "0 0 1 1" }
        };

        static class Globals
        {
            public static Boolean[,] aviabilityMatrix;

        }

        Boolean menuIsOpen = false;
        Boolean gridIsOpen = false;
        Boolean menuIsCreated = false;
        Boolean gridIsCreated = false;
        Boolean refreshMenu = false;

        [ChatCommand("matrixShow")]
        private void chatCommand_matrixShow(BasePlayer player, string command, string[] args)
        {
            PrintToChat($"Pos X: {args[0]} Y: {args[1]} is {Globals.aviabilityMatrix[Convert.ToInt16(args[0]), Convert.ToInt16(args[1])]}");
        }
        [ChatCommand("matrixSet")]
        private void chatCommand_matrixSet(BasePlayer player, string command, string[] args)
        {
            Globals.aviabilityMatrix[Convert.ToInt16(args[0]), Convert.ToInt16(args[1])] = Convert.ToBoolean(args[2]);
            PrintToChat($"Pos X: {args[0]} Y: {args[1]} is set to {Globals.aviabilityMatrix[Convert.ToInt16(args[0]), Convert.ToInt16(args[1])]}");
        }

        [ChatCommand("menu")]
        private void chatCommand_menu(BasePlayer player, string command, string[] args)
        {
            if (args.Length > 0)
            {
                switch (args[0])
                {
                    case "add":
                        switch (args[1])
                        {
                            case "button":
                                {
                                    var givenArgs = new Dictionary<string, string>() {
                                    {"xpos","-1"},
                                    {"ypos","-1"},
                                    {"xend","-1"},
                                    {"yend","-1"},
                                    {"width","4"},
                                    {"height","2"},
                                    {"offset","1"},
                                    {"color","red"},
                                    {"text","default text"},
                                    {"command","chat.say defaultCommand" },
                                    {"autoclose","false"}
                                    /*
                                    startpos: left top aviable cell, offseted 1 cell 
                                    width:4
                                    height:2
                                    endpos:none
                                    */
                                };
                                    double xMin = 0, yMin = 0, xMax = 0, yMax = 0;


                                    //create string of all values
                                    string myArgs = "";
                                    for (int i = 3; i < args.Length; i++)
                                    {
                                        myArgs += $" {args[i]}";
                                    }

                                    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                                    PrintToConsole("values string created: " + myArgs);

                                    //find all key:value in myArgs and add them in dictionary;
                                    //menu add button color:green                   askAviability w/h
                                    //menu add button xpos:4 ypos:5 
                                    //menu add button xpos:4 ypos:5 width:4 higth:2
                                    //menu add button width:4 heigth:2              askAviability w/h
                                    //??? menu add button xend:10 yend:10           askAviability ???
                                    string key, value;
                                    for (int i = 2; i < args.Length; i++)
                                    {
                                        key = args[i].Substring(0, args[i].IndexOf(":"));
                                        value = args[i].Substring(args[i].IndexOf(":") + 1);
                                        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                                        PrintToConsole($"got key: {key} and values: {value}");

                                        givenArgs[key] = value;
                                    }

                                    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                                    PrintToConsole("pringing dictionary");
                                    foreach (KeyValuePair<string, string> prop in givenArgs)
                                    {
                                        PrintToConsole($"Key: {prop.Key} Value: {prop.Value}");
                                    }

                                    /* more complicated way
                                    string slice = myArgs.Substring(0, myArgs.IndexOf(":",i));
                                    string key = slice.Substring(slice.LastIndexOf(" "), slice.Length - slice.LastIndexOf(" "));
                                    string slice2 = myArgs.Substring(0, myArgs.IndexOf(":", i+1));
                                    string value = 
                                    */

                                    //implement all dictionary key:values
                                    int xPos = Convert.ToInt16(givenArgs["xpos"]);
                                    int yPos = Convert.ToInt16(givenArgs["ypos"]);
                                    int width = Convert.ToInt16(givenArgs["width"]);
                                    int height = Convert.ToInt16(givenArgs["height"]);
                                    string color = ColorLib[givenArgs["color"]];
                                    string text = givenArgs["text"];
                                    string buttonCommand = givenArgs["command"];
                                    string autoclose = givenArgs["autoclose"];    //not connected yet
                                    int offset = Convert.ToInt16(givenArgs["offset"]);
                                    /*
                                    startpos: left top aviable cell, offseted 1 cell 
                                    width:4
                                    height:2
                                    endpos:none 
                                     */




                                    // search should start from 0 0 and xPos yPos should be returned from aviability
                                    if (xPos == -1)
                                    {
                                        int[] getPos = askAviability(width, height, offset); //send width/height  ?...endpos
                                                                                             //get xPos yPos xEnd yEnd or "cantfit"

                                        PrintToConsole($"xMin: {getPos[0]} yMin: {getPos[1]} xMax: {getPos[2]} yMax: {getPos[3]}");

                                        xMin = Math.Round((Convert.ToDouble(getPos[0]) * 1 / 12), 3);           //*xOffset 
                                        yMin = Math.Round((1 - (Convert.ToDouble(getPos[1]) * 1 / 12 * 16 / 9)), 3);     //*yOffset
                                        xMax = Math.Round((Convert.ToDouble(getPos[2]) * 1 / 12), 3);
                                        yMax = Math.Round((1 - (Convert.ToDouble(getPos[3]) * 1 / 12 * 16 / 9)), 3);


                                        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                                        PrintToConsole("pirvlei shedegi");
                                        PrintToConsole($"xMin: {xMin} yMin: {yMin} xMax: {xMax} yMax: {yMax}");
                                    }
                                    else
                                    {
                                        //update Aviability with xPos yPos xEnd yEnd

                                    }

                                    //draw it 
                                    var drawButton = container.Add(new CuiButton
                                    {
                                        Button = {
                                            Command = buttonCommand,
                                            //Command = string.Format("menu_close {0} {1}",arg1, arg2),
                                            Color = color
                                        },
                                        RectTransform = {
                                            AnchorMin = $"{xMin} {yMin}",
                                            AnchorMax = $"{xMax} {yMax}"
                                        },
                                        Text = {
                                            Text = text,
                                            FontSize = 30,
                                            Align = UnityEngine.TextAnchor.MiddleCenter,
                                        }
                                    }, "button_panel", "draw_button");

                                    refreshMenu = true;
                                    //when you have xPos and yPos you should calculate xEnd yEnd based on width/height

                                }
                                break;
                            case "text":
                                //adding textfunctionality
                                break;
                        }
                        break;

                    case "remove":
                        //remove functionality

                        break;
                    case "mouseOn":
                        rust.RunServerCommand("containerUpdate true");
                        refreshMenu = true;
                        break;
                    case "mouseOff":
                        rust.RunServerCommand("containerUpdate false");
                        refreshMenu = true;
                        break;
                }
            }

            if (refreshMenu)
            {
                CuiHelper.DestroyUi(player, "menu_panel");
                CuiHelper.AddUi(player, container);
                if (!gridIsOpen) //check
                {
                    CuiHelper.DestroyUi(player, "grid_panel");
                }
                refreshMenu = false;
                return;
            }

            if (menuIsOpen)
            {
                CuiHelper.DestroyUi(player, "menu_panel");
                menuIsOpen = false;
            }
            else
            {
                if (!menuIsCreated)
                {
                    generate_menu(player);
                    menuIsCreated = true;
                }
                CuiHelper.AddUi(player, container);
                if (!gridIsOpen)
                {
                    CuiHelper.DestroyUi(player, "grid_panel");
                }
                menuIsOpen = true;
            }


        }


        #endregion

        #region myFunctions
        //my function that creates CuiElement from CuiPanel
        CuiElement createElementFromCuiPanel(CuiPanel panel, string parent = "Hud", string name = null)
        {
            if (String.IsNullOrEmpty(name))
            {
                name = CuiHelper.GetGuid();
            }
            CuiElement cuiElement = new CuiElement()
            {
                Name = name,
                Parent = parent,
                FadeOut = panel.FadeOut
            };
            if (panel.Image != null)
            {
                cuiElement.Components.Add(panel.Image);
            }
            if (panel.RawImage != null)
            {
                cuiElement.Components.Add(panel.RawImage);
            }
            cuiElement.Components.Add(panel.RectTransform);
            if (panel.CursorEnabled)
            {
                cuiElement.Components.Add(new CuiNeedsCursorComponent());
            }

            return cuiElement;
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
        //all chat args should be named chatArgs
        //all console args              cmdArgs
        [ConsoleCommand("containerUpdate")]
        private void cmd_containerUpdate(ConsoleSystem.Arg cmdArgs) //
        {
            PrintToChat("mouse " + cmdArgs.Args[0]);
            CuiElement menuPanelElement = createElementFromCuiPanel(new CuiPanel
            {
                Image = {
                    Color = "0 0 0 0.5"
                },
                RectTransform = {
                    AnchorMin = "0.2 0.2",
                    AnchorMax = "0.8 0.8"
                },
                CursorEnabled = Convert.ToBoolean(cmdArgs.Args[0])


            }, "Hud", "menu_panel");

            container[0] = menuPanelElement;
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
            menuIsOpen = false;
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
            if (gridIsOpen)
            {
                //hide gridPanel from container
                CuiHelper.DestroyUi(player, "grid_panel");
                gridIsOpen = false;
            }
            else
            {
                if (!gridIsCreated)
                {
                    generate_grid(player);
                    gridIsCreated = true;
                }
                CuiHelper.DestroyUi(player, "menu_panel");
                CuiHelper.AddUi(player, container);
                gridIsOpen = true;
            }

        }




        #region aviability

        public void createAviability(int x, int y)
        {

            Globals.aviabilityMatrix = new Boolean[x, y]; //global
                                                          /*
                                                          for (int i = 0; i < x; i++) {
                                                              for (int j = 0; j < y; j++) {

                                                              }
                                                          }
                                                          */

        }
        private int[] askAviability(int width, int height, int offset)
        {
            //calculate width and height of button (matrixuli zomebi)
            int xPos = 0, yPos = 0, xEnd = 0, yEnd = 0;
            //Globals.aviabilityMatrix[6, 0] = true;
            //Globals.aviabilityMatrix[6, 1] = true;
            //Globals.aviabilityMatrix[6, 2] = true;
            int hopY = 0;
            Boolean success;
            Boolean firstXcolSuccess = false;
            Boolean isOpen = true;
            //find aviable position
            for (int x = 0; x < Globals.aviabilityMatrix.GetLength(0); x++) //??? <= matrix.length - width - offset
            {
                isOpen = true;
                //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                PrintToConsole($"x: {x}");
                PrintToConsole("matrix width: " + Globals.aviabilityMatrix.GetLength(0));
                for (int y = 0; y < Globals.aviabilityMatrix.GetLength(1) - height; y++) //??? -offset checkPointY shouldnot go too down
                {
                    //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                    PrintToConsole($"y: {y}");
                    PrintToConsole("matrix height: " + Globals.aviabilityMatrix.GetLength(1));
                    //2nd iteration: y is hopped now  y+=hopY
                    if (offset > 0 && isOpen)
                    {
                        if (x < offset)
                        {
                            x = offset;
                        }
                        y = offset;
                        isOpen = false;
                        PrintToConsole($"setting offset y: {y} x: {x}");

                    }
                    success = true;//hopes it finds space
                    //check if all button space + offset is free in matrix
                    //takes x,y position and checks space around that position
                    //breaks if finds any nonAviable Position and returns Success = false, and next y position
                    //if everything is Aviable returns Success = true
                    PrintToConsole($"checkPoint Y iteration: {y}");
                    for (int btnX = x - offset; btnX < x + width + offset; btnX++) //?? btnX <= X
                    {
                        PrintToConsole($"checkSpace X iteration: {btnX}");
                        for (int btnY = y - offset; btnY < y + height + offset; btnY++)
                        {
                            PrintToConsole($"offset: {offset} \n btnX: {btnX}  btnY: {btnY}");

                            if (Globals.aviabilityMatrix[btnX, btnY])
                            {
                                PrintToConsole($"btnX: {btnX} btnY {btnY}");
                                hopY += 1;
                                PrintToConsole($"hopY: {hopY}");
                            };
                        }
                        if (hopY > 0)
                        {
                            y += hopY - 1; //-1 because of y++ in for loop, 
                                           //offset mxolod qveda mxarisa emateba
                                           //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                            PrintToConsole($"hop {hopY} y: {y}");
                            hopY = 0;
                            success = false;
                            break; //goes ouf of the current loop
                        }
                    }

                    if (success)
                    {

                        xPos = x;             //xMin
                        yPos = y + height;    //yMin
                        xEnd = x + width;
                        yEnd = y;

                        //update matrix
                        PrintToChat($"xMin: {xPos} yMin: {yPos} xMax: {xEnd} yMax {yEnd}");
                        for (int matrixPosX = xPos; matrixPosX < xEnd; matrixPosX++)
                        {
                            for (int matrixPosY = yEnd; matrixPosY < yPos; matrixPosY++) //yEnd yPos reversed, because matrix Y is reversed
                            {
                                Globals.aviabilityMatrix[matrixPosX, matrixPosY] = true;
                            }
                        }

                        firstXcolSuccess = true;
                        break;
                    }
                    //here we go after BREAK

                    //calculate correct values using gridscale
                    //an ise iyos ro matrica igebdes matricul width height da abrunebdes matricul koordinatebs
                    //da am koordinatebis konvertacia gridscale it garet xdebodes
                    //xPos = x yPos = y xEnd = x*width yEnd = y*heigth
                    // aq kidev erti didi problemaa jigaro, eseti Start da End poziciebi ramdenad
                    // gawyobs shen, Y is reversed in grid, da tanac anchorMin da anchorMax ze unda midiodes
                    // pizdec
                    PrintToConsole($"Last check y: {y}");
                }
                if (firstXcolSuccess)
                {
                    PrintToChat("gilocav, maladec");
                    break;

                }
            }


            return new int[4] { xPos, yPos, xEnd, yEnd }; //double array with size of 4;
        }

        #endregion

        #endregion




        void generate_grid(BasePlayer player, double gridscale = 12, double linewidth = 1)
        {
            int x = 0;
            int y = 0;
            //generate panel for grid


            //generating vertical lines in grid_panel
            #region verticals
            linewidth /= 1000;
            var xOffset = (1 / gridscale);

            for (int i = 1; i * xOffset < 1; i++)
            {
                PrintToConsole("vertical lines");
                PrintToConsole(i.ToString());
                PrintToConsole((i * xOffset + ""));
                PrintToConsole("start: " + (Math.Round((i * xOffset - linewidth / 2), 3)));
                PrintToConsole("end  : " + (Math.Round((i * xOffset - linewidth / 2), 3) + linewidth));

                x += 1;

                container.Add(new CuiPanel
                {
                    Image = {
                    Color = "1 1 1 0.5",
                    FadeIn = Convert.ToSingle(i * xOffset * 2)
                   // FadeOut = Convert.ToSingle(i * xOffset * 2)
                },
                    RectTransform = {
                    AnchorMin = (Math.Round((i*xOffset-linewidth/2),3)+" 0"),
                    AnchorMax = ( (Math.Round((i*xOffset-linewidth/2),3)+linewidth )+" 1")
                    //AnchorMax = (AnchorMin.x+linewidth)+" 1"
                },
                }, "grid_panel", $"vertical_line_{i}");
            }
            #endregion

            //generating horizontal lines in grid_panel
            #region horizontals
            linewidth = Math.Round(linewidth * 16 / 9, 3); //Round because its multiplied on 16/9
            var yOffset = 1 / gridscale * 16 / 9;

            for (int i = 1; i * yOffset < 1; i++) // 16/9 considering aspect ratio 
            {

                PrintToConsole("horizontal lines");
                PrintToConsole(i.ToString());
                PrintToConsole((i * yOffset + ""));
                PrintToConsole("start: " + (1 - (Math.Round((i * yOffset - linewidth / 2), 3) + linewidth)));
                PrintToConsole("end  : " + (1 - Math.Round((i * yOffset - linewidth / 2), 3)));

                // Puts(i.ToString());
                // Puts((i * yOffset + ""));
                // Puts(Math.Round((i * yOffset - linewidth / 2), 3) + " 0");
                // Puts(Math.Round((i * yOffset + linewidth / 2), 3) + " 0");

                y += 1;

                container.Add(new CuiPanel
                {
                    Image = {
                    Color = "1 1 1 0.5",
                    FadeIn = Convert.ToSingle(i * yOffset * 9/16 * 3)
                    //FadeOut = Convert.ToSingle(i * yOffset * 9/16 * 3)
            },
                    RectTransform = {
                    AnchorMin = "0 "+(1 - ( Math.Round( (i * yOffset - linewidth / 2)  , 3) + linewidth )),
                    AnchorMax = "1 "+(Math.Round(1-(i*yOffset-linewidth/2),3)) //not reversing Y,it wont make any sence for grid lines
                    //AnchorMax = (AnchorMin.y+linewidth)+" 1"
                },
                }, "grid_panel", $"horizontal_line_{i}");

                createAviability(x + 1, y + 1); //it shouldnot be created here
                //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                PrintToConsole("" + Globals.aviabilityMatrix[3, 0]);
            }
            #endregion

        }


        //clickable argument in command
        void generate_menu(BasePlayer player)
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
            PrintToConsole("created menu");

            //========================================================================
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

            //========================================================================

            var buttonPanel = container.Add(new CuiPanel
            {
                Image = {
                    Color = "0 0 0 0" //fully transparent
                },
                RectTransform = {
                    AnchorMin = "0 0",
                    AnchorMax = "1 1"
                },
            }, "menu_panel", "button_panel");
            //========================================================================

            var closeButton = container.Add(new CuiButton
            {
                Button = {
                    Command = "menu_close " + player.userID.ToString(),
                    //Command = string.Format("menu_close {0} {1}",arg1, arg2),
                    Color = "1 0 0 1"
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
                    Color = "0 1 0 1"
                },
                RectTransform = {
                    AnchorMin = "0.9 0",
                    AnchorMax = "1 0.1"
                },
                Text = {
                    Text = "Grid",
                    FontSize = 20,
                    Align = UnityEngine.TextAnchor.MiddleCenter,
                },

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
        }


    }



}
