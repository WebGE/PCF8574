using System;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using GHI.Pins;

using MicroToolsKit.Hardware.IO;

namespace TestFezPanda
{
    public class Program
    {
        public static void Main()
        {
            // Pour acc�der au bus I2C, relier le PCF8574 au connecteur TWI de la carte Tinkerkit
            byte SLA = 0x38; // Adresse (7 bits) du PCF8574A reli� aux Leds
            Int16 Frequency = 100; // Fr�quence d'horloge du bus I2C en kHz

            byte stateLED = 0xFE; // Etat initial des LED. (Un 0 logique => Led �clair�e)

            // Cr�ation d'un objet Leds
            PCF8574 Leds = new PCF8574(SLA, Frequency);

            Leds.Write(stateLED); // Initialisation de l'�tat des Leds
            Thread.Sleep(500);

            while (true)
            {
                // Modification de l'�tat des LED
                if (stateLED != 0)
                {
                    stateLED = (byte)(stateLED << 1);
                }
                else
                {
                    stateLED = 0xFF;
                }

                //Ecriture sur les Leds
                Leds.Write(stateLED);
                Debug.Print(stateLED.ToString());
                Thread.Sleep(500); // Pour la simulation
            }
        }
    }
}
