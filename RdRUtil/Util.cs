
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Sockets;
using System.Drawing;


namespace RdR
{
    /// <summary>
    /// Common Data Helpers and Calls that deal with specific and inspecific objects 
    /// </summary>
    public static class Util
    {
        public enum AddressType
        {
            IPV4,
            IPV6
        }

#region -----  Other data helpers  -----
        /// <summary>
        /// Returns the bool value of an object passed.
        /// </summary>
        /// <param name="iWhat">Any object string or otherwise</param>
        /// <returns>bool value of the object passed.</returns>
        public static bool GetBoolean(object iWhat)
        {
            if ( RdR.Util.IsEmpty(iWhat) )
                return false;

            bool xOK;

            if ( bool.TryParse(iWhat.ToString(), out xOK) )
                return xOK;

            return (iWhat.ToString() == "1");
        }

        /// <summary>
        /// Get the phone number based on a string or object's ToString() parsing out hypens and parenthases
        /// </summary>
        /// <param name="iWhat">Any object string or otherwise</param>
        /// <returns>String raw phone phone number</returns>
        public static string GetPhone(object iWhat)
        {
            if ( RdR.Util.IsEmpty(iWhat) )
                return "";

            string xPhone = iWhat.ToString().Trim();

            xPhone.Replace("-", "");
            xPhone.Replace("(", "");
            xPhone.Replace(")", "");

            return xPhone;
        }

        /// <summary>
        /// Returns a Dictionary Associative array from a DataRow
        /// </summary>
        /// <param name="iDR_Data">DataRow object with Columns and values</param>
        /// <returns>Dictionay of keys/values</returns>
        public static Dictionary<string, object> MakeAssArray(DataRow iDR_Data)
        {
            Dictionary<string, object> xD_Data = new Dictionary<string, object>();

            foreach ( DataColumn iCol in iDR_Data.Table.Columns )
            {
                xD_Data.Add(iCol.ColumnName, iDR_Data[iCol.ColumnName]);
            }

            return xD_Data;
        }
#endregion


#region -----  GetString(+1) -----
        /// <summary>
        /// Esentially .ToString() but handles blank
        /// </summary>
        /// <param name="iWhat">Any object string or otherwise</param>
        /// <returns>string value of object or "" {blank} if object is empty</returns>
        public static string GetString(object iWhat)
        {
            return RdR.Util.GetString(iWhat, "");
        }

