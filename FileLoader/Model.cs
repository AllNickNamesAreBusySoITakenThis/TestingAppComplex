using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileLoader
{   
    public class Model
    {
        public static event PropertyChangedEventHandler StaticPropertyChanged;
        static void OnStaticPropertyChanged(string name)
        {
            StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(name));
        }

        static ObservableCollection<string> log = new ObservableCollection<string>();
        public static ObservableCollection<string> Log
        {
            get { return log; }
            set
            {
                log = value;
                OnStaticPropertyChanged("Log");
            }
        }
        public static List<PointerData> LoadData(string filepath)
        {
            List<PointerData> result = new List<PointerData>();
            try
            {
                if(File.Exists(filepath))
                {
                    if (CheckFileFormat(filepath))
                    {
                        var fileData = File.ReadAllLines(filepath);
                        var firstLine = fileData[0].Split(new char[] { ';' });
                        for (int i = 1; i < fileData.Length; i++)
                        {
                            var cLine = fileData[i].Split(new char[] { ';' });
                            Dictionary<string, object> dict = new Dictionary<string, object>();
                            for (int j = 0; j < cLine.Length; j++)
                            {
                                dict.Add(firstLine[j].ToUpper(), string.IsNullOrEmpty(cLine[j]) ? "0" : cLine[j]);
                            }
                            result.Add(new PointerData(dict));
                        }           
                    }          
                }
                else
                {
                    Log.Add(string.Format("Файл не найден: {0}", filepath));
                }
            }
            catch (Exception ex)
            {
                Log.Add(string.Format("Ошибка при чтении файла с данными: {0}", ex.Message));
            }
            return result;
        }
        public static bool CheckFileFormat(string filename)
        {            
            try
            {                
                var filedata = File.ReadAllLines(filename);
                if(filedata.Length>0)
                {
                    bool result = true;
                    var firstLine = filedata[0].Split(new char[] { ';' }).ToList();
                    firstLine = (from s in firstLine select s.ToUpper()).ToList();
                    var properties = typeof(PointerData).GetProperties();
                    foreach(var pr in properties)
                    {
                        if (!firstLine.Contains(pr.Name.ToUpper()))
                        {
                            Log.Add(string.Format("Отсутствует столбец {0}", pr.Name));
                            result = false;
                        }
                    }
                    return result;
                }
                else
                {
                    Log.Add("Указанный файл пуст");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Log.Add(string.Format("Ошибка проверки формата файла: {0}", ex.Message));
                return false;
            }
        }
        public async static Task WriteToDatabase(string constr, List<PointerData> pointers)
        {
            try
            {
                if (pointers.Count > 0)
                {
                    MongoClient client = new MongoClient(constr);
                    IMongoDatabase database = client.GetDatabase("pointersDB");
                    var collection = database.GetCollection<PointerData>("pointersData");
                    var filter = Builders<PointerData>.Filter.Empty;
                    await collection.DeleteManyAsync(filter);
                    foreach (var pointer in pointers)
                    {
                        await collection.InsertOneAsync(pointer);
                        Log.Add(string.Format("В БД добавлена запись о объекте {0}", pointer.Description));
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Add(string.Format("Ошибка записи в БД: {0}", ex.Message));
            }
        }

        //public async static void ReadFromDatabase(string constr)
        //{
        //    try
        //    {
        //        MongoClient client = new MongoClient(constr);
        //        IMongoDatabase database = client.GetDatabase("pointersDB");
        //        var collection = database.GetCollection<BsonDocument>("pointersData");
        //        List<PointerData> pointers = new List<PointerData>();
        //        var filter = new BsonDocument();
        //        //{
        //        //    {"_id", false }
        //        //};
        //        var data = await collection.Find(filter).ToListAsync();
        //        foreach(var line in data)
        //        {
        //            pointers.Add(new PointerData(line["Name"].AsString, line["Description"].AsString, line["East"].AsDouble, line["North"].AsDouble, line["Hight"].AsDouble, line["Value"].AsDouble));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //}
    }
}
