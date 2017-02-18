using System;
using System.Threading;
using Microsoft.SPOT;

using MicroToolsKit.Hardware.IO;

namespace TestNetduino
{
    public class Program
    {
        public static void Main()
        {
            // Pour accéder au bus I2C, relier le PCF8574 au connecteur TWI de la carte Tinkerkit
            byte SLA = 0x38; // Adresse (7 bits) du PCF8574A relié aux Leds
            Int16 Frequency = 100; // Fréquence d'horloge du bus I2C en kHz

            byte stateLED = 0xFE; // Etat initial des LED. (Un 0 logique => Led éclairée)

            // Création d'un objet Leds
            PCF8574 Leds = new PCF8574(SLA, Frequency);

            Leds.Write(stateLED); // Initialisation de l'état des Leds
            Thread.Sleep(500);

            while (true)
            {
                // Modification de l'état des LED
                if (stateLED != 0)
                {
                    stateLED = (byte)(stateLED << 1);
                }
                else
                {
                    stateLED = 0xFF;
                }

                try
                {
                    Leds.Write(stateLED);
                    Debug.Print(stateLED.ToString());
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
