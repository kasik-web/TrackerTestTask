using TrackerTestTask.Func;

namespace TrackerTestTask.Viber
{
    public class ViberSender
    {
        public static object SetMessage(string receiver, List<WalkInfo> walks)
        {
            Message message = new();
            
            int numRows = (walks.Count + 1);
            if (numRows > 6)
            {
                numRows = 6;
            }

                message = new Message()
            {
                receiver = receiver,
                min_api_version = 7,
                sender = new Sender()
                {
                    name = "Ваш верный бот"
                },
                tracking_data = "tracking data",
                type = "rich_media",
                rich_media = new RichMedia()
                {
                    Type = "rich_media",
                    ButtonsGroupColumns = 6,
                    ButtonsGroupRows = numRows,
                    BgColor = "#F8EDC1",
                    Buttons = new List<Button>()                    
                },
                keyboard = new Keyboard()
                {
                    Type = "keyboard",
                    DefaultHeight = false,
                    InputFieldState = "hidden",
                    Buttons = new KeyboardButton[]
                    {
                        new KeyboardButton()
                        {
                            ActionType = "reply",
                            ActionBody = "back",
                            Text = "Назад",
                            TextSize = "large",
                            TextVAlign = "middle",
                            TextHAlign = "center",
                            BgColor = "#BD31F9",
                            Silent = true
                        }
                    }
                }
            };

            int counter = 0;

            foreach (var walk in walks)
            {
                if (counter == 0 || counter == 5)
                {
                    message.rich_media.Buttons.Add(new Button()
                    {
                        Columns = 2,
                        Rows = 1,
                        ActionType = "none",
                        ActionBody = "",
                        Image = "",
                        BgColor = "#FFFFFF",
                        Text = "Дата",
                        TextSize = "regular",
                        TextVAlign = "middle",
                        TextHAlign = "center",
                        TextOpacity = 100,
                        TextWrap = true
                    });

                    message.rich_media.Buttons.Add(new Button()
                    {
                        Columns = 2,
                        Rows = 1,
                        ActionType = "none",
                        ActionBody = "",
                        Image = "",
                        BgColor = "#FFFFFF",
                        Text = "Відстань",
                        TextSize = "regular",
                        TextVAlign = "middle",
                        TextHAlign = "center",
                        TextOpacity = 100,
                        TextWrap = true
                    });

                    message.rich_media.Buttons.Add(new Button()
                    {
                        Columns = 2,
                        Rows = 1,
                        ActionType = "none",
                        ActionBody = "",
                        Image = "",
                        BgColor = "#FFFFFF",
                        Text = "Час",
                        TextSize = "regular",
                        TextVAlign = "middle",
                        TextHAlign = "center",
                        TextOpacity = 100,
                        TextWrap = true
                    });
                }

                message.rich_media.Buttons.Add(new Button()
                {
                    Columns = 2,
                    Rows = 1,
                    ActionType = "none",
                    ActionBody = "",
                    Image = "",
                    BgColor = "#FFFFFF",
                    Text = walk.Date.ToString("dd.MM.yy"),
                    TextSize = "regular",
                    TextVAlign = "middle",
                    TextHAlign = "center",
                    TextOpacity = 100,
                    TextWrap = true
                });

                message.rich_media.Buttons.Add(new Button()
                {
                    Columns = 2,
                    Rows = 1,
                    ActionType = "none",
                    ActionBody = "",
                    Image = "",
                    BgColor = "#FFFFFF",
                    Text = walk.WalkDistance.ToString(),
                    TextSize = "regular",
                    TextVAlign = "middle",
                    TextHAlign = "center",
                    TextOpacity = 100,
                    TextWrap = true
                });

                message.rich_media.Buttons.Add(new Button()
                {
                    Columns = 2,
                    Rows = 1,
                    ActionType = "none",
                    ActionBody = "",
                    Image = "",
                    BgColor = "#FFFFFF",
                    Text = walk.Duration,
                    TextSize = "regular",
                    TextVAlign = "middle",
                    TextHAlign = "center",
                    TextOpacity = 100,
                    TextWrap = true
                });

                counter++;
            }

            return message;
        }        

        public static object SetMessage(string receiver, string msg)
        {
            Message message = new();
            message = new Message()
            {
                receiver = receiver,
                min_api_version = 2,
                sender = new Sender()
                {
                    name = "Ваш верный бот"
                },
                tracking_data = "tracking data",
                type = "text",
                text = msg
            };

            return message;
        }

        public static object SetMessage(string receiver, string msg, string imei)
        {
            Message message = new();
            message = new Message()
            {
                receiver = receiver,
                min_api_version = 2,
                sender = new Sender()
                {
                    name = "Ваш верный бот"
                },
                tracking_data = "tracking data",
                type = "text",
                text = msg,
                keyboard = new Keyboard()
                {
                    Buttons = new KeyboardButton[]
                    {
                        new KeyboardButton()
                        {
                            ActionType = "reply",
                            ActionBody = $"top10&{imei}",
                            Text = "Топ 10 прогулянок",
                            TextSize = "large",
                            TextVAlign = "middle",
                            TextHAlign = "center",
                            BgColor = "#BD31F9",
                            Silent = true
                        }
                    }
                }
            };

            return message;
        }   
    }
}
