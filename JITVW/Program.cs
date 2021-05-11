using System;
using System.Collections.Generic;
using System.IO;
using System.Configuration;

namespace JITVW
{
    class Program
    {

        static void Main(string[] args)
        {
            Ejecutar();
        }

        private static List<Jit> ReadTXT()
        {
            string[] lines = System.IO.File.ReadAllLines(ConfigurationManager.AppSettings[1]);
            string modeloTecho = ConfigurationManager.AppSettings[3];
            List<string> datas = new List<string>();

            List<Jit> listVw = new List<Jit>();

            foreach (string line in lines)
            {

                Console.WriteLine();

                if (line.Substring(27, 18).Substring(0, 3) == modeloTecho)
                {
                    datas.Add(line);
                }

            }

            foreach (string data in datas)
            {
                Jit vw = new Jit();
                DateTime fecha = Convert.ToDateTime(data.Substring(1, 9));
                vw.Fecha = fecha.ToString("yyyy-MM-dd");
                vw.Secuencia = int.Parse(data.Substring(11, 3));
                vw.Pkn = int.Parse(data.Substring(14, 10));
                vw.Modelo = data.Substring(27, 18);
                vw.NumeroSerie = data.Substring(45, 6);
                listVw.Add(vw);
                Console.WriteLine(data);
            }

            return listVw;
        }

             
            
          
          private static void replacePath(string oldName)
          {
              DateTime fechaHoy = DateTime.Now;
              string newName = ConfigurationManager.AppSettings[2]+ fechaHoy.ToString("dd-MM-yyyy")+".txt";
              System.IO.File.Move(oldName, newName);
          }
          private static void Ejecutar()
          {

              string path = ConfigurationManager.AppSettings[1];
              JitDatabase JDB = new JitDatabase();

              try
              {
                  if (File.Exists(path))
                  {
                      JDB.Agregar(ReadTXT());
                      replacePath(path);
                      Console.WriteLine("REGISTROS INSERTADOS");
                  }

              }
              catch (Exception err)
              {
                  Console.WriteLine(err.ToString());
              }

          }
        
    }

}
