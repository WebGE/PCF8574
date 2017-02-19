using System.Threading;
using Microsoft.SPOT;

using MicroToolsKit.Hardware.IO;

namespace TestFezPanda
{
    public class Program
    {
        public static void Main()
        {
            byte state = 0xFE;

            PCF8574 Leds = new PCF8574(); // SLA = 0x38, Frequency = 100kHz

            Leds.Write(state);
            Thread.Sleep(500);

            while (true)
            {
                if (state != 0)
                {
                    state = (byte)(state << 1);
                }
                else
                {
                    state = 0xFF;
                }

                try
                {
                    Leds.Write(state);
                    Debug.Print(state.ToString());
                }
                catch (System.IO.IOException ex)
                {

                    Debug.Print(ex.Message);
                }
                finally
                {
                    Thread.Sleep(500);
                }
            }
        }
    }
}
