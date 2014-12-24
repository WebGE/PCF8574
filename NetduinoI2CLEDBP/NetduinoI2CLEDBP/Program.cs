using System;
using Toolbox;

namespace TestNetduinoI2CLEDBP
{
    public class Program
    {
           public static void Main()
        {   // Pour accéder au bus I2C, relier le PCF8574 au connecteur TWI de la 
            // carte Tinkerkit. Placer des résistances de rappel (3,3k) entre le +5V et les sorties SCL et SDA

            // Paramètres du bus I2C
            byte addLeds_I2C = 0x38; // Adresse (7 bits) du PCF8574A relié aux LED
            byte addBPs_I2C = 0x3F;  // Adresse (7 bits) du PCF8574A relié aux BP
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
