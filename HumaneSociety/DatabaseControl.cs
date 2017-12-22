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
        public List<Animal> SearchAnimals()
        {
            try
            {
                SqlConnection mySqlConnection = new SqlConnection(connectionUsed);
                mySqlCommand = new SqlCommand("SELECT * FROM hs.Animals ORDER BY SpeciesID DESC", mySqlConnection); //put table name to search from, specify search
                mySqlConnection.Open();
                myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

                //column order: name, species, room#, isAdopted, isImmunized, price, foodPerWeek
                List<Animal> animalsSearched = new List<Animal>();
                while (myDataReader.Read())
                {
                    Animal storeAnimal = animalFactory.CreateAnimal(GetOneOrMoreValues("SpeciesName", "Species", "SpeciesName", myDataReader.GetInt32(2)));
                    storeAnimal.AnimalID = myDataReader.GetInt32(0);
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

                return animalsSearched;
                //Console.WriteLine("   | Name | Species | Room | Adoption Status | Immunization Status | Price | Food Per Week");
                //for(int i = 0; i < animalNames.Count; i++)
                //{
                //    Console.WriteLine(i + 1 + ". {0}|{1}|{2}|{3}|{4}|{5}|{6}", animalNames[i], animalSpecies[i], animalRoom[i], animalIsAdopted[i], animalIsImmunized[i], animalPrice[i], animalFood[i]);
                //}
            }
            catch
            {

            }
            return null;
        }
        public List<User> RetrieveUsers()
        {
            try
            {
                SqlConnection mySqlConnection = new SqlConnection(connectionUsed);
                mySqlCommand = new SqlCommand("SELECT * FROM hs.Adopters ORDER BY HasPaid DESC", mySqlConnection); //put table name to search from, specify search
                mySqlConnection.Open();
                myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

                //column order: name, email, address, animalAdopted, HasPaid
                List<User> adoptersSearched = new List<User>();
                while (myDataReader.Read())
                {
                    User adopter = new Adopter();
                    adopter.AdopterID = myDataReader.GetInt32(0);
                    adopter.Name = myDataReader.GetString(1);
                    adopter.Email = myDataReader.GetString(2);
                    adopter.StreetAddress = myDataReader.GetInt32(3).ToString();
                    adopter.AdoptedAnimalID = myDataReader.GetInt32(4);
                    adopter.HasPaid = myDataReader.GetBoolean(5);
                    adoptersSearched.Add(adopter);
                }
                myDataReader.Close();
                conn.Close();

                return adoptersSearched;
                //Console.WriteLine("   | Name | Species | Room | Adoption Status | Immunization Status | Price | Food Per Week");
                //for(int i = 0; i < animalNames.Count; i++)
                //{
                //    Console.WriteLine(i + 1 + ". {0}|{1}|{2}|{3}|{4}|{5}|{6}", animalNames[i], animalSpecies[i], animalRoom[i], animalIsAdopted[i], animalIsImmunized[i], animalPrice[i], animalFood[i]);
                //}
            }
            catch
            {

            }
            return null;
        }
        /// <summary>
        /// Returns type DataTable.  To get specific value, after method call enter '.Rows[i][i]'.  Add .ToString() to the end to make it a string (if it's a string-able value) 
        /// </summary>
        /// <param name="columnNameOne">Enter columnNameOne for specific search, or enter "*" to retrieve all data from table which contain searchValue.</param>
        /// <returns>My result</returns>
        public string GetOneOrMoreValues<T>(string columnNameOne, string tableName, string columnNameTwo, T searchValue)
        {
            conn.Close();
            conn.Open();
            databaseCommand = new SqlDataAdapter("SELECT " + columnNameOne + " FROM hs." + tableName + " WHERE " + columnNameTwo + " = " + searchValue, conn);
            fillerTable = new DataTable();
            databaseCommand.Fill(fillerTable);
            conn.Close();

            return fillerTable.Rows[0][0].ToString();
        }
        /// <summary>
        /// Returns type DataTable.  Returns all data or column-specific data based on columnNameOne entry.
        /// </summary>
        /// <param name="columnNameOne">Enter columnNameOne for specific search, or enter "*" to retrieve all data from table which contain searchValue.</param>
        /// <returns>My result</returns>
        public List<string> GetAllValues(string columnNameOne, string tableName)
        {
            conn.Close();
            SqlDataReader myDataReader = null;
            SqlCommand mySqlCommand = new SqlCommand("SELECT " + columnNameOne + " FROM hs." + tableName, conn);
            conn.Open();
            myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
            fillerTable = new DataTable();
            databaseCommand.Fill(fillerTable);
            List<string> values = new List<string>();
            while (myDataReader.Read())
            {
                values.Add(myDataReader.ToString());
            }
            conn.Close();

            return values;
        }
        /// <summary>
        /// Changes a single value on the specified data table.  Arguments 2 and 3 are where the data will be changed.  Arguments 4 and 5 are what specifies WHERE it will change.
        /// </summary>
        public void ChangeSingleValue<T>(string tableName, string columnValueToChange, T valueToInsert, string columnToVerifyWith, T verifyColumnValue)
        {
            string queryToLaunch = "UPDATE hs." + tableName + " SET " + columnValueToChange + " = " + valueToInsert + " WHERE " + columnToVerifyWith + " = " + verifyColumnValue + ";";
            using (SqlConnection openCon = new SqlConnection(connectionUsed))
            {
                using (SqlCommand querySaveStaff = new SqlCommand(queryToLaunch))
                {
                    //try catch finally
                    openCon.Open();
                    querySaveStaff.Connection = openCon;

                    querySaveStaff.ExecuteNonQuery();
                    openCon.Close();
                }
            }
        }
        public void ScanList<T>(List<T> scannedList)
        {

        }
        public bool AddAnimal(Animal animal)   //put arguments here to add full adoptable animal to database
        {
            int roomCheck = VerifyRoomIsAvailable(animal.RoomNumber);
            if (roomCheck == animal.RoomNumber)
            {
                return false;
            }
            else
            {
                int roomID = GetIDSaveValue(animal.RoomNumber, "RoomID", "hs.Rooms", "RoomNumber");

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
                            querySaveStaff.Parameters.Add("@RoomNumber", SqlDbType.Int).Value = roomID;
                            querySaveStaff.Parameters.Add("@IsAdopted", SqlDbType.Bit).Value = animal.IsAdopted;
                            querySaveStaff.Parameters.Add("@HasShots", SqlDbType.Bit).Value = animal.IsImmunized;
                            querySaveStaff.Parameters.Add("@Price", SqlDbType.Float).Value = animal.Price;
                            querySaveStaff.Parameters.Add("@FoodPerWeek", SqlDbType.Int).Value = animal.OunceFoodPerWeek;

                            querySaveStaff.ExecuteNonQuery();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("An error occurred: '{0}'", e);
                        }
                        finally
                        {
                            if (openCon.State == System.Data.ConnectionState.Open)
                            {
                                openCon.Close();
                            }
                        }

                    }
                    return true;
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

        int GetDuplicateAddressID(string streetAddress, int cityID, int stateID, int zipCodeID)
        {
            SqlDataReader myDataReader = null;
            SqlConnection mySqlConnection = new SqlConnection(connectionUsed);
            SqlCommand mySqlCommand = new SqlCommand("SELECT AddressID FROM  hs.Addresses WHERE Street1 = '" + streetAddress + "' AND CityID = '" + cityID + "' AND StateID = '" + stateID + "' AND ZipCodeID = '" + zipCodeID + "';", mySqlConnection);
            mySqlConnection.Open();
            myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

            List<int> tableIDs = new List<int>();
            while (myDataReader.Read())
            {
                tableIDs.Add(myDataReader.GetInt32(0));
            }
            if (tableIDs.Count > 0)
            {
                return tableIDs[0];
            }
            return 0;
        }

        int GetIDSaveAddress(string streetAddress, int cityID, int stateID, int zipCodeID)
        {
            int duplicateID = GetDuplicateAddressID(streetAddress, cityID, stateID, zipCodeID);
            if (duplicateID > 0)
            {
                return duplicateID;
            }
            using (SqlConnection connection = new SqlConnection(connectionUsed))
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO hs.Addresses output INSERTED.StateID VALUES(@Street1, @CityID, @StateID, @ZipCodeID)", connection))
                {
                    cmd.Parameters.AddWithValue("@Street1", streetAddress);
                    cmd.Parameters.AddWithValue("@CityID", cityID);
                    cmd.Parameters.AddWithValue("@StateID", stateID);
                    cmd.Parameters.AddWithValue("@ZipCodeID", zipCodeID);
                    connection.Open();

                    int insertedID = (int)cmd.ExecuteScalar();

                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    return insertedID;
                }
            }
        }

        void SaveProfileData(string name, string email, int addressID, bool hasPaid)
        {
            using (SqlConnection connection = new SqlConnection(connectionUsed))
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO hs.Adopters output INSERTED.AdopterID VALUES(@AdopterName, @AdopterEmail, @AddressID, @AnimalAdopted, @HasPaid)", connection))
                {
                    cmd.Parameters.AddWithValue("@AdopterName", name);
                    cmd.Parameters.AddWithValue("@AdopterEmail", email);
                    cmd.Parameters.AddWithValue("@AddressID", addressID);
                    cmd.Parameters.AddWithValue("@AnimalAdopted", DBNull.Value);
                    cmd.Parameters.AddWithValue("@HasPaid", hasPaid);
                    connection.Open();
                    try
                    {
                        int insertedID = (int)cmd.ExecuteScalar();

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("An error occurred: '{0}'", e);
                    }
                    finally
                    {
                        if (connection.State == System.Data.ConnectionState.Open)
                        {
                            connection.Close();
                        }
                    }
                }
            }
        }

        public void SaveAdopter(User user)
        {
            int zipCodeID = GetIDSaveValue(user.ZipCode, "ZipCodeID", "hs.Zip_Codes", "Number");
            int stateID = GetIDSaveValue(user.State, "StateID", "hs.States", "StateName");
            int cityID = GetIDSaveValue(user.City, "CityID", "hs.Cities", "CityName");
            int addressID = GetIDSaveAddress(user.StreetAddress, cityID, stateID, zipCodeID);
            SaveProfileData(user.Name, user.Email, addressID, false);
        }


        string GetSpecies(int speciesID)
        {
            SqlDataReader myDataReader = null;
            SqlConnection mySqlConnection = new SqlConnection(connectionUsed);
            SqlCommand mySqlCommand = new SqlCommand("SELECT SpeciesName FROM hs.Species WHERE SpeciesID = " + speciesID + ";", mySqlConnection);
            mySqlConnection.Open();
            myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
            string speciesName = "";
            while (myDataReader.Read())
            {
                speciesName = myDataReader.GetString(0);
            }
            myDataReader.Close();
            conn.Close();
            return speciesName;
        }

        public List<Animal> GetAllAnimals()
        {
            SqlDataReader myDataReader = null;
            SqlConnection mySqlConnection = new SqlConnection(connectionUsed);
            SqlCommand mySqlCommand = new SqlCommand("SELECT * FROM hs.Animals;", mySqlConnection);
            mySqlConnection.Open();
            myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
            List<Animal> animals = new List<Animal>();
            while (myDataReader.Read())
            {
                string species = GetSpecies(myDataReader.GetInt32(2));
                Animal animal = animalFactory.CreateAnimal(species);
                animal.AnimalID = myDataReader.GetInt32(0);
                animal.Name = myDataReader.GetString(1);
                animal.RoomNumber = myDataReader.GetInt32(3);
                animal.IsAdopted = myDataReader.GetBoolean(4);
                animal.IsImmunized = myDataReader.GetBoolean(5);
                animal.Price = myDataReader.GetDouble(6);
                animal.OunceFoodPerWeek = myDataReader.GetInt32(7);
                animals.Add(animal);
            }
            myDataReader.Close();
            conn.Close();
            if ( animals.Count == 0 )
            {
                return null;
            }
            return animals;
        }

        public int GetDuplicateID<T>(T valueToCheckFor, string IDName, string tableName, string columnName)
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
        private int VerifyRoomIsAvailable(int roomID)
        {
            int roomNumberUsed = GetDuplicateID(roomID, "RoomNumber", "hs.Rooms", "RoomNumber");
            return roomNumberUsed;
        }

        int GetIDSaveValue<T>(T columnValue, string primaryKey, string tableName, string columnName)
        {
            int duplicateID = GetDuplicateID(columnValue, primaryKey, tableName, columnName);
            if (duplicateID > 0)
            {
                return duplicateID;
            }
            using (SqlConnection connection = new SqlConnection(connectionUsed))
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO " + tableName + " output INSERTED." + primaryKey + " VALUES(@" + columnName + ")", connection))
                {
                    cmd.Parameters.AddWithValue("@" + columnName  + "", columnValue);
                    connection.Open();

                    int insertedID = (int)cmd.ExecuteScalar();

                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    return insertedID;
                }
            }
        }

    }
}