using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace tentamen_hamster
{
    public static class FileWriter
    {
        public static void WriteData(string filePath, string data)
        {
            try 
            {
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine(data);
                    writer.Flush();
                }
            
            } catch(IOException ioe)
            {

            }
        } 
    }
}
