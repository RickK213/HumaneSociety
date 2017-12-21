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
        public SqlDataReader myDataReader = null;
        public SqlDataAdapter databaseCommand;
        public SqlCommandBuilder scb;
        public SqlCommand mySqlCommand;
        DataTable fillerTable;

        AnimalFactory animalFactory = new ConcreteAnimalFactory();
        
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
        public void SearchAnimals(Animal animal)
        {
            try
            {
                SqlConnection mySqlConnection = new SqlConnection(connectionUsed);
                mySqlCommand = new SqlCommand("SELECT * FROM hs.Animals ORDER BY SpeciesID DESC", mySqlConnection); //put table name to search from, specify search
                mySqlConnection.Open();
                myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

                //column order: name, species, room#, isAdopted, isImmunized, price, foodPerWeek
                //List<string> animalNames = new List<string>();
                //List<int> animalSpecies = new List<int>();
                //List<int> animalRoom = new List<int>();
                //List<bool> animalIsAdopted = new List<bool>();
                //List<bool> animalIsImmunized = new List<bool>();
                //List<double> animalPrice = new List<double>();
                //List<int> animalFood = new List<int>();
                List<Animal> animalsSearched = new List<Animal>();
                while (myDataReader.Read())
                {
                    Animal storeAnimal = animalFactory.CreateAnimal(GetSpecies(myDataReader.GetInt32(2)));
                    storeAnimal.Name = myDataReader.GetString(1);
                    storeAnimal.RoomNumber = myDataReader.GetInt32(3);
                    storeAnimal.IsAdopted = myDataReader.GetBoolean(4);
                    storeAnimal.IsImmunized = myDataReader.GetBoolean(5);
                    storeAnimal.Price = myDataReader.GetDouble(6);
                    storeAnimal.OunceFoodPerWeek = myDataReader.GetInt32(7);
                    animalsSearched.Add(storeAnimal);
                }
                myDataReader.Close();
                conn.Close();
                //Console.WriteLine("   | Name | Species | Room | Adoption Status | Immunization Status | Price | Food Per Week");
                //for(int i = 0; i < animalNames.Count; i++)
                //{
                //    Console.WriteLine(i + 1 + ". {0}|{1}|{2}|{3}|{4}|{5}|{6}", animalNames[i], animalSpecies[i], animalRoom[i], animalIsAdopted[i], animalIsImmunized[i], animalPrice[i], animalFood[i]);
                //}
            }
            catch
            {

            }
        }
        private string GetSpecies(int speciesKey)
        {
            mySqlCommand = new SqlCommand("SELECT * FROM hs.Animals ORDER BY SpeciesID DESC", conn);
            return "";
        }
        public void ScanList<T>(List<T> scannedList)
        {

        }
        //public string GetLeaderboard()
        //{

        //    SqlDataReader myDataReader = null;

        //    //SqlConnection mySqlConnection = new SqlConnection(connectionString);
        //    mySqlCommand = new SqlCommand("SELECT TOP 5 Player_Name, Total_Profit FROM ls.High_Scores ORDER BY Total_Profit DESC;", mySqlConnection);
        //    mySqlConnection.Open();
        //    myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

        //    List<string> playerNames = new List<string>();
        //    List<double> playerScores = new List<double>();
        //    while (myDataReader.Read())
        //    {
        //        playerNames.Add(myDataReader.GetString(0));
        //        playerScores.Add(myDataReader.GetDouble(1));
        //    }
        //    // Always call Close when done reading.
        //    myDataReader.Close();
        //    // Close the connection when done with it.
        //    mySqlConnection.Close();

        //    string leaderboard = "L E A D E R B O A R D\n";
        //    leaderboard += "=====================\n\n";
        //    for (int i = 0; i < playerNames.Count; i++)
        //    {
        //        leaderboard += (i + 1) + ": " + playerNames[i] + "\n   ";
        //        leaderboard += playerScores[i].ToString("C2") + "\n\n";
        //    }
        //    return leaderboard;
        //}
        public void AddAnimal(Animal animal)   //put arguments here to add full adoptable animal to database
        {
            string sqlQuery = "INSERT INTO hs.Animals VALUES(@Name, @SpeciesID, @RoomNumber, @IsAdopted, @HasShots, @Price, @FoodPerWeek);"; //put name of table here (dbo.HighScores) and change @'s to appropriate terms
            using (SqlConnection openCon = new SqlConnection(connectionUsed))
            {
                using (SqlCommand querySaveStaff = new SqlCommand(sqlQuery))
                {
                    //name, species, room#, isAdopted, isImmunized, price, foodPerWeek
                    try
                    {
                        //
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
                        openCon.Close();
                    }
                    catch
                    {
                        //enter UI method that says not all information successfully passed through, try again
                    }

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