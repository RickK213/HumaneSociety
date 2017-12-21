using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSociety
{
    public class DatabaseControl
    {
        public SqlConnection conn;
        public SqlTransaction transaction;
        public SqlDataAdapter sda;
        public SqlCommandBuilder scb;
        public SqlCommand cmd;
        DataTable dt;
        
        string rickConnection = "Data Source=localhost;Initial Catalog=HumaneSociety;Integrated Security=True";
        string alexConnection = "Data Source=localhost;Initial Catalog = HumaneSociety; Integrated Security = True";
        string connectionUsed;
        public DatabaseControl()
        {
            string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string path = (System.IO.Path.GetDirectoryName(executable));
            AppDomain.CurrentDomain.SetData("DataDirectory", path);
            
            //link where database is
            try
            {
                conn = new SqlConnection(rickConnection);
                conn.Open();
                conn.Close();
                connectionUsed = rickConnection;
            }
            catch
            {
                conn = new SqlConnection(alexConnection);
                conn.Open();
                conn.Close();
                connectionUsed = alexConnection;
            }

        }
        public void SearchAnimals()
        {
            conn.Close();
            conn.Open();
            sda = new SqlDataAdapter("SELECT * FROM // ORDER BY score DESC", conn); //put table name to search from, specify search
            dt = new DataTable();
            sda.Fill(dt);
            Console.WriteLine("TOP 5 SCORES");
            Console.WriteLine("1. " + dt.Rows[0][1].ToString() + " played " + dt.Rows[0][2].ToString() + " days with a score of: " + dt.Rows[0][3].ToString());
            Console.WriteLine("2. " + dt.Rows[1][1].ToString() + " played " + dt.Rows[1][2].ToString() + " days with a score of: " + dt.Rows[1][3].ToString());
            Console.WriteLine("3. " + dt.Rows[2][1].ToString() + " played " + dt.Rows[2][2].ToString() + " days with a score of: " + dt.Rows[2][3].ToString());
            Console.WriteLine("4. " + dt.Rows[3][1].ToString() + " played " + dt.Rows[3][2].ToString() + " days with a score of: " + dt.Rows[3][3].ToString());
            Console.WriteLine("5. " + dt.Rows[4][1].ToString() + " played " + dt.Rows[4][2].ToString() + " days with a score of: " + dt.Rows[4][3].ToString());
        }
        public void AddAnimal(Animal animal)   //put arguments here to add full adoptable animal to database
        {
            string sqlQuery = "INSERT INTO hs.Animals VALUES(@Name, @SpeciesID, @RoomNumber, @IsAdopted, @HasShots, @Price, @FoodPerWeek);"; //put name of table here (dbo.HighScores) and change @'s to appropriate terms
            using (SqlConnection openCon = new SqlConnection(connectionUsed))
            {
                using (SqlCommand querySaveStaff = new SqlCommand(sqlQuery))
                {
                    //name, species, room#, isAdopted, isImmunized, price, foodPerWeek
                    openCon.Open();
                    querySaveStaff.Connection = openCon;
                    querySaveStaff.Parameters.Add("@Name", SqlDbType.VarChar, 50).Value = animal.Name;
                    querySaveStaff.Parameters.Add("@SpeciesID", SqlDbType.Int).Value = animal.SpeciesID;
                    querySaveStaff.Parameters.Add("@RoomNumber", SqlDbType.Int).Value = animal.RoomNumber;
                    querySaveStaff.Parameters.Add("@IsAdopted", SqlDbType.Bit).Value = animal.IsAdopted;
                    querySaveStaff.Parameters.Add("@HasShots", SqlDbType.Bit).Value = animal.IsImmunized;
                    querySaveStaff.Parameters.Add("@Price", SqlDbType.Float).Value = animal.Price;
                    querySaveStaff.Parameters.Add("@FoodPerWeek", SqlDbType.Int).Value = animal.OunceFoodPerWeek;

                    querySaveStaff.ExecuteNonQuery();

                }
            }
        }
        public void CloseConnection()
        {
            conn.Close();
        }
        public void CommitConnection()
        {

        }
        public void RollbackTransaction()
        {
            transaction.Rollback();
            conn.Close();
        }

        public void SaveAdopter(User adopter)
        {
            int zipCodeID = SaveZipCode(adopter.ZipCode);
            //int stateID = SaveState(adopter.State);
            //string sqlQuery = "INSERT INTO hs.Adopters VALUES(@Street1, @CityID, @StateID, @ZipCodeID;";
            //using (SqlConnection openCon = new SqlConnection(connectionUsed))
            //{
            //    using (SqlCommand querySaveAddress = new SqlCommand(sqlQuery))
            //    {
            //        openCon.Open();
            //        querySaveAddress.Connection = openCon;
            //        querySaveAddress.Parameters.Add("@Name", SqlDbType.VarChar, 50).Value = animal.Name;
            //        querySaveAddress.Parameters.Add("@SpeciesID", SqlDbType.Int).Value = animal.SpeciesID;
            //        querySaveAddress.Parameters.Add("@RoomNumber", SqlDbType.Int).Value = animal.RoomNumber;
            //        querySaveAddress.Parameters.Add("@IsAdopted", SqlDbType.Bit).Value = animal.IsAdopted;
            //        querySaveAddress.ExecuteNonQuery();
            //        openCon.Close();
            //    }
            //}
        }

        public int GetDuplicateID(string valueToCheckFor, string IDName, string tableName, string columnName)
        {

            SqlDataReader myDataReader = null;
            SqlConnection mySqlConnection = new SqlConnection(connectionUsed);
            SqlCommand mySqlCommand = new SqlCommand("SELECT " + IDName + " FROM " + tableName + " WHERE " + columnName + " = '" + valueToCheckFor + "';", mySqlConnection);
            mySqlConnection.Open();
            myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

            List<int> tableIDs = new List<int>();
            while (myDataReader.Read())
            {
                tableIDs.Add(myDataReader.GetInt32(0));
            }
            if (tableIDs.Count>0)
            {
                return tableIDs[0];
            }
            return 0;
        }

        public int SaveZipCode(string zipCode)
        {
            int duplicateZipCodeID = GetDuplicateID(zipCode, "ZipCodeID", "hs.Zip_Codes", "Number");
            if (duplicateZipCodeID > 0)
            {
                return duplicateZipCodeID;
            }
            using (SqlConnection connection = new SqlConnection(connectionUsed))
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO hs.Zip_Codes output INSERTED.ZipCodeID VALUES(@Number)", connection))
                {
                    cmd.Parameters.AddWithValue("@Number", zipCode);
                    connection.Open();

                    int zipCodeID = (int)cmd.ExecuteScalar();

                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    return zipCodeID;
                }
            }
        }

    }
}