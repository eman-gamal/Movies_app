using CsvHelper;
using CsvHelper.Configuration;
using IronXL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace Movie_API.Models
{
    public class DbInitializer
    {
        private async void Get_Movies_From_API(Sources_Select_All_Result source)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var json = await client.GetStringAsync(source.URL);
            
            //dynamic array = JsonConvert.DeserializeObject(json);

            List<string> data_needed = source.Data_Needed.Split(',').ToList();
            string[] s = json.Split('{');
            foreach (string item in s)
            {
                
                //if(item.Contains("title"))
                //{
                //    string[] items = item.Split(data_needed[0]);
                //}

                Console.WriteLine(item);
            }

        }

        private void Get_Movies_From_CSV(Sources_Select_All_Result source)
        {
            
            if (File.Exists(source.URL+".csv"))
            {

                CsvConfiguration csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true,
                    Delimiter = ","
                };

                // Read column headers from file
                CsvReader csv = new CsvReader(File.OpenText(source.URL+".csv"), csvConfiguration);

                csv.Read();
                csv.ReadHeader();

                List<string> data_needed = source.Data_Needed.Split(',').ToList();
                

                DataTable dataTable = new DataTable();

                foreach (string header in data_needed)
                {
                    dataTable.Columns.Add(new System.Data.DataColumn(header));
                }

                while (csv.Read())
                {
                    DataRow row = dataTable.NewRow();

                    foreach (DataColumn column in dataTable.Columns)
                    {
                        row[column.ColumnName] = csv.GetField(column.ColumnName);
                    }

                    dataTable.Rows.Add(row);
                }

                Movies_DB_Entities db = new Movies_DB_Entities();

                foreach(System.Data.DataRow row1 in dataTable.Rows)
                {
                        db.Movie_Insert(row1[0].ToString(), Convert.ToInt32(Convert.ToDateTime(row1[1]).Year), row1[2].ToString(), "Movie", Convert.ToDecimal(row1[3]), Convert.ToDecimal(row1[4]), source.Name);
                }


            }

        }

        private void Get_Movies_From_Excel(Sources_Select_All_Result source)
        {
            WorkBook workbook = WorkBook.Load(source.URL+".xlsx");
            WorkSheet sheet = workbook.DefaultWorkSheet;
            System.Data.DataTable dataTable = sheet.ToDataTable(true);

            List<string> data_needed = source.Data_Needed.Split(',').ToList();
            Movies_DB_Entities db = new Movies_DB_Entities();

            foreach (System.Data.DataRow row in dataTable.Rows)
            {
                //object v = row[data_needed[1]];

                string title = row[data_needed[0]] != null ? row[data_needed[0]].ToString() : null;
                int date = 0;
                if(!(row[data_needed[1]] is DBNull))
                    date = Convert.ToInt32(row[data_needed[1]]);
                string description = row[data_needed[2]] != null ? row[data_needed[2]].ToString() : null;
                string type = row[data_needed[3]] != null ? row[data_needed[3]].ToString() : null;
                decimal rate = 0;
                if (!(row[data_needed[4]] is DBNull))
                    rate = Convert.ToDecimal(row[data_needed[4]]);
                decimal popularity = 0;
                if (!(row[data_needed[5]] is DBNull))
                    popularity = Convert.ToDecimal(row[data_needed[5]]);


                db.Movie_Insert(title, date, description, type, rate, popularity, source.Name);
            }
        }

        public static void Seed()
        {
            Movies_DB_Entities db = new Movies_DB_Entities();
            DbInitializer initializer = new DbInitializer();
            if(!db.Movies.Any())
            {
                
                List<Sources_Select_All_Result> Sources = db.Sources_Select_All().ToList();
                foreach(Sources_Select_All_Result Source in Sources)
                {
                    if (Source.Type.Trim() == "API")
                        initializer.Get_Movies_From_API(Source);
                    else if (Source.Type.Trim() == "CSV")
                        initializer.Get_Movies_From_CSV(Source);
                    else if (Source.Type.Trim() == "Excel")
                        initializer.Get_Movies_From_Excel(Source);
                }
            }

        }

        
    }
}