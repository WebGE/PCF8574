#define LCD

using System.Threading;
using testMicroToolsKit.Hardware.IO;
#if LCD
using Microtoolskit.Hardware.Displays;
#endif

namespace Netduino
{
    public class Program
    {
        public static void Main()
        {
            byte state = 0xFE;
            PCF8574 Leds = new PCF8574(); // SLA = 0x38, Frequency = 100kHz
#if LCD
            ELCD162 lcd = new ELCD162("COM1");
            lcd.Init(); lcd.ClearScreen(); lcd.CursorOff();
            lcd.PutString("Chaser demo"); lcd.SetCursor(0, 1);
#endif
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
#if LCD
                    lcd.SetCursor(0, 1); lcd.PutString("Value = " + state + "     ");
#else
                    Debug.Print(state.ToString());
#endif
                }
                catch (System.IO.IOException ex)
                {
#if LCD
                    lcd.SetCursor(0, 1); lcd.PutString(ex.Message);
#else
                    Debug.Print(ex.Message);
#endif
                }
                finally
                {
                    Thread.Sleep(1000);
                }
            }
        }
    }
}
