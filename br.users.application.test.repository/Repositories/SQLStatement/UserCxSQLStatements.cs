using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace br.users.application.test.repository.Repositories.SQLStatement
{
    public class UserCxSQLStatements
    {
        public static readonly string GetAllUsers = @"SELECT " +
                                                    "id_user AS UserID, " +
                                                    "name_user AS UserName, " +
                                                    "email_user AS UserEmail, " +
                                                    "age_user AS UserAge, " +
                                                    "gender_user AS UserGender, " +
                                                    "password_user AS UserPassword, " +
                                                    "picture_user AS UserPicture, "+
                                                    "official_number_user AS UserOfficialNumber " +
                                                    "FROM users_cx";

        public static readonly string InsertUserData = @"INSERT INTO users_cx(name_user, " +
                                                       "email_user, " +
                                                       "age_user, " +
                                                       "gender_user," +
                                                       "password_user," +
                                                       "picture_user," +
                                                       "official_number_user," +
                                                       "search_field) " +
                                                       "VALUES (@P_NAME_USER, " +
                                                       "@P_EMAIL_USER, " +
                                                       "@P_AGE_USER, " +
                                                       "@P_GENDER_USER, " +
                                                       "@P_PASSWORD_USER," +
                                                       "NULL, " +
                                                       "@P_OFFICIAL_NUMBER_USER, " +
                                                       "@P_SEARCH_FIELD)";

        public static readonly string UpdateUserData = @"UPDATE users_cx SET "+
                                                       "name_user = @P_NAME_USER, " +
                                                       "email_user = @P_EMAIL_USER, " +
                                                       "age_user = @P_AGE_USER, " +
                                                       "gender_user = @P_GENDER_USER, " +
                                                       "password_user = @P_PASSWORD_USER, " +
                                                       "picture_user = @P_PICTURE_USER, " +
                                                       "official_number_user = @P_OFFICIAL_NUMBER_USER, " +
                                                       "search_field = @P_SEARCH_FIELD " +
                                                       "WHERE id_user = @P_USER_ID";

        public static readonly string DeleteUserData = "DELETE FROM users_cx WHERE id_user = @P_USER_ID";

        public static readonly string GetUsersWithFilters = "SELECT id_user AS UserID," +
                                                            "name_user AS UserName, " +
                                                            "email_user AS UserEmail, " +
                                                            "age_user AS UserAge, " +
                                                            "gender_user AS UserGender, " +
                                                            "password_user AS UserPassword, " +
                                                            "picture_user AS UserPicture, " +
                                                            "official_number_user AS UserOfficialNumber " +
                                                            "FROM users_cx " +
                                                            "WHERE (@P_SEARCH_FIELD IS NULL OR UPPER(search_field) LIKE @P_SEARCH_FIELD) " +
                                                            "AND (@P_EMAIL_USER IS NULL OR UPPER(email_user) = @P_EMAIL_USER) " +
                                                            "AND (@P_HAS_IMG IS NULL OR (@P_HAS_IMG = FALSE AND picture_user IS NOT NULL) OR (@P_HAS_IMG = TRUE AND picture_user IS NULL))";
    }
}
