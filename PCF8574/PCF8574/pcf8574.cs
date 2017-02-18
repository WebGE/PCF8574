using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;

namespace MicroToolsKit
{
    namespace Hardware
    {
        namespace IO
        {
            /// <summary>
            /// PCF8574 - Remote 8-bit I/O expander for I2C-bus with interrupt class
            /// </summary>
            /// <remarks>
            /// You may have some additional information about this class on http://webge.github.io/PCF8574/
            /// </remarks>
            public class PCF8574
            {
                /// <summary>
                /// Slave Adress and frequency configuration
                /// </summary>
                private I2CDevice.Configuration config;

                private I2CDevice i2cBus;

                /// <summary>
                /// Transaction time out = 1s before throwing System.IO.IOException 
                /// </summary>
                const UInt16 TRANSACTIONTIMEOUT = 1000;

                /// <summary>
                /// 7-bit Slave Adress
                /// </summary>
                UInt16 sla;

                /// <summary>
                /// PCF8574 8-bit I/O expander
                /// </summary>
                /// <param name="SLA">PCF8574(@=0x20 to 0x27) or PCF8574A(@=0x38 to 0x3f), 0x38 by default</param>
                /// <param name="Frequency">100khz to 400kHz, 100kHz by default </param>
                public PCF8574(UInt16 SLA = 0x38, Int16 Frequency = 100)
                {
                    sla = SLA;
                    config = new I2CDevice.Configuration(SLA, Frequency);
                }

                /// <summary>
                /// Write byte on PCF8574 I/O port 
                /// </summary>
                /// <param name="value"> data to write</param>
                /// <remarks>
                /// System.IO.IOException trowed with "I2CBus error SLA" message if TRANSACTION TIME OUT.
                /// </remarks>
                public void Write(Byte value)
                {
                    byte[] outbuffer = new byte[] { value };

                    I2CDevice.I2CTransaction[] XAction = new I2CDevice.I2CTransaction[] { I2CDevice.CreateWriteTransaction(outbuffer) };
                    i2cBus = new I2CDevice(config);
                    int transferred = i2cBus.Execute(XAction, TRANSACTIONTIMEOUT);
                    i2cBus.Dispose();
                    if (transferred < (outbuffer.Length))
                        throw new System.IO.IOException("I2CBus error:" + sla.ToString());
                }

                /// <summary>
                /// Read byte on PCF8574 I/O port.
                /// </summary>
                /// <remarks>
                /// System.IO.IOException trowed with "I2CBus error SLA" message if TRANSACTION TIME OUT.
                /// </remarks>
                public byte Read()
                {
                    byte[] inbuffer = new byte[1];

                    I2CDevice.I2CTransaction[] XAction = new I2CDevice.I2CTransaction[] { I2CDevice.CreateReadTransaction(inbuffer) };
                    i2cBus = new I2CDevice(config);
                    int transferred = i2cBus.Execute(XAction, 1000);
                    i2cBus.Dispose();
                    if (transferred < (inbuffer.Length))
                        throw new System.IO.IOException("I2CBus error" + sla.ToString());
                    else
                        return inbuffer[0];
                }
            }
        }
    }
}
