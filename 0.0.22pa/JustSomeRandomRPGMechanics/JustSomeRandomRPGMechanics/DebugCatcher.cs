using System;
using System.Collections.Generic;
using System.Text;

namespace JustSomeRandomRPGMechanics
{
    static class DebugMessageHolder
    {
        static string[] messageHistory=new string[GameVariables.WindowHeight-GameVariables.MapDisplayHeight-1];//-1 cause for some reason console shifts text upwards when displaying last element in mesHistory
        static int position=0;
        public static string[] GetMessageHistory()
        {
            return messageHistory;
        }
        public static int GetLastMessageIndex()
        {
            return position-1;
        }
        public static void AddMessage(string message)
        {
            if (position == GameVariables.WindowHeight - GameVariables.MapDisplayHeight-1)
            {
                ShiftMessagesBackwards();
            }
            messageHistory[position] = message;
            position++;
        }
        private static void ShiftMessagesBackwards()
        {
            for(int i = 0; i <position-1; i++)
            {
                messageHistory[i] = messageHistory[i + 1];
            }
            position--;
        }
        public static void Clear()
        {
            position = 0;
        }
    }
}
