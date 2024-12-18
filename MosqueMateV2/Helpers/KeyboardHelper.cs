using System.Windows.Input;

namespace MosqueMateV2.Helpers
{
    public class KeyboardHelper
    {
        public static void ActionPressCTRLKey(KeyEventArgs keyEvent,Key letter, Action action)
        {
            if ((keyEvent.Key == letter && Keyboard.IsKeyDown(Key.LeftCtrl)) ||
                (keyEvent.Key == letter && Keyboard.IsKeyDown(Key.RightCtrl)))
            {
                try
                {
                    action();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                }
            }
        }    
        public static void ActionPressKEy(KeyEventArgs keyEvent,Key letter, Action action)
        {
            if (keyEvent.Key == letter)
            {
                try
                {
                    action();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                }
            }
        }
    }
}
