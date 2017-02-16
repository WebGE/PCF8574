using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;

namespace ToolBoxes
{
    /// <summary>
    /// PCF8574 Remote 8-bit I/O expander forI2C-bus class
    /// </summary>
    public class PCF8574
    {
        I2CDevice.Configuration Config;
        I2CDevice BusI2C;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="I2C_Add_7bits">PCF8574(@=0x20 to 0x27) PCF8574A(@=0x38 to 0x3f)</param>
        /// <param name="FreqBusI2C">100khz to 400kHz</param>
        public PCF8574(ushort I2C_Add_7bits, Int16 FreqBusI2C)
        {
            Config = new I2CDevice.Configuration(I2C_Add_7bits, FreqBusI2C);
        }
  
        /// <summary>
        /// Write a byte on PCF8574 port I/O
        /// </summary>
        /// <param name="value">Byte to write</param>
        public void Write(Byte value)
        {
            // Création d'un buffer et d'une transaction pour l'accès au circuit en écriture
            byte[] outbuffer = new byte[] { value };
            I2CDevice.I2CTransaction writeTransaction = I2CDevice.CreateWriteTransaction(outbuffer);
            // Tableaux des transactions 
            I2CDevice.I2CTransaction[] T_WriteByte = new I2CDevice.I2CTransaction[] { writeTransaction };
            BusI2C = new I2CDevice(Config); // Connexion virtuelle de l'objet PCF8574  au bus I2C 
            BusI2C.Execute(T_WriteByte, 1000); // Exécution de la transaction
            BusI2C.Dispose(); // Déconnexion virtuelle de l'objet PCF8574 du bus I2C
        }

        /// <summary>
        /// Read the PCF8574 port I/O 
        /// </summary>
        /// <returns>I/O port state</returns>
        public byte Read()
        {
            // Création d'un buffer et d'une transaction pour l'accès au circuit en lecture
            byte[] inbuffer = new byte[1];
            I2CDevice.I2CTransaction readTransaction = I2CDevice.CreateReadTransaction(inbuffer);
            // Tableaux des transactions           
            I2CDevice.I2CTransaction[] T_ReadByte = new I2CDevice.I2CTransaction[] { readTransaction };
            BusI2C = new I2CDevice(Config);  // Connexion virtuelle de l'objet PCF8574  au bus I2C 
            BusI2C.Execute(T_ReadByte, 1000); // Exécution de la transaction   
            BusI2C.Dispose(); // Déconnexion virtuelle de l'objet PCF8574 du bus I2C
            return inbuffer[0];
        }
    }
}