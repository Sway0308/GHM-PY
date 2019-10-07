using System;
using System.Data;

namespace Gat.Base
{
    /// <summary>
    /// 型別轉換函式。
    /// </summary>
    public class DbTypeConverter
    {
        /// <summary>
        /// 將指定型別轉換為 TypeCode 列舉型別。
        /// </summary>
        /// <param name="type">型別。</param>
        public static TypeCode ToTypeCode(Type type)
        {
            if (type.IsGenericType && (type.GetGenericTypeDefinition() == typeof(Nullable<>)))
            {
                type = type.GetGenericArguments()[0];
            }
            else if (type.IsByRef)
            {
                type = type.GetElementType();
            }

            if (typeof(bool).IsAssignableFrom(type))
            {
                return TypeCode.Boolean;
            }
            if (typeof(byte).IsAssignableFrom(type))
            {
                return TypeCode.Byte;
            }
            if (typeof(char).IsAssignableFrom(type))
            {
                return TypeCode.Char;
            }
            if (typeof(DateTime).IsAssignableFrom(type))
            {
                return TypeCode.DateTime;
            }
            if (typeof(DBNull).IsAssignableFrom(type))
            {
                return TypeCode.DBNull;
            }
            if (typeof(decimal).IsAssignableFrom(type))
            {
                return TypeCode.Decimal;
            }
            if (typeof(double).IsAssignableFrom(type))
            {
                return TypeCode.Double;
            }
            if (typeof(short).IsAssignableFrom(type))
            {
                return TypeCode.Int16;
            }
            if (typeof(int).IsAssignableFrom(type))
            {
                return TypeCode.Int32;
            }
            if (typeof(long).IsAssignableFrom(type))
            {
                return TypeCode.Int64;
            }
            if (typeof(sbyte).IsAssignableFrom(type))
            {
                return TypeCode.SByte;
            }
            if (typeof(float).IsAssignableFrom(type))
            {
                return TypeCode.Single;
            }
            if (typeof(string).IsAssignableFrom(type))
            {
                return TypeCode.String;
            }
            if (typeof(ushort).IsAssignableFrom(type))
            {
                return TypeCode.UInt16;
            }
            if (typeof(uint).IsAssignableFrom(type))
            {
                return TypeCode.UInt32;
            }
            if (typeof(ulong).IsAssignableFrom(type))
            {
                return TypeCode.UInt64;
            }
            return TypeCode.Object;
        }

        /// <summary>
        /// 將指定型別轉換為 FieldDbType 列舉型別。
        /// </summary>
        /// <param name="type">型別。</param>
        public static EFieldDbType ToFieldDbType(Type type)
        {
            TypeCode oTypeCode;

            oTypeCode = ToTypeCode(type);
            switch (oTypeCode)
            {
                case TypeCode.Boolean:
                    return EFieldDbType.Boolean;
                case TypeCode.Char:
                case TypeCode.String:
                    return EFieldDbType.String;
                case TypeCode.DateTime:
                    return EFieldDbType.DateTime;
                case TypeCode.Decimal:
                    return EFieldDbType.Currency;
                case TypeCode.Double:
                case TypeCode.Single:
                    return EFieldDbType.Double;
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    return EFieldDbType.Integer;
                default:
                    throw new GException("{0} can't convert to FieldDbType", type.Name);
            }
        }

        /// <summary>
        /// 將 EFieldDbType 轉型為 DbType 型別。
        /// </summary>
        /// <param name="fieldDbType">欄位資料型別。</param>
        public static DbType ToDbType(EFieldDbType fieldDbType)
        {
            switch (fieldDbType)
            {
                case EFieldDbType.String:
                    return DbType.String;
                case EFieldDbType.Text:
                    return DbType.String;
                case EFieldDbType.Boolean:
                    return DbType.Boolean;
                case EFieldDbType.Integer:
                    return DbType.Int32;
                case EFieldDbType.Double:
                    return DbType.Double;
                case EFieldDbType.Currency:
                    return DbType.Currency;
                case EFieldDbType.DateTime:
                    return DbType.DateTime;
                case EFieldDbType.GUID:
                    return DbType.Guid;
                case EFieldDbType.Binary:
                    return DbType.Binary;
                default:
                    throw new GException("{0} can't convert to DbType", fieldDbType);
            }
        }

        /// <summary>
        /// 將 EFieldDbType 轉型為 System.Type。
        /// </summary>
        /// <param name="fieldDbType">欄位資料型別。</param>
        public static Type ToType(EFieldDbType fieldDbType)
        {
            switch (fieldDbType)
            {
                case EFieldDbType.String:
                    return typeof(string);
                case EFieldDbType.Text:
                    return typeof(string);
                case EFieldDbType.Boolean:
                    return typeof(bool);
                case EFieldDbType.Integer:
                    return typeof(int);
                case EFieldDbType.Double:
                    return typeof(double);
                case EFieldDbType.Currency:
                    return typeof(decimal);
                case EFieldDbType.DateTime:
                    return typeof(DateTime);
                case EFieldDbType.GUID:
                    return typeof(Guid);
                case EFieldDbType.Binary:
                    return typeof(byte[]);
                default:
                    throw new GException("{0} can't convert to System.Type", fieldDbType);
            }
        }
    }
}
