using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;


namespace Toolbox
{
    class PCF8574
    {
        // Attributs
        private I2CDevice.Configuration ConfigPCF8574;
        private I2CDevice BusI2C;

        // Constructeur
        public PCF8574(ushort I2C_Add_7bits, Int16 FreqBusI2C)
        {
            ConfigPCF8574 = new I2CDevice.Configuration(I2C_Add_7bits, FreqBusI2C);    
        }

        // Méthodes publiques
        // ----------------------------------------------------------------------------------------------        
        public void WriteByte(Byte value)
        {
            // Création d'un buffer et d'une transaction pour l'accès au circuit en écriture
            byte[] outbuffer = new byte[] { value };
            I2CDevice.I2CTransaction writeTransaction = I2CDevice.CreateWriteTransaction(outbuffer);
            // Tableaux des transactions 
            I2CDevice.I2CTransaction[] T_WriteByte = new I2CDevice.I2CTransaction[] { writeTransaction };
            BusI2C = new I2CDevice(ConfigPCF8574); // Connexion virtuelle de l'objet PCF8574  au bus I2C 
            BusI2C.Execute(T_WriteByte, 1000); // Exécution de la transaction
            BusI2C.Dispose(); // Déconnexion virtuelle de l'objet PCF8574 du bus I2C
        }

        
        public byte ReadByte()
        {           
            // Création d'un buffer et d'une transaction pour l'accès au circuit en lecture
            byte[] inbuffer = new byte[1];
            I2CDevice.I2CTransaction readTransaction = I2CDevice.CreateReadTransaction(inbuffer);
            // Tableaux des transactions           
            I2CDevice.I2CTransaction[] T_ReadByte = new I2CDevice.I2CTransaction[] { readTransaction };
            BusI2C = new I2CDevice(ConfigPCF8574);  // Connexion virtuelle de l'objet PCF8574  au bus I2C 
            BusI2C.Execute(T_ReadByte, 1000); // Exécution de la transaction   
            BusI2C.Dispose(); // Déconnexion virtuelle de l'objet PCF8574 du bus I2C
            return inbuffer[0];
        }
    }
}
