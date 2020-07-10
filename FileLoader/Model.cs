using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GeoJsonObjectModel;
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
        public static string[] LoadFileData(string filepath)
        {
            if (File.Exists(filepath))
            {
                return File.ReadAllLines(filepath);
            }
            else
            {
                Log.Add(string.Format("Файл не найден: {0}", filepath));
                return null;
            }
        }
        public static List<PointerData> LoadData(string[] fileData)
        {
            List<PointerData> result = new List<PointerData>();
            try
            {
                if (CheckFileFormat(fileData))
                {
                    var firstLine = fileData[0].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 1; i < fileData.Length; i++)
                    {
                        var cLine = fileData[i].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        if (cLine.Length == 6)
                        {
                            Dictionary<string, object> dict = new Dictionary<string, object>();
                            for (int j = 0; j < cLine.Length; j++)
                            {
                                dict.Add(firstLine[j].ToUpper(), cLine[j]);
                            }
                            var toAdd = PointerData.GetPointerData(dict);
                            if (toAdd != null)
                                result.Add(toAdd);
                            else
                            {
                                Log.Add(string.Format("Пропущена строка {0} - некорректные данные в строке.", i));
                            }
                        }
                        else
                        {
                            Log.Add(string.Format("Пропущена строка {0} - недостаточно данных в строке.", i));
                        }
                    }
                }               
            }
            catch (Exception ex)
            {
                Log.Add(string.Format("Ошибка при чтении файла с данными: {0}", ex.Message));
            }
            return result;
        }
        public static bool CheckFileFormat(string[] filedata)
        {            
            try
            {                
                //var filedata = File.ReadAllLines(filename);
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
                    //await collection.DeleteManyAsync(filter);
                    foreach (var pointer in pointers)
                    {
                        var filter = Builders<PointerData>.Filter.Eq("Name",pointer.Name);
                        await collection.ReplaceOneAsync(filter,pointer, new ReplaceOptions { IsUpsert = true });
                        Log.Add(string.Format("В БД добавлена запись об объекте {0}", pointer.Description));
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Add(string.Format("Ошибка записи в БД: {0}", ex.Message));
            }
        }

        public async static Task WriteGeoJsonToDatabase(string constr, List<PointerData> pointers)
        {
            try
            {
                MongoClient client = new MongoClient(constr);
                IMongoDatabase database = client.GetDatabase("pointersDB");
                var collection = database.GetCollection<BsonDocument>("pointersGeoData");
                foreach(var pointer in pointers)
                {
                    var doc = new BsonDocument()
                    {
                        {"Name",pointer.Name },
                        {"Description",pointer.Description },
                        {"Value",pointer.Value },
                        {"Location",GeoJson.Point(new GeoJson3DProjectedCoordinates(pointer.East,pointer.North,pointer.Hight)).ToBsonDocument() }
                    };
                    await collection.InsertOneAsync(doc);
                }
            }
            catch (Exception ex)
            {
                Log.Add(string.Format("Ошибка записи GEOJSON в БД: {0}", ex.Message));
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
