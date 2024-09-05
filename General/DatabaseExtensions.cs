using FastMember;
using MySql.Data.MySqlClient;
using shopping_api.Models;
using System.Data;

namespace shopping_api.General {
    public static class DatabaseExtensions {

        public static void mapDataToObject<T>(this IDataReader dataReader, T newObject) {
            if(newObject == null) {
                throw new ArgumentNullException(nameof(newObject));
            }

            // Fast Member Usage
            TypeAccessor objectMemberAccessor = TypeAccessor.Create(newObject.GetType());
            MemberSet members = objectMemberAccessor.GetMembers();

            for(int i = 0; i < dataReader.FieldCount; i++) {
                string dbName = dataReader.GetName(i);

                if(dataReader.IsDBNull(i)) {
                    continue;
                }

                foreach(Member member in members) {
                    Attribute attribute = member.GetAttribute(typeof(ColumnAttribute), false);

                    if(attribute is ColumnAttribute column) {
                        if(column.DatabaseColumnName == dbName) {
                            Type type = Nullable.GetUnderlyingType(member.Type) ?? member.Type;

                            switch(type.Name) {
                                case "String":
                                    objectMemberAccessor[newObject, member.Name] = dataReader.GetString(i);
                                    break;
                                case "DateTime":
                                    objectMemberAccessor[newObject, member.Name] = dataReader.GetDateTime(i);
                                    break;
                                case "Boolean":
                                    objectMemberAccessor[newObject, member.Name] = dataReader.GetBoolean(i);
                                    break;
                                default:
                                    objectMemberAccessor[newObject, member.Name] = dataReader.GetValue(i);
                            break;
                        }
                    }
                    }
                }
            }
        }

    }
}
