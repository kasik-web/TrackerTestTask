using System.Reflection;

namespace TrackerTestTask.Viber
{
    public class Message
    {
        public string receiver { get; set; }
        public int min_api_version { get; set; }
        public Sender sender { get; set; }
        public string tracking_data { get; set; }
        public string type { get; set; }
        public string text { get; set; }
        public RichMedia rich_media { get; set; }
        public Keyboard keyboard { get; set; }
    }

    public class Sender
    {
        public string name { get; set; }
    }

    public class RichMedia
    {
        public string Type { get; set; }
        public int ButtonsGroupColumns { get; set; }
        public int ButtonsGroupRows { get; set; }
        public string BgColor { get; set; }
        public List<Button> Buttons { get; set; }
    }

    public class Button
    {
        public int Columns { get; set; }
        public int Rows { get; set; }
        public string ActionType { get; set; }
        public string ActionBody { get; set; }
        public string Image { get; set; }
        public string BgColor { get; set; }
        public string Text { get; set; }
        public string TextSize { get; set; }
        public string TextVAlign { get; set; }
        public string TextHAlign { get; set; }
        public int TextOpacity { get; set; }
        public bool TextWrap { get; set; }
    }

    public class Keyboard
    {
        public string Type { get; set; }
        public bool DefaultHeight { get; set; }
        public string InputFieldState { get; set; }
        public KeyboardButton[] Buttons { get; set; }
    }

    public class KeyboardButton
    {
        public string ActionType { get; set; }
        public string ActionBody { get; set; }
        public string Text { get; set; }
        public string TextSize { get; set; }
        public string TextVAlign { get; set; }
        public string TextHAlign { get; set; }
        public string BgColor { get; set; }
        public bool Silent { get; set; }
    }
}
