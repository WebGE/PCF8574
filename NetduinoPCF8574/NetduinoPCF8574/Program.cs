using System;
using System.Threading;
using Microsoft.SPOT;

using ToolBoxes;

namespace TestNetduinoPCF8574
{
    public class Program
    { // Documentation de la classe PCF8574 http://webge.github.io/PCF8574/
        public static void Main()
        { // Pour accéder au bus I2C, relier le PCF8574 au connecteur TWI de la carte Tinkerkit
            // Paramètres du bus I2C
            byte addLeds_I2C = 0x38; // Adresse (7 bits) du PCF8574A relié aux Leds
            Int16 Freq = 100; // Fréquence d'horloge du bus I2C en kHz

            byte stateLED = 0xFE; // Etat initial des LED. (Un 0 logique => Led éclairée)

            // Création d'un objet Leds
            var Leds = new PCF8574(addLeds_I2C, Freq);

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

                //Ecriture sur les Leds
                Leds.Write(stateLED);
                Debug.Print(stateLED.ToString());
                Thread.Sleep(500); // Pour la simulation
            }
        }
    }
}