using System;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;

using Toolbox;

namespace NetduinoI2CLEDBP
{
    public class Program
    {
           public static void Main()
        {
            // Paramètres du bus I2C
            byte addLeds_I2C = 0x20; // Adresse (7 bits) du PCF8574 relié aux LED
            byte addBPs_I2C = 0x27;  // Adresse (7 bits) du PCF8574 relié aux BP
            Int16 FreqLed = 100; // Fréquence d'horloge du bus I2C en kHz 
            Int16 FreqBP = 200; // Elle peut être différente pour chaque composant
            
            // Création d'un objet Leds
            PCF8574 Leds = new PCF8574(addLeds_I2C, FreqLed);

            // Création d'un objet BPs
            PCF8574 BPs = new PCF8574(addBPs_I2C, FreqBP);


            byte stateLED = 0xFF; // Etat initial des Leds
            Leds.WriteByte(stateLED);
            
            while (true)
             {
                 // Lecture des BPs
                 stateLED = BPs.ReadByte();
                 stateLED = (byte)~stateLED;
                 // Ecriture sur les Leds
                 Leds.WriteByte(stateLED);                 
             }    
           }
    }
}