        /// <summary>
        /// Esentially .ToString(), but it allows for a specificed default using iDefault if IsEmpty()
        /// </summary>
        /// <param name="iWhat">Any object string, number, or otherwise</param>
        /// <param name="iDefault">Default value to return if IsEmpty</param>
        /// <returns>string value of object or default if passed and object is empty</returns>
        public static string GetString(object iWhat, string iDefault)
        {
            if ( RdR.Util.IsEmpty(iWhat) )
                return iDefault;

            return iWhat.ToString();
        }
        #endregion


#region -----  GetNumber() -----
        /// <summary>
        /// Converts Object to number.  0 is returned if IsEmpty()
        /// </summary>
        /// <param name="iWhat">Any object string, number, or otherwise</param>
        /// <returns>decimal value of object or 0 if object is empty</returns>
        public static decimal GetNumber(object iWhat)
        {
            return RdR.Util.GetNumber(iWhat, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iWhat">Any object string, number, or otherwise</param>
        /// <param name="iDefault">Default value to return if IsEmpty</param>
        /// <returns>decimal value of object or default if passed and object is empty</returns>
        public static decimal GetNumber(object iWhat, decimal iDefault)
        {
            if ( RdR.Util.IsEmpty(iWhat) )
                return iDefault;

            var fixMe = iWhat.ToString();
            var outtie = string.Empty;

            foreach ( char checkMe in fixMe.ToCharArray() )
            {
                if ( char.IsNumber(checkMe) || checkMe == '.' || checkMe == '-' )
                {
                    outtie += checkMe.ToString();
                }
            }

            if ( Util.IsEmpty(outtie) )
            {
                return iDefault;
            }

            return Convert.ToDecimal(outtie);
        }
#endregion


#region -----  GetInt() -----
        /// <summary>
        /// Converts Object to number.  0 is returned if IsEmpty()
        /// </summary>
        /// <param name="iWhat">Any object string, number, or otherwise</param>
        /// <returns>Integer value of object or 0 if object is empty</returns>
        public static Int32 GetInt(object iWhat)
        {
            return RdR.Util.GetInt(iWhat, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iWhat">Any object string, number, or otherwise</param>
        /// <param name="iDefault">Default value to return if IsEmpty</param>
        /// <returns>Integer value of object or default if passed and object is empty</returns>
        public static Int32 GetInt(object iWhat, Int32 iDefault)
        {
            if ( RdR.Util.IsEmpty(iWhat) )
            {
                return iDefault;
            }

            var fixMe = iWhat.ToString();
            var outtie = string.Empty;

            foreach ( char checkMe in fixMe.ToCharArray() )
            {
                if ( char.IsNumber(checkMe) || checkMe == '-' || checkMe == '.' )
                {
                    outtie += checkMe.ToString();
                }
            }

            if ( Util.IsEmpty(outtie) )
            {
                return iDefault;
            }

            if ( outtie.Contains(".") )
            {
                var tmpO = Convert.ToDecimal(outtie);

                return (int)Math.Round(tmpO);
            }

            return Convert.ToInt32(outtie);
        }
#endregion

        
#region -----  GetDate  -----
        public static DateTime GetDate(object wannabeDate)
        {
            return Util.GetDate(wannabeDate, null);
        }

        public static DateTime GetDate(object wannabeDate, DateTime? iDefault)
        {
            if ( Util.IsEmpty(wannabeDate) )
            {
                return iDefault.Value;
            }

            DateTime dude;

            if ( DateTime.TryParse(wannabeDate.ToString(), out dude) )
            {
                return dude;
            }

            return iDefault.Value;
        }
#endregion


#region -----  IsEmpty() -----
        /// <summary>
        /// Fancy Dancy Method to check if a passed object is empty via object type or other.
        /// </summary>
        /// <param name="iWhat">Any object string, number, DataTable, Array, or otherwise</param>
        /// <returns>true/false if object is Empty</returns>
        public static bool IsEmpty(object iWhat)
        {
            if ( iWhat == null || iWhat == DBNull.Value )
                return true;

            if ( iWhat is Array )
                return ((Array)iWhat).Length == 0;

            if ( iWhat is DataTable )
                return ((DataTable)iWhat).Rows.Count == 0;

            if ( iWhat is ICollection )
            {
                return ((ICollection)iWhat).Count == 0;
            }

            if ( String.IsNullOrEmpty(iWhat.ToString()) )
            {
                return true;
            }

            if ( iWhat is Color )
            {
                return ((Color)iWhat == Color.Empty);
            }

            if ( iWhat.ToString() == "0000-00-00" || iWhat.ToString() == "0000-00-00 00:00:00" )
            {
                return true;
            }

            decimal xDude;

            if ( decimal.TryParse(iWhat.ToString(), out xDude) )
            {
                return (xDude == 0);
            }

            bool xLogi;

            if ( bool.TryParse(iWhat.ToString(), out xLogi) && !xLogi )
            {
                return true;
            }

            return false;
        }
#endregion

        public static bool IsEqual(object iWhat1, object iWhat2)
        {
            if ( RdR.Util.IsEmpty(iWhat1) )
                return RdR.Util.IsEmpty(iWhat2);

            if ( RdR.Util.IsEmpty(iWhat2) )
                return RdR.Util.IsEmpty(iWhat1);

            return iWhat1.ToString().ToLower() == iWhat2.ToString().ToLower();
        }

        public static bool IsNumber(object wannabeNumber)
        {
            if ( wannabeNumber == null || string.IsNullOrEmpty(wannabeNumber.ToString()) )
            {
                return false;
            }

            foreach ( char sPiece in wannabeNumber.ToString() )
            {
                if ( !char.IsNumber(sPiece) && sPiece != '.' && sPiece != '-' )
                {
                    return false;
                }
            }

            return true;
        }

        public static string MyIP()
        {
            return Util.MyIP(AddressType.IPV4);
        }

        public static string MyIP(AddressType returnType)
        {
            IPHostEntry hostInfo = Dns.GetHostEntry(Dns.GetHostName());

            foreach ( IPAddress checkMe in hostInfo.AddressList )
            {
                if ( checkMe.AddressFamily == AddressFamily.InterNetworkV6 && returnType == AddressType.IPV6 )
                {
                    return checkMe.ToString();
                }

                if ( checkMe.AddressFamily == AddressFamily.InterNetwork && returnType == AddressType.IPV4 )
                {
                    return checkMe.ToString();
                }
            }

            return string.Empty;
        }

        public static String GetBytes(long byteCount)
        {
            string[] suffy = { "B", "KB", "MB", "GB", "TB", "PB", "EB" };

            if ( byteCount == 0 )
            {
                return "0" + suffy[0];
            }

            long bytes = Math.Abs(byteCount);
            int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            double num = Math.Round(bytes / Math.Pow(1024, place), 1);

            return (Math.Sign(byteCount) * num).ToString() + suffy[place];
        }

        public static IEnumerable<string> GetEnumValues<T>()
        {
            return Enum.GetNames(typeof(T));
        }
    }
}