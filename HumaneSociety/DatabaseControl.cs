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
            finally
            {
                conn.Close();
            }

        }

        public List<User> RetrieveUsers()
        {
            List<User> adoptersSearched = new List<User>();
            try
            {
                SqlConnection mySqlConnection = new SqlConnection(connectionUsed);
                mySqlCommand = new SqlCommand("SELECT * FROM hs.Adopters ORDER BY HasPaid DESC", mySqlConnection); //put table name to search from, specify search
                mySqlConnection.Open();
                myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

                while (myDataReader.Read())
                {
                    User adopter = new Adopter();
                    adopter.AdopterID = myDataReader.GetInt32(0);
                    adopter.Name = myDataReader.GetString(1);
                    adopter.Email = myDataReader.GetString(2);
                    adopter.StreetAddress = myDataReader.GetInt32(3).ToString();
                    if (myDataReader.IsDBNull(4))
                    {
                        adopter.AdoptedAnimalID = null;
                    }
                    else
                    {
                        adopter.AdoptedAnimalID = myDataReader.GetInt32(4);
                    }

                    adopter.HasPaid = myDataReader.GetBoolean(5);
                    adopter.PreferedAnimalPersonality = myDataReader.GetString(6);
                    adoptersSearched.Add(adopter);
                }
                myDataReader.Close();
                conn.Close();

                return adoptersSearched;
            }
            catch
            {

            }
            return adoptersSearched;
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
                    try
                    {
                        openCon.Open();
                        querySaveStaff.Connection = openCon;

                        querySaveStaff.ExecuteNonQuery();
                        openCon.Close();
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
            }
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

        int GetDuplicateAddressID(string streetAddress, int cityID, int stateID, int zipCodeID)
        {
            try
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
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: '{0}'", e);
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return 0;
        }

        int GetIDSaveAddress(string streetAddress, int cityID, int stateID, int zipCodeID)
        {
            try
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
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: '{0}'", e);
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return 0;
        }

        void SaveProfileData(string name, string email, int addressID, bool hasPaid, string preferedAnimalPersonality)
        {
            using (SqlConnection connection = new SqlConnection(connectionUsed))
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO hs.Adopters output INSERTED.AdopterID VALUES(@AdopterName, @AdopterEmail, @AddressID, @AnimalAdopted, @HasPaid, @Personality)", connection))
                {
                    cmd.Parameters.AddWithValue("@AdopterName", name);
                    cmd.Parameters.AddWithValue("@AdopterEmail", email);
                    cmd.Parameters.AddWithValue("@AddressID", addressID);
                    cmd.Parameters.AddWithValue("@AnimalAdopted", DBNull.Value);
                    cmd.Parameters.AddWithValue("@HasPaid", hasPaid);
                    cmd.Parameters.AddWithValue("@Personality", preferedAnimalPersonality);
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
            SaveProfileData(user.Name, user.Email, addressID, false, user.PreferedAnimalPersonality);
        }

        string GetSingleValueFromID(int IDNumber, string columnName, string tableName, string IDName)
        {
            try
            {
                SqlDataReader myDataReader = null;
                SqlConnection mySqlConnection = new SqlConnection(connectionUsed);
                SqlCommand mySqlCommand = new SqlCommand("SELECT " + columnName + " FROM " + tableName + " WHERE " + IDName + " = " + IDNumber + ";", mySqlConnection);
                mySqlConnection.Open();
                myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
                string value = "";
                while (myDataReader.Read())
                {
                    value = myDataReader.GetValue(0).ToString();
                }
                myDataReader.Close();
                conn.Close();
                return value;
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: '{0}'", e);
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return "";
        }

        public List<Animal> GetAllAnimals()
        {
            try
            {
                SqlDataReader myDataReader = null;
                SqlConnection mySqlConnection = new SqlConnection(connectionUsed);
                SqlCommand mySqlCommand = new SqlCommand("SELECT * FROM hs.Animals;", mySqlConnection);
                mySqlConnection.Open();
                myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
                List<Animal> animals = new List<Animal>();
                while (myDataReader.Read())
                {
                    string species = GetSingleValueFromID(myDataReader.GetInt32(2), "SpeciesName", "hs.Species", "SpeciesID");
                    Animal animal = animalFactory.CreateAnimal(species);
                    animal.AnimalID = myDataReader.GetInt32(0);
                    animal.Name = myDataReader.GetString(1);
                    string roomNumber = GetSingleValueFromID(myDataReader.GetInt32(3), "RoomNumber", "hs.Rooms", "RoomID");
                    animal.RoomNumber = Convert.ToInt32(roomNumber);
                    animal.IsAdopted = myDataReader.GetBoolean(4);
                    animal.IsImmunized = myDataReader.GetBoolean(5);
                    animal.Price = myDataReader.GetDouble(6);
                    animal.OunceFoodPerWeek = myDataReader.GetInt32(7);
                    animals.Add(animal);
                }
                myDataReader.Close();
                conn.Close();
                return animals;
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: '{0}'", e);
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return null;
        }

        public int GetDuplicateID<T>(T valueToCheckFor, string IDName, string tableName, string columnName)
        {
            try
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
                if (tableIDs.Count > 0)
                {
                    return tableIDs[0];
                }
                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: '{0}'", e);
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
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
            try
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
                        cmd.Parameters.AddWithValue("@" + columnName + "", columnValue);
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
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: '{0}'", e);
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return 0;
        }

    }
}