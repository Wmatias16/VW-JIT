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

              List<string> datas = new List<string>();

              List<Jit> listVw = new List<Jit>();

              foreach (string line in lines)
              {
                  datas.Add(line.Replace(" ", String.Empty));
              }

              foreach (string data in datas)
              {

                  if (data.Length == 42)
                  {
                      Jit vw = new Jit();

                      vw.Fecha = data.Substring(1, 9);
                      vw.Secuencia = int.Parse(data.Substring(10, 3));
                      vw.Pkn = int.Parse(data.Substring(13, 10));
                      vw.Modelo = data.Substring(23, 13);
                      vw.NumeroSerie = int.Parse(data.Substring(36, 6));

                      listVw.Add(vw);
                  }

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
